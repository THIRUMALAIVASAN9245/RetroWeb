namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using UnitOfWork;

    public class RetrospectiveInformationService : IRetrospectiveInformationService
    {
        private IRetrospectiveUnitOfWork retrospectiveUnitOfWork;

        public RetrospectiveInformationService(IRetrospectiveUnitOfWork retrospectiveUnitOfWork)
        {
            this.retrospectiveUnitOfWork = retrospectiveUnitOfWork;
        }

        public RetroInfoModel GetRetroInfo(int id)
        {
            return this.retrospectiveUnitOfWork.RetrospectiveInformationRepository.GetRetroInfo(id);
        }

        public int PostRetroInfo(RetroInfoModel retroInfoModel)
        {
            return this.retrospectiveUnitOfWork.RetrospectiveInformationRepository.PostRetroInfo(retroInfoModel);
        }

        public RetroInfoGetAllModel GetRetroInfo(RetroSearch retroSearch)
        {
            return this.retrospectiveUnitOfWork.RetrospectiveInformationRepository.GetRetroInfo(retroSearch);
        }
    }
}