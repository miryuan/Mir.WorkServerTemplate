namespace Mir.WorkServer.DependencyInjection.Orm
{
    public class MirSqlSugarClient : SqlSugar.SqlSugarClient
    {
        public MirSqlSugarClient(Model.DbConnection config) : base(new SqlSugar.ConnectionConfig()
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
