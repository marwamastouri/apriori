using System.Collections.Generic;
using System.Text;

namespace Apriori
{
    public sealed class ItemSupport
    {
        public ISet<string> Item { get; set; }
        public float Frequency { get; set; }
        public int Support { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}:", Support);
            var isFirstItem = true;
            foreach (var item in Item)
            {
                if (!isFirstItem)
                {
                    builder.Append(", ");
                }
                builder.Append(item);
                isFirstItem = false;
            }
            return builder.ToString();
        }
    }
}