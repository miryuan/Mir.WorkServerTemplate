using SqlSugar;

namespace Mir.WorkServer.Extension.Orm
{
    public interface ISqlSugarDbContextFactory
    {
        SqlSugarClient DbContext(string name);
    }
}
