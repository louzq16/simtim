namespace simTim
{
    partial class chart
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
            this.button_send = new System.Windows.Forms.Button();
            this.richTextBox_chart = new System.Windows.Forms.RichTextBox();
            this.button_leavechart = new System.Windows.Forms.Button();
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.label_hint = new System.Windows.Forms.Label();
            this.button_sendfile = new System.Windows.Forms.Button();
            this.progressBar_file = new System.Windows.Forms.ProgressBar();
            this.progressBar_recvfile = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(509, 282);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(85, 29);
            this.button_send.TabIndex = 0;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // richTextBox_chart
            // 
            this.richTextBox_chart.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBox_chart.Location = new System.Drawing.Point(12, 2);
            this.richTextBox_chart.Name = "richTextBox_chart";
            this.richTextBox_chart.ReadOnly = true;
            this.richTextBox_chart.Size = new System.Drawing.Size(582, 274);
            this.richTextBox_chart.TabIndex = 1;
            this.richTextBox_chart.Text = "";
            // 
            // button_leavechart
            // 
            this.button_leavechart.Location = new System.Drawing.Point(657, 352);
            this.button_leavechart.Name = "button_leavechart";
            this.button_leavechart.Size = new System.Drawing.Size(88, 33);
            this.button_leavechart.TabIndex = 2;
            this.button_leavechart.Text = "离开会话";
            this.button_leavechart.UseVisualStyleBackColor = true;
            this.button_leavechart.Click += new System.EventHandler(this.button_leavechart_Click);
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(12, 314);
            this.textBox_input.Multiline = true;
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.Size = new System.Drawing.Size(582, 78);
            this.textBox_input.TabIndex = 3;
            // 
            // label_hint
            // 
            this.label_hint.AutoSize = true;
            this.label_hint.Location = new System.Drawing.Point(670, 317);
            this.label_hint.Name = "label_hint";
            this.label_hint.Size = new System.Drawing.Size(55, 15);
            this.label_hint.TabIndex = 4;
            this.label_hint.Text = "label1";
            // 
            // button_sendfile
            // 
            this.button_sendfile.Location = new System.Drawing.Point(404, 282);
            this.button_sendfile.Name = "button_sendfile";
            this.button_sendfile.Size = new System.Drawing.Size(82, 29);
            this.button_sendfile.TabIndex = 5;
            this.button_sendfile.Text = "发送文件";
            this.button_sendfile.UseVisualStyleBackColor = true;
            this.button_sendfile.Click += new System.EventHandler(this.button_sendfile_Click);
            // 
            // progressBar_file
            // 
            this.progressBar_file.Location = new System.Drawing.Point(616, 40);
            this.progressBar_file.Name = "progressBar_file";
            this.progressBar_file.Size = new System.Drawing.Size(172, 23);
            this.progressBar_file.TabIndex = 6;
            // 
            // progressBar_recvfile
            // 
            this.progressBar_recvfile.Location = new System.Drawing.Point(616, 110);
            this.progressBar_recvfile.Name = "progressBar_recvfile";
            this.progressBar_recvfile.Size = new System.Drawing.Size(172, 23);
            this.progressBar_recvfile.TabIndex = 7;
            // 
            // chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 397);
            this.Controls.Add(this.progressBar_recvfile);
            this.Controls.Add(this.progressBar_file);
            this.Controls.Add(this.button_sendfile);
            this.Controls.Add(this.label_hint);
            this.Controls.Add(this.textBox_input);
            this.Controls.Add(this.button_leavechart);
            this.Controls.Add(this.richTextBox_chart);
            this.Controls.Add(this.button_send);
            this.Name = "chart";
            this.Text = "chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.chart_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.RichTextBox richTextBox_chart;
        private System.Windows.Forms.Button button_leavechart;
        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.Label label_hint;
        private System.Windows.Forms.Button button_sendfile;
        private System.Windows.Forms.ProgressBar progressBar_file;
        private System.Windows.Forms.ProgressBar progressBar_recvfile;
    }
}