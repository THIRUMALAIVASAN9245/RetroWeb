namespace Retrospective.Application.API.Models
{
    public class ProjectDetails
    {
        public int Id { get; set; }
        public string Project { get; set; }
        public string Portfolio { get; set; }
        public string SOW_Name { get; set; }
        public string SOW_Coverage { get; set; }
        public string Remarks { get; set; }
    }
}