using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Threading.Tasks;


public class SubmitClaimModel : PageModel
{
    private readonly ClaimsDbContext _context;

    public SubmitClaimModel(ClaimsDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ClaimInput Input { get; set; }

    public async Task<IActionResult> OnPostAsync(IFormFile Document)
    {
        if (!ModelState.IsValid) return Page();

        string uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        // Ensure the uploads directory exists
        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        string filePath = null;
        if (Document != null)
        {
            // Create a unique filename to avoid conflicts
            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Document.FileName)}";
            filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            // Save the file
            using var stream = new FileStream(filePath, FileMode.Create);
            await Document.CopyToAsync(stream);
        }

        var claim = new Claim
        {
            LecturerId = 1, // Replace with dynamic LecturerId
            HoursWorked = Input.HoursWorked,
            HourlyRate = Input.HourlyRate,
            AdditionalNotes = Input.AdditionalNotes,
            DocumentPath = filePath
        };

        _context.Claims.Add(claim);
        await _context.SaveChangesAsync();

        return RedirectToPage("ClaimSubmitted");
    }
}

public class ClaimInput
{
    public decimal HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }
    public string AdditionalNotes { get; set; }
}
