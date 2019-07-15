namespace Retrospective.Application.API.UnitOfWork
{
    using Retrospective.Application.API.Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Repository;
    using System;

    public class RetrospectiveUnitOfWork : Disposable, IRetrospectiveUnitOfWork
    {
        private readonly RetroToolEntities retrospectiveToolEntities;

        private ProjectRepository projectRepository;
        private AgilePointRepository agilePointRepository;
        private ImageInfoRepository imageInfoRepository;
        private RetroInfoDetailRepository retroInfoDetailRepository;
        private ImageCategoryRepository imageCategoryRepository;
        private RetrospectiveInformationRepository retrospectiveInformationRepository;

        public RetrospectiveUnitOfWork(IRetrospectiveContext retrospectiveToolContext)
        {
            this.retrospectiveToolEntities = (RetroToolEntities)retrospectiveToolContext;
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                return projectRepository ?? (projectRepository = new ProjectRepository(retrospectiveToolEntities));
            }
        }

        public IAgilePointRepository AgilePointRepository
        {
            get
            {
                return agilePointRepository ?? (agilePointRepository = new AgilePointRepository(retrospectiveToolEntities));
            }
        }

        public IImageInfoRepository ImageInfoRepository
        {
            get
            {
                return imageInfoRepository ?? (imageInfoRepository = new ImageInfoRepository(retrospectiveToolEntities));
            }
        }

        public IImageCategoryRepository ImageCategoryRepository
        {
            get
            {
                return imageCategoryRepository ?? (imageCategoryRepository = new ImageCategoryRepository(retrospectiveToolEntities));
            }
        }

        public IRetroInfoDetailRepository RetroInfoDetailRepository
        {
            get
            {
                return retroInfoDetailRepository ?? (retroInfoDetailRepository = new RetroInfoDetailRepository(retrospectiveToolEntities));
            }
        }
        public IRetrospectiveInformationRepository RetrospectiveInformationRepository
        {
            get
            {
                return retrospectiveInformationRepository ?? (retrospectiveInformationRepository = new RetrospectiveInformationRepository(retrospectiveToolEntities));
            }
        }

        public void Save()
        {
            try
            {
                retrospectiveToolEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                DisposeCore();

                throw ex;
            }
        }

        protected override void DisposeCore()
        {
            if (retrospectiveToolEntities != null)
            {
                retrospectiveToolEntities.Dispose();
            }
        }
    }
}