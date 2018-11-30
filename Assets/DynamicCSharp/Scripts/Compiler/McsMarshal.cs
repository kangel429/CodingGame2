using System.IO;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DynamicCSharp.Compiler
{
    internal sealed class McsMarshal : ICompiler
    {
        // Private
        private ICodeCompiler compiler = null;
        private CompilerParameters parameters = null;
        private byte[] assemblyData = null;

        // Properties
        public byte[] AssemblyData
        {
            get { return assemblyData; }
        }

        // Constructor
        public McsMarshal()
        {
            // Create our managed compiler
            compiler = new McsCompiler();

            // Create the parameters
            parameters = new CompilerParameters();
            {
                parameters.GenerateExecutable = false;
                parameters.GenerateInMemory = false;
                parameters.CompilerOptions = "/optimize";
                parameters.IncludeDebugInformation = false;
            }
        }

        // Methods
        public void AddReference(string reference)
        {
            // Add a reference
            parameters.ReferencedAssemblies.Add(reference);
        }

        public void AddReferences(IEnumerable<string> references)
        {
            // Add all references
            foreach (string reference in references)
                AddReference(reference);
        }

        public ScriptCompilerError[] Compile(string[] source)
        {
            // Invoke the compiler
            CompilerResults results = compiler.CompileAssemblyFromSourceBatch(parameters, source);

            // Build errors
            ScriptCompilerError[] errors = new ScriptCompilerError[results.Errors.Count];

            // Clear parameters
            parameters.ReferencedAssemblies.Clear();

            // Create error copy
            int index = 0;
            foreach(CompilerError err in results.Errors)
            {
                // Generate the error
                errors[index] = new ScriptCompilerError
                {
                    errorCode = err.ErrorNumber,
                    errorText = err.ErrorText,
                    fileName = err.FileName,
                    line = err.Line,
                    column = err.Column,
                    isWarning = err.IsWarning,
                };

                // Increment index
                index++;
            }

            // Check for success
            if(results.CompiledAssembly != null)
            {
                // Find the output name
                string name = results.CompiledAssembly.GetName().Name + ".dll";

                // Read the file
                assemblyData = File.ReadAllBytes(name);

                // Delete the temp file
                File.Delete(name);
            }

            // Get the errors
            return errors;
        }
    }
}
