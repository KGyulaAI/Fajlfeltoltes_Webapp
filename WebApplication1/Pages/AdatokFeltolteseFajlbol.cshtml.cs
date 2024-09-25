using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly ValasztasDbContext _context;

        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, ValasztasDbContext context)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //F�jl felt�lt�se
            var UploadFilePath = Path.Combine(_env.ContentRootPath, "uploads", UploadFile.FileName);
            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            //Adatb�zisba t�lt�s
            StreamReader sr = new StreamReader(UploadFilePath);
            //...
            sr.Close();
            return Page();
        }
    }
}
