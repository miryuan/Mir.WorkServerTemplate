using SqlSugar;

namespace Mir.WorkServer.DependencyInjection.Orm
{
    public interface ISqlSugarDbContextFactory
    {
        SqlSugarClient DbContext(string name);
    }
}
