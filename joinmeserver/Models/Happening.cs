using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace joinmeserver.Models
{
    public class Happening
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string CreatedByUser { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public List<Comment> Comments { get; set; }
        public List<User> Users { get; set; }
    }
}
