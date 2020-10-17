namespace Mir.WorkServer.Extension.Orm
{
    public class MirSqlSugarClient : SqlSugar.SqlSugarClient
    {
        public MirSqlSugarClient(Model.Connection config) : base(new SqlSugar.ConnectionConfig()
        {
            ConnectionString = config.ConnectionString,
            DbType = config.DbType,
            IsAutoCloseConnection = config.IsAutoCloseConnection
        })
        {
            DbName = config.Name;
            Default = config.IsDefault;
        }

        public string DbName { set; get; }
        public bool Default { set; get; }
    }
}
