using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator {
    public class MySplitContainer : SplitContainer {
        protected override void OnMouseDown(MouseEventArgs e) {
            // base.OnMouseDown(e);
            IsSplitterFixed = true;
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            // base.OnMouseUp(e);
            IsSplitterFixed = false;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (IsSplitterFixed) {
                if (e.Button.Equals(MouseButtons.Left)) {
                    if (Orientation.Equals(Orientation.Vertical)) {
                        if (e.X > 0 && e.X < Width) {
                            SplitterDistance = e.X;
                            Refresh();
                        }
                    } else if (e.Y > 0 && e.Y < Height) {
                        SplitterDistance = e.Y;
                        Refresh();
                    }
                } else
                    IsSplitterFixed = false;
            }
        }
    }
}
