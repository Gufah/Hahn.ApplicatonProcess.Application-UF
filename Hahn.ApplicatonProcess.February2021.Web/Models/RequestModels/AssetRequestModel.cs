﻿using FluentValidation;
using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Hahn.ApplicatonProcess.February2021.Web.Models
{
    public class AssetRequestModel
    {
        [JsonProperty("asset_name")]
        public string AssetName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("department")]
        public Department Department { get; set; }

        [JsonProperty("country")]
        public string DepartmentCountry { get; set; }

        [JsonProperty("department_email")]
        public string DepartmentEmail{ get; set; }

        [JsonProperty("purchase_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("broken")]
        public bool Broken { get; set; } = false;

    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-mm-dd";
        }
    }
}