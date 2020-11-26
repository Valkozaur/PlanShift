namespace PlanShift.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data;
    using PlanShift.Data.Models;

    public class TableStoredProcedureCallerCaller
    {
        private readonly ApplicationDbContext context;

        public TableStoredProcedureCallerCaller(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ShiftCalendar>> ExecuteTableProcedure(string sql, string businessId, string userId)
        {
            //var concatenatedParameters = new StringBuilder();
            //for (int i = 0; i < parameters.Length; i++)
            //{
            //    var parameter = parameters[i];

            //    if (i == 0)
            //    {
            //        concatenatedParameters.Append($" '{parameter}'");
            //        continue;
            //    }

            //    concatenatedParameters.Append($", '{parameter}'");
            

            return await this.context.ShiftsCalendar.FromSqlInterpolated($"{sql} '{businessId}', '{userId}'").ToListAsync();
        }
    }
}