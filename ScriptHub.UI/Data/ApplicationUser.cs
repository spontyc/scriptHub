using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ScriptHub.UI.Data {
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser {
		public IList<Script> OwnedScripts { get; set; }
	}
}