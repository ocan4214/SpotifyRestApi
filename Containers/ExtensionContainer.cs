using System.Reflection;
using System.Runtime.Serialization;

namespace SpotifyRestApi.Containers
{
    public static class ExtensionContainer
    {
        public static string GetEnumMemberValue(this Enum @enum)
        {
            var attr = @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            else
                return attr.Value;
        }
    }
}
