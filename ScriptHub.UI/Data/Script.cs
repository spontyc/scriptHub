using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptHub.UI.Data {

	[Table("Scripts")]
	public class Script {
		public int Id { get; set; }

		public ApplicationUser Owner { get; set; }

		[Required]
		public string OwnerId { get; set; }

		[Required]
		public DateTime CreateDate { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		public string Code { get; set; }
	}
}