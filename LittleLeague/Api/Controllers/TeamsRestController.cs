using _3rdPartyApis.Configuration;
using _3rdPartyApis.Eventing;
using log4net;
using Ninject;
using System.Linq;
using System.Web.Http;

namespace Api.Controllers
{
    using Domain.Model;
    using DbContext = Domain.DbContext;

    [RoutePrefix("api")]
    public class TeamsRestController : ApiController
    {
        ILog log;
        IKernel kernel;

        public TeamsRestController()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("Default");

            kernel = new StandardKernel();
            kernel.Bind<ILog>().ToConstant(log);
            kernel.Bind<ITeamConfiguration>().To<TeamConfiguration>();
            kernel.Bind<IEventPublisher>().To<EventPublisher>();
        }

        //TODO:Read operations should be moved to an OData controller.
        [HttpGet]
        [Route("teams")]
        public IHttpActionResult GetTeams()
        {
            using (var db = new DbContext(kernel))
            {
                return Ok(db.Teams.ToList());
            }
        }

        //TODO:Read operations should be moved to an OData controller.
        [HttpGet]
        [Route("teams/{teamId}")]
        public IHttpActionResult GetTeam(int teamId)
        {
            using (var db = new DbContext(kernel))
            {
                return Ok(db.Teams.Where(x => x.TeamId == teamId).SingleOrDefault());
            }
        }
        
        //TODO:What happens when adding a team takes more than a name?
        [HttpPost]
        [Route("teams")]
        public IHttpActionResult PostTeam([FromBody]string name)
        {
            using (var db = new DbContext(kernel))
            {
                db.Teams.Add(new Team(name, kernel));
                db.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        [Route("teams/{teamId}")]
        public IHttpActionResult DeleteTeam(int teamId)
        {
            using (var db = new DbContext(kernel))
            {
                var team = db.Teams.Where(x => x.TeamId == teamId).SingleOrDefault();
                if (team != null)
                {
                    db.Teams.Remove(team);
                    db.SaveChanges();
                }
                return Ok();
            }
        }
    }
}
