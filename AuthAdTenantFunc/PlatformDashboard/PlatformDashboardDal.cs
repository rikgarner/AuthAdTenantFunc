using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AuthAdTenantFunc.PlatformDashboard
{
    public interface IPlatformDashboardDal
    {

    }
    public class PlatformDashboardDal
    {
        public async Task<PlatformStatisticsModel> Execute()
        {
            SqlConnection dbConnection = new SqlConnection
            {
                ConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString")
            };
            var statisticModel = new PlatformStatisticsModel();
            await using (dbConnection)
            {
                await dbConnection.OpenAsync();
                var data = await dbConnection.QueryMultipleAsync("Mgt.PlatformStatisticsFetch");
                var block1 = data.ReadSingle();
                statisticModel.TotalUserCount = block1.TotalUsers;
                statisticModel.LiveUserCount = block1.LiveUsers;
                statisticModel.GhostUserCount = block1.GhostedUsers;
                statisticModel.PhantomUserCount = block1.PhantomedUsers;

                var genderData = data.Read<PlatformStatisticGenderModel>().ToList();
                statisticModel.GenderBreakdown = genderData;

                var veriData = data.Read<PlatformStatisticVerificationModel>().ToList();
                statisticModel.VerificationBreakdown = veriData;

                statisticModel.CompletionBreakdown = data.Read<PlatformStatisticCompletionTypeModel>().ToList();

                statisticModel.InvitationTypeBreakdown = data.Read<PlatformStatisticInvitationTypeModel>().ToList();

                statisticModel.ProfileTypeBreakdown = data.Read<PlatformStatisticProfileTypeModel>().ToList();

                await dbConnection.CloseAsync();
                return statisticModel;
            }
        }
    }

  
}
