using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScriptHub.UI.Data;
using ScriptHub.UI.ViewModels;

namespace ScriptHub.UI.Controllers {
	[Route("[controller]/[action]")]
	public class ScriptsController : Controller {
		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<ApplicationUser> _userManager;

		public ScriptsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
			_dbContext = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index() {
			var scripts = await _dbContext.ScriptModels.ToListAsync();
			return View(scripts);
		}

		[Authorize]
		public async Task<IActionResult> Create() {
			var script = new CreateScriptViewModel();
			return View(script);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateScriptViewModel createdScript) {
			var user = await _userManager.GetUserAsync(User);
			var script = new Script {
			Code = createdScript.Code
			, CreateDate = DateTime.Now
			, IsPrivate = createdScript.IsVisible
			, Name = createdScript.Name
			, OwnerId = user.Id
			};
			_dbContext.ScriptModels.Add(script);
			await _dbContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}