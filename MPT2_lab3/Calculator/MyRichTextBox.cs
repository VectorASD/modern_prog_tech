using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    public class MyRichTextBox : RichTextBox {
        private const int WM_USER = 0x0400;
        private const int EM_SETEVENTMASK = (WM_USER + 69);
        private const int WM_SETREDRAW = 0x0b;
        private IntPtr OldEventMask;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public bool updateMode = false;
        private int updateLevel = 0;

        private void BeginUpdate() {
            if (!updateMode) {
                SendMessage(Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
                OldEventMask = (IntPtr)SendMessage(Handle, EM_SETEVENTMASK, IntPtr.Zero, IntPtr.Zero);
                updateMode = true;
            }
            updateLevel++;
        }

        private void EndUpdate() {
            if (!updateMode) return;

            if (--updateLevel <= 0) {
                SendMessage(Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
                SendMessage(Handle, EM_SETEVENTMASK, IntPtr.Zero, OldEventMask);
                updateMode = false;
            }
        }

        public static void Check() {
            MyRichTextBox rich = new(); // здесь ставить точку останова
            rich.Updater(() => {
                rich.Updater(() => {
                    rich.Updater(() => {
                    });
                });
            });
        }

        public void Updater(Action func) {
            try {
                BeginUpdate();
                func();
            } finally { EndUpdate(); }
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);

            Control? item = this;
            while (item is not null && item is not Form) item = item.Parent;
            // if (item is Form f) MessageBox.Show("focus: " + f.ActiveControl); // причём здесь MySplitContainer?!

            if (item is Form form && form.ActiveControl is Control focused) {
                if (focused is Button) // не даёт расфокусироваться из-за кнопки
                    Focus();
            }
        }
    }
}
