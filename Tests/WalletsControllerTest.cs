using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.DTOs;

namespace Tests
{
    public class WalletsControllerTest
    {
        [Fact]
        public async Task TestPostWallet()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var controller = new WalletsController(context);
            var result = await controller.PostWallet(new WalletDTO { Balance = 10, Name = "TestWallet" });
            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Single(context.WalletsDB);
            var wallet = context.WalletsDB.First();
            Assert.Equal("TestWallet", wallet.Name);
            Assert.Equal(10, wallet.Balance);
        }

        [Fact]
        public async Task TestGetWallet()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var controller = new WalletsController(context);
            await controller.PostWallet(new WalletDTO { Balance = 10, Name = "TestWallet" });
            var result = await controller.GetWallet();
            Assert.IsType<List<WalletDTO>>(result.Value);
            Assert.Contains(result.Value, (i) => i.Id == 1 && i.Balance == 10 && i.Name == "TestWallet");
        }
    }
}
