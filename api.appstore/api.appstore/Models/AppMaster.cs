//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace api.appstore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppMaster
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
        public string ScreenShots { get; set; }
        public string Documents { get; set; }
        public bool Published { get; set; }
    }
}
