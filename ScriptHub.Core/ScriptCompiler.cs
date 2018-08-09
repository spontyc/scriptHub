using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ScriptHub.Core {
	public sealed class ScriptCompiler : IScriptCompiler {
		public async Task<T> EvaluateAsync<T>(string code) {
			return await CSharpScript.EvaluateAsync<T>(code).ConfigureAwait(false);
		}

		public async Task<object> EvaluateAsync(string code) {
			return await CSharpScript.EvaluateAsync(code).ConfigureAwait(false);
		}

		public async Task<T> EvaluateAsync<T>(string code, object globals) {
			return await CSharpScript.EvaluateAsync<T>(code, globals: globals).ConfigureAwait(false);
		}

		public async Task<object> EvaluateAsync(string code, object globals) {
			return await CSharpScript.EvaluateAsync(code, globals: globals).ConfigureAwait(false);
		}

		public async Task<ScriptState> RunScript<T>(string code) {
			var script = CSharpScript.Create<T>(code);
			script.Compile();
			return await script.RunAsync().ConfigureAwait(false);
		}

		public async Task<ScriptState> RunScript<T>(string code, object globals) {
			var script = CSharpScript.Create<T>(code, globalsType: globals.GetType());
			script.Compile();
			return await script.RunAsync(globals).ConfigureAwait(false);
		}
	}
}