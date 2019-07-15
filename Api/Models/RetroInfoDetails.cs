namespace Retrospective.Application.API.Models
{
    using System;

    public class RetroInfoDetails
    {
        public int Id { get; set; }
        public int RetroInfoId { get; set; }
        public string Text { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public Nullable<int> ImageCategoryId { get; set; }
        public string Color { get; set; }
    }
}