using System;
using System.Collections.Generic;
using System.Text;
using AuthAdTenantFunc.PlatformDashboard;

namespace AuthAdTenantFunc
{
    public class PlatformStatisticsModel
    {
        public int LiveUserCount { get; set; }
        public int TotalUserCount { get; set; }
        public int GhostUserCount { get; set; }
        public int PhantomUserCount { get; set; }
        public List<PlatformStatisticVerificationModel> VerificationBreakdown  = new List<PlatformStatisticVerificationModel>();
        public List<PlatformStatisticGenderModel> GenderBreakdown = new List<PlatformStatisticGenderModel>();
        public List<PlatformStatisticCompletionTypeModel> CompletionBreakdown = new List<PlatformStatisticCompletionTypeModel>();
        public List<PlatformStatisticProfileTypeModel> ProfileTypeBreakdown = new List<PlatformStatisticProfileTypeModel>();
        public List<PlatformStatisticInvitationTypeModel> InvitationTypeBreakdown = new List<PlatformStatisticInvitationTypeModel>();
    }

    public class PlatformStatisticGenderModel
    {
        public string Gender { get; set; }
        public int Number { get; set; }
        public decimal FractionPercent { get; set; }
    }
    public class PlatformStatisticVerificationModel
    {
        public bool IsVerified { get; set; }
        public int Number { get; set; }
        public decimal FractionPercent { get; set; }

        public string Verified
        {
            get
            {
                return IsVerified ? "Verified Profiles" : "Unverified Profiles";
            }
        }
    }
    public class PlatformStatisticProfileTypeModel
    {
        public decimal FractionPercent { get; set; }
        public int Number { get; set; }
        public string ProfileType { get; set; }
    }
    public class PlatformStatisticCompletionTypeModel
    {
        public decimal FractionPercent { get; set; }
        public int Number { get; set; }
        public bool IsComplete { get; set; }
    }
    public class PlatformStatisticInvitationTypeModel
    {
        public decimal FractionPercent { get; set; }
        public int Number { get; set; }
        public string InvitationType { get; set; }
    }
}
