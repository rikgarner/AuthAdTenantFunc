using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthAdTenantFunc
{
    public class RequestBase
    {
        private string? _userIdentifier;

        protected virtual IEnumerable<string> Props()
        {
            return Enumerable.Empty<string>();
        }
        public string Dump()
        {
            var b = Props().Aggregate("", (buffer, item) => string.Concat(buffer, $"{item};"));
            return b;
        }

        public string? UserIdentifier
        {
            private get => _userIdentifier;
            set => _userIdentifier = value;
        }

        public string UserIdentifierId => _userIdentifier ?? string.Empty;
    }
}
