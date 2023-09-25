namespace Bookify.Web.Services
{
	public interface IImageService
	{
		Task<(bool isUploaded, string? errorMessage)> UploadAsync(IFormFile image, string imageName, string folderPath);
		void Delete(string imagePath, string? imageThumbnailPath = null);
	}
}