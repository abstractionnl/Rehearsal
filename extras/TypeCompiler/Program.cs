using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Rehearsal.Messages;
using TypeLite;
using TypeLite.Net4;

namespace TypeCompiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileName = args.FirstOrDefault();
            
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Usage: TypeCompiler.exe <filename>");

            var ts = TypeScript.Definitions()
                .AsConstEnums(false)
                .WithIndentation("  ")
                .WithMemberFormatter(id => Char.ToLower(id.Name[0]) + id.Name.Substring(1))
                .WithModuleNameFormatter(mod => mod.Name.Replace(".Messages", "").TrimEnd("Model"))
                .WithTypeFormatter((type, formatter) => type.Type.Name.TrimEnd("Model"));
            
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
}