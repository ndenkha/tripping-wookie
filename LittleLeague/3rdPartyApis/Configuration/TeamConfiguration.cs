using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3rdPartyApis.Configuration
{
    public class TeamConfiguration : _3rdPartyApis.Configuration.ITeamConfiguration
    {
        public int GetMaxPlayerCount(string teamName)
        {
            switch (teamName)
            {
                case "Cowboys":
                    return 8;
                case "Giants":
                    return 9;
                default:
                    return 10;
            }
        }
    }
}
