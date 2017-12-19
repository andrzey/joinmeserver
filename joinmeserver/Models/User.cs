using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace joinmeserver.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string FacebookId { get; set; }
        public string FirstName { get; set; }
        public List<string> Interests { get; set; }
    }
}