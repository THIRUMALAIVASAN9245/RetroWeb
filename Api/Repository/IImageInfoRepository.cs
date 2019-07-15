namespace Retrospective.Application.API.Repository
{
    using Retrospective.Application.API.Models;
    using System.Collections.Generic;

    public interface IImageInfoRepository
    {
        IList<ImageInfoDetails> GetImageInfoList();
    }
}
