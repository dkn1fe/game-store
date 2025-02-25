namespace GameStore.Utils
{
    public static class AppUtils
    {
        public static string FirstCharLow(string str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string GetTransformationUrl(string url, string preset)
        {
            string[] parts = url.Split(new string[] { "/upload/" }, StringSplitOptions.None);
            return parts[0] + "/upload/" + preset + "/" + parts[1];
        }
    }
}
