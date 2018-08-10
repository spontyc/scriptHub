using System.ComponentModel.DataAnnotations;

namespace ScriptHub.UI.ViewModels {
	public sealed class CreateScriptViewModel {

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		public string Code { get; set; }

		[Required]
		public bool IsVisible { get; set; }
	}
}