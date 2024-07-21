namespace ROBOMVC.Util;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class DisplayAttribute : Attribute
{
    public string Name { get; }

    public DisplayAttribute(string name) => Name = name;
}
