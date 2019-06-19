using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.appstore.Models
{
    public class CommentReview
    {
        public int Id { get; set; }       
        public string Comment { get; set; }
        public string Review { get; set; }
        public string Username { get; set; }
        public DateTime? CommentDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewUsername { get; set; }
    }
}