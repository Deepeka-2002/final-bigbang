using BigBang.Models;

namespace BigBang.Interface
{
    public interface IGallery
    {
        IEnumerable<ImageGallery> GetAllImages();
        Task<ImageGallery> ImageUpload(IFormFile imageFile);

    }
}
