namespace RFSync
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.bStop = new System.Windows.Forms.Button();
            this.cConsole = new System.Windows.Forms.Label();
            this.cTimer = new System.Windows.Forms.Timer(this.components);
            this.cData = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.bGet = new System.Windows.Forms.Button();
            this.bSet = new System.Windows.Forms.Button();
            this.tOffset = new System.Windows.Forms.TextBox();
            this.tData = new System.Windows.Forms.TextBox();
            this.tSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tRun = new System.Windows.Forms.TextBox();
            this.bRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bStop
            // 
            this.bStop.Location = new System.Drawing.Point(63, 402);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(43, 23);
            this.bStop.TabIndex = 0;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // cConsole
            // 
            this.cConsole.BackColor = System.Drawing.Color.Black;
            this.cConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cConsole.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cConsole.Location = new System.Drawing.Point(15, 67);
            this.cConsole.Name = "cConsole";
            this.cConsole.Size = new System.Drawing.Size(623, 317);
            this.cConsole.TabIndex = 1;
            this.cConsole.Text = "label1";
            this.cConsole.Click += new System.EventHandler(this.cConsole_Click);
            // 
            // cTimer
            // 
            this.cTimer.Enabled = true;
            this.cTimer.Tick += new System.EventHandler(this.cTimer_Tick);
            // 
            // cData
            // 
            this.cData.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cData.Location = new System.Drawing.Point(15, 24);
            this.cData.Name = "cData";
            this.cData.Size = new System.Drawing.Size(623, 21);
            this.cData.TabIndex = 2;
            this.cData.Text = "0000000000000000000000000000000000000000000000000000000000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Console";
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(15, 402);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(42, 23);
            this.bStart.TabIndex = 7;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bGet
            // 
            this.bGet.Location = new System.Drawing.Point(402, 401);
            this.bGet.Name = "bGet";
            this.bGet.Size = new System.Drawing.Size(51, 23);
            this.bGet.TabIndex = 8;
            this.bGet.Text = "Get";
            this.bGet.UseVisualStyleBackColor = true;
            this.bGet.Click += new System.EventHandler(this.bGet_Click);
            // 
            // bSet
            // 
            this.bSet.Location = new System.Drawing.Point(334, 402);
            this.bSet.Name = "bSet";
            this.bSet.Size = new System.Drawing.Size(40, 23);
            this.bSet.TabIndex = 10;
            this.bSet.Text = "Set";
            this.bSet.UseVisualStyleBackColor = true;
            this.bSet.Click += new System.EventHandler(this.bSet_Click);
            // 
            // tOffset
            // 
            this.tOffset.Location = new System.Drawing.Point(139, 404);
            this.tOffset.Name = "tOffset";
            this.tOffset.Size = new System.Drawing.Size(32, 20);
            this.tOffset.TabIndex = 12;
            // 
            // tData
            // 
            this.tData.Location = new System.Drawing.Point(228, 404);
            this.tData.Name = "tData";
            this.tData.Size = new System.Drawing.Size(100, 20);
            this.tData.TabIndex = 13;
            // 
            // tSize
            // 
            this.tSize.Location = new System.Drawing.Point(180, 404);
            this.tSize.Name = "tSize";
            this.tSize.Size = new System.Drawing.Size(39, 20);
            this.tSize.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(136, 388);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(177, 388);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(228, 388);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Data";
            // 
            // tRun
            // 
            this.tRun.Location = new System.Drawing.Point(473, 404);
            this.tRun.Name = "tRun";
            this.tRun.Size = new System.Drawing.Size(100, 20);
            this.tRun.TabIndex = 18;
            // 
            // bRun
            // 
            this.bRun.Location = new System.Drawing.Point(579, 404);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(59, 23);
            this.bRun.TabIndex = 19;
            this.bRun.Text = "Run";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 444);
            this.Controls.Add(this.bRun);
            this.Controls.Add(this.tRun);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tSize);
            this.Controls.Add(this.tData);
            this.Controls.Add(this.tOffset);
            this.Controls.Add(this.bSet);
            this.Controls.Add(this.bGet);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cData);
            this.Controls.Add(this.cConsole);
            this.Controls.Add(this.bStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "RFSync";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Label cConsole;
        private System.Windows.Forms.Timer cTimer;
        private System.Windows.Forms.Label cData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bGet;
        private System.Windows.Forms.Button bSet;
        private System.Windows.Forms.TextBox tOffset;
        private System.Windows.Forms.TextBox tData;
        private System.Windows.Forms.TextBox tSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tRun;
        private System.Windows.Forms.Button bRun;
    }
}

