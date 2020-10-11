using Microsoft.Extensions.DependencyInjection;
using Mir.WorkServer.Service;
using SqlSugar;

namespace Mir.WorkServer.Extension
{
    public static class SqlSugarExtension
    {
        /// <summary>
        /// 注入本地配置文件数据库(Scoped)
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddScopedSettingService(this IServiceCollection service, DbType dbType, bool isAutoCloseConnection, string connectionString)
        {
            return service.AddScoped(factory => new SettingService(
                 dbType, isAutoCloseConnection,
                 connectionString
            ));
        }

        /// <summary>
        /// 注入本地配置文件数据库(Singleton)
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonSettingService(this IServiceCollection service, DbType dbType, bool isAutoCloseConnection, string connectionString)
        {
            return service.AddSingleton(factory => new SettingService(
                 dbType, isAutoCloseConnection,
                 connectionString
            ));
        }

        /// <summary>
        /// 注入本地配置文件数据库(Transient)
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientSettingService(this IServiceCollection service, DbType dbType, bool isAutoCloseConnection, string connectionString)
        {
            return service.AddTransient(factory => new SettingService(
                 dbType, isAutoCloseConnection,
                 connectionString
            ));
        }

        /// <summary>
        /// 注入默认数据库服务(Transient)
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientDefaultSqlSugarService(this IServiceCollection service, DbType dbType, bool isAutoCloseConnection, string connectionString)
        {
            return service.AddTransient(factory => new DefaultDataBaseService(
                 dbType, isAutoCloseConnection,
                 connectionString
            ));
        }
    }
}
