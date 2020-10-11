namespace Mir.WorkServer.Model
{
    /// <summary>
    /// appsettings.json中数据库连接读取
    /// </summary>
    public class Connection
    {
        public string Name { get; set; }
        public SqlSugar.DbType DbType { get; set; }
        public bool IsAutoCloseConnection { get; set; }
        public string ConnectionString { get; set; }
    }
}
