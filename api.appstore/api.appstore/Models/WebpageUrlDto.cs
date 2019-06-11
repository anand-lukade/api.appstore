using System;
using System.Collections.Generic;

namespace api.appstore.Models
{
    public class WebpageUrlDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WebPageUrl { get; set; }
        public List<string> Documents { get; set; } = new List<string>();
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
    }
}