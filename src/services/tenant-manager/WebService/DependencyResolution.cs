// <copyright file="DependencyResolution.cs" company="3M">
// Copyright (c) 3M. All rights reserved.
// </copyright>

using System.Reflection;
using Autofac;
using Mmm.Iot.Common.Services;
using Mmm.Iot.TenantManager.Services;

namespace Mmm.Iot.TenantManager.WebService
{
    public class DependencyResolution : DependencyResolutionBase
    {
        protected override void SetupCustomRules(ContainerBuilder builder)
        {
            var assembly = typeof(StatusService).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
        }
    }
}