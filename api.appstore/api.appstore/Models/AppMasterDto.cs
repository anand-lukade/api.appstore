using System;
using System.Collections.Generic;

namespace api.appstore.Models
{
    public class AppMasterDto
    {
        public System.Guid Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Icon { get; set; }
        public string AndriodSmartPhoneBuild { get; set; }
        public string AndriodTabletBuild { get; set; }
        public string IphoneBuild { get; set; }
        public string IphonePackageName { get; set; }
        public string IpadBuild { get; set; }
        public string IpadPackageName { get; set; }
        public List<string> ScreenShots { get; set; } = new List<string>();
        public List<string> Documents { get; set; } = new List<string>();
        public bool Published { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }        
    }
}