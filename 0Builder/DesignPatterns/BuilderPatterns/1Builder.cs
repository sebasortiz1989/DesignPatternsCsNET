using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace BuilderPatterns
{
    public class Builder : LifeWithoutBuilder
    {
        public class HtmlElement
        {
            public string Name, Text;
            public List<HtmlElement> Elements = new List<HtmlElement>();
            private const int indentSize = 2;

            public HtmlElement()
            {
            }

            public HtmlElement(string name, string text)
            {
                Name = name;
                Text = text;
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.Append($"{i}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string(' ', indentSize * (indent + 1)));
                    sb.AppendLine(Text);
                }

                foreach (var element in Elements)
                {
                    sb.Append(element.ToStringImpl(indent + 1));
                }
                
                sb.Append($"{i}</{Name}>");

                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(indent: 0);
            }
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            private HtmlElement root = new HtmlElement();

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            public void AddChild(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement {Name = rootName};
            }
        }

        public static void Main(string[] args)
        {
            LifeWithoutBuilderMain(args);
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            WriteLine(builder.ToString());
        }
    }
}