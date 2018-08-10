using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScriptHub.Core;
using ScriptHub.UI.Data;

namespace ScriptHub.UI.Controllers {
	[Produces("application/json")]
	[Route("[controller]/[action]")]
	public class ApiController : Controller {
		private readonly IScriptCompiler _compiler;
		private readonly ApplicationDbContext _context;


		public ApiController(IScriptCompiler compiler, ApplicationDbContext context) {
			_compiler = compiler;
			_context = context;
		}


		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Execute(string code) {
			
			try {
				var result = await _compiler.RunScript<object>(code);
                if (result.Exception != null) return Ok(result.Exception.Message);

				return Ok(result.ReturnValue);
			} catch (Exception e) {
				return Ok(e.Message);
			}
		}

		[HttpGet]
		[Route("{script}/{globals}")]
		public async Task<IActionResult> Execute(string script, string globals) {
			try {
				var code = await _context.ScriptModels.FirstOrDefaultAsync(s => s.Name == script);

				var globalScript = string.Format("new {{ {0} }}", globals);

				var d = await _compiler.EvaluateAsync<object>(globalScript);
				var param = new DynamicGlobals() {Param =  d};
                var result = await _compiler.RunScript<object>(code.Code, param);
				if (result.Exception != null) return Ok(result.Exception.Message);

				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }
	}
}