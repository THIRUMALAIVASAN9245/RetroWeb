using System.Collections.Generic;

namespace Retrospective.Application.API.Models
{
    public class RetroInfoGetAllModel
    {
        public int TotalRecords { get; set; }
        public IList<RetroInfoModel> RetroInfoModel { get; set; }
}
}