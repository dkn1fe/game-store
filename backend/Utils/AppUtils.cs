namespace GameStore.Utils
{
    public static class AppUtils
    {
        public static string FirstCharLow(string str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
