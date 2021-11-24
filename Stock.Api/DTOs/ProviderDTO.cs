using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }

        public string phone { get; set; }

        public string email { get; set; }



      

    }
}
