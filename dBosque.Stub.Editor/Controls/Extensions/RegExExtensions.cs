using System.Linq;
using System.Text.RegularExpressions;

namespace dBosque.Stub.Editor.Controls.Extensions
{
    /// <summary>
    /// Regular expression extensions
    /// </summary>
    public static class RegExExtensions
    {
        /// <summary>
        /// Get all groups that have a name and not a number
        /// </summary>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string[] GetNamedGroups(this Regex regex)
        {
            int dummy;
            return regex.GetGroupNames().Where(a => !int.TryParse(a, out dummy)).ToArray();
        }
    }
}
