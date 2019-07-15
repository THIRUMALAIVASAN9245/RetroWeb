namespace Retrospective.Application.API.Controllers
{
    using Models;
    using Retrospective.Application.API.Service;

    using System.Collections.Generic;
    using System.Web.Http;

    public class ImageInfoController : ApiController
    {
        private IImageInfoService imageInfoService;
        public ImageInfoController(IImageInfoService imageInfoService)
        {
            this.imageInfoService = imageInfoService;
        }

        // GET: api/ImageInfo
        public IList<ImageInfoDetails> Get()
        {
            return this.imageInfoService.GetImageInfoList();
        }
    }
}
