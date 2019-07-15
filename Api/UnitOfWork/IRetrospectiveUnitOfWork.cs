namespace Retrospective.Application.API.UnitOfWork
{
    using Retrospective.Application.API.Repository;

    public interface IRetrospectiveUnitOfWork
    {
        IProjectRepository ProjectRepository { get; }

        IAgilePointRepository AgilePointRepository { get; }

        IImageInfoRepository ImageInfoRepository { get; }

        IRetroInfoDetailRepository RetroInfoDetailRepository { get; }

        IRetrospectiveInformationRepository RetrospectiveInformationRepository { get; }

        IImageCategoryRepository ImageCategoryRepository { get; }

        void Save();
    }
}