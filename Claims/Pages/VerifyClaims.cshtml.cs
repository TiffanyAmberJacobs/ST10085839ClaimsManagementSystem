using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class VerifyClaimsModel : PageModel
{
    private readonly ClaimsDbContext _context;

    public List<Claim> PendingClaims { get; private set; }

    public VerifyClaimsModel(ClaimsDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        PendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
    }

    public async Task<IActionResult> OnPostAsync(string action)
    {
        var parts = action.Split('_');
        var status = parts[0] == "approve" ? "Approved" : "Rejected";
        var claimId = int.Parse(parts[1]);

        var claim = await _context.Claims.FindAsync(claimId);
        if (claim != null)
        {
            claim.Status = status;
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}
