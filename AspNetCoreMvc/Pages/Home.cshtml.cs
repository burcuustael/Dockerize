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
                // Dosya adýný düzgün bir þekilde oluþturun
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // Dosya yolunu doðru bir þekilde oluþturun
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", fileName);

                // Dosya yolu güvenlik kontrolleri
                if (!path.StartsWith(Directory.GetCurrentDirectory())) // Güvenlik için kontrol ekleyin
                {
                    return BadRequest("Geçersiz dosya yolu.");
                }

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Dosya yükleme baþarýlý, istediðiniz iþlemi yapabilirsiniz.
                    return Page();
                }
                catch (Exception ex)
                {
                    // Hata iþleme - kullanýcýya hata mesajýný sunabilirsiniz
                    return BadRequest($"Dosya yükleme hatasý: {ex.Message}");
                }
            }

            // Dosya seçilmediyse veya boþsa, gerekli iþlemi yapabilirsiniz.
            return Page();
        }

    }
}
