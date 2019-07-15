namespace Retrospective.Application.API.Repository
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRetroInfoDetailRepository
    {
        IList<RetroInfoDetails> GetRetroInfoDetailsById(int id);

        int SaveRetroInfoDetails(RetroInfoDetails retroInfoDetails);

        RetroInfoDetails UpdateRetroInfoDetails(RetroInfoDetails retroInfoDetails);

        RetroInfoDetails DeleteRetroInfoDetails(int id);

        IQueryable<RetroDownload> GetRetroDownloadById(int id);
    }
}
