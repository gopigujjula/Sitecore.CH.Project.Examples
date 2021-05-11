using System;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Model
{
    internal class Asset
    {
        public long EntityId { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public long MasterFile { get; set; }
        public string DuplicateFileMapperId { get; set; }
    }
}
