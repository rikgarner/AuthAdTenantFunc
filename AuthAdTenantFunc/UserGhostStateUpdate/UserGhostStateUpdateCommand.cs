using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace AuthAdTenantFunc
{
    public class UserGhostStateUpdateCommand : IRequest<CommandResult>, ILoggableData
    {
        public int TalentId { get; set; }
        public bool IsGhosted { get; set; }
        public string Reason { get; set; }

        public string Dump()
        {
            var b = Props().Aggregate("", (buffer, item) => string.Concat(buffer, $"{item};"));
            return b;
        }

       

        internal IEnumerable<string> Props()
        {
            yield return $"TalentId:{TalentId}";
            yield return $"IsGhosted:{IsGhosted}";
            yield return $"Reason:{Reason}";
        }
    }
}
