namespace Retrospective.Application.API.Repository
{
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Models;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class RetroInfoDetailRepository : GenericRepository<tbl_mst_retroinfodetails>, IRetroInfoDetailRepository
    {
        private IRetrospectiveContext context;

        public RetroInfoDetailRepository(DbContext context) : base(context)
        {
            this.context = (IRetrospectiveContext)context;
        }

        public IList<RetroInfoDetails> GetRetroInfoDetailsById(int id)
        {
            var retroInfoDetailList = this.GetAll().Where(s => s.retroinfodetails_retroinfo_id == id).Select(x => new RetroInfoDetails
            {
                Id = x.retroinfodetails_id,
                RetroInfoId = x.retroinfodetails_retroinfo_id,
                Text = x.retroinfodetails_text,
                Top = x.retroinfodetails_top,
                Left = x.retroinfodetails_left,
                Color = x.retroinfodetails_color,
                ImageCategoryId = x.retroinfodetails_category_id
            }).ToList();

            return retroInfoDetailList;
        }

        public int SaveRetroInfoDetails(RetroInfoDetails retroInfoDetails)
        {
            var retroInfoDetail = new tbl_mst_retroinfodetails
            {
                retroinfodetails_retroinfo_id = retroInfoDetails.RetroInfoId,
                retroinfodetails_text = retroInfoDetails.Text,
                retroinfodetails_top = retroInfoDetails.Top,
                retroinfodetails_left = retroInfoDetails.Left,
                retroinfodetails_category_id = retroInfoDetails.ImageCategoryId,
                retroinfodetails_color = retroInfoDetails.Color
            };

            var response = this.InsertandSave(retroInfoDetail);
            return response.retroinfodetails_id;
        }

        public RetroInfoDetails UpdateRetroInfoDetails(RetroInfoDetails retroInfoDetails)
        {
            var retroInfoDetail = GetRetroInfoDetail(retroInfoDetails.Id);

            if (retroInfoDetail != null)
            {
                retroInfoDetail.retroinfodetails_retroinfo_id = retroInfoDetails.RetroInfoId;
                retroInfoDetail.retroinfodetails_text = retroInfoDetails.Text;
                retroInfoDetail.retroinfodetails_top = retroInfoDetails.Top;
                retroInfoDetail.retroinfodetails_left = retroInfoDetails.Left;
                retroInfoDetail.retroinfodetails_category_id = retroInfoDetails.ImageCategoryId;
                retroInfoDetail.retroinfodetails_color = retroInfoDetails.Color;

                Update(retroInfoDetail);
            }

            return retroInfoDetails;
        }

        public RetroInfoDetails DeleteRetroInfoDetails(int id)
        {
            var retroInfoDetail = GetRetroInfoDetail(id);

            var retroInfoDetails = new RetroInfoDetails
            {
                Id = retroInfoDetail.retroinfodetails_id,
                RetroInfoId = retroInfoDetail.retroinfodetails_retroinfo_id,
                Text = retroInfoDetail.retroinfodetails_text,
                Top = retroInfoDetail.retroinfodetails_top,
                Left = retroInfoDetail.retroinfodetails_left,
                ImageCategoryId = retroInfoDetail.retroinfodetails_category_id,
                Color = retroInfoDetail.retroinfodetails_color
            };

            Delete(retroInfoDetail);

            return retroInfoDetails;
        }

        public IQueryable<RetroDownload> GetRetroDownloadById(int id)
        {
            var getRetroInfo = from retroInfo in this.GetAll().Where(x => x.retroinfodetails_retroinfo_id == id)
                               join imageinfodetail in this.context.tbl_mst_imageinfodetails
                               on retroInfo.retroinfodetails_category_id equals imageinfodetail.imageinfodetails_id
                               select new RetroDownload
                               {
                                   Id = imageinfodetail.imageinfodetails_id,
                                   Text = retroInfo.retroinfodetails_text,
                                   ImageCategory = imageinfodetail.imageinfodetails_text,
                                   Color = imageinfodetail.imageinfodetails_color
                               };

            return getRetroInfo;
        }

        private tbl_mst_retroinfodetails GetRetroInfoDetail(int id)
        {
            return this.GetAll().Where(x => x.retroinfodetails_id == id).FirstOrDefault();
        }
    }
}