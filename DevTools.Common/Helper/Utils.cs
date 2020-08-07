using System.Collections.Generic;
using System.Text;

namespace DevTools.Common.Helper
{
    public static class Utils
    {
        public static string InterpolateTags(string templateMessage, List<string> key, List<string> value)
        {
            StringBuilder content = new StringBuilder(templateMessage);

            for (int i = 0; i < value.Count; i++)
                content.Replace($"%{key[i]}%", value[i]);

            return content.ToString();
        }

    }
}