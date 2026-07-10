using System.ComponentModel.DataAnnotations;
using WebAPI.Models.DTOs;

namespace WebAPI.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        public decimal? Balance { get; set; }

        public WalletDTO ToDto()
        {
            return new WalletDTO()
            {
                Id = this.Id,
                Name = this.Name,
                Balance = this.Balance,
            };
        }
    }
}
