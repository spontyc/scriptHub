using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;

namespace ScriptHub.Core {
	public interface IScriptCompiler {
		Task<T> EvaluateAsync<T>(string code);
		Task<object> EvaluateAsync(string code);
		Task<T> EvaluateAsync<T>(string code, object globals);
		Task<object> EvaluateAsync(string code, object globals);
		Task<ScriptState> RunScript<T>(string code);
		Task<ScriptState> RunScript<T>(string code, object globals);
	}
}