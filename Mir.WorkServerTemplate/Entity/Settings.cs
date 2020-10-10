using SqlSugar;
using System;

namespace Mir.WorkServer.Entity
{
    /// <summary>
    /// 程序的配置文件数据表实体
    /// </summary>
    [SugarTable("Settings", TableDescription = "程序的配置文件数据表实体")]
    public class Settings
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true, ColumnDescription = "主键")]
        public int id { get; set; }
        /// <summary>
        /// 配置键
        /// </summary>
        [SugarColumn(ColumnDescription = "主键")]
        public string key { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        [SugarColumn(ColumnDescription = "主键")]
        public string value { get; set; }
    }
}
