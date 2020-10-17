using Mir.WorkServer.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Mir.WorkServer.Service
{
    /// <summary>
    /// 本地配置文件数据库
    /// </summary>
    public class SettingService
    {
        /// <summary>
        /// SqlSugar客户端
        /// </summary>
        private SqlSugarClient Client { get; set; }
        /// <summary>
        /// 配置文件字典
        /// </summary>
        public Dictionary<string, string> SettingsDictionary { get; private set; }
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
            //Client.Aop.OnLogExecuting = (sql, paramters) =>
            //{
            //    Console.WriteLine(sql);
            //};

            CheckSettingsTable();
        }

        private List<Settings> GetKeyValueList()
        {
            return Client.Queryable<Settings>().ToList();
        }

        /// <summary>
        /// 刷新配置文件字典
        /// </summary>
        public void RefreshDictionary()
        {
            if (SettingsDictionary != null)
                SettingsDictionary.Clear();
            else
                SettingsDictionary = new Dictionary<string, string>();

            var li = GetKeyValueList();
            foreach (var item in li)
                SettingsDictionary.Add(item.key, item.value);
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddSetting(string key, string value)
        {
            bool flag = false;
            if (!SettingsDictionary.ContainsKey(key.Trim()))
            {
                flag = Client.Insertable<Settings>(new Settings() { key = key.Trim(), value = value.Trim() }).ExecuteCommand() > 0 ? true : false;
                RefreshDictionary();
            }

            return flag;
        }

        /// <summary>
        /// 检查Key是否在字典中
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckKey(string key)
        {
            return SettingsDictionary.ContainsKey(key.Trim());
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public string GetSetting(string key)
        {
            if (CheckKey(key))
                return SettingsDictionary[key.Trim()];
            else
                return string.Empty;
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
            RefreshDictionary();
        }

        #region 创建表和数据库以及填充基础数据
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
            li.Add(new Settings() { key = "DBBuildDate", value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            Client.Insertable<Settings>(li).ExecuteCommand();
        }
        #endregion
    }
}
