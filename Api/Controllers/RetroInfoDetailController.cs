namespace Retrospective.Application.API.Controllers
{
    using Models;
    using Retrospective.Application.API.Hubs;
    using Retrospective.Application.API.Service;

    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Description;

    public class RetroInfoDetailController : ApiControllerWithHub<RetrospectiveHub>
    {
        private IRetroInfoDetailService retroInfoDetailService;
        public RetroInfoDetailController(IRetroInfoDetailService retroInfoDetailService)
        {
            this.retroInfoDetailService = retroInfoDetailService;
        }

        // GET: api/RetroInfoDetail
        public RetroInfoDetailsObject Get(int id)
        {
            return this.retroInfoDetailService.GetRetroInfoDetailsById(id);
        }

        // POST: api/RetroInfoDetail
        [ResponseType(typeof(int))]
        public HttpResponseMessage Post(RetroInfoDetails retroInfoDetails)
        {
            var response = this.retroInfoDetailService.SaveRetroInfoDetails(retroInfoDetails);
            if (response != 0)
            {
                retroInfoDetails.Id = response;
                var subscribed = Hub.Clients.Group(retroInfoDetails.RetroInfoId.ToString());
                subscribed.addItem(retroInfoDetails);
                return Request.CreateResponse(HttpStatusCode.OK, retroInfoDetails);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, response);
        }

        // PUT: api/RetroInfoDetail
        [ResponseType(typeof(RetroInfoDetails))]
        public HttpResponseMessage Put(RetroInfoDetails retroInfoDetails)
        {
            var response = this.retroInfoDetailService.UpdateRetroInfoDetails(retroInfoDetails);

            var subscribed = Hub.Clients.Group(retroInfoDetails.RetroInfoId.ToString());
            subscribed.updateItem(retroInfoDetails);

            return Request.CreateResponse(HttpStatusCode.OK, retroInfoDetails);
        }

        // DELETE: api/RetroInfoDetail/
        [ResponseType(typeof(int))]
        public HttpResponseMessage Delete(int id)
        {
            var response = this.retroInfoDetailService.DeleteRetroInfoDetails(id);

            var subscribed = Hub.Clients.Group(response.RetroInfoId.ToString());
            subscribed.deleteItem(response);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
