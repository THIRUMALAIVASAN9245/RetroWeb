namespace Retrospective.Application.API.Controllers
{
    using Models;
    using Retrospective.Application.API.Service;

    using System.Collections.Generic;
    using System.Web.Http;

    public class ProjectController : ApiController
    {
        private IProjectService projectService;
        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        // GET: api/Project
        public IList<ProjectDetails> Get()
        {
            return this.projectService.GetProjectList();
        }
    }
}
