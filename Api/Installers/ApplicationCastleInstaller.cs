namespace Retrospective.Application.API.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Common;
    using Retrospective.Application.API.Entity;
    using Retrospective.Application.API.Repository;
    using Retrospective.Application.API.Service;
    using Retrospective.Application.API.UnitOfWork;
    using System.Web.Http.Controllers;

    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<IProjectService>().ImplementedBy<ProjectService>().LifestylePerWebRequest());
            container.Register(Component.For<IProjectRepository>().ImplementedBy<ProjectRepository>().LifestylePerWebRequest());

            container.Register(Component.For<IImageInfoRepository>().ImplementedBy<ImageInfoRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IImageInfoService>().ImplementedBy<ImageInfoService>().LifestylePerWebRequest());

            container.Register(Component.For<IAgilePointRepository>().ImplementedBy<AgilePointRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IAgilePointService>().ImplementedBy<AgilePointService>().LifestylePerWebRequest());

            container.Register(Component.For<IRetroInfoDetailRepository>().ImplementedBy<RetroInfoDetailRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IRetroInfoDetailService>().ImplementedBy<RetroInfoDetailService>().LifestylePerWebRequest());

            container.Register(Component.For<IRetrospectiveInformationRepository>().ImplementedBy<RetrospectiveInformationRepository>().LifestylePerWebRequest());
            container.Register(Component.For<IRetrospectiveInformationService>().ImplementedBy<RetrospectiveInformationService>().LifestylePerWebRequest());

            container.Register(Component.For<IImageCategoryRepository>().ImplementedBy<ImageCategoryRepository>().LifestylePerWebRequest());

            container.Register(Component.For<IRetrospectiveUnitOfWork>().ImplementedBy<RetrospectiveUnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For<IRetrospectiveContext>().ImplementedBy<RetroToolEntities>().LifestylePerWebRequest());
            container.Register(Component.For(typeof(IGenericRepository<>)).ImplementedBy(typeof(GenericRepository<>)).LifestylePerWebRequest());
            container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
        }
    }
}