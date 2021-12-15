using System.Linq;

namespace SmartPlug.Common
{
    public static class HashCodeUtils
    {
        public static int CombineHashCode(params int[] codes)
        {
            return codes.Length == 1
                ? codes[0]
                : codes.Aggregate(0,
                    (current,
                        t) => (current << 5) + current ^ t);
        }

        public static int CombineObjectsHashCodes(params object[] codes)
        {
            return CombineHashCode(codes.Select(x => x?.GetHashCode() ?? 0).ToArray());
        }
    }
}