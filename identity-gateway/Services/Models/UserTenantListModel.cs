using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mmm.Platform.IoT.IdentityGateway.Services.Models
{
    public class UserTenantListModel
    {
        public UserTenantListModel()
        {
        }

        public UserTenantListModel(List<UserTenantModel> models)
        {
            this.Models = models;
        }

        public UserTenantListModel(string batchMethod, List<UserTenantModel> models)
        {
            this.BatchMethod = batchMethod;
            this.Models = models;
        }

        [JsonProperty("Method", Order=10)]
        public string BatchMethod { get; set; }

        [JsonProperty("Models", Order=20)]
        public List<UserTenantModel> Models { get; set; }
    }
}