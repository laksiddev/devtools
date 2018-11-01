using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildStamper
{
    public class VersionDefinition : IComparable
    {
        public VersionDefinition() : this(null, null, null, null) { }

        public VersionDefinition(ushort major, ushort minor) : this(major, minor, null, null) { }

        public VersionDefinition(ushort major, ushort minor, ushort revision) : this(major, minor, revision, null) { }

        public VersionDefinition(ushort major, ushort minor, ushort revision, ushort build) 
        {
            if ((major == UInt16.MaxValue) || (minor == UInt16.MaxValue) || (revision == UInt16.MaxValue) || (build == UInt16.MaxValue))
                throw new ArgumentException("Version numbers are not allowed beyond 65534.");

            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        private VersionDefinition(ushort? major, ushort? minor, ushort? revision, ushort? build)
        {
            if (((major.HasValue) && (major == UInt16.MaxValue)) || 
                ((minor.HasValue) && (minor == UInt16.MaxValue)) || 
                ((revision.HasValue) && (revision == UInt16.MaxValue)) || 
                ((build.HasValue) && (build == UInt16.MaxValue)))
                throw new ArgumentException("Version numbers are not allowed beyond 65534.");

            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        public bool IsValid()
        {
            return (VersionDepth >= 2);
        }

        public int VersionDepth
        {
            get
            {
                if (Major.HasValue && Minor.HasValue && Revision.HasValue && Build.HasValue)
                {
                    return 4;
                }
                else if (Major.HasValue && Minor.HasValue && Revision.HasValue && !Build.HasValue)
                {
                    return 3;
                }
                else if (Major.HasValue && Minor.HasValue && !Revision.HasValue && !Build.HasValue)
                {
                    return 2;
                }
                else if (Major.HasValue && !Minor.HasValue && !Revision.HasValue && !Build.HasValue)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public ushort? Major { get; set; }
        public ushort? Minor { get; set; }
        public ushort? Revision { get; set; }
        public ushort? Build { get; set; }

        public static VersionDefinition FromString(string versionString)
        {
            if (versionString == null)
                return null;

            string[] versionComponents = versionString.Split('.');
            if (versionComponents.Length > 4)   // there more than 4 components to the version number. It's invalid.
                return null;
            if (versionComponents.Length <= 1)   // there are 0 or 1 components to the version number. It's invalid.
                return null;

            VersionDefinition currentVersion = new VersionDefinition();
            ushort versionValue;
            if (!UInt16.TryParse(versionComponents[0], out versionValue))
            {
                return null; // invalid version format
            }
            else if(versionValue == UInt16.MaxValue)
            {
                return null;
            }
            currentVersion.Major = versionValue;

            if (!UInt16.TryParse(versionComponents[1], out versionValue))
            {
                return null; // invalid version format
            }
            else if (versionValue == UInt16.MaxValue)
            {
                return null;
            }
            currentVersion.Minor = versionValue;

            if (versionComponents.Length >= 3)
            {
                if (!UInt16.TryParse(versionComponents[2], out versionValue))
                {
                    return null; // invalid version format
                }
                else if (versionValue == UInt16.MaxValue)
                {
                    return null;
                }
                currentVersion.Revision = versionValue;
            }

            if (versionComponents.Length >= 4)
            {
                if (!UInt16.TryParse(versionComponents[3], out versionValue))
                {
                    return null; // invalid version format
                }
                else if (versionValue == UInt16.MaxValue)
                {
                    return null;
                }
                currentVersion.Build = versionValue;
            }

            return currentVersion;
        }

        public VersionDefinition Clone()
        {
            return VersionDefinition.FromString(this.ToString());

        }

        public override string ToString()
        {
            if (Major.HasValue && Minor.HasValue && Revision.HasValue && Build.HasValue)
            {
                return String.Join(".", Major.Value.ToString(), Minor.Value.ToString(), Revision.Value.ToString(), Build.Value.ToString());
            }
            else if (Major.HasValue && Minor.HasValue && Revision.HasValue && !Build.HasValue)
            {
                return String.Join(".", Major.Value.ToString(), Minor.Value.ToString(), Revision.Value.ToString());
            }
            else if (Major.HasValue && Minor.HasValue && !Revision.HasValue && !Build.HasValue)
            {
                return String.Join(".", Major.Value.ToString(), Minor.Value.ToString());
            }
            else
            {
                return "InvalidFormat";
            }
        }

        public int CompareTo(object obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return 1;

            if (!(obj is VersionDefinition))
                return 1;

            VersionDefinition compareVerion = (VersionDefinition)obj;
            if (this.Major.HasValue && !compareVerion.Major.HasValue)
                return 1;
            if (!this.Major.HasValue && compareVerion.Major.HasValue)
                return -1;

            int iComparison;
            if (this.Major.HasValue && compareVerion.Major.HasValue)
            {
                iComparison = this.Major.Value.CompareTo(compareVerion.Major.Value);
                if (iComparison != 0)
                    return iComparison;
            }
            // Major values are the same (both null or both the same uint)

            if (this.Minor.HasValue && !compareVerion.Minor.HasValue)
                return 1;
            if (!this.Minor.HasValue && compareVerion.Minor.HasValue)
                return -1;

            if (this.Minor.HasValue && compareVerion.Minor.HasValue)
            {
                iComparison = this.Minor.Value.CompareTo(compareVerion.Minor.Value);
                if (iComparison != 0)
                    return iComparison;
            }
            // Minor values are the same (both null or both the same uint)

            if (this.Revision.HasValue && !compareVerion.Revision.HasValue)
                return 1;
            if (!this.Revision.HasValue && compareVerion.Revision.HasValue)
                return -1;

            if (this.Revision.HasValue && compareVerion.Revision.HasValue)
            {
                iComparison = this.Revision.Value.CompareTo(compareVerion.Revision.Value);
                if (iComparison != 0)
                    return iComparison;
            }
            // Revision  values are the same (both null or both the same uint)

            if (this.Build.HasValue && !compareVerion.Build.HasValue)
                return 1;
            if (!this.Build.HasValue && compareVerion.Build.HasValue)
                return -1;

            if (this.Build.HasValue && compareVerion.Build.HasValue)
            {
                iComparison = this.Build.Value.CompareTo(compareVerion.Build.Value);
                if (iComparison != 0)
                    return iComparison;
            }
            // Build values are the same (both null or both the same uint)

            return 0;
        }

        //private static int PerformComparison(VersionDefinition lhs, VersionDefinition rhs)
        //{
        //    if (lhs.Major.HasValue && !rhs.Major.HasValue)
        //        return 1;
        //    if (!lhs.Major.HasValue && rhs.Major.HasValue)
        //        return -1;

        //    int iComparison;
        //    if (lhs.Major.HasValue && rhs.Major.HasValue)
        //    {
        //        iComparison = lhs.Major.Value.CompareTo(rhs.Major.Value);
        //        if (iComparison != 0)
        //            return iComparison;
        //    }
        //    // Major values are the same (both null or both the same uint)

        //    if (lhs.Minor.HasValue && !rhs.Minor.HasValue)
        //        return 1;
        //    if (!lhs.Minor.HasValue && rhs.Minor.HasValue)
        //        return -1;

        //    if (lhs.Minor.HasValue && rhs.Minor.HasValue)
        //    {
        //        iComparison = lhs.Minor.Value.CompareTo(rhs.Minor.Value);
        //        if (iComparison != 0)
        //            return iComparison;
        //    }
        //    // Minor values are the same (both null or both the same uint)

        //    if (lhs.Revision.HasValue && !rhs.Revision.HasValue)
        //        return 1;
        //    if (!lhs.Revision.HasValue && rhs.Revision.HasValue)
        //        return -1;

        //    if (lhs.Revision.HasValue && rhs.Revision.HasValue)
        //    {
        //        iComparison = lhs.Revision.Value.CompareTo(rhs.Revision.Value);
        //        if (iComparison != 0)
        //            return iComparison;
        //    }
        //    // Revision  values are the same (both null or both the same uint)

        //    if (lhs.Build.HasValue && !rhs.Build.HasValue)
        //        return 1;
        //    if (!lhs.Build.HasValue && rhs.Build.HasValue)
        //        return -1;

        //    if (lhs.Build.HasValue && rhs.Build.HasValue)
        //    {
        //        iComparison = lhs.Build.Value.CompareTo(rhs.Build.Value);
        //        if (iComparison != 0)
        //            return iComparison;
        //    }
        //    // Build values are the same (both null or both the same uint)

        //    return 0;
        //}

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            return (this.CompareTo(obj) == 0);
        }

        public static bool operator ==(VersionDefinition lhs, VersionDefinition rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                return (Object.ReferenceEquals(rhs, null));
            }

            return (lhs.CompareTo(rhs) == 0);
        }

        public static bool operator !=(VersionDefinition lhs, VersionDefinition rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                return !(Object.ReferenceEquals(rhs, null));
            }

            return (lhs.CompareTo(rhs) != 0);
        }

        public override int GetHashCode()
        {
            return Major.GetHashCode() ^ Minor.GetHashCode() ^ Revision.GetHashCode() ^ Build.GetHashCode();
        }
    }
}
