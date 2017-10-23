
namespace dBosque.Stub.Repository.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Return the specified string, or the other in case the first is null or empty
        /// </summary>
        /// <param name="val"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static string IfEmpty(this string val, string other = null)
        {
            return string.IsNullOrWhiteSpace(val) ? other : val;
        }

        public static string IfEmptyFormat(this string val, string format, string other = null)
        {
            return string.IsNullOrWhiteSpace(val) ? (other??""): string.Format(format,val);
        }
    }
}
