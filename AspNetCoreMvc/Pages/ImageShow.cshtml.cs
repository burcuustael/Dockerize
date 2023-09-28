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
            // "name" parametresini kullanarak iþlemlerinizi gerçekleþtirin
            // Örneðin, bu isme sahip bir resmi silmek istiyorsanýz:

            // Önce dosyanýn tam yolunu hesaplayýn
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", name);

            // Dosyayý silme iþlemi
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                // Silme iþlemi baþarýlýysa, gerekli iþlemi yapabilirsiniz.
            }
        
            // Ýþlemin sonucuna baðlý olarak bir sonuç döndürebilirsiniz.
            return RedirectToPage("ImageShow"); // Ýþlem baþarýlýysa baþka bir sayfaya yönlendirme örneði.
        }

    }
}
