using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BuildStamper
{
    public class BuildStamperUtility
    {
        private const string __assemblyInfoFileNameToStamp = "AssemblyInfo.cs";
        private const string __csprojFileNameToStamp = ".csproj";

        public void EnumerateDirectories(string path, VersionDefinition newVersion)
        {
            bool? wasAssemblyInfoFileProcessed = null;
            foreach (string filename in Directory.EnumerateFiles(path))
            {
                if (filename.EndsWith(__assemblyInfoFileNameToStamp))
                {
                    wasAssemblyInfoFileProcessed = StampAssemblyInfoFile(filename, newVersion);
                }

                if ((wasAssemblyInfoFileProcessed.HasValue) && (wasAssemblyInfoFileProcessed.Value))
                    break;
            }

            if (!wasAssemblyInfoFileProcessed.HasValue)
            {
                // No assembly info file was found so we should process csproj files
                bool? wasCsprojFileProcessed = null;
                foreach (string filename in Directory.EnumerateFiles(path))
                {
                    if (filename.EndsWith(__csprojFileNameToStamp))
                    {
                        wasCsprojFileProcessed = StampCsprojFile(filename, newVersion);
                    }
                }
            }

            foreach (string subdirectory in Directory.EnumerateDirectories(path))
            {
                EnumerateDirectories(subdirectory, newVersion);
            }
        }

        public bool StampAssemblyInfoFile(string file, VersionDefinition newVersion)
        {
            AssemblyVersionDefinition currentAssemblyVersion = ReadAssemblyInfoFileAttributes(file);

            if (currentAssemblyVersion.AssemblyVersion == null)
                return false;   // No assembly version

            if (currentAssemblyVersion.AssemblyFileVersion == null)
                return false;   // No assembly file version

            if (currentAssemblyVersion.AssemblyVersion.VersionDepth > 3)   // there are more 4 (or more) components to the version number
                return false;

            DateTime currentTime = DateTime.Now;
            ushort? iBuildNumber = GetBuildNumber(currentTime);
            if (!iBuildNumber.HasValue)
                return false;

            AssemblyVersionDefinition newAssemblyVersion = new AssemblyVersionDefinition();
            if (newVersion != null)
            {
                newAssemblyVersion.AssemblyVersion = newVersion.Clone();
                newAssemblyVersion.AssemblyProjectVersion = newVersion.Clone();

                newAssemblyVersion.AssemblyFileVersion = newVersion.Clone();
                newAssemblyVersion.AssemblyFileVersion.Build = iBuildNumber;
            }
            else
            {
                newAssemblyVersion.AssemblyVersion = null;
                newAssemblyVersion.AssemblyProjectVersion = null;

                newAssemblyVersion.AssemblyFileVersion = currentAssemblyVersion.AssemblyVersion.Clone();
                newAssemblyVersion.AssemblyFileVersion.Build = iBuildNumber;
            }

            if ((currentAssemblyVersion.AssemblyFileVersion == newAssemblyVersion.AssemblyFileVersion) &&
                (newAssemblyVersion.AssemblyVersion == null) &&
                (newAssemblyVersion.AssemblyProjectVersion == null))
            {
                return false; // no need update since the fileVersion hasn't changed
            }

            ReplaceAssemblyInfoVersionInFile(file, currentAssemblyVersion, newAssemblyVersion);
            Console.WriteLine(file);

            return true;
        }

        public AssemblyVersionDefinition ReadAssemblyInfoFileAttributes(string file)
        {
            //CodeCompileUnit compiledCode;
            DDW.Collections.TokenCollection tokens;
            List<string> literals;
            using (StreamReader sr = new StreamReader(file))
            {
                DDW.Lexer lex = new DDW.Lexer(sr);
                tokens = lex.Lex();
                literals = lex.StringLiterals;

                sr.Close();
            }

            DDW.Parser parser = new DDW.Parser(file);
            DDW.CompilationUnitNode node = parser.Parse(tokens, literals);
            AssemblyVersionDefinition assemblyVersion = new AssemblyVersionDefinition();
            VersionDefinition attributeVersion = null;
            foreach (DDW.AttributeNode attribute in node.DefaultNamespace.Attributes)
            {
                if ((attribute.Name.QualifiedIdentifier == "AssemblyVersion") &&
                    (attribute.Arguments.Count > 0) &&
                    (attribute.Arguments[0].Expression is DDW.StringPrimitive))
                {
                    string assemblyVersionString = ((DDW.StringPrimitive)attribute.Arguments[0].Expression).Value;
                    attributeVersion = VersionDefinition.FromString(assemblyVersionString);
                    assemblyVersion.AssemblyVersion = attributeVersion;
                }
                else if ((attribute.Name.QualifiedIdentifier == "AssemblyFileVersion") &&
                    (attribute.Arguments.Count > 0) &&
                    (attribute.Arguments[0].Expression is DDW.StringPrimitive))
                {
                    string assemblyFileVersionString = ((DDW.StringPrimitive)attribute.Arguments[0].Expression).Value;
                    attributeVersion = VersionDefinition.FromString(assemblyFileVersionString);
                    assemblyVersion.AssemblyFileVersion = attributeVersion;
                }
                else if ((attribute.Name.QualifiedIdentifier == "AssemblyInformationalVersion") &&
                    (attribute.Arguments.Count > 0) &&
                    (attribute.Arguments[0].Expression is DDW.StringPrimitive))
                {
                    string assemblyProjectVersionString = ((DDW.StringPrimitive)attribute.Arguments[0].Expression).Value;
                    attributeVersion = VersionDefinition.FromString(assemblyProjectVersionString);
                    assemblyVersion.AssemblyProjectVersion = attributeVersion;
                }
            }

            return assemblyVersion;
        }

        public void ReplaceAssemblyInfoVersionInFile(string file, AssemblyVersionDefinition currentAssemblyVersion, AssemblyVersionDefinition newAssemblyVersion)
        {
            string assemblyInfoText = File.ReadAllText(file);
            if ((currentAssemblyVersion.AssemblyVersion != null) && (newAssemblyVersion.AssemblyVersion != null))
            {
                assemblyInfoText = assemblyInfoText.Replace("AssemblyVersion(\"" + currentAssemblyVersion.AssemblyVersion.ToString() + "\")", "AssemblyVersion(\"" + newAssemblyVersion.AssemblyVersion.ToString() + "\")");
            }
            if ((currentAssemblyVersion.AssemblyFileVersion != null) && (newAssemblyVersion.AssemblyFileVersion != null))
            {
                assemblyInfoText = assemblyInfoText.Replace("AssemblyFileVersion(\"" + currentAssemblyVersion.AssemblyFileVersion.ToString() + "\")", "AssemblyFileVersion(\"" + newAssemblyVersion.AssemblyFileVersion.ToString() + "\")");
            }
            if ((currentAssemblyVersion.AssemblyProjectVersion != null) && (newAssemblyVersion.AssemblyProjectVersion != null))
            {
                assemblyInfoText = assemblyInfoText.Replace("AssemblyInformationalVersion(\"" + currentAssemblyVersion.AssemblyProjectVersion.ToString() + "\")", "AssemblyInformationalVersion(\"" + newAssemblyVersion.AssemblyProjectVersion.ToString() + "\")");
            }
            File.WriteAllText(file, assemblyInfoText);
        }

        public bool StampCsprojFile(string file, VersionDefinition newVersion)
        {
            AssemblyVersionDefinition currentAssemblyVersion = ReadCsprojFileAttributes(file);

            if (currentAssemblyVersion.AssemblyVersion == null)
                return false;   // No assembly version

            if (currentAssemblyVersion.AssemblyFileVersion == null)
                return false;   // No assembly file version

            if (currentAssemblyVersion.AssemblyVersion.VersionDepth > 3)   // there are more 4 (or more) components to the version number
                return false;

            DateTime currentTime = DateTime.Now;
            ushort? iBuildNumber = GetBuildNumber(currentTime);
            if (!iBuildNumber.HasValue)
                return false;


            AssemblyVersionDefinition newAssemblyVersion = new AssemblyVersionDefinition();
            if (newVersion != null)
            {
                newAssemblyVersion.AssemblyVersion = newVersion.Clone();
                newAssemblyVersion.AssemblyProjectVersion = newVersion.Clone();

                newAssemblyVersion.AssemblyFileVersion = newVersion.Clone();
                newAssemblyVersion.AssemblyFileVersion.Build = iBuildNumber;
            }
            else
            {
                newAssemblyVersion.AssemblyVersion = null;
                newAssemblyVersion.AssemblyProjectVersion = null;

                newAssemblyVersion.AssemblyFileVersion = currentAssemblyVersion.AssemblyVersion.Clone();
                newAssemblyVersion.AssemblyFileVersion.Build = iBuildNumber;
            }

            if ((currentAssemblyVersion.AssemblyFileVersion == newAssemblyVersion.AssemblyFileVersion) &&
                (newAssemblyVersion.AssemblyVersion == null) &&
                (newAssemblyVersion.AssemblyProjectVersion == null))
            {
                return false; // no need update since the fileVersion hasn't changed
            }

            ReplaceCsprojVersionInFile(file, currentAssemblyVersion, newAssemblyVersion);
            Console.WriteLine(file);

            return true;
        }

        public AssemblyVersionDefinition ReadCsprojFileAttributes(string file)
        {
            string csprojContents;
            using (StreamReader sr = new StreamReader(file))
            {
                csprojContents = sr.ReadToEnd();
                sr.Close();
            }

            XmlDocument csprojDocument = new XmlDocument();
            csprojDocument.LoadXml(csprojContents);

            AssemblyVersionDefinition assemblyVersion = new AssemblyVersionDefinition();
            VersionDefinition attributeVersion = null;
            XmlNode versionNode;
            versionNode = csprojDocument.SelectSingleNode("/Project/PropertyGroup/AssemblyVersion");
            if (versionNode != null)
            {
                string assemblyVersionString = versionNode.InnerText;
                attributeVersion = VersionDefinition.FromString(assemblyVersionString);
                assemblyVersion.AssemblyVersion = attributeVersion;
            }

            versionNode = csprojDocument.SelectSingleNode("/Project/PropertyGroup/FileVersion");
            if (versionNode != null)
            {
                string assemblyFileVersionString = versionNode.InnerText;
                attributeVersion = VersionDefinition.FromString(assemblyFileVersionString);
                assemblyVersion.AssemblyFileVersion = attributeVersion;
            }

            versionNode = csprojDocument.SelectSingleNode("/Project/PropertyGroup/Version");
            if (versionNode != null)
            {
                string assemblyProjectVersionString = versionNode.InnerText;
                attributeVersion = VersionDefinition.FromString(assemblyProjectVersionString);
                assemblyVersion.AssemblyProjectVersion = attributeVersion;
            }

            return assemblyVersion;
        }

        public void ReplaceCsprojVersionInFile(string file, AssemblyVersionDefinition currentAssemblyVersion, AssemblyVersionDefinition newAssemblyVersion)
        {
            string assemblyInfoText = File.ReadAllText(file);
            if (newAssemblyVersion.AssemblyVersion != null)
            {
                assemblyInfoText = assemblyInfoText.Replace("<AssemblyVersion>" + currentAssemblyVersion.AssemblyVersion.ToString() + "</AssemblyVersion>", "<AssemblyVersion>" + newAssemblyVersion.AssemblyVersion.ToString() + "</AssemblyVersion>");
            }
            if (newAssemblyVersion.AssemblyFileVersion != null)
            {
                assemblyInfoText = assemblyInfoText.Replace("<FileVersion>" + currentAssemblyVersion.AssemblyFileVersion.ToString() + "</FileVersion>", "<FileVersion>" + newAssemblyVersion.AssemblyFileVersion.ToString() + "</FileVersion>");
            }
            if ((currentAssemblyVersion.AssemblyFileVersion != null) && (newAssemblyVersion.AssemblyProjectVersion != null))
            {
                assemblyInfoText = assemblyInfoText.Replace("<Version>" + currentAssemblyVersion.AssemblyProjectVersion.ToString() + "</Version>", "<Version>" + newAssemblyVersion.AssemblyProjectVersion.ToString() + "</Version>");
            }
            File.WriteAllText(file, assemblyInfoText);
        }

        public ushort? GetBuildNumber(DateTime stampTime)
        {
            string yearCode = stampTime.Year.ToString().Substring(2, 2); // The year within the century. Assumes the main version would change within the century
            string dayCode = stampTime.DayOfYear.ToString("000");
            //string hourCode = ComputeHourCode(stampTime.Hour).ToString("00"); // Convert hours to 1 digit number

            string buildCode = yearCode + dayCode;  // + hourCode;
            int iBuildNumber;
            if (Int32.TryParse(buildCode, out iBuildNumber))
            {
                if (iBuildNumber < 50000)
                {
                    return (ushort)iBuildNumber;
                }
                else
                {
                    // dates after 2050 will cause a problem. Loop those back to 00 for the year.
                    return (ushort)(iBuildNumber - 50000);
                }
            }

            return null;
        }

        // A unique function to convert a 24 hour number to a 1 digit field.
        private ushort ComputeHourCode(int hours)
        {
            if (hours < 0)
                return 0;

            ushort hourCode = (ushort)(hours / 2); // convert 0-23 to 0-11

            if (hourCode > 2)
            {
                hourCode -= 2;
            }
            else if ((hourCode == 1) || (hourCode == 2))
            {
                hourCode -= 1;
            }

            return hourCode;
        }
    }
}
