using BigBang.Interface;
using BigBang.Models;

namespace BigBang.Services
{
    public class ImageService :IGallery
    {
        
        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GetAllImages
        public IEnumerable<ImageGallery> GetAllImages()
        {
            return _context.imagegallery.ToList();
        }

        // Post
        public async Task<ImageGallery> ImageUpload(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Gallery");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var image = new ImageGallery
            {
                Image = fileName
            };

            _context.imagegallery.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

    }
}
