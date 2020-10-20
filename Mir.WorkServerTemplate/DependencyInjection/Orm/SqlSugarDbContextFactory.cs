using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mir.WorkServer.DependencyInjection.Orm
{
    public class SqlSugarDbContextFactory : ISqlSugarDbContextFactory
    {
        private readonly IEnumerable<MirSqlSugarClient> _clients;

        public SqlSugarDbContextFactory(IEnumerable<MirSqlSugarClient> clients)
        {
            _clients = clients;
        }

        public SqlSugarClient DbContext(string name = "Default")
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            MirSqlSugarClient dbContext;
            if (name == "Default")
            {
                dbContext = _clients.ToList().Find(c => c.Default);
            }
            else
            {
                dbContext = _clients.FirstOrDefault(it => it.DbName.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            if (dbContext == null)
                throw new ArgumentException("can not find a match dbcontext!");

            return dbContext;
        }
    }
}
