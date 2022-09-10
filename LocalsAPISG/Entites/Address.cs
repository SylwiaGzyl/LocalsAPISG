
using System.ComponentModel.DataAnnotations;

namespace LocalsAPISG.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public virtual Locals Locals { get; set; }


    }
}
