using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsectSurvey.Models
{
    public class Item
    {
        // [JsonProperty(PropertyName = "id")]
        // public string Id { get; set; }

        // [JsonProperty(PropertyName = "name")]
        // public string Name { get; set; }

        // [JsonProperty(PropertyName = "description")]
        // public string Description { get; set; }

        // [JsonProperty(PropertyName = "isComplete")]
        // public bool Completed { get; set; }

        public int QuestionID { get; set; }
        public int TableID { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
    }
}
