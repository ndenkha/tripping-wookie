using System;
namespace _3rdPartyApis.Configuration
{
    interface ITeamConfiguration
    {
        int GetMaxPlayerCount(string teamName);
    }
}
