using BigBang.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IGallery _user;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageController(IGallery user, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAllImages()
        {
            var images = _user.GetAllImages();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<FileContentResult>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Gallery");
                var filePath = Path.Combine(uploadsFolder, image.Image);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                imageList.Add(File(imageBytes, "image/jpeg"));
            }

            return new JsonResult(imageList);
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile imageFile)
        {
            try
            {
                var uploadedImage = await _user.ImageUpload(imageFile);
                return CreatedAtAction("Post", uploadedImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        }
    }
