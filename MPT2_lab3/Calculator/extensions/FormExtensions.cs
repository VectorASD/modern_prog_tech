using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.extensions {
    public static class FormExtensions {
        public static IEnumerable<T> Descendants<T>(this Control control) where T : class {
            foreach (Control child in control.Controls) {
                if (child is T item)
                    yield return item; // опа, опа-па! не знал про yield !!! :/
                if (child.HasChildren)
                    foreach (T descendant in Descendants<T>(child))
                        yield return descendant;
            }
        }
    }
}
