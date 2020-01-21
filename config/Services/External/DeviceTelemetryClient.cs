﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mmm.Platform.IoT.Config.Services.Helpers;
using Mmm.Platform.IoT.Common.Services;
using Mmm.Platform.IoT.Common.Services.Config;

namespace Mmm.Platform.IoT.Config.Services.External
{
    public class DeviceTelemetryClient : IDeviceTelemetryClient
    {
        private readonly IHttpClientWrapper httpClient;
        private readonly string serviceUri;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string TENANT_HEADER = "ApplicationTenantID";
        private const string TENANT_ID = "TenantID";
        public DeviceTelemetryClient(
            IHttpClientWrapper httpClient,
            AppConfig config,
            IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.serviceUri = config.ExternalDependencies.TelemetryServiceUrl;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateRuleAsync(RuleApiModel rule, string etag)
        {
            SetHttpClientHeaders();
            rule.ETag = etag;

            await this.httpClient.PutAsync($"{this.serviceUri}/rules/{rule.Id}", $"Rule {rule.Id}", rule);
        }

        private void SetHttpClientHeaders()
        {
            if (this._httpContextAccessor != null && this.httpClient != null)
            {
                string tenantId = this._httpContextAccessor.HttpContext.Request.GetTenant();
                this.httpClient.SetHeaders(new Dictionary<string, string> { { TENANT_HEADER, tenantId } });
            }
        }
    }
}
