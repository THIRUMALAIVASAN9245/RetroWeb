namespace Retrospective.Application.API.Entity
{
    using System.Data.Entity;

    public interface IRetrospectiveContext
    {         
         DbSet<tbl_mst_agiledescinfo> tbl_mst_agiledescinfo { get; set; }
         DbSet<tbl_mst_imageinfo> tbl_mst_imageinfo { get; set; }
         DbSet<tbl_mst_projectinfo> tbl_mst_projectinfo { get; set; }
         DbSet<tbl_mst_retroinfodetails> tbl_mst_retroinfodetails { get; set; }
         DbSet<tbl_trn_retroinfo> tbl_trn_retroinfo { get; set; }
         DbSet<tbl_mst_imageinfodetails> tbl_mst_imageinfodetails { get; set; }        
    }
}