using SqlSugar;

namespace Mir.WindowsServiceTemplate.DependencyInjection.Orm
{
    public interface ISqlSugarDbContextFactory
    {
        SqlSugarClient DbContext(string name);
    }
}
