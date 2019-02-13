namespace QCimiss
{
    using Qdata;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private Button button1;
        private Button button2;
        private IContainer components = null;

        public Form1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new QCmss().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new wuhanhourdata().Show();
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
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.button1.Location = new Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x73, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "cimiss数据查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0x9f, 12);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x81, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "武汉自动站图形查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x402, 560);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Name = "Form1";
            this.Text = "Form1";
            base.ResumeLayout(false);
        }
    }
}

