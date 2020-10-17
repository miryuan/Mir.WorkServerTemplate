using SqlSugar;
using System;

namespace Mir.WorkServer.Service
{
    public class SqlSugarContextService
    {
        public SqlSugarClient Client { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        public SqlSugarContextService(Model.Connection config)
        {
            Client = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = config.DbType,
                ConnectionString = config.ConnectionString,
                IsAutoCloseConnection = config.IsAutoCloseConnection,
                InitKeyType = InitKeyType.Attribute //SystemTable用于Codefirst从model库生成数据库表的;而Attribute用于DbFirst
            });
        }
    }
}
