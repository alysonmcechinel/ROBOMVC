namespace ROBOMVC.Util;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var type = enumValue.GetType();
        var memberInfo = type.GetMember(enumValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs.Length > 0)
            {
                return ((DisplayAttribute)attrs[0]).Name;
            }
        }
        return enumValue.ToString();
    }
}
