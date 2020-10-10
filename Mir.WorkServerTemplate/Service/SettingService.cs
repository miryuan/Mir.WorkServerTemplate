using Mir.WorkServer.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace Mir.WorkServer.Service
{
    /// <summary>
    /// 本地配置文件数据库
    /// </summary>
    public class SettingService
    {
        public SqlSugarClient Client { get; private set; }//这个时候因为TestService3给静态变相的Db也变成静态的了
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType">SqlSugar数据库类型</param>
        /// <param name="isAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="connectionString">链接字符串</param>
        public SettingService(DbType dbType, bool isAutoCloseConnection, string connectionString)
        {
            Client = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = dbType,
                ConnectionString = connectionString,
                IsAutoCloseConnection = isAutoCloseConnection,
                InitKeyType = InitKeyType.Attribute //SystemTable用于Codefirst从model库生成数据库表的;而Attribute用于DbFirst
            });
            Client.Aop.OnLogExecuting = (sql, paramters) =>
            {
                Console.WriteLine(sql);
            };

            CheckSettingsTable();
        }

        /// <summary>
        /// 检查表是否存在，若不存在则创建它
        /// </summary>
        private void CheckSettingsTable()
        {
            var tables = Client.DbMaintenance.GetTableInfoList();
            int idx = tables.FindIndex(c => c.Name == typeof(Settings).Name);
            if (idx == -1)
            {
                InitSettingsTable();
                InitSettingsTableData();
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        private void InitSettingsTable()
        {
            Client.CodeFirst.InitTables<Settings>();
        }

        /// <summary>
        /// 创建表数据
        /// </summary>
        private void InitSettingsTableData()
        {
            List<Settings> li = new List<Settings>();
            li.Add(new Settings() { key = "ApplicationTitle", value = "模板程序" });

            Client.Insertable<Settings>(li).ExecuteCommand();
        }
    }
}
