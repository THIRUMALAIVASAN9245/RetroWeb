namespace Retrospective.Application.API.Repository
{
    using Retrospective.Application.API.Models;

    public interface IRetrospectiveInformationRepository
    {
        int PostRetroInfo(RetroInfoModel retroInfoModel);
        RetroInfoModel GetRetroInfo(int id);
        RetroInfoGetAllModel GetRetroInfo(RetroSearch retroSearch);
        bool UpdateRetroInfo(int id);
    }
}
