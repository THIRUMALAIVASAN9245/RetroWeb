namespace Retrospective.Application.API.Repository
{
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Models;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ImageCategoryRepository : GenericRepository<tbl_mst_imageinfodetails>, IImageCategoryRepository
    {
        private IRetrospectiveContext context;

        public ImageCategoryRepository(DbContext context) : base(context)
        {
            this.context = (IRetrospectiveContext)context;
        }

        public IList<ImageCategory> GetImageCategoryList(int id)
        {
            var imageCategoryList = this.GetAll()
            .Where(y => y.imageinfodetails_imageinfo_id == id)
                .Select(x => new ImageCategory
                {
                    Id = x.imageinfodetails_id,
                    Text = x.imageinfodetails_text,
                    Color = x.imageinfodetails_color,
                    Type = x.imageinfodetails_type
                }).ToList();

            return imageCategoryList;
        }
    }
}