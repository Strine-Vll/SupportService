namespace Domain.CustomAttributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class DescriptionAttribute : Attribute
{
    public string DescriptionField { get; }

    public DescriptionAttribute(string description)
    {
        DescriptionField = description;
    }
}