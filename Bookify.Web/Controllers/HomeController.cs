using System.Diagnostics;

namespace Bookify.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var numberOfBooks = _context.Books.Count(c => !c.IsDeleted);
            var lastAddedBooks = _context.Books
                                .Include(b => b.Author)
                                .Where(b => !b.IsDeleted)
                                .OrderByDescending(b => b.Id)
                                .Take(12)
                                .ToList();
            var viewModel = new HomeViewModel
            {
                numberOfBooks = numberOfBooks,
                LastAddedBooks = _mapper.Map<IEnumerable<BookViewModel>>(lastAddedBooks)
               
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}