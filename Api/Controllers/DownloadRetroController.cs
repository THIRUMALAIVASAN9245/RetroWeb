namespace Retrospective.Application.API.Controllers
{
    using OfficeOpenXml;
    using OfficeOpenXml.Drawing;
    using OfficeOpenXml.Style;
    using Retrospective.Application.API.Hubs;
    using Retrospective.Application.API.Models;
    using Retrospective.Application.API.Service;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.RegularExpressions;
    using System.Web.Http;

    public class DownloadRetroController : ApiControllerWithHub<RetrospectiveHub>
    {
        private IRetroInfoDetailService retroInfoDetailService;
        private IRetrospectiveInformationService retrospectiveInformationService;

        public DownloadRetroController(IRetroInfoDetailService retroInfoDetailService, IRetrospectiveInformationService retrospectiveInformationService)
        {
            this.retroInfoDetailService = retroInfoDetailService;
            this.retrospectiveInformationService = retrospectiveInformationService;
        }

        // POST: api/DownloadRetro
        public HttpResponseMessage Post(PostRetroDownload postRetroDownload)
        {
            var resultdata = this.retroInfoDetailService.GetRetroDownloadById(postRetroDownload.Id);
            var headerData = this.retrospectiveInformationService.GetRetroInfo(postRetroDownload.Id);

            var subscribed = Hub.Clients.Group(postRetroDownload.Id.ToString());
            subscribed.downloadCompleted(true);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(GetExcelSheet(resultdata, headerData, postRetroDownload.ImageData));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            string fileType = System.Configuration.ConfigurationManager.AppSettings["FileType"];
            string fileName = "Retrospective" + fileType;
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };
            return result;
        }

        private Stream GetExcelSheet(DataSet resultdata, RetroInfoModel headerData, string imageData)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Retrospective");
                worksheet.Cells["A8"].LoadFromDataTable(resultdata.Tables[0], true);

                worksheet.Cells[2, 2, 2, 2].Value = "Retrospective";
                worksheet.Cells[2, 2, 2, 3].Merge = true;
                worksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 2, 2, 2].Style.Font.Color.SetColor(Color.White);
                worksheet.Cells[2, 2, 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var fillRetroTitle = worksheet.Cells[2, 2, 2, 2].Style.Fill;
                fillRetroTitle.PatternType = ExcelFillStyle.Solid;
                fillRetroTitle.BackgroundColor.SetColor(Color.Purple);

                worksheet.Cells[3, 2, 3, 2].Value = "Retro Name";
                worksheet.Cells[4, 2, 4, 2].Value = "Project Name";
                worksheet.Cells[5, 2, 5, 2].Value = "Sprint";
                worksheet.Cells[6, 2, 6, 2].Value = "Date";

                worksheet.Cells[3, 3, 3, 3].Value = headerData.retroinfo_name;
                worksheet.Cells[4, 3, 4, 3].Value = headerData.retroinfo_projectinfo_name;
                worksheet.Cells[5, 3, 5, 3].Value = headerData.retroinfo_sprint;
                worksheet.Cells[6, 3, 6, 3].Value = headerData.retroinfo_date;

                var fillRetroHeader = worksheet.Cells[3, 2, 6, 3].Style.Fill;
                fillRetroHeader.PatternType = ExcelFillStyle.Solid;
                fillRetroHeader.BackgroundColor.SetColor(Color.Orange);

                worksheet.Cells[8, 1, 8, resultdata.Tables[0].Columns.Count].Style.Font.Bold = true;
                worksheet.Cells[8, 1, 8, resultdata.Tables[0].Columns.Count].Style.Font.Color.SetColor(Color.White);
                worksheet.Cells[8, 1, 8, resultdata.Tables[0].Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var fillDataHeader = worksheet.Cells[8, 1, 8, resultdata.Tables[0].Columns.Count].Style.Fill;
                fillDataHeader.PatternType = ExcelFillStyle.Solid;
                fillDataHeader.BackgroundColor.SetColor(Color.Purple);

                for (int i = 1; i <= resultdata.Tables[0].Columns.Count; i++)
                {
                    var fillRetroDetals = worksheet.Cells[9, i, 8 + resultdata.Tables[0].Rows.Count, i].Style.Fill;
                    fillRetroDetals.PatternType = ExcelFillStyle.Solid;
                    var colColor = resultdata.Tables[1].Rows[i - 1][0].ToString();
                    var color = ColorTranslator.FromHtml(colColor);
                    fillRetroDetals.BackgroundColor.SetColor(color);
                }

                if (imageData != null && imageData != string.Empty)
                {
                    Bitmap image = LoadImage(imageData);
                    ExcelPicture picture = null;
                    if (image != null)
                    {
                        picture = worksheet.Drawings.AddPicture("picRetro", image);
                        picture.From.Column = resultdata.Tables[0].Columns.Count + 1;
                        picture.From.Row = 2;
                        picture.SetSize(800, 600);
                    }
                }                           

                var stream = new MemoryStream(package.GetAsByteArray());
                return stream;
            }
        }

        private Bitmap LoadImage(string imageData)
        {
            var base64Data = Regex.Match(imageData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            byte[] bytes = Convert.FromBase64String(base64Data);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            Bitmap bm3 = new Bitmap(image);
            return bm3;
        }
    }
}
