using Calculator;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TestProject {
    [TestClass]
    public sealed class AboutTests {
        private static string LabelCollector(Control item) {
            StringBuilder sb = new();
            foreach (Control control in item.Controls.Cast<Control>())
                if (control is Label label) {
                    sb.Append(label.Text);
                    sb.Append('\0');
                }
            return sb.ToString();
        }

        private static string StringHash(string text) {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            byte[] hash = SHA256.HashData(bytes);

            return bytes.Length + "_" + BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        [TestMethod]
        public void CheckText() {
            string expected = "2052_254249ca2bc90b0dbc77bc987b90297bf11e2778e8796cfc2dbaa6cf7283962d";
            AboutForm form = new();
            string text = LabelCollector(form);

            string actual = StringHash(text);

            Assert.AreEqual(expected, actual);
        }
    }
}
