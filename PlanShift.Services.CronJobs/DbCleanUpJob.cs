using System.Threading.Tasks;
using PlanShift.Data.Common;

namespace PlanShift.Services.CronJobs
{
    public class DbCleanUpJob
    {
        private readonly IDbQueryRunner queryRunner;

        public DbCleanUpJob(IDbQueryRunner queryRunner)
        {
            this.queryRunner = queryRunner;
        }

        public async Task Work()
        {
            await this.queryRunner.RunQueryAsync(
                "EXEC DeleteInvitationVerifications");

            await this.queryRunner.RunQueryAsync(
                "EXEC DeleteFromShiftChangesOlderThan3Months");

            await this.queryRunner.RunQueryAsync(
                "EXEC DeleteFromShiftApplicationsOlderThan3Months");
        }
    }
}