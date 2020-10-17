namespace Mir.WorkServer.Model
{
    /// <summary>
    /// appsettings.json中数据库连接读取
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 数据链接名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否为默认数据库
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 数据库链接类型（MySql,SqlServer,Sqlite,Oracle,PostgreSQL）
        /// </summary>
        public SqlSugar.DbType DbType { get; set; }
        /// <summary>
        /// 是否自动关闭链接
        /// </summary>
        public bool IsAutoCloseConnection { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
