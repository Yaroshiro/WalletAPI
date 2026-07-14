using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Data;
using WebAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class WalletsController : ControllerBase
{
    private readonly DataContext _context;
    public WalletsController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest req)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-at-least-32-characters"));
        var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken("https://localhost:5000", "https://localhost:5000/api/wallets",signingCredentials: credentials, expires:DateTime.UtcNow.AddHours(1));
        var jwt = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Ok(new { accessToken = jwt });
    }

    // GET: api/Wallets
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletDTO>>> GetWallet()
    {
        return await _context.WalletsDB.Select(w => w.ToDto()).ToListAsync();
    }

    // GET: api/Wallets/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<WalletDTO>> GetWallet(int id)
    {
        var wallet = await _context.WalletsDB.FindAsync(id);

        if (wallet == null)
        {
            return NotFound();
        }

        return wallet.ToDto();
    }

    // PUT: api/Wallets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWallet(int id, WalletDTO walletDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var wallet = await _context.WalletsDB.FindAsync(id);

        if (wallet == null)
        {
            return NotFound();
        }

        try
        {
            wallet?.Name = walletDTO.Name;
            wallet?.Balance = walletDTO.Balance;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WalletExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Wallets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WalletDTO>> PostWallet(WalletDTO walletDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        Wallet wallet = new Wallet()
        {
            Name = walletDto.Name,
            Balance = walletDto.Balance,
            CreatedAt = DateTime.UtcNow
        };        
        _context.WalletsDB.Add(wallet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWallet), new { id = wallet.Id }, wallet.ToDto());
    }

    // DELETE: api/Wallets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWallet(int? id)
    {
        var wallet = await _context.WalletsDB.FindAsync(id);
        if (wallet == null)
        {            
            return NotFound();
        }

        _context.WalletsDB.Remove(wallet);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WalletExists(int? id)
    {
        return _context.WalletsDB.Any(e => e.Id == id);
    }

    // PATCH: api/Wallets/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchWallet(int id, WalletDTO walletDTO)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var wallet = await _context.WalletsDB.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            if (walletDTO.Name != null)
                wallet?.Name = walletDTO.Name;
            if (walletDTO.Balance != null)
                wallet?.Balance = walletDTO.Balance;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WalletExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
}
