namespace Bookify.Web.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage ="Max Length Can't Be More Than 100 Characters")]
        public string Name { get; set; } = null!;
    }
}
