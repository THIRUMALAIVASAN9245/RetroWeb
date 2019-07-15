namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;

    public interface IAgilePointService
    {
        IList<AgilePointDetails> GetAgilePointList();

        int SaveAgilePointDetails(AgilePointDetails agilePointDetails);
    }
}
