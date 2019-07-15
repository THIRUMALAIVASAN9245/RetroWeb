namespace Retrospective.Application.API.Controllers
{
    using Models;
    using Retrospective.Application.API.Hubs;
    using Retrospective.Application.API.Service;

    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Description;

    public class AgilePointController : ApiControllerWithHub<RetrospectiveHub>
    {
        private IAgilePointService agilePointService;
        public AgilePointController(IAgilePointService agilePointService)
        {
            this.agilePointService = agilePointService;
        }

        // GET: api/AgilePoint
        public IList<AgilePointDetails> Get()
        {
            return this.agilePointService.GetAgilePointList();
        }

        // POST: api/AgilePoint
        [ResponseType(typeof(int))]
        public HttpResponseMessage Post(AgilePointDetails agilePointDetails)
        {
            var response = this.agilePointService.SaveAgilePointDetails(agilePointDetails);
            if (response != 0)
            {
                agilePointDetails.Id = response;
                var subscribed = Hub.Clients.Group("AgilePoint");
                subscribed.addRetroItem(agilePointDetails);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, response);
        }
    }
}
