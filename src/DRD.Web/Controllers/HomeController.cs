using DRD.Infrastructure.Identity;
using DRD.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;

	public HomeController(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
		var vm = new HomeVM();

		// Si l'utilisateur est connecté, on charge son nom complet
		if (User?.Identity?.IsAuthenticated == true)
		{
			var user = await _userManager.GetUserAsync(User);
			vm.UserFullName = $"{user?.FirstName} {user?.LastName}".Trim();
		}

		return View(vm);
	}
}
