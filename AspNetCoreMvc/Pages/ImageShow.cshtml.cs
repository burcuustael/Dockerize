using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCoreMvc.Pages
{
    public class ImageShowModel : PageModel
    {
        private readonly IFileProvider _fileProvider;

        public ImageShowModel(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public List<string> Images =  new List<string>();

        public IActionResult OnGet()
        {
            Images = _fileProvider.GetDirectoryContents("wwwroot/images")
            .Where(x => !x.IsDirectory)
            .Select(x => x.Name)
            .ToList();

            return Page();
        }

        public IActionResult OnPostImageShow(string name)
        {
            // "name" parametresini kullanarak i�lemlerinizi ger�ekle�tirin
            // �rne�in, bu isme sahip bir resmi silmek istiyorsan�z:

            // �nce dosyan�n tam yolunu hesaplay�n
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", name);

            // Dosyay� silme i�lemi
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                // Silme i�lemi ba�ar�l�ysa, gerekli i�lemi yapabilirsiniz.
            }
        
            // ��lemin sonucuna ba�l� olarak bir sonu� d�nd�rebilirsiniz.
            return RedirectToPage("ImageShow"); // ��lem ba�ar�l�ysa ba�ka bir sayfaya y�nlendirme �rne�i.
        }

    }
}
