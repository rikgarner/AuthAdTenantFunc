using System;
using System.Collections.Generic;
using System.Text;

namespace AuthAdTenantFunc
{
    public class PlatformStatisticsModel
    {
        public int LiveUserCount { get; set; }
        public int TotalUserCount { get; set; }
        public int GhostUserCount { get; set; }
        public int PhantomUserCount { get; set; }

        public List<PlatformStatisticGenderModel> GenderBreakdown = new List<PlatformStatisticGenderModel>();
    }

    public class PlatformStatisticGenderModel
    {
        public string Gender { get; set; }
        public int Number { get; set; }
        public decimal FractionPercent { get; set; }
    }
}
