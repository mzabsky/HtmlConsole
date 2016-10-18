using System;

namespace HtmlConsole.Css
{
    public class Style
    {
        public string Property { get; }
        public string Value { get; }

        public Style(string property, string value)
        {
            Property = property;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Property}: {Value};";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Style) obj);
        }

        protected bool Equals(Style other)
        {
            return string.Equals(Property, other.Property, StringComparison.OrdinalIgnoreCase) && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}