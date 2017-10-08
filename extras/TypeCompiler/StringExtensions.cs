namespace TypeCompiler
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string s, string end)
        {
            while (s.EndsWith(end))
            {
                s = s.Substring(0, s.Length - end.Length);
            }

            return s;
        }
        
        public static string TrimStart(this string s, string start)
        {
            while (s.StartsWith(start))
            {
                s = s.Substring(start.Length, s.Length - start.Length);
            }

            return s;
        }
    }
}