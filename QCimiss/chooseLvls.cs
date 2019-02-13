namespace QCimiss
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class chooseLvls : Form
    {
        public CheckedListBox checkedListBox1;
        private IContainer components = null;
        public DataGridViewCell mycell;
        public QCmss qc;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;

        public chooseLvls(QCmss qc)
        {
            this.InitializeComponent();
            this.qc = qc;
        }

        private void chooseLvls_FormClosing(object sender, FormClosingEventArgs e)
        {
            string str = "";
            string str2 = "";
            if (this.checkedListBox1.Items.Count > 0)
            {
                foreach (object obj2 in this.checkedListBox1.CheckedItems)
                {
                    DataRowView view = (DataRowView) obj2;
                    str2 = str2 + view.Row["台站级别"] + ",";
                    str = str + view.Row["代码"] + ",";
                }
                str = str.TrimEnd(new char[] { ',' });
                str2 = str2.TrimEnd(new char[] { ',' });
            }
            this.mycell.Value = str2;
            this.mycell.Tag = str;
            base.Hide();
            this.qc.Activate();
            if (this.qc.dataGridView1.CurrentCell == this.mycell)
            {
                SendKeys.Send("{ENTER}");
            }
            e.Cancel = true;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(chooseLvls));
            this.toolStrip1 = new ToolStrip();
            this.toolStripButton1 = new ToolStripButton();
            this.toolStripButton2 = new ToolStripButton();
            this.checkedListBox1 = new CheckedListBox();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton1, this.toolStripButton2 });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(280, 0x19);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = (Image) manager.GetObject("toolStripButton1.Image");
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(0x24, 0x16);
            this.toolStripButton1.Text = "全选";
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = (Image) manager.GetObject("toolStripButton2.Image");
            this.toolStripButton2.ImageTransparentColor = Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new Size(0x30, 0x16);
            this.toolStripButton2.Text = "全不选";
            this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(0, 0x19);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(280, 0xfc);
            this.checkedListBox1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(280, 0x115);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.toolStrip1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "chooseLvls";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "chooseLvls";
            base.FormClosing += new FormClosingEventHandler(this.chooseLvls_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
        }
    }
}

