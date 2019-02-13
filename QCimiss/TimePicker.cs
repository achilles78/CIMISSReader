namespace QCimiss
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TimePicker : Form
    {
        private IContainer components = null;
        private Panel panel1;

        public TimePicker()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1b9, 0x1e7);
            this.panel1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b9, 0x1e7);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TimePicker";
            this.Text = "时间选择";
            base.ResumeLayout(false);
        }
    }
}

