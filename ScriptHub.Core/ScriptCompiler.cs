using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CSharp.RuntimeBinder;

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
			var script = CSharpScript.Create<T>(code, ScriptOptions.Default.WithImports("System", "System.IO"));
			script.Compile();
			return await script.RunAsync().ConfigureAwait(false);
		}

		public async Task<ScriptState> RunScript<T>(string code, object globals) {
			var refs = new List<MetadataReference> {
			MetadataReference.CreateFromFile(typeof(RuntimeBinderException).GetTypeInfo().Assembly.Location)
			, MetadataReference.CreateFromFile(typeof(DynamicAttribute).GetTypeInfo().Assembly.Location)
			};

			var script = CSharpScript.Create<T>(code, globalsType: globals.GetType()
			, options: ScriptOptions.Default.WithReferences(refs)
			.AddReferences(Assembly.GetAssembly(typeof(DynamicObject)), Assembly.GetAssembly(typeof(CSharpArgumentInfo))
			, Assembly.GetAssembly(typeof(ExpandoObject))).WithImports("System.Dynamic", "System", "System.IO"));
			script.Compile();
			return await script.RunAsync(globals).ConfigureAwait(false);
		}
	}
}