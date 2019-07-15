namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using UnitOfWork;

    public class AgilePointService : IAgilePointService
    {
        private IRetrospectiveUnitOfWork retrospectiveUnitOfWork;

        public AgilePointService(IRetrospectiveUnitOfWork retrospectiveUnitOfWork)
        {
            this.retrospectiveUnitOfWork = retrospectiveUnitOfWork;
        }

        public IList<AgilePointDetails> GetAgilePointList()
        {
            return this.retrospectiveUnitOfWork.AgilePointRepository.GetAgilePointList();
        }

        public int SaveAgilePointDetails(AgilePointDetails agilePointDetails)
        {

            var result = this.retrospectiveUnitOfWork.AgilePointRepository.SaveAgilePointDetails(agilePointDetails);
            this.retrospectiveUnitOfWork.Save();
            return result;
        }
    }
}