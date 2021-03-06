// <copyright file="DeviceGroupCondition.cs" company="3M">
// Copyright (c) 3M. All rights reserved.
// </copyright>

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mmm.Iot.Config.Services.Models
{
    public class DeviceGroupCondition
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Operator")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OperatorType Operator { get; set; }

        [JsonProperty("Value")]
        public object Value { get; set; }
    }
}