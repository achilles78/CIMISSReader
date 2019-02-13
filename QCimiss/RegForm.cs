namespace QCimiss
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class RegForm : Form
    {
        private Button button1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label lbphorpss;
        public LoadForm ldfrm;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;

        public RegForm(LoadForm ldfrm)
        {
            this.InitializeComponent();
            this.ldfrm = ldfrm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem != null)
            {
                if (this.textBox1.Text.Trim() == this.comboBox1.SelectedValue.ToString())
                {
                    if (this.textBox2.Text == this.textBox3.Text)
                    {
                        if (this.textBox2.Text == "")
                        {
                            MessageBox.Show("密码不能为空");
                        }
                        else
                        {
                            try
                            {
                                this.ldfrm.tbuser.Rows[this.comboBox1.SelectedIndex]["password"] = this.textBox2.Text;
                                this.ldfrm.adapter.Update(this.ldfrm.tbs);
                                MessageBox.Show("设置密码成功");
                                this.ldfrm.comboBox1.SelectedIndex = this.comboBox1.SelectedIndex;
                                this.ldfrm.textBox1.Text = this.textBox2.Text;
                                this.ldfrm.button1.PerformClick();
                                base.Close();
                            }
                            catch
                            {
                                MessageBox.Show("未知错误，请重试一遍");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("输入的2次密码不相同");
                    }
                }
                else if (this.textBox1.PasswordChar == '*')
                {
                    MessageBox.Show("旧不正确，请重试");
                }
                else
                {
                    MessageBox.Show("手机号码不正确，请确认姓名和手机号");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem != null)
            {
                DataRowView selectedItem = (DataRowView) this.comboBox1.SelectedItem;
                if (selectedItem.Row["password"].ToString() != "")
                {
                    this.lbphorpss.Text = "输入您的旧密码";
                    this.textBox1.PasswordChar = '*';
                    this.comboBox1.ValueMember = "password";
                }
                else
                {
                    this.lbphorpss.Text = "输入您的手机号";
                    this.textBox1.PasswordChar = '\0';
                    this.comboBox1.ValueMember = "phone";
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
            this.comboBox1 = new ComboBox();
            this.label1 = new Label();
            this.lbphorpss = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.textBox3 = new TextBox();
            this.button1 = new Button();
            base.SuspendLayout();
            this.comboBox1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x80, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 0x1d);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x6a, 0x15);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择您的姓名";
            this.lbphorpss.AutoSize = true;
            this.lbphorpss.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lbphorpss.Location = new Point(0, 0x26);
            this.lbphorpss.Name = "lbphorpss";
            this.lbphorpss.Size = new Size(0x7a, 0x15);
            this.lbphorpss.TabIndex = 2;
            this.lbphorpss.Text = "输入您的手机号";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0, 0x47);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4a, 0x15);
            this.label3.TabIndex = 3;
            this.label3.Text = "输入密码";
            this.label4.AutoSize = true;
            this.label4.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label4.Location = new Point(0, 0x68);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x6a, 0x15);
            this.label4.TabIndex = 4;
            this.label4.Text = "再次输入密码";
            this.textBox1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0x80, 0x23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x79, 0x1d);
            this.textBox1.TabIndex = 5;
            this.textBox2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox2.Location = new Point(0x80, 0x44);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new Size(0x79, 0x1d);
            this.textBox2.TabIndex = 6;
            this.textBox3.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox3.Location = new Point(0x80, 0x65);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new Size(0x79, 0x1d);
            this.textBox3.TabIndex = 7;
            this.button1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button1.Location = new Point(0xb0, 0x86);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x49, 0x23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xfc, 0xab);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lbphorpss);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.comboBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "RegForm";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "设置密码";
            base.Load += new EventHandler(this.RegForm_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void RegForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.DisplayMember = "name";
            this.comboBox1.DataSource = this.ldfrm.tbuser;
            this.comboBox1.SelectedIndex = this.ldfrm.comboBox1.SelectedIndex;
        }
    }
}

