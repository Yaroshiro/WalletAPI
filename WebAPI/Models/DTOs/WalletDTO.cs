using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
{
    public class WalletDTO
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Balance { get; set; }
    }
}
