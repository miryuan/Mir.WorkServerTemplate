using SqlSugar;
using System;

namespace Mir.WorkServer.Service
{
    public class DefaultDataBaseService
    {
        public SqlSugarClient Client { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        public DefaultDataBaseService(DbType dbType, bool isAutoCloseConnection, string connectionString)
        {
            Client = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = dbType,
                ConnectionString = connectionString,
                IsAutoCloseConnection = isAutoCloseConnection,
                InitKeyType = InitKeyType.Attribute //SystemTable用于Codefirst从model库生成数据库表的;而Attribute用于DbFirst
            }); 
        }
    }
}
