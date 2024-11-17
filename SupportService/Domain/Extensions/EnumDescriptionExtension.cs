using Domain.CustomAttributes;
using System.Reflection;

namespace Domain.Extensions;

public static class EnumDescriptionExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute =
            (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

        return attribute?.DescriptionField ?? value.ToString();
    }
}