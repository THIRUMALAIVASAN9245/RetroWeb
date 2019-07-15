namespace Retrospective.Application.API.Repository
{
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Models;

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class RetrospectiveInformationRepository : GenericRepository<tbl_trn_retroinfo>, IRetrospectiveInformationRepository
    {
        private IRetrospectiveContext context;

        public RetrospectiveInformationRepository(DbContext context) : base(context)
        {
            this.context = (IRetrospectiveContext)context;
        }

        public RetroInfoModel GetRetroInfo(int id)
        {
            var getRetroInfo = this.GetAll().Where(x => x.retroinfo_id == id).Select(x => new RetroInfoModel
            {
                retroinfo_id = x.retroinfo_id,
                retroinfo_name = x.retroinfo_name,
                retroinfo_projectinfo_id = x.retroinfo_projectinfo_id,
                retroinfo_sprint = x.retroinfo_sprint,
                retroinfo_date = x.retroinfo_date,
                retroinfo_imagepath = x.retroinfo_imagepath,
                retroinfo_status = x.retroinfo_status,
                retroinfo_imageinfo_id = x.retroinfo_imageinfo_id
            }).FirstOrDefault();

            if (getRetroInfo != null)
            {
                var result = this.context.tbl_mst_projectinfo.Where(a => a.projectinfo_id == getRetroInfo.retroinfo_projectinfo_id).Select(y => y.projectinfo_name).FirstOrDefault();
                getRetroInfo.retroinfo_projectinfo_name = result;
            }

            return getRetroInfo;
        }

        public int PostRetroInfo(RetroInfoModel retroInfoModel)
        {
            var myData = new tbl_trn_retroinfo
            {
                retroinfo_name = retroInfoModel.retroinfo_name,
                retroinfo_projectinfo_id = retroInfoModel.retroinfo_projectinfo_id,
                retroinfo_sprint = retroInfoModel.retroinfo_sprint,
                retroinfo_date = DateTime.UtcNow.ToString("yyy-MM-dd"),
                retroinfo_imagepath = retroInfoModel.retroinfo_imagepath,
                retroinfo_imageinfo_id = retroInfoModel.retroinfo_imageinfo_id,
                retroinfo_status = retroInfoModel.retroinfo_status,
                retroinfo_isdeleted = retroInfoModel.retroinfo_isdeleted,
            };
            var response = this.InsertandSave(myData);
            return response.retroinfo_id;
        }                

        public RetroInfoGetAllModel GetRetroInfo(RetroSearch retroSearch)
        {
            retroSearch.PageNumber--;
            var getRetroInfo = this.context.tbl_trn_retroinfo
                                   .Where(s => (retroSearch.ProjectId == 0 || s.retroinfo_projectinfo_id == retroSearch.ProjectId))
                                   .Join(this.context.tbl_mst_projectinfo, a => a.retroinfo_projectinfo_id, b => b.projectinfo_id, (a, b) => new { a, b })
                                   .Select(x => new RetroInfoModel
                                   {
                                       retroinfo_id = x.a.retroinfo_id,
                                       retroinfo_name = x.a.retroinfo_name,
                                       retroinfo_projectinfo_name = x.b.projectinfo_name,
                                       retroinfo_sprint = x.a.retroinfo_sprint,
                                       retroinfo_date = x.a.retroinfo_date,
                                       retroinfo_imagepath = x.a.retroinfo_imagepath,
                                       retroinfo_isdeleted = x.a.retroinfo_isdeleted,
                                       retroinfo_status = x.a.retroinfo_status,
                                       retroinfo_projectinfo_id = x.a.retroinfo_projectinfo_id
                                   }).OrderBy(f => f.retroinfo_projectinfo_id);

            var allRecords = getRetroInfo.Skip(retroSearch.PageNumber * retroSearch.PageIndex)
                                   .Take(retroSearch.PageIndex).ToList();

            var getRetroDetails= new RetroInfoGetAllModel
            {
                TotalRecords = getRetroInfo.Count(),
                RetroInfoModel = allRecords
            };
            return getRetroDetails;
        }

        public bool UpdateRetroInfo(int id)
        {
            var retroInfoDetail = GetRetroInfoDetail(id);

            if (retroInfoDetail != null)
            {
                retroInfoDetail.retroinfo_status = true;

                Update(retroInfoDetail);
            }

            return retroInfoDetail.retroinfo_status;
        }

        private tbl_trn_retroinfo GetRetroInfoDetail(int id)
        {
            return this.GetAll().Where(x => x.retroinfo_id == id).FirstOrDefault();
        }
    }
}