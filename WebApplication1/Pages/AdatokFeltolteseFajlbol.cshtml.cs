using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

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
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //Fájl feltöltése
            var UploadFilePath = Path.Combine(_env.ContentRootPath, "uploads", UploadFile.FileName);
            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            //Adatbázisba töltés
            StreamReader sr = new StreamReader(UploadFilePath);
            List<Part> partok = new List<Part>();
            while (!sr.EndOfStream)
            {
                var part = sr.ReadLine().Split()[4];
                if (!partok.Select(x => x.RovidNev).Contains(part))
                {
                    partok.Add(new Part { RovidNev = part });
                }
            }
            sr.Close();
            foreach (var part in partok)
            {
                _context.Partok.Add(part);
            }
            _context.SaveChanges();

            sr = new StreamReader(UploadFilePath);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor.Split();
                Jelolt ujJelolt = new Jelolt();
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                ujJelolt.PartRovidNev = elemek[4];
                _context.Jeloltek.Add(ujJelolt);
            }
            sr.Close();

            _context.SaveChanges();
            return Page();
        }
    }
}
