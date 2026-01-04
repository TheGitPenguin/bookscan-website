using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookScan_website.Models;
using bookScan_website.Repositories;

namespace bookScan_website.Services
{
    public class VersionItemServices
    {
        static int GetValueVersionItem(VersionItem item)
        {
            return item.ServerVersion * 1000 + item.ClientMajorVersion * 100 + item.ClientMinorVersion * 10 + item.IdPublicationSteps;
        }
        private JsonRead? _JsonRead;

        private void EnsureJsonReader()
        {
            if (_JsonRead == null)
            {
                _JsonRead = new JsonRead();
            }
        }

        public IEnumerable<VersionItem> GetAllVersionItems()
        {
            EnsureJsonReader();

            VersionsFile? versionsFile = _JsonRead.GetVerionsFile();

            if (versionsFile == null || versionsFile.versions == null || versionsFile.versions.Length == 0)
            {
                return Enumerable.Empty<VersionItem>();
            }

            return versionsFile.versions
                .OrderByDescending(GetValueVersionItem)
                .ToArray();
        }

        public VersionItem GetLastVersionItem()
        {
            EnsureJsonReader();

            VersionsFile? versionsFile = _JsonRead.GetVerionsFile();

            if (versionsFile == null)
            {
                VersionItem defaultVersionItem = new VersionItem();
                defaultVersionItem.ServerVersion = 0;
                defaultVersionItem.ClientMajorVersion = 0;
                defaultVersionItem.ClientMinorVersion = 0;
                defaultVersionItem.IdPublicationSteps = 0;

                return defaultVersionItem;
            }

            VersionItem[] versionItems = versionsFile.versions;

            if (versionItems.Length == 0)
            {
                VersionItem defaultVersionItem = new VersionItem();
                defaultVersionItem.ServerVersion = 0;
                defaultVersionItem.ClientMajorVersion = 0;
                defaultVersionItem.ClientMinorVersion = 0;
                defaultVersionItem.IdPublicationSteps = 0;

                return defaultVersionItem;
            }

            VersionItem? lastVersionItem = null;

            if (lastVersionItem == null)
            {
                lastVersionItem = versionItems[0];
            }

            foreach (VersionItem item in versionItems)
            {
                int valueItem = GetValueVersionItem(item);
                int valueLastItem = GetValueVersionItem(lastVersionItem);

                if (valueItem > valueLastItem || item.ServerVersion > lastVersionItem.ServerVersion)
                {
                    lastVersionItem = item;
                    continue;
                }
            }

            return lastVersionItem;
        }
    }
}