namespace Bookify.Web.Services
{
    public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
		private int _maxAllowedSize = 2097152;

		public ImageService(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<(bool isUploaded, string? errorMessage)> UploadAsync(IFormFile image, string imageName, string folderPath)
		{
			var extension = Path.GetExtension(image.FileName);

			if (!_allowedExtensions.Contains(extension))
				return (isUploaded: false, errorMessage: Errors.NotAllowedExtension);

			if (image.Length > _maxAllowedSize)
				return (isUploaded: false, errorMessage: Errors.MaxSize);

			var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{folderPath}", imageName);

			using var stream = File.Create(path);
			await image.CopyToAsync(stream);
			stream.Dispose();

			

			return (isUploaded: true, errorMessage: null);
		}

		public void Delete(string imagePath, string? imageThumbnailPath = null)
		{
			var oldImagePath = $"{_webHostEnvironment.WebRootPath}{imagePath}";

			if (File.Exists(oldImagePath))
				File.Delete(oldImagePath);

			if (!string.IsNullOrEmpty(imageThumbnailPath))
			{
				var oldThumbPath = $"{_webHostEnvironment.WebRootPath}{imageThumbnailPath}";

				if (File.Exists(oldThumbPath))
					File.Delete(oldThumbPath);
			}
		}

	}
}