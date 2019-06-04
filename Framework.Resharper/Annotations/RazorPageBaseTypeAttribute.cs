using System;

namespace Framework.Resharper.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorPageBaseTypeAttribute : Attribute
    {
        public RazorPageBaseTypeAttribute([NotNull] string baseType)
        {
            this.BaseType = baseType;
        }
        public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
        {
            this.BaseType = baseType;
            this.PageName = pageName;
        }

        [NotNull] public string BaseType { get; private set; }
        [CanBeNull] public string PageName { get; private set; }
    }
}