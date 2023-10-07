namespace Bookify.Web.Core.ViewModels
{
    public class HomeViewModel
    {
        public int numberOfBooks { get; set; }
        public IEnumerable<BookViewModel> LastAddedBooks { get; set; } = new List<BookViewModel>();
    }
}
