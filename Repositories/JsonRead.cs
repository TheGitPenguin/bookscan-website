using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using bookScan_website.Models;

namespace bookScan_website.Repositories
{
    public class JsonRead
    {
        private long GetUnixTimeSeconds()
        {
            DateTimeOffset dto = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return dto.ToUnixTimeSeconds();
        }

        private VersionsFile? _VersionsFile;
        private long _LastUpdate;
        public JsonRead()
        {
            string fileName = "/var/bookscan/versions.json";
            string jsonString = File.ReadAllText(fileName);

            _LastUpdate = GetUnixTimeSeconds();

            _VersionsFile = JsonSerializer.Deserialize<VersionsFile>(jsonString);
        }

        public VersionsFile? GetVerionsFile()
        {
            if (GetUnixTimeSeconds() - _LastUpdate >= 20 || _VersionsFile == null)
            {
                string fileName = "/var/bookscan/versions.json";
                string jsonString = File.ReadAllText(fileName);

                _LastUpdate = GetUnixTimeSeconds();
                _VersionsFile = JsonSerializer.Deserialize<VersionsFile>(jsonString);
            }

            return _VersionsFile;
        }
    }
}