using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Data;
using WebAPI.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class WalletsController : ControllerBase
{
    private readonly DataContext _context;
    public WalletsController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Wallet
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WalletDTO>>> GetWallet()
    {
        return await _context.WalletsDB.Select(w => w.ToDto()).ToListAsync();
    }

    // GET: api/Wallet/5
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

    // PUT: api/Wallet/5
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

    // POST: api/Wallet
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

    // DELETE: api/Wallet/5
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

    // PATCH: api/Wallet/5
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
