using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreMvc.Pages
{
    public class HomeModel : PageModel
    {
        private readonly IFileProvider _fileProvider;

        public HomeModel(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Dosya ad�n� d�zg�n bir �ekilde olu�turun
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // Dosya yolunu do�ru bir �ekilde olu�turun
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", fileName);

                // Dosya yolu g�venlik kontrolleri
                if (!path.StartsWith(Directory.GetCurrentDirectory())) // G�venlik i�in kontrol ekleyin
                {
                    return BadRequest("Ge�ersiz dosya yolu.");
                }

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Dosya y�kleme ba�ar�l�, istedi�iniz i�lemi yapabilirsiniz.
                    return Page();
                }
                catch (Exception ex)
                {
                    // Hata i�leme - kullan�c�ya hata mesaj�n� sunabilirsiniz
                    return BadRequest($"Dosya y�kleme hatas�: {ex.Message}");
                }
            }

            // Dosya se�ilmediyse veya bo�sa, gerekli i�lemi yapabilirsiniz.
            return Page();
        }

    }
}
