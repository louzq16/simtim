namespace simTim
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_logout = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.button_check = new System.Windows.Forms.Button();
            this.textBox_friendid = new System.Windows.Forms.TextBox();
            this.label_friends = new System.Windows.Forms.Label();
            this.label_friIP = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label_welcome = new System.Windows.Forms.Label();
            this.dataGridView_one = new System.Windows.Forms.DataGridView();
            this.friendsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_chart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_one)).BeginInit();
            this.SuspendLayout();
            // 
            // button_logout
            // 
            this.button_logout.Location = new System.Drawing.Point(20, 369);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(90, 35);
            this.button_logout.TabIndex = 0;
            this.button_logout.Text = "注销";
            this.button_logout.UseVisualStyleBackColor = true;
            this.button_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // button_exit
            // 
            this.button_exit.Location = new System.Drawing.Point(141, 369);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(89, 35);
            this.button_exit.TabIndex = 1;
            this.button_exit.Text = "退出程序";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // button_check
            // 
            this.button_check.Location = new System.Drawing.Point(143, 158);
            this.button_check.Name = "button_check";
            this.button_check.Size = new System.Drawing.Size(87, 35);
            this.button_check.TabIndex = 3;
            this.button_check.Text = "查询好友";
            this.button_check.UseVisualStyleBackColor = true;
            this.button_check.Click += new System.EventHandler(this.button_check_Click);
            // 
            // textBox_friendid
            // 
            this.textBox_friendid.Location = new System.Drawing.Point(96, 65);
            this.textBox_friendid.Name = "textBox_friendid";
            this.textBox_friendid.Size = new System.Drawing.Size(134, 25);
            this.textBox_friendid.TabIndex = 4;
            // 
            // label_friends
            // 
            this.label_friends.AutoSize = true;
            this.label_friends.Location = new System.Drawing.Point(15, 69);
            this.label_friends.Name = "label_friends";
            this.label_friends.Size = new System.Drawing.Size(75, 15);
            this.label_friends.TabIndex = 5;
            this.label_friends.Text = "好友学号:";
            // 
            // label_friIP
            // 
            this.label_friIP.AutoSize = true;
            this.label_friIP.Location = new System.Drawing.Point(17, 117);
            this.label_friIP.Name = "label_friIP";
            this.label_friIP.Size = new System.Drawing.Size(68, 15);
            this.label_friIP.TabIndex = 6;
            this.label_friIP.Text = "好友ip：";
            // 
            // textBox_IP
            // 
            this.textBox_IP.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_IP.Location = new System.Drawing.Point(96, 114);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.ReadOnly = true;
            this.textBox_IP.Size = new System.Drawing.Size(134, 25);
            this.textBox_IP.TabIndex = 7;
            // 
            // label_welcome
            // 
            this.label_welcome.AutoSize = true;
            this.label_welcome.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_welcome.Location = new System.Drawing.Point(34, 239);
            this.label_welcome.Name = "label_welcome";
            this.label_welcome.Size = new System.Drawing.Size(76, 46);
            this.label_welcome.TabIndex = 8;
            this.label_welcome.Text = "欢迎您!\r\n尊敬的";
            // 
            // dataGridView_one
            // 
            this.dataGridView_one.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView_one.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_one.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.friendsID,
            this.state,
            this.IP});
            this.dataGridView_one.Location = new System.Drawing.Point(316, 12);
            this.dataGridView_one.MultiSelect = false;
            this.dataGridView_one.Name = "dataGridView_one";
            this.dataGridView_one.RowHeadersVisible = false;
            this.dataGridView_one.RowTemplate.Height = 27;
            this.dataGridView_one.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_one.Size = new System.Drawing.Size(324, 392);
            this.dataGridView_one.TabIndex = 9;
            // 
            // friendsID
            // 
            this.friendsID.HeaderText = "好友ID";
            this.friendsID.Name = "friendsID";
            this.friendsID.ReadOnly = true;
            this.friendsID.Width = 80;
            // 
            // state
            // 
            this.state.HeaderText = "状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.state.Width = 60;
            // 
            // IP
            // 
            this.IP.HeaderText = "好友IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            // 
            // button_chart
            // 
            this.button_chart.Location = new System.Drawing.Point(535, 420);
            this.button_chart.Name = "button_chart";
            this.button_chart.Size = new System.Drawing.Size(87, 35);
            this.button_chart.TabIndex = 10;
            this.button_chart.Text = "发起会话";
            this.button_chart.UseVisualStyleBackColor = true;
            this.button_chart.Click += new System.EventHandler(this.button_chart_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 498);
            this.Controls.Add(this.button_chart);
            this.Controls.Add(this.dataGridView_one);
            this.Controls.Add(this.label_welcome);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label_friIP);
            this.Controls.Add(this.label_friends);
            this.Controls.Add(this.textBox_friendid);
            this.Controls.Add(this.button_check);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_logout);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_one)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.Button button_check;
        private System.Windows.Forms.TextBox textBox_friendid;
        private System.Windows.Forms.Label label_friends;
        private System.Windows.Forms.Label label_friIP;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label_welcome;
        private System.Windows.Forms.DataGridView dataGridView_one;
        private System.Windows.Forms.DataGridViewTextBoxColumn friendsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.Button button_chart;
    }
}