namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using UnitOfWork;

    public class ProjectService : IProjectService
    {
        private IRetrospectiveUnitOfWork retrospectiveUnitOfWork;

        public ProjectService(IRetrospectiveUnitOfWork retrospectiveUnitOfWork)
        {
            this.retrospectiveUnitOfWork = retrospectiveUnitOfWork;
        }

        public IList<ProjectDetails> GetProjectList()
        {
            return this.retrospectiveUnitOfWork.ProjectRepository.GetProjectList();
        }
    }
}