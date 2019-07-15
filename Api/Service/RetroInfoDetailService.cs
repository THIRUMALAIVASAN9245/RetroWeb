namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using UnitOfWork;
    using System;

    public class RetroInfoDetailService : IRetroInfoDetailService
    {
        private IRetrospectiveUnitOfWork retrospectiveUnitOfWork;

        public RetroInfoDetailService(IRetrospectiveUnitOfWork retrospectiveUnitOfWork)
        {
            this.retrospectiveUnitOfWork = retrospectiveUnitOfWork;
        }

        public RetroInfoDetailsObject GetRetroInfoDetailsById(int id)
        {
            var retroInfo = this.retrospectiveUnitOfWork.RetrospectiveInformationRepository.GetRetroInfo(id);

            return new RetroInfoDetailsObject
            {
                RetroInfoDetails = this.retrospectiveUnitOfWork.RetroInfoDetailRepository.GetRetroInfoDetailsById(id),
                RetroInfoModel = this.retrospectiveUnitOfWork.RetrospectiveInformationRepository.GetRetroInfo(id),
                AgilePointDetails = this.retrospectiveUnitOfWork.AgilePointRepository.GetAgilePointList(),
                ImageCategory = this.retrospectiveUnitOfWork.ImageCategoryRepository.GetImageCategoryList(retroInfo.retroinfo_imageinfo_id)
            };
        }

        public int SaveRetroInfoDetails(RetroInfoDetails retroInfoDetails)
        {
            int result = this.retrospectiveUnitOfWork.RetroInfoDetailRepository.SaveRetroInfoDetails(retroInfoDetails);
            this.retrospectiveUnitOfWork.Save();
            return result;
        }

        public RetroInfoDetails UpdateRetroInfoDetails(RetroInfoDetails retroInfoDetails)
        {
            var result = this.retrospectiveUnitOfWork.RetroInfoDetailRepository.UpdateRetroInfoDetails(retroInfoDetails);
            this.retrospectiveUnitOfWork.Save();
            return result;
        }

        public RetroInfoDetails DeleteRetroInfoDetails(int id)
        {
            var result = this.retrospectiveUnitOfWork.RetroInfoDetailRepository.DeleteRetroInfoDetails(id);
            this.retrospectiveUnitOfWork.Save();
            return result;
        }

        public DataSet GetRetroDownloadById(int id)
        {
            this.retrospectiveUnitOfWork.RetrospectiveInformationRepository.UpdateRetroInfo(id);
            this.retrospectiveUnitOfWork.Save();

            var retroDownload = this.retrospectiveUnitOfWork.RetroInfoDetailRepository.GetRetroDownloadById(id);

            IEnumerable<IGrouping<string, RetroDownload>> retroGrouping = retroDownload.OrderBy(y => y.Id).GroupBy(x => x.ImageCategory);
            var retroInfoGrouping = retroGrouping.ToList();

            var maxRowCount = retroInfoGrouping.Select(group => new
            {
                Metric = group.Key,
                Count = group.Count()
            }).OrderByDescending(a => a.Count).FirstOrDefault();

            var result = getDataTable(retroInfoGrouping, maxRowCount.Count);            

            return result;
        }

        private DataSet getDataTable(List<IGrouping<string, RetroDownload>> retroInfoGrouping, int maxRowCount)
        {
            DataSet dsRetroDownload = new DataSet();

            DataTable retroInfoGroup = new DataTable();
            DataTable colorDataTable = new DataTable();
            colorDataTable.Columns.Add("Color", typeof(string));

            foreach (var retro in retroInfoGrouping)
            {
                retroInfoGroup.Columns.Add(retro.Key, typeof(string));

                var value = retro.Select(x => x.Color).FirstOrDefault();              
                colorDataTable.Rows.Add(value);
            }

            for (int i = 0; i < maxRowCount; i++)
            {
                DataRow newRow = retroInfoGroup.Rows.Add();

                for (int j = 0; j < retroInfoGrouping.Count; j++)
                {
                    var retroInfo = retroInfoGrouping[j].ToList();

                    if (i < retroInfo.Count)
                    {
                        var retroData = retroInfo[i];
                        newRow[retroData.ImageCategory] = retroData.Text;
                    }
                }
            }

            dsRetroDownload.Tables.Add(retroInfoGroup);
            dsRetroDownload.Tables.Add(colorDataTable);

            return dsRetroDownload;
        }
    }
}