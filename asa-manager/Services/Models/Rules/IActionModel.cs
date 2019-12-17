// Copyright (c) Microsoft. All rights reserved.

using System.Collections.Generic;
using Mmm.Platform.IoT.AsaManager.Services.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mmm.Platform.IoT.AsaManager.Services.Models.Rules
{
    [JsonConverter(typeof(ActionConverter))]
    public interface IActionModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Type")]
        ActionType Type { get; set; }

        // Dictionary should always be initialized as a case-insensitive dictionary
        [JsonProperty("Parameters")]
        IDictionary<string, object> Parameters { get; set; }
    }

    public enum ActionType
    {
        Email
    }
}
