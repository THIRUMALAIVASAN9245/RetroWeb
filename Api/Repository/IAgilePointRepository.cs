namespace Retrospective.Application.API.Repository
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;

    public interface IAgilePointRepository
    {
        IList<AgilePointDetails> GetAgilePointList();

        int SaveAgilePointDetails(AgilePointDetails agilePointDetails);
    }
}
