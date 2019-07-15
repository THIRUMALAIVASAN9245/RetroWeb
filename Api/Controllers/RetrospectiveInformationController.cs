namespace Retrospective.Application.API.Controllers
{
    using Retrospective.Application.API.Models;
    using Retrospective.Application.API.Service;

    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class RetrospectiveInformationController : ApiController
    {
        private IRetrospectiveInformationService retrospectiveInformationService;
        public RetrospectiveInformationController(IRetrospectiveInformationService retrospectiveInformationService)
        {
            this.retrospectiveInformationService = retrospectiveInformationService;
        }

        public HttpResponseMessage Post(RetroInfoModel retroInfoModel)
        {
            var response = this.retrospectiveInformationService.PostRetroInfo(retroInfoModel);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public RetroInfoModel Get(int id)
        {
            return this.retrospectiveInformationService.GetRetroInfo(id);
        }

        public HttpResponseMessage GetAll([FromUri] RetroSearch retroSearch)
        {
            var response = this.retrospectiveInformationService.GetRetroInfo(retroSearch);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}