//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Retrospective.Application.API.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_mst_retroinfodetails
    {
        public int retroinfodetails_id { get; set; }
        public int retroinfodetails_retroinfo_id { get; set; }
        public string retroinfodetails_text { get; set; }
        public string retroinfodetails_top { get; set; }
        public string retroinfodetails_left { get; set; }
        public bool retroinfodetails_isdeleted { get; set; }
        public Nullable<int> retroinfodetails_category_id { get; set; }
        public string retroinfodetails_color { get; set; }
    }
}
