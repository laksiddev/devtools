using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildStamper
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectPath;
            VersionDefinition newVision;
            if (args.Length <= 1)
            {
                if (args.Length == 0)
                {
                    projectPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                    projectPath = projectPath.Replace(@"file:///", "");
                    projectPath = Path.GetDirectoryName(projectPath);
                }
                else
                {
                    projectPath = Path.GetDirectoryName(args[0]);
                }

                DirectoryInfo dirInfo = new DirectoryInfo(projectPath);
                if (!dirInfo.Exists)
                {
                    Console.WriteLine(String.Format("The specified directory does not exist: {0}", projectPath));
                    return;
                }
                else
                {
                    projectPath = dirInfo.FullName;
                }

                Console.WriteLine(String.Format("BuildStamper: {0}", projectPath));

                newVision = null;
            }
            else
            {
                if (args[0].StartsWith("-"))
                {
                    projectPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                    projectPath = projectPath.Replace(@"file:///", "");
                    projectPath = Path.GetDirectoryName(projectPath);
                }
                else
                {
                    projectPath = Path.GetDirectoryName(args[0]);
                }

                DirectoryInfo dirInfo = new DirectoryInfo(projectPath);
                if (!dirInfo.Exists)
                {
                    Console.WriteLine(String.Format("The specified directory does not exist: {0}", projectPath));
                    return;
                }
                else
                {
                    projectPath = dirInfo.FullName;
                }

                Console.WriteLine(String.Format("BuildStamper: {0}", projectPath));

                newVision = null;
                for (int i= 0; i<args.Length; i++)
                {
                    if ((String.Compare(args[i], "-v", StringComparison.CurrentCultureIgnoreCase) ==0) && ((i+1) < args.Length))
                    {
                        string versionString = args[i + 1];
                        VersionDefinition tempVision = VersionDefinition.FromString(versionString);
                        if (tempVision.VersionDepth == 3)
                        {
                            // only use this version if it's a three level version number
                            newVision = tempVision;
                        }
                        else
                        {
                            Console.WriteLine("Invalid version number. Version number must be in the format [major].[minor].[revision].");
                        }
                    }
                }
            }

            if (newVision != null)
            {
                Console.WriteLine(String.Format("Updating version number to: {0}", newVision.ToString()));
            }

            BuildStamperUtility stamper = new BuildStamperUtility();
            stamper.EnumerateDirectories(projectPath, newVision);
        }
    }
}
