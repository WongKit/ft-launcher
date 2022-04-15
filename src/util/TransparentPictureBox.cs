using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// Allow transparency on layered PictureBoxes. Stack Overflow to the rescue!
/// https://stackoverflow.com/questions/36099017/how-to-make-two-transparent-layer-with-c
/// </summary>
namespace FT_Launcher {
    class TransparentPictureBox : PictureBox {
        public TransparentPictureBox() {
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e) {
            if (Parent != null && this.BackColor == Color.Transparent) {
                using (var bmp = new Bitmap(Parent.Width, Parent.Height)) {
                    Parent.Controls.Cast<Control>()
                          .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                          .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                          .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                          .ToList()
                          .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));

                    e.Graphics.DrawImage(bmp, -Left, -Top);

                }
            }
            base.OnPaint(e);
        }
    }
}