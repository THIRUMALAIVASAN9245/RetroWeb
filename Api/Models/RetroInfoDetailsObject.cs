using System.Collections.Generic;

namespace Retrospective.Application.API.Models
{
    public class RetroInfoDetailsObject
    {
        public IList<RetroInfoDetails> RetroInfoDetails { get; set; }

        public RetroInfoModel RetroInfoModel { get; set; }

        public IList<AgilePointDetails> AgilePointDetails { get; set; }

        public IList<ImageCategory> ImageCategory { get; set; }
    }
}