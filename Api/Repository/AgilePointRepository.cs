namespace Retrospective.Application.API.Repository
{
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Models;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class AgilePointRepository : GenericRepository<tbl_mst_agiledescinfo>, IAgilePointRepository
    {
        private IRetrospectiveContext context;

        public AgilePointRepository(DbContext context) : base(context)
        {
            this.context = (IRetrospectiveContext)context;
        }

        public IList<AgilePointDetails> GetAgilePointList()
        {
            var agilePointList = this.GetAll().Select(x => new AgilePointDetails
            {
                Id = x.agiledescinfo_id,
                Type = x.agiledescinfo_type,
                Text = x.agiledescinfo_text
            }).ToList();

            return agilePointList;
        }

        public int SaveAgilePointDetails(AgilePointDetails agilePointDetails)
        {
            var agilePointDetail = new tbl_mst_agiledescinfo
            {
                agiledescinfo_text = agilePointDetails.Text,
                agiledescinfo_type = agilePointDetails.Type                
            };

            var response = this.InsertandSave(agilePointDetail);
            return response.agiledescinfo_id;
        }
    }
}