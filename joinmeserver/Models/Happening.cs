using System;
using System.Collections.Generic;

namespace joinmeserver.Models
{
    public class Happening
    {
        public Guid Id { get; set; }
        public string CreatedByUser { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public List<Comment> Comments { get; set; }
        public List<User> Users { get; set; }
    }
}
