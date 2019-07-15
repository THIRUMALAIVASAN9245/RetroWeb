namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using UnitOfWork;

    public class ImageInfoService : IImageInfoService
    {
        private IRetrospectiveUnitOfWork retrospectiveUnitOfWork;

        public ImageInfoService(IRetrospectiveUnitOfWork retrospectiveUnitOfWork)
        {
            this.retrospectiveUnitOfWork = retrospectiveUnitOfWork;
        }

        public IList<ImageInfoDetails> GetImageInfoList()
        {
            return this.retrospectiveUnitOfWork.ImageInfoRepository.GetImageInfoList();
        }
    }
}