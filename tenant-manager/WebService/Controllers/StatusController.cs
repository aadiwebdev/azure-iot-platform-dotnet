// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmm.Platform.IoT.Common.Services;
using Mmm.Platform.IoT.Common.WebService.v1.Filters;
using MMM.Azure.IoTSolutions.TenantManager.WebService.Models;
using MMM.Azure.IoTSolutions.TenantManager.WebService.Runtime;

namespace MMM.Azure.IoTSolutions.TenantManager.WebService.Controllers
{
    [Route("v1/[controller]"), TypeFilter(typeof(ExceptionsFilterAttribute))]
    public sealed class StatusController : ControllerBase
    {
        private readonly IConfig config;
        private readonly IStatusService statusService;

        public StatusController(IConfig config, IStatusService statusService)
        {
            this.config = config;
            this.statusService = statusService;
        }
        [HttpGet]
        public async Task<StatusModel> GetAsync()
        {
            try
            {
                var result = new StatusModel(await this.statusService.GetStatusAsync(false));
                result.Properties.Add("Port", this.config.Port.ToString());
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while attempting to get the service status", e);
            }
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return new StatusCodeResult(200);
        }
    }
}
