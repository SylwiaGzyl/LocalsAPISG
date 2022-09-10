using System.ComponentModel.DataAnnotations;

namespace LocalsAPISG.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int LocalsId { get; set; }
        public virtual Locals Locals { get; set; }
    }
}
