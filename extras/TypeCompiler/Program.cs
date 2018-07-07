using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using TypeLite;
using TypeLite.Net4;
using TypeLite.TsModels;

namespace TypeCompiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileName = args.FirstOrDefault();
            
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Usage: TypeCompiler.exe <filename>");

            var ts = TypeScript.Definitions(new CustomGenerator())
                .AsConstEnums(false)
                .WithIndentation("  ")
                .WithMemberFormatter(id => Char.ToLower(id.Name[0]) + id.Name.Substring(1))
                //.WithModuleNameFormatter(mod => "")
                .WithModuleNameFormatter(mod => mod.Name.TrimStart("Rehearsal.Messages."))
                .WithVisibility((tsclass, name) => true)
                .WithTypeFormatter((type, formatter) => type.Type.Name);
            
            foreach (var type in typeof(QuestionListModel).Assembly
                .GetTypes()
                .SelectMany(x => new[] {x}.Union(x.GetNestedTypes()))
                .Where(t => t.IsClass || t.IsEnum)
                .Where(t => !t.IsNestedPrivate)
                .Where(IsModel))
            {
                Console.WriteLine($"Adding {type.FullName}");
                ts.For(type);
            }
            
            using (var writer = new StreamWriter(new FileStream(fileName, FileMode.Create)))
            {
                writer.Write(ts.Generate());
            }
        }

        private static bool IsModel(Type type) => type.IsNested 
            ? IsModel(type.DeclaringType) 
            : type.Name.EndsWith("Model") || type.Name.EndsWith("Request");

        
    }

    public class CustomGenerator : TsGenerator
    {
        protected override void AppendEnumDefinition(TsEnum enumModel, ScriptBuilder sb, TsGeneratorOutput output) {
            string typeName = this.GetTypeName(enumModel);
            string visibility = (output & TsGeneratorOutput.Enums) == TsGeneratorOutput.Enums || (output & TsGeneratorOutput.Constants) == TsGeneratorOutput.Constants ? "export " : "";

            _docAppender.AppendEnumDoc(sb, enumModel, typeName);

            string constSpecifier = this.GenerateConstEnums ? "const " : string.Empty;
            sb.AppendLineIndented(string.Format("{0}{2}enum {1} {{", visibility, typeName, constSpecifier));

            using (sb.IncreaseIndentation()) {
                int i = 1;
                foreach (var v in enumModel.Values) {
                    _docAppender.AppendEnumValueDoc(sb, v);
                    sb.AppendLineIndented(string.Format(i < enumModel.Values.Count ? "{0} = \"{1}\"," : "{0} = \"{1}\"", v.Name, v.Name));
                    i++;
                }
            }

            sb.AppendLineIndented("}");

            _generatedEnums.Add(enumModel);
        }
    }
}