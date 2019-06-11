using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.appstore.Models
{
    public class ThirdPartyDto
    {
        public System.Guid Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string ThirdPartyAppUrl { get; set; }
        public List<string> Documents { get; set; } = new List<string>();
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
    }
}