using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Models
{
    public class BaseReponseModel<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = true;
        [JsonProperty("errors")]
        public string[] Errors { get; set; } = {};
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
