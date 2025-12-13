using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookScan_website.Models
{
    public class VersionsFile
    {
        public VersionSteps PublicationSteps { get; set;}
        public VersionItem[] versions { get; set;} // yes, it's missing upper case character 
    }
}