namespace Retrospective.Application.API.Repository
{
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Models;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ImageInfoRepository : GenericRepository<tbl_mst_imageinfo>, IImageInfoRepository
    {
        private IRetrospectiveContext context;

        public ImageInfoRepository(DbContext context) : base(context)
        {
            this.context = (IRetrospectiveContext)context;
        }

        public IList<ImageInfoDetails> GetImageInfoList()
        {
            var imageInfoDetails = this.GetAll().Where(a => a.imageinfo_isdeleted == false).Select(x => new ImageInfoDetails
            {
                Id = x.imageinfo_id,
                Name = x.imageinfo_name,
                Path = x.imageinfo_path
            }).ToList();

            return imageInfoDetails;
        }
    }
}