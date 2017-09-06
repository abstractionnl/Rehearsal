namespace TypeCompiler
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string s, string end)
        {
            if (s.EndsWith(end))
            {
                return s.Substring(0, s.Length - end.Length);
            }

            return s;
        }
    }
}