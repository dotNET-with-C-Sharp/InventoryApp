using InventoryApp.Interfaces;

namespace InventoryApp.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> SaveImageAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

            // Generate a unique filename
            //var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Create folder path
            var folderPath = Path.Combine(_environment.WebRootPath, "images", folderName);

            // Create folder if not exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Complete file path
            var filePath = Path.Combine(folderPath, fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative path of the saved file
            return $"/images/{folderName}/{fileName}";
        }
    }
}
