using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookScan_website.Models
{
    public class VersionItem
    {
        public int ServerVersion { get; set;}
        public int ClientMajorVersion { get; set;}
        public int ClientMinorVersion { get; set;}
        public int IdPublicationSteps { get; set;}
        public string ChangelogFr { get; set;}
        public string[] ChangelogPointFr { get; set;}
        public string ChangelogEn { get; set;}
        public string[] ChangelogPointEn { get; set;}
    }
}