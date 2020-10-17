using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mir.WorkServer.Extension.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mir.WorkServer.Extension
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注入SqlSugar数据库组件
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="contextLifetime">生命周期</param>
        /// <param name="section">appsettings.json中数据库的key名称</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSqlSugar(this IServiceCollection services,
            //ServiceLifetime contextLifetime = ServiceLifetime.Transient,
            string section = "DBConnection")
        {
            ServiceLifetime contextLifetime = ServiceLifetime.Transient;


            var configuration = (IConfigurationRoot)services.FirstOrDefault(p => p.ServiceType == typeof(IConfigurationRoot)).ImplementationInstance;
            var connectOptions = configuration.GetSection(section).Get<List<Model.Connection>>();
            if (connectOptions != null)
            {
                
                switch (contextLifetime)
                {
                    case ServiceLifetime.Scoped:
                        {
                            foreach (var option in connectOptions) services.AddScoped(s => new MirSqlSugarClient(option));
                            services.AddSingleton<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
                        }
                        break;
                    case ServiceLifetime.Singleton:
                        {
                            foreach (var option in connectOptions) services.AddSingleton(s => new MirSqlSugarClient(option));
                            services.AddScoped<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
                        }
                        break;
                    case ServiceLifetime.Transient:
                        {
                            foreach (var option in connectOptions) services.AddTransient(s => new MirSqlSugarClient(option));
                            services.AddTransient<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
                        }
                        break;
                    default:
                        {
                            foreach (var option in connectOptions) services.AddTransient(s => new MirSqlSugarClient(option));
                            services.AddTransient<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
                        }
                        break;
                }
            }

            return services;
        }

        ///// <summary>
        ///// 添加sqlSugar多数据库支持
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        ///// <param name="contextLifetime"></param>
        ///// <param name="section"></param>
        ///// <returns></returns>
        //public static IServiceCollection AddSqlSugarDbContext(this IServiceCollection services,
        //    IConfiguration configuration,
        //    ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        //    string section = "DBConnection")
        //{
        //    var connectOptions = configuration.GetSection(section).Get<List<SqlSugarDbConnectOption>>();
        //    if (connectOptions != null)
        //    {
        //        foreach (var option in connectOptions)
        //        {
        //            if (contextLifetime == ServiceLifetime.Scoped)
        //                services.AddScoped(s => new BucketSqlSugarClient(option));
        //            if (contextLifetime == ServiceLifetime.Singleton)
        //                services.AddSingleton(s => new BucketSqlSugarClient(option));
        //            if (contextLifetime == ServiceLifetime.Transient)
        //                services.AddTransient(s => new BucketSqlSugarClient(option));
        //        }
        //        if (contextLifetime == ServiceLifetime.Singleton)
        //            services.AddSingleton<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
        //        if (contextLifetime == ServiceLifetime.Scoped)
        //            services.AddScoped<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
        //        if (contextLifetime == ServiceLifetime.Transient)
        //            services.AddTransient<ISqlSugarDbContextFactory, SqlSugarDbContextFactory>();
        //    }
        //    return services;
        //}
    }
}
