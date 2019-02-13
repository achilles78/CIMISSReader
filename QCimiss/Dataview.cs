namespace QCimiss
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class Dataview : Form
    {
        private IContainer components = null;
        public DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private string titles;
        private ToolStripMenuItem 导出ToolStripMenuItem;

        public Dataview(string tts)
        {
            this.InitializeComponent();
            this.titles = tts;
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        private void Dataview_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AllowUserToAddRows = false;
            string[] strArray = this.titles.Split(new char[] { ',' });
            int index = 0;
            if (strArray.Length == this.dataGridView1.Columns.Count)
            {
                foreach (DataGridViewColumn column in this.dataGridView1.Columns)
                {
                    column.HeaderText = strArray[index];
                    index++;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
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
            this.dataGridView1 = new DataGridView();
            this.menuStrip1 = new MenuStrip();
            this.导出ToolStripMenuItem = new ToolStripMenuItem();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.menuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(0, 0x19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x309, 500);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowStateChanged += new DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.导出ToolStripMenuItem });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(0x309, 0x19);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new Size(0x2c, 0x15);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new EventHandler(this.导出ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x309, 0x20d);
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.menuStrip1);
            base.MainMenuStrip = this.menuStrip1;
            base.Name = "Dataview";
            this.Text = "Dataview";
            base.Load += new EventHandler(this.Dataview_Load);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataSource = this.dataGridView1.DataSource as DataTable;
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "逗号分隔符文件|*.csv"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(new BufferedStream(stream), Encoding.Default);
                writer.WriteLine(this.titles);
                foreach (DataRow row in dataSource.Rows)
                {
                    string str = "";
                    for (int i = 0; i < dataSource.Columns.Count; i++)
                    {
                        str = str + row[i].ToString().Trim() + ",";
                    }
                    str = str.TrimEnd(new char[] { ',' });
                    writer.WriteLine(str);
                }
                writer.Close();
                stream.Close();
            }
        }
    }
}

