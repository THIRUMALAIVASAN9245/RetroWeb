namespace Retrospective.Application.API.Service
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using System.Data;

    public interface IRetroInfoDetailService
    {
        RetroInfoDetailsObject GetRetroInfoDetailsById(int id);

        int SaveRetroInfoDetails(RetroInfoDetails retroInfoDetails);

        RetroInfoDetails UpdateRetroInfoDetails(RetroInfoDetails retroInfoDetails);

        RetroInfoDetails DeleteRetroInfoDetails(int id);

        DataSet GetRetroDownloadById(int id);
    }
}
