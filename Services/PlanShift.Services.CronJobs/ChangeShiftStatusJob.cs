using System.Threading.Tasks;
using PlanShift.Data.Common;

namespace PlanShift.Services.CronJobs
{
    public class ChangeShiftStatusJob
    {
        private readonly IDbQueryRunner dbQueryRunner;

        public ChangeShiftStatusJob(IDbQueryRunner dbQueryRunner)
        {
            this.dbQueryRunner = dbQueryRunner;
        }

        public async Task Work()
        {
            await this.dbQueryRunner.RunQueryAsync(
                "EXEC ChangeShiftStatusToPassed");
        }
    }
}