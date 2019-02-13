namespace QCimiss
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Windows.Forms;

    public class citycode : Form
    {
        private string citysdisplay = "";
        private string ckdcitys = "";
        public string cnn = "Data Source=10.104.129.84;Initial Catalog=HistoryData;Persist Security Info=True;User ID=yw;Password=yw123";
        private IContainer components = null;
        public DataGridViewCell mycell;
        public QCmss qcmssform;
        private TreeView treeView1;

        public citycode(QCmss qs)
        {
            this.InitializeComponent();
            this.qcmssform = qs;
        }

        public bool BuildTree(TreeView tv, DataSet ds)
        {
            tv.Nodes.Clear();
            ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["地区名称"], ds.Tables[0].Columns["father"], false);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(row["father"].ToString()))
                {
                    TreeNode node = this.CreateNode(row["地区名称"].ToString());
                    node.Tag = row;
                    tv.Nodes.Add(node);
                    this.PopulateSubTree(row, node);
                }
            }
            return true;
        }

        private void CheckNode(TreeNodeCollection nodes, bool check)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = check;
                if ((node.Nodes != null) && (node.Nodes.Count != 0))
                {
                    this.CheckNode(node.Nodes, check);
                }
            }
        }

        private void citycode_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ckdcitys = "";
            this.citysdisplay = "";
            this.getallnodes(this.treeView1.Nodes);
            this.ckdcitys = this.ckdcitys.TrimEnd(new char[] { ',' });
            this.citysdisplay = this.citysdisplay.TrimEnd(new char[] { ',' });
            this.mycell.Value = this.citysdisplay;
            this.mycell.Tag = this.ckdcitys;
            base.Hide();
            this.qcmssform.Activate();
            if (this.qcmssform.dataGridView1.CurrentCell == this.mycell)
            {
                SendKeys.Send("{ENTER}");
            }
            e.Cancel = true;
        }

        private void citycode_Load(object sender, EventArgs e)
        {
            string selectCommandText = "SELECT [代码值],[地区名称],[father] FROM [HistoryData].[us_yw].[QCIMISSadminCodes]";
            SqlConnection selectConnection = new SqlConnection(this.cnn);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommandText, selectConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this.BuildTree(this.treeView1, dataSet);
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
        }

        private void ckborsAndpar(TreeNode node)
        {
            if (node.Checked)
            {
                if (node.Level > 0)
                {
                    TreeNode parent = node.Parent;
                    if (this.ckborther(node))
                    {
                        node.Parent.Checked = true;
                        node.Parent.Collapse();
                        this.ckborsAndpar(parent);
                        foreach (TreeNode node3 in node.Parent.Nodes)
                        {
                            node3.Checked = false;
                        }
                    }
                }
                else
                {
                    node.Collapse();
                }
            }
        }

        private bool ckborther(TreeNode node)
        {
            bool flag = true;
            if (node.Level > 0)
            {
                foreach (TreeNode node2 in node.Parent.Nodes)
                {
                    if (!node2.Checked)
                    {
                        return false;
                    }
                }
                return flag;
            }
            return false;
        }

        private TreeNode CreateNode(string text) => 
            new TreeNode { Text = text };

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void getallnodes(TreeNodeCollection aNodes)
        {
            foreach (TreeNode node in aNodes)
            {
                if (node.Checked)
                {
                    this.ckdcitys = this.ckdcitys + ((DataRow) node.Tag)["代码值"].ToString() + ",";
                    this.citysdisplay = this.citysdisplay + ((DataRow) node.Tag)["地区名称"].ToString() + ",";
                }
                if (node.Nodes.Count > 0)
                {
                    this.getallnodes(node.Nodes);
                }
            }
        }

        private void InitializeComponent()
        {
            this.treeView1 = new TreeView();
            base.SuspendLayout();
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(0x11c, 0x20a);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new TreeViewEventHandler(this.treeView1_AfterCheck);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x20a);
            base.Controls.Add(this.treeView1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "citycode";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "citycode";
            base.FormClosing += new FormClosingEventHandler(this.citycode_FormClosing);
            base.Load += new EventHandler(this.citycode_Load);
            base.ResumeLayout(false);
        }

        private void PopulateSubTree(DataRow dbRow, TreeNode node)
        {
            foreach (DataRow row in dbRow.GetChildRows("NodeRelation"))
            {
                TreeNode node2 = this.CreateNode(row["地区名称"].ToString());
                node2.Tag = row;
                node.Nodes.Add(node2);
                this.PopulateSubTree(row, node2);
            }
        }

        private void SetChildNode(TreeNode node, bool check)
        {
            foreach (TreeNode node2 in node.Nodes)
            {
                node2.Checked = check;
                if (node.Nodes.Count > 0)
                {
                    this.SetChildNode(node2, check);
                }
            }
        }

        private void SetParentNode(TreeNode node, bool check)
        {
            if (node.Level > 0)
            {
                node.Parent.Checked = check;
                this.SetParentNode(node.Parent, check);
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                this.SetParentNode(e.Node, false);
                this.SetChildNode(e.Node, false);
                this.ckborsAndpar(e.Node);
            }
        }
    }
}

