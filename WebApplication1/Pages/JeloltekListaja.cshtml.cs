using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class JeloltekListajaModel : PageModel
    {
        private readonly WebApplication1.Models.ValasztasDbContext _context;

        public JeloltekListajaModel(WebApplication1.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IList<Jelolt> Jelolt { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Jelolt = await _context.Jeloltek
                .Include(j => j.Part).ToListAsync();
        }
    }
}
