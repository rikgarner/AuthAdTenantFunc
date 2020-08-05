using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;

namespace AuthAdTenantFunc
{
    public class UserGhostStateUpdateCommand : RequestBase , IRequest<CommandResult>
    {
        public int TalentId { get; set; }
        public bool IsGhosted { get; set; }
        public string Reason { get; set; }
        
        protected override IEnumerable<string> Props()
        {
            yield return $"TalentId:{TalentId}";
            yield return $"IsGhosted:{IsGhosted}";
            yield return $"Reason:{Reason}";
        }
    }

    public class UserGhostStateUpdateCommandHandler : IRequestHandler<UserGhostStateUpdateCommand, CommandResult>
    {
        public async Task<CommandResult> Handle(UserGhostStateUpdateCommand request, CancellationToken cancellationToken)
        {
            var dbConnection = new SqlConnection
            {
                ConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString")
            };

            await using (dbConnection)
            {
                await dbConnection.OpenAsync(cancellationToken);

                var rows= await dbConnection.ExecuteAsync(
                    "Mgt.TalentProfileGhostStatusUpdate @TalentId, @ActionedUserEmail, @IsGhosted, @Reason", new
                    {
                        TalentId = request.TalentId,
                        ActionedUserEmail = request.UserIdentifierId,
                        IsGhosted = request.IsGhosted,
                        Reason = request.Reason
                    });
                await dbConnection.CloseAsync();
                return new CommandResult() { RowCount = rows };
            }
            
        }
    }
}
