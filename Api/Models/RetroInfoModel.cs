namespace Retrospective.Application.API.Models
{
    using System;

    public class RetroInfoModel
    {
        public int retroinfo_id { get; set; }
        public string retroinfo_name { get; set; }
        public int retroinfo_projectinfo_id { get; set; }
        public string retroinfo_projectinfo_name { get; set; }
        public string retroinfo_sprint { get; set; }
        public string retroinfo_date { get; set; }
        public string retroinfo_imagepath { get; set; }
        public int retroinfo_imageinfo_id { get; set; }        
        public Nullable<bool> retroinfo_isdeleted { get; set; }
        public bool retroinfo_status { get; set; }
    }
}