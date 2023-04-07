using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonCtrls
{
    public partial class GroupRadio : Control
    {

        #region [Properties]

        string[] _stringOptions = new string[] { "Normal", "Error", "Running" };
        [DisplayName("Options"), Description("Set Option Strings Accordingly"), Category("Custom Settings")]
        public string[] StringOptions
        {
            get
            {
                return _stringOptions;
            }

            set
            {
                _stringOptions = value;

                // calculate boxes for each option...
                CalcBoxSizes();

                // force to redraw...
                UpdateUI();
            }
        }

        int _normalOptionIndex = 99;
        [DisplayName("Normal Option Index"), Description("Set Index Of Normal State (Zero-Based)"), Category("Custom Settings")]
        public int NormalOptionIndex
        {
            get
            {
                return _normalOptionIndex;
            }
            set
            {
                _normalOptionIndex = value;
                UpdateUI();
            }
        }

        int _errorOptionIndex = 98;
        [DisplayName("Error Option Index"), Description("Set Index Of Error State (Zero-Based)"), Category("Custom Settings")]
        public int ErrorOptionIndex
        {
            get
            {
                return _errorOptionIndex;
            }
            set
            {
                _errorOptionIndex = value;
                UpdateUI();
            }
        }

        Color _normalOptionColor = Color.LightBlue;
        [DisplayName("Normal Color"), Description("Set Color For Normal State"), Category("Custom Settings")]
        public Color NormalOptionColor
        {
            get
            {
                return _normalOptionColor;
            }
            set
            {
                _normalOptionColor = value;
                _brNormal = new SolidBrush(_normalOptionColor);
                UpdateUI();
            }
        }

        Color _errorOptionColor = Color.Red;
        [DisplayName("Error Color"), Description("Set Color For Error State"), Category("Custom Settings")]
        public Color ErrorOptionColor
        {
            get
            {
                return _errorOptionColor;
            }
            set
            {
                _errorOptionColor = value;
                _brError = new SolidBrush(_errorOptionColor);
                UpdateUI();
            }
        }

        Color _neutralOptionColor = Color.Lime;
        [DisplayName("Neutral Color"), Description("Set Color For Neutral State"), Category("Custom Settings")]
        public Color NeutralOptionColor
        {
            get
            {
                return _neutralOptionColor;
            }

            set
            {
                _neutralOptionColor = value;
                _brNeutral = new SolidBrush(_neutralOptionColor);
                UpdateUI();
            }
        }

        int _indexChecked = 0;
        [DisplayName("Checked Index"), Description("Set Index Of Option Checked"), Category("Custom Settings")]
        public int IndexChecked
        {
            get
            {
                return _indexChecked;
            }

            set
            {
                if (value != _indexChecked)
                {
                    _indexChecked = value;
                    UpdateUI();
                }
            }
        }

        int _marginLeft = 16;
        [DisplayName("Left Margin"), Description("Set The Left Margin"), Category("Custom Settings")]
        public int MarginLeft
        {
            get
            {
                return _marginLeft;
            }

            set
            {
                _marginLeft = value;

                // calculate boxes for each option...
                CalcBoxSizes();

                //
                UpdateUI();
            }
        }

        #endregion

        public GroupRadio()
        {
            //
            CalcOptionBoxes();

            //
            _brOff = new SolidBrush(Color.Gray);
            _brNormal = new SolidBrush(NormalOptionColor);
            _brError = new SolidBrush(ErrorOptionColor);
            _brNeutral = new SolidBrush(NeutralOptionColor);

            //
            this.SizeChanged += OnSizeChanged;

            //
            this.MouseClick += GroupRadio_MouseClick;
        }

        public event EventHandler<GroupRadioClickEventArgs> OptionButtonClicked;
        protected void OnObjectItemRightClicked(GroupRadioClickEventArgs e)
        {
            OptionButtonClicked?.Invoke(this, e);
        }

        private void GroupRadio_MouseClick(object sender, MouseEventArgs e)
        {
            int i = 0;
            foreach (Rectangle rc in _rcOptionsRect)
            {
                if (rc.Contains(e.Location))
                {
                    OnObjectItemRightClicked(new GroupRadioClickEventArgs(i));
                    return;
                }
                i++;
            }
        }

        #region [Object Sizing]

        protected override Size DefaultSize
        {
            get
            {
                return new Size(160, 20);
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
        }

        void OnSizeChanged(object sender, EventArgs e)
        {
            CalcBoxSizes();
            Refresh();
        }

        #endregion


        #region [Painting]

        public virtual void UpdateUI()
        {
            if (InvokeRequired)
                Invoke(new Action(() => Refresh()));
            else
                Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            this.DrawObject(gfx);
        }

        protected virtual void DrawObject(Graphics gfx)
        {
            DrawOptions(gfx);
            DrawBorder(gfx);
        }

        protected virtual void DrawBorder(Graphics gfx)
        {
            //gfx.DrawRectangle(defaultPen, DisplayRectangle);
        }

        protected virtual void DrawOptions(Graphics gfx)
        {
            for (int i = 0; i < _rcOptionsRect.Length; i++)
            {
                if (i == IndexChecked)
                {
                    if (IndexChecked == NormalOptionIndex)
                        DrawOption(gfx, _rcOptionsRect[i], StringOptions[i], OptionCircleColorSelector.Normal);
                    else if (IndexChecked == ErrorOptionIndex)
                        DrawOption(gfx, _rcOptionsRect[i], StringOptions[i], OptionCircleColorSelector.Error);
                    else
                        DrawOption(gfx, _rcOptionsRect[i], StringOptions[i], OptionCircleColorSelector.Neutral);
                }
                else
                {
                    DrawOption(gfx, _rcOptionsRect[i], StringOptions[i], OptionCircleColorSelector.Off);
                }
            }
        }

        protected virtual void DrawOption(Graphics gfx, Rectangle rc, string text, OptionCircleColorSelector selector)
        {
            Rectangle rcCircle = new Rectangle(rc.X, rc.Y, rc.Height, rc.Height);
            Rectangle rcText = new Rectangle(rc.X + rc.Height, rc.Y, rc.Width - rc.Height, rc.Height);


            // draw circle
            switch (selector)
            {
                case OptionCircleColorSelector.Off:
                    gfx.FillEllipse(_brOff, rcCircle);
                    break;

                case OptionCircleColorSelector.Normal:
                    gfx.FillEllipse(_brNormal, rcCircle);
                    break;

                case OptionCircleColorSelector.Error:
                    gfx.FillEllipse(_brError, rcCircle);
                    break;

                case OptionCircleColorSelector.Neutral:
                    gfx.FillEllipse(_brNeutral, rcCircle);
                    break;
            }
            gfx.DrawEllipse(defaultPen, rcCircle);

            // draw text
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            gfx.DrawString(text, base.Font, Brushes.Black, rcText, sf);
        }

        // default brishes and pens
        SolidBrush _brNormal;
        SolidBrush _brError;
        SolidBrush _brNeutral;
        SolidBrush _brOff;
        Pen defaultPen = new Pen(Color.FromArgb(0, 0, 0), 1.0f);

        // drawing area
        protected Rectangle _rcDrawingRect;
        protected Rectangle[] _rcOptionsRect;

        protected virtual void CalcBoxSizes()
        {
            // get object paint area
            _rcDrawingRect = DisplayRectangle;
            _rcDrawingRect.Inflate(-4, -4);
            _rcDrawingRect.Width--;
            _rcDrawingRect.Height--;

            // set sizing area...
            CalcOptionBoxes();
        }

        protected virtual void CalcOptionBoxes()
        {
            int cntOptions = StringOptions.Length;
            if (cntOptions <= 0) return;

            _rcOptionsRect = new Rectangle[cntOptions];
            int option_width = (_rcDrawingRect.Width - MarginLeft) / cntOptions;
            for (int i = 0; i < cntOptions; i++)
            {
                _rcOptionsRect[i] = new Rectangle(i * option_width + MarginLeft, _rcDrawingRect.Y, option_width, _rcDrawingRect.Height);
            }
        }

        #endregion

    }

    public class GroupRadioClickEventArgs : EventArgs
    {
        public GroupRadioClickEventArgs(int index)
        {
            _index_clicked = index;
        }
        private int _index_clicked;

        public int IndexClicked
        {
            get { return _index_clicked; }
        }
    }

    public enum OptionCircleColorSelector
    {
        Off = 0,
        Normal = 1,
        Error = 2,
        Neutral = 3
    }
}
