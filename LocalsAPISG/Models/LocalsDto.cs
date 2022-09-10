using System.Collections.Generic;

namespace LocalsAPISG.Models
{
    public class LocalsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<MenuDto> Dishes { get; set; }
    }
}
