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
                .WithMemberFormatter(id => Char.ToLower(id.Name[0]) + id.Name.Substring(1));
            
            foreach (var type in typeof(QuestionList).Assembly
                .GetTypes()
                .SelectMany(x => new[] {x}.Union(x.GetNestedTypes()))
                .Where(t => t.IsClass || t.IsEnum)
                .Where(t => !t.IsNestedPrivate))
            {
                ts.For(type);
            }
            
            using (var writer = new StreamWriter(new FileStream(fileName, FileMode.Create)))
            {
                writer.Write(ts.Generate());
            }
        }
    }
}