namespace QCimiss
{
    using cma.cimiss;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class QCmss : Form
    {
        private Button btopentxt;
        private Button btview;
        private Button button1;
        public Button button2;
        public citycode ccform = null;
        public chooseElement CEform = null;
        public string choosedelement = "";
        public string choosedlvls = "";
        private chooseLvls ClvlFrm = null;
        public string cnn = "Data Source=10.104.129.84;Initial Catalog=HistoryData;Persist Security Info=True;User ID=yw;Password=yw123";
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private IContainer components = null;
        public string[] dataclasses = new string[] { "SURF;地面资料", "UPAR;高空资料", "OCEN;海洋资料", "RADI;辐射资料", "AGME;农气资料", "NAFP;数值模式", "CAWN;大气成分", "HPXY;历史代用", "DISA;气象灾害", "RADA;雷达资料", "SATE;卫星资料", "SCEX;科考资料", "SEVP;服务产品", "OTHE;其它资料" };
        public DataGridView dataGridView1;
        public DataTable dtdatavalues = null;
        public DataTable dtparas = null;
        private Dataview dv = null;
        public List<Datainfo> interfaces;
        private int limitedrows = 10;
        public Dictionary<string, string> list = null;
        public DateTime loadtime;
        public List<Parainfo> paras;
        public List<Datainfo> pss;
        private RadioButton rbarray2d;
        private RadioButton rbdownload;
        private RadioButton rbgridarray2d;
        private RadioButton rbsaveasfile;
        private RichTextBox richTextBox1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        public string user = "";

        public QCmss()
        {
            this.InitializeComponent();
            this.load();
            this.list = new Dictionary<string, string>();
            foreach (string str in this.dataclasses)
            {
                string[] strArray = str.Split(new char[] { ';' });
                this.list.Add(strArray[1], strArray[0]);
            }
            this.dtparas = new DataTable();
            DataColumn column = new DataColumn("id", typeof(string));
            DataColumn column2 = new DataColumn("name", typeof(string));
            DataColumn column3 = new DataColumn("value", typeof(string));
            DataColumn column4 = new DataColumn("type", typeof(string));
            this.dtparas.Columns.Add(column);
            this.dtparas.Columns.Add(column2);
            this.dtparas.Columns.Add(column3);
            this.dtparas.Columns.Add(column4);
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.DataSource = this.dtparas;
            this.dataGridView1.Columns["id"].Visible = false;
            this.dataGridView1.Columns["type"].HeaderText = "是否必填";
            this.dataGridView1.Columns["type"].ReadOnly = true;
            this.dataGridView1.Columns["type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns["name"].ReadOnly = true;
            this.dataGridView1.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns["name"].HeaderText = "参数";
            this.dataGridView1.Columns["value"].HeaderText = "  参数值   ";
            this.dataGridView1.Columns["value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            foreach (DataGridViewColumn column5 in this.dataGridView1.Columns)
            {
                column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btopentxt_Click(object sender, EventArgs e)
        {
            if (this.btopentxt.Tag != null)
            {
                string tag = (string) this.btopentxt.Tag;
                Process.Start(tag);
            }
        }

        private void btview_Click(object sender, EventArgs e)
        {
            if (this.btview.Tag != null)
            {
                string tag = (string) this.btview.Tag;
                Process.Start(tag);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.comboBox2.SelectedItem != null) && (this.comboBox3.SelectedItem != null))
            {
                this.button1.Enabled = false;
                if (this.chkneededpara())
                {
                    string str;
                    Dictionary<string, string> param = this.getparas();
                    if (this.rbarray2d.Checked)
                    {
                        RetArray2D arrayd = Cimiss.getArray2D(((Datainfo) this.comboBox3.SelectedItem).id, param);
                        if (arrayd.request.errorCode == 0)
                        {
                            str = string.Concat(new object[] { "insert into [HistoryData].[us_yw].[QCIMISSQueryLog] values('", this.loadtime.ToString(), "','", this.user, "','", arrayd.request.requestTime, "','", arrayd.request.responseTime, "',", arrayd.request.takeTime, ",'", arrayd.request.requestParams, "',", arrayd.request.rowCount.ToString(), ")" });
                            try
                            {
                                sql.insertsql(this.cnn, str);
                            }
                            catch
                            {
                            }
                            this.dtdatavalues = this.R2dToDatatable(arrayd);
                            this.dv = new Dataview(this.dataGridView1.Tag as string);
                            this.dv.dataGridView1.DataSource = this.dtdatavalues;
                            this.dv.Show();
                            this.dv.dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                        }
                        else
                        {
                            MessageBox.Show("查询错误，" + arrayd.request.errorMessage);
                        }
                    }
                    else
                    {
                        RetFilesInfo info;
                        if (this.rbdownload.Checked)
                        {
                            FolderBrowserDialog dialog = new FolderBrowserDialog();
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                info = Cimiss.downFiles(((Datainfo) this.comboBox3.SelectedItem).id, param, dialog.SelectedPath);
                                if (info.request.errorCode == 0)
                                {
                                    this.btview.Tag = dialog.SelectedPath;
                                    this.btview.Enabled = true;
                                    str = string.Concat(new object[] { "insert into [HistoryData].[us_yw].[QCIMISSQueryLog] values('", this.loadtime.ToString(), "','", this.user, "','", info.request.requestTime, "','", info.request.responseTime, "',", info.request.takeTime, ",'", info.request.requestParams, "',", info.request.rowCount.ToString(), ")" });
                                    try
                                    {
                                        sql.insertsql(this.cnn, str);
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("查询错误，" + info.request.errorMessage);
                                }
                            }
                        }
                        else if (this.rbsaveasfile.Checked)
                        {
                            SaveFileDialog dialog2 = new SaveFileDialog {
                                Filter = "逗号分隔符文件|*.csv|txt文件|*.txt"
                            };
                            if (dialog2.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dialog2.FileName;
                                string format = (dialog2.FilterIndex == 1) ? "csv" : "text";
                                info = Cimiss.saveasFile(((Datainfo) this.comboBox3.SelectedItem).id, param, fileName, format);
                                if (info.request.errorCode == 0)
                                {
                                    str = string.Concat(new object[] { "insert into [HistoryData].[us_yw].[QCIMISSQueryLog] values('", this.loadtime.ToString(), "','", this.user, "','", info.request.requestTime, "','", info.request.responseTime, "',", info.request.takeTime, ",'", info.request.requestParams, "',", info.request.rowCount.ToString(), ")" });
                                    try
                                    {
                                        sql.insertsql(this.cnn, str);
                                    }
                                    catch
                                    {
                                    }
                                    this.btopentxt.Enabled = true;
                                    this.btopentxt.Tag = fileName;
                                    if (format == "csv")
                                    {
                                        string str4 = "";
                                        using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
                                        {
                                            reader.ReadLine();
                                            str4 = (this.dataGridView1.Tag as string) + "\r\n" + reader.ReadToEnd();
                                        }
                                        using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.Default))
                                        {
                                            writer.Write(str4);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("查询错误，" + info.request.errorMessage);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("必填参数未填完成！");
                }
                this.button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.load();
        }

        private bool chkneededpara()
        {
            foreach (DataRow row in this.dtparas.Rows)
            {
                if ((row["type"].ToString() == "是") && (row["value"].ToString() == ""))
                {
                    return false;
                }
            }
            return true;
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.comboBox1.DroppedDown = true;
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Text = "";
            this.pss = null;
            KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>) this.comboBox1.SelectedItem;
            string json = Cimiss.Api_getDatasetInfo("DataClassId", selectedItem.Value, "null");
            this.pss = JsonHelper.DeserializeJsonToList<Datainfo>(json);
            this.comboBox2.DataSource = this.pss;
            this.comboBox2.SelectedIndexChanged -= new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox2.DisplayMember = "name";
            this.comboBox2.ValueMember = "id";
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            if (this.pss.Count <= 0)
            {
                this.comboBox3.DataSource = null;
            }
        }

        private void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {
            this.comboBox2.DroppedDown = true;
        }

        private void comboBox2_MouseHover(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox3.Text = "";
            this.interfaces = null;
            if (this.comboBox2.SelectedItem != null)
            {
                Datainfo selectedItem = (Datainfo) this.comboBox2.SelectedItem;
                string json = Cimiss.Api_getDatasetInfo("DataCode", selectedItem.id, selectedItem.id);
                this.interfaces = JsonHelper.DeserializeJsonToList<Datainfo>(json);
            }
            this.comboBox3.DataSource = this.interfaces;
            this.comboBox3.SelectedIndexChanged -= new EventHandler(this.comboBox3_SelectedIndexChanged);
            this.comboBox3.DisplayMember = "name";
            this.comboBox3.ValueMember = "id";
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
        }

        private void comboBox3_MouseDown(object sender, MouseEventArgs e)
        {
            this.comboBox3.DroppedDown = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.paras = null;
            this.CEform = null;
            this.ccform = null;
            this.ClvlFrm = null;
            if (this.comboBox3.SelectedItem != null)
            {
                Datainfo selectedItem = (Datainfo) this.comboBox3.SelectedItem;
                string json = Cimiss.Api_getDatasetInfo("CustomApiId", selectedItem.id, ((Datainfo) this.comboBox2.SelectedItem).id);
                this.paras = JsonHelper.DeserializeJsonToList<Parainfo>(json);
            }
            this.updatedatatable(this.dtparas, this.paras);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex == 2))
            {
                if (this.dtparas.Rows[e.RowIndex]["id"].ToString() == "elements")
                {
                    if (this.CEform == null)
                    {
                        List<ElementTable> list = JsonHelper.DeserializeJsonToList<ElementTable>(Cimiss.Api_getElements(((Datainfo) this.comboBox2.SelectedItem).id));
                        if (list != null)
                        {
                            this.CEform = new chooseElement();
                            this.CEform.qcmssform = this;
                            this.CEform.mycell = ((DataGridView) sender).CurrentCell;
                            this.CEform.mycell.ReadOnly = true;
                            foreach (ElementTable table in list)
                            {
                                this.CEform.checkedListBox1.Items.Add(table.id);
                            }
                            this.CEform.checkedListBox1.ValueMember = "userEleCode";
                            this.CEform.checkedListBox1.DisplayMember = "eleName";
                            this.CEform.Show();
                        }
                    }
                    else if (this.CEform.Visible)
                    {
                        this.CEform.Activate();
                    }
                    else
                    {
                        this.CEform.Show();
                    }
                }
                if ((this.dtparas.Rows[e.RowIndex]["id"].ToString() == "times") || (this.dtparas.Rows[e.RowIndex]["id"].ToString() == "timeRange"))
                {
                    TimePicker picker = new TimePicker();
                }
                if (this.dtparas.Rows[e.RowIndex]["id"].ToString() == "staLevels")
                {
                    if (this.ClvlFrm == null)
                    {
                        KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>) this.comboBox1.SelectedItem;
                        string str2 = selectedItem.Value.Split(new char[] { '_' })[0];
                        string sltstr = "SELECT *  FROM [HistoryData].[us_yw].[QCIMISSstaLevels] where [站网名称代码]='" + str2 + "' order by [代码]";
                        DataTable table2 = sql.readsql(this.cnn, sltstr);
                        if (table2.Rows.Count >= 0)
                        {
                            this.ClvlFrm = new chooseLvls(this);
                            this.ClvlFrm.mycell = ((DataGridView) sender).CurrentCell;
                            this.ClvlFrm.mycell.ReadOnly = true;
                            this.ClvlFrm.checkedListBox1.DataSource = table2;
                            this.ClvlFrm.checkedListBox1.ValueMember = "代码";
                            this.ClvlFrm.checkedListBox1.DisplayMember = "台站级别";
                            this.ClvlFrm.Text = table2.Rows[0]["站网名称"].ToString();
                            this.ClvlFrm.Show();
                        }
                    }
                    else if (this.ClvlFrm.Visible)
                    {
                        this.ClvlFrm.Activate();
                    }
                    else
                    {
                        this.ClvlFrm.Show();
                    }
                }
                if (this.dtparas.Rows[e.RowIndex]["id"].ToString() == "adminCodes")
                {
                    if (this.ccform == null)
                    {
                        this.ccform = new citycode(this);
                        this.ccform.mycell = ((DataGridView) sender).CurrentCell;
                        this.ccform.mycell.ReadOnly = true;
                        this.ccform.Show();
                    }
                    else if (this.ccform.Visible)
                    {
                        this.ccform.Activate();
                    }
                    else
                    {
                        this.ccform.Show();
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Rows[e.RowIndex].Cells["type"].Value.ToString() == "是")
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((((e.RowIndex > 0) && (this.user == "")) && (e.ColumnIndex == 2)) && (this.dtparas.Rows[e.RowIndex]["id"].ToString() == "limitCnt"))
            {
                this.dtparas.Rows[e.RowIndex]["value"] = this.limitedrows;
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            if ((this.dataGridView1.CurrentCell != null) && (this.dtparas.Rows[this.dataGridView1.CurrentCell.RowIndex]["id"].ToString() != "elements"))
            {
                paramhelp paramhelp = JsonHelper.DeserializeJsonToObject<paramhelp>(Cimiss.IdentifierCode_findByparamId(this.dtparas.Rows[this.dataGridView1.CurrentCell.RowIndex]["id"].ToString()));
                this.richTextBox1.Text = paramhelp.valueFormat;
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

        private Dictionary<string, string> getparas()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string> {
                { 
                    "dataCode",
                    ((Datainfo) this.comboBox2.SelectedItem).id
                }
            };
            foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
            {
                if (row.Cells["value"].Value.ToString() != "")
                {
                    if (row.Cells["value"].Tag != null)
                    {
                        dictionary.Add(row.Cells["id"].Value.ToString(), (string) row.Cells["value"].Tag);
                    }
                    else
                    {
                        dictionary.Add(row.Cells["id"].Value.ToString(), row.Cells["value"].Value.ToString());
                    }
                }
            }
            return dictionary;
        }

        private string getstrparas(Dictionary<string, string> strparams)
        {
            string str = "";
            foreach (string str2 in strparams.Keys)
            {
                string str4 = str;
                str = str4 + str2 + ":" + strparams[str2] + ",";
            }
            return str.TrimEnd(new char[] { ',' });
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new DataGridView();
            this.splitContainer1 = new SplitContainer();
            this.splitContainer3 = new SplitContainer();
            this.comboBox1 = new ComboBox();
            this.button2 = new Button();
            this.comboBox3 = new ComboBox();
            this.comboBox2 = new ComboBox();
            this.button1 = new Button();
            this.btopentxt = new Button();
            this.btview = new Button();
            this.rbgridarray2d = new RadioButton();
            this.rbdownload = new RadioButton();
            this.rbsaveasfile = new RadioButton();
            this.rbarray2d = new RadioButton();
            this.splitContainer2 = new SplitContainer();
            this.richTextBox1 = new RichTextBox();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer2.BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            base.SuspendLayout();
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.ScrollBars = ScrollBars.Horizontal;
            this.dataGridView1.Size = new Size(0x3f0, 0x1a2);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellChanged += new EventHandler(this.dataGridView1_CurrentCellChanged);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new Size(0x3f0, 730);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 6;
            this.splitContainer3.Dock = DockStyle.Fill;
            this.splitContainer3.Location = new Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer3.Panel1.Controls.Add(this.button2);
            this.splitContainer3.Panel1.Controls.Add(this.comboBox3);
            this.splitContainer3.Panel1.Controls.Add(this.comboBox2);
            this.splitContainer3.Panel2.Controls.Add(this.btopentxt);
            this.splitContainer3.Panel2.Controls.Add(this.btview);
            this.splitContainer3.Panel2.Controls.Add(this.rbgridarray2d);
            this.splitContainer3.Panel2.Controls.Add(this.rbdownload);
            this.splitContainer3.Panel2.Controls.Add(this.button1);
            this.splitContainer3.Panel2.Controls.Add(this.rbsaveasfile);
            this.splitContainer3.Panel2.Controls.Add(this.rbarray2d);
            this.splitContainer3.Size = new Size(0x3f0, 110);
            this.splitContainer3.SplitterDistance = 490;
            this.splitContainer3.TabIndex = 6;
            this.comboBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.comboBox1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(3, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x17f, 0x1d);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.MouseDown += new MouseEventHandler(this.comboBox1_MouseDown);
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button2.Location = new Point(0x198, 1);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x1c);
            this.button2.TabIndex = 5;
            this.button2.Text = "登录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.comboBox3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.comboBox3.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new Point(3, 0x3d);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(480, 0x1d);
            this.comboBox3.TabIndex = 2;
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
            this.comboBox3.MouseDown += new MouseEventHandler(this.comboBox3_MouseDown);
            this.comboBox2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.comboBox2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new Point(3, 0x1f);
            this.comboBox2.MaxDropDownItems = 15;
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(480, 0x1d);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox2.MouseDown += new MouseEventHandler(this.comboBox2_MouseDown);
            this.button1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.button1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button1.Location = new Point(0x1a1, 11);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x55, 0x1c);
            this.button1.TabIndex = 4;
            this.button1.Text = "执行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.btopentxt.Enabled = false;
            this.btopentxt.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btopentxt.Location = new Point(0x1a1, 0x4f);
            this.btopentxt.Name = "btopentxt";
            this.btopentxt.Size = new Size(0x55, 0x1c);
            this.btopentxt.TabIndex = 9;
            this.btopentxt.Text = "打开文件";
            this.btopentxt.UseVisualStyleBackColor = true;
            this.btopentxt.Click += new EventHandler(this.btopentxt_Click);
            this.btview.Enabled = false;
            this.btview.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btview.Location = new Point(0x1a1, 0x2d);
            this.btview.Name = "btview";
            this.btview.Size = new Size(0x55, 0x1c);
            this.btview.TabIndex = 8;
            this.btview.Text = "文件夹";
            this.btview.UseVisualStyleBackColor = true;
            this.btview.Click += new EventHandler(this.btview_Click);
            this.rbgridarray2d.AutoSize = true;
            this.rbgridarray2d.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.rbgridarray2d.Location = new Point(0x111, 30);
            this.rbgridarray2d.Name = "rbgridarray2d";
            this.rbgridarray2d.Size = new Size(0x5c, 0x19);
            this.rbgridarray2d.TabIndex = 3;
            this.rbgridarray2d.TabStop = true;
            this.rbgridarray2d.Text = "图形显示";
            this.rbgridarray2d.UseVisualStyleBackColor = true;
            this.rbdownload.AutoSize = true;
            this.rbdownload.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.rbdownload.Location = new Point(0x111, 0x37);
            this.rbdownload.Name = "rbdownload";
            this.rbdownload.Size = new Size(0x5c, 0x19);
            this.rbdownload.TabIndex = 2;
            this.rbdownload.TabStop = true;
            this.rbdownload.Text = "下载文件";
            this.rbdownload.UseVisualStyleBackColor = true;
            this.rbsaveasfile.AutoSize = true;
            this.rbsaveasfile.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.rbsaveasfile.Location = new Point(0x111, 80);
            this.rbsaveasfile.Name = "rbsaveasfile";
            this.rbsaveasfile.Size = new Size(0x6c, 0x19);
            this.rbsaveasfile.TabIndex = 2;
            this.rbsaveasfile.TabStop = true;
            this.rbsaveasfile.Text = "保存为文件";
            this.rbsaveasfile.UseVisualStyleBackColor = true;
            this.rbarray2d.AutoSize = true;
            this.rbarray2d.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.rbarray2d.Location = new Point(0x111, 5);
            this.rbarray2d.Name = "rbarray2d";
            this.rbarray2d.Size = new Size(0x5c, 0x19);
            this.rbarray2d.TabIndex = 1;
            this.rbarray2d.TabStop = true;
            this.rbarray2d.Text = "表格显示";
            this.rbarray2d.UseVisualStyleBackColor = true;
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer2.Size = new Size(0x3f0, 0x268);
            this.splitContainer2.SplitterDistance = 0x1a2;
            this.splitContainer2.TabIndex = 6;
            this.richTextBox1.Dock = DockStyle.Fill;
            this.richTextBox1.Location = new Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new Size(0x3f0, 0xc2);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3f0, 730);
            base.Controls.Add(this.splitContainer1);
            base.Name = "QCmss";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "CIMISS数据查询";
            base.Load += new EventHandler(this.QCmss_Load);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.EndInit();
            this.splitContainer2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void load()
        {
            new LoadForm(this).ShowDialog();
        }

        private void QCmss_Load(object sender, EventArgs e)
        {
            BindingSource source = new BindingSource {
                DataSource = this.list
            };
            this.comboBox1.DataSource = source;
            this.comboBox1.SelectedIndexChanged -= new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.DisplayMember = "Key";
            this.comboBox1.ValueMember = "Value";
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
        }

        private DataTable R2dToDatatable(RetArray2D r2d)
        {
            DataTable table = new DataTable();
            if (r2d.data.GetLength(0) > 0)
            {
                string[] strArray = r2d.request.requestElems.Split(new char[] { ',' });
                foreach (string str in strArray)
                {
                    DataColumn column = new DataColumn(str, typeof(string));
                    table.Columns.Add(column);
                }
                foreach (string[] strArray2 in r2d.data)
                {
                    DataRow row = table.NewRow();
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        row[i] = strArray2[i];
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        private void updatedatatable(DataTable dt, List<Parainfo> listparas)
        {
            dt.Rows.Clear();
            if ((listparas != null) && (listparas.Count > 0))
            {
                foreach (Parainfo parainfo in listparas)
                {
                    if (parainfo.id != "dataCode")
                    {
                        DataRow row = dt.NewRow();
                        row["id"] = parainfo.id;
                        row["name"] = parainfo.name;
                        row["type"] = parainfo.type;
                        if ((parainfo.id == "limitCnt") && (this.user == ""))
                        {
                            row["value"] = this.limitedrows.ToString();
                        }
                        dt.Rows.Add(row);
                    }
                    if ((parainfo.attId != null) && (parainfo.attId != ""))
                    {
                        string attId = parainfo.attId;
                        if (attId != null)
                        {
                            if (attId != "staEle")
                            {
                                if (attId == "file")
                                {
                                    goto Label_0195;
                                }
                                if (attId == "nwpEleGrid")
                                {
                                    goto Label_01D8;
                                }
                            }
                            else
                            {
                                this.rbarray2d.Enabled = true;
                                this.rbsaveasfile.Enabled = true;
                                this.rbgridarray2d.Enabled = false;
                                this.rbdownload.Enabled = false;
                                this.rbarray2d.Checked = true;
                            }
                        }
                    }
                    continue;
                Label_0195:
                    this.rbarray2d.Enabled = false;
                    this.rbsaveasfile.Enabled = false;
                    this.rbgridarray2d.Enabled = false;
                    this.rbdownload.Enabled = true;
                    this.rbdownload.Checked = true;
                    continue;
                Label_01D8:
                    this.rbarray2d.Enabled = false;
                    this.rbsaveasfile.Enabled = false;
                    this.rbgridarray2d.Enabled = true;
                    this.rbdownload.Enabled = true;
                    this.rbdownload.Checked = true;
                }
            }
        }

        public int Limitedrows
        {
            get => 
                this.limitedrows;
            set
            {
                this.limitedrows = value;
            }
        }
    }
}

