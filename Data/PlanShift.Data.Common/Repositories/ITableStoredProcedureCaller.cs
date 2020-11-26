namespace PlanShift.Data.Common.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITableStoredProcedureCaller<TEntity>
        where TEntity : class
    {
        public Task<IEnumerable<TEntity>> ExecuteTableProcedure(string sql, string  businessId, string userId);
    }
}
