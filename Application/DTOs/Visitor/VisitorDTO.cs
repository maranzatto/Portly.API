namespace Portly.Application.DTOs.Visitor
{
    public class VisitorDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool isActive { get; set; }
    }
}

