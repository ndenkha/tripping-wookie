using System;

namespace _3rdPartyApis.Configuration
{
    public interface ITeamConfiguration
    {
        int GetMaxPlayerCount(string teamName);
    }
}
