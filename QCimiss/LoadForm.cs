namespace QCimiss
{
    using cma.cimiss;
    using QCimiss.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoadForm : Form
    {
        public SqlDataAdapter adapter;
        private bool APIright = false;
        public Button button1;
        private Button button2;
        public string cnn = "Data Source=10.104.129.84;Initial Catalog=HistoryData;Persist Security Info=True;User ID=yw;Password=yw123";
        public ComboBox comboBox1;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool loaded = false;
        private DateTime loadtime;
        private QCmss qc = null;
        public DataSet tbs;
        public DataTable tbuser = null;
        public TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;

        public LoadForm(QCmss qc)
        {
            this.InitializeComponent();
            this.qc = qc;
        }

        private int apitest()
        {
            Dictionary<string, string> param = new Dictionary<string, string> {
                { 
                    "dataCode",
                    "STA_INFO_SURF_CHN"
                },
                { 
                    "elements",
                    "Station_Name"
                },
                { 
                    "staIds",
                    "57494"
                }
            };
            RetArray2D arrayd = Cimiss.getArray2D("getStaInfoByStaId", param);
            if (arrayd.request.errorCode != 0)
            {
                MessageBox.Show("API账号密码有误:\r\n" + arrayd.request.errorMessage);
                return -1;
            }
            this.APIright = true;
            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.saveapi();
            if (this.APIright)
            {
                if (this.textBox1.Text == "")
                {
                    if ((this.comboBox1.SelectedItem != null) && (this.comboBox1.SelectedValue.ToString() == ""))
                    {
                        MessageBox.Show("您尚未注册，请先设置初始密码！");
                        new RegForm(this).ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("请输入密码！");
                    }
                }
                else if (this.textBox1.Text != this.comboBox1.SelectedValue.ToString())
                {
                    MessageBox.Show("密码不正确！");
                }
                else
                {
                    this.qc.user = this.comboBox1.Text;
                    this.qc.Text = this.qc.Text.Split(new char[] { ' ' })[0] + "  已登录";
                    this.loaded = true;
                    this.loadlog(this.comboBox1.Text);
                    this.qc.button2.Visible = false;
                    Settings.Default.lastuser = this.comboBox1.Text;
                    Settings.Default.Save();
                    base.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new RegForm(this).ShowDialog();
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
            this.comboBox1 = new ComboBox();
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.textBox2 = new TextBox();
            this.textBox3 = new TextBox();
            base.SuspendLayout();
            this.comboBox1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x61, 0x57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x7c, 0x1d);
            this.comboBox1.TabIndex = 0;
            this.textBox1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0x61, 130);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new Size(0x7c, 0x1d);
            this.textBox1.TabIndex = 1;
            this.button1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button1.Location = new Point(240, 0x5b);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x5b, 0x1d);
            this.button1.TabIndex = 2;
            this.button1.Text = "登陆";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button2.Location = new Point(240, 0x7c);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x5b, 0x1d);
            this.button2.TabIndex = 3;
            this.button2.Text = "密码设置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "API账户";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(12, 0x31);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "API密码";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(12, 0x5b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "登录用户";
            this.label4.AutoSize = true;
            this.label4.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.Location = new Point(12, 0x85);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "登录密码";
            this.textBox2.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox2.Location = new Point(0x61, 7);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x7c, 0x1a);
            this.textBox2.TabIndex = 8;
            this.textBox3.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox3.Location = new Point(0x61, 0x2f);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new Size(0x7c, 0x1a);
            this.textBox3.TabIndex = 9;
            base.AcceptButton = this.button1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x157, 0xa5);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.comboBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "LoadForm";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "登陆";
            base.FormClosing += new FormClosingEventHandler(this.LoadForm_FormClosing);
            base.Load += new EventHandler(this.LoadForm_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.APIright)
            {
                if (!this.loaded)
                {
                    MessageBox.Show("未登录，仅显示前" + this.qc.Limitedrows.ToString() + "行数据");
                    this.qc.Text = this.qc.Text.Split(new char[] { ' ' })[0] + "  未登录";
                    this.loadlog("");
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            this.textBox2.Text = Settings.Default.APIuser;
            this.textBox3.Text = Settings.Default.APIpassword;
            string cmdText = "SELECT [name],[phone],[password] FROM [HistoryData].[us_yw].[QCIMISSuserinfos]";
            SqlConnection connection = new SqlConnection(this.cnn);
            this.adapter = new SqlDataAdapter();
            this.adapter.SelectCommand = new SqlCommand(cmdText, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(this.adapter);
            try
            {
                this.adapter.UpdateCommand = builder.GetUpdateCommand();
            }
            catch (Exception)
            {
            }
            this.tbs = new DataSet();
            this.adapter.Fill(this.tbs);
            if (this.tbs.Tables.Count == 1)
            {
                this.tbuser = this.tbs.Tables[0];
            }
            this.comboBox1.DataSource = this.tbuser;
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.ValueMember = "password";
            for (int i = 0; i < this.comboBox1.Items.Count; i++)
            {
                DataRowView view = (DataRowView) this.comboBox1.Items[i];
                if (view["name"].ToString() == Settings.Default.lastuser)
                {
                    this.comboBox1.SelectedIndex = i;
                    this.textBox1.TabIndex = 0;
                    break;
                }
            }
        }

        private void loadlog(string user)
        {
            Computer computer = new Computer();
            string cpuID = computer.CpuID;
            string macAddress = computer.MacAddress;
            string loginUserName = computer.LoginUserName;
            string ipAddress = computer.IpAddress;
            string computerName = computer.ComputerName;
            this.loadtime = DateTime.Now;
            this.qc.loadtime = this.loadtime;
            string strinsert = "insert into [HistoryData].[us_yw].[QCIMISSLoadLog] values('" + this.loadtime.ToString() + "','" + user + "','" + ipAddress + "','" + macAddress + "','" + computerName + "','" + loginUserName + "','" + cpuID + "','" + Settings.Default.APIuser + "')";
            sql.insertsql(this.cnn, strinsert);
        }

        private void saveapi()
        {
            Settings.Default.APIuser = this.textBox2.Text;
            Settings.Default.APIpassword = this.textBox3.Text;
            Settings.Default.Save();
            this.apitest();
        }
    }
}

