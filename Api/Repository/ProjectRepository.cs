namespace Retrospective.Application.API.Repository
{
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Models;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ProjectRepository : GenericRepository<tbl_mst_projectinfo>, IProjectRepository
    {
        private IRetrospectiveContext context;

        public ProjectRepository(DbContext context) : base(context)
        {
            this.context = (IRetrospectiveContext)context;
        }

        public IList<ProjectDetails> GetProjectList()
        {
            var projectList = this.GetAll().Select(x => new ProjectDetails
            {
                Id = x.projectinfo_id,
                Project = x.projectinfo_name
            }).ToList();

            return projectList;
        }
    }
}