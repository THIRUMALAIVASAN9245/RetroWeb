namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;

    public interface IRetrospectiveInformationService
    {
        RetroInfoModel GetRetroInfo(int id);
        int PostRetroInfo(RetroInfoModel retroInfoModel);
        RetroInfoGetAllModel GetRetroInfo(RetroSearch retroSearch);
    }
}
