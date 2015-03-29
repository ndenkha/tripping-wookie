using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api")]
    public class TeamsRestController : ApiController
    {
        [HttpGet]
        [Route("teams")]
        public IHttpActionResult GetTeams()
        {
            return Ok(new List<string>() { "Team1", "Team2" });
        }
    }
}
