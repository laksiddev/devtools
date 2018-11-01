using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildStamper
{
    public class AssemblyVersionDefinition
    {
        public VersionDefinition AssemblyVersion { get; set; }
        public VersionDefinition AssemblyFileVersion { get; set; }
        public VersionDefinition AssemblyProjectVersion { get; set; }
    }
}
