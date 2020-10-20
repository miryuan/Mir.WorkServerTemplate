using SqlSugar;

namespace Mir.WorkServer.DependencyInjection.Orm
{
    /// <summary>
    /// 数据库配置扩展
    /// </summary>
    public class MirSqlSugarDbConnectOption : ConnectionConfig
    {
        /// <summary>
        /// 数据库连接名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 是否默认库
        /// </summary>
        public bool Default { set; get; } = true;
    }
}
