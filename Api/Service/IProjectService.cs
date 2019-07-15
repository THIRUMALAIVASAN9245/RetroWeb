namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;

    public interface IProjectService
    {
        IList<ProjectDetails> GetProjectList();
    }
}
