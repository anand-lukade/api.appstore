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
    
    public partial class ThirdParty
    {
        public System.Guid Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string ThirdPartyAppUrl { get; set; }
        public string Documents { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public Nullable<int> Download { get; set; }
    }
}
