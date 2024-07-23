namespace xtrance
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelURL = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.textBoxSleep = new System.Windows.Forms.TextBox();
            this.labelSleep = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(31, 215);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(153, 33);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Start";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(125, 12);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.ReadOnly = true;
            this.textBoxURL.Size = new System.Drawing.Size(175, 20);
            this.textBoxURL.TabIndex = 1;
            this.textBoxURL.Text = "https://xtrance.info";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(125, 38);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(175, 20);
            this.textBoxUser.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(125, 64);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(175, 20);
            this.textBoxPassword.TabIndex = 3;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(31, 274);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(730, 370);
            this.textBoxLog.TabIndex = 4;
            // 
            // labelURL
            // 
            this.labelURL.AutoSize = true;
            this.labelURL.Location = new System.Drawing.Point(84, 15);
            this.labelURL.Name = "labelURL";
            this.labelURL.Size = new System.Drawing.Size(35, 13);
            this.labelURL.TabIndex = 5;
            this.labelURL.Text = "URL :";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(60, 41);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(61, 13);
            this.labelUser.TabIndex = 6;
            this.labelUser.Text = "Username :";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(60, 67);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(59, 13);
            this.labelPassword.TabIndex = 7;
            this.labelPassword.Text = "Password :";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(201, 215);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(153, 33);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Stop";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(125, 116);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(175, 20);
            this.textBoxFrom.TabIndex = 9;
            this.textBoxFrom.Text = "1";
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(125, 142);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(175, 20);
            this.textBoxTo.TabIndex = 10;
            this.textBoxTo.Text = "1";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(83, 119);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(36, 13);
            this.labelFrom.TabIndex = 11;
            this.labelFrom.Text = "From :";
            this.labelFrom.Click += new System.EventHandler(this.labelFrom_Click);
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(93, 145);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(26, 13);
            this.labelTo.TabIndex = 12;
            this.labelTo.Text = "To :";
            // 
            // textBoxSleep
            // 
            this.textBoxSleep.Location = new System.Drawing.Point(530, 12);
            this.textBoxSleep.Name = "textBoxSleep";
            this.textBoxSleep.Size = new System.Drawing.Size(175, 20);
            this.textBoxSleep.TabIndex = 13;
            this.textBoxSleep.Text = "1";
            // 
            // labelSleep
            // 
            this.labelSleep.AutoSize = true;
            this.labelSleep.Location = new System.Drawing.Point(413, 15);
            this.labelSleep.Name = "labelSleep";
            this.labelSleep.Size = new System.Drawing.Size(111, 13);
            this.labelSleep.TabIndex = 14;
            this.labelSleep.Text = "Sleep time (seconds) :";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(466, 44);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(58, 13);
            this.labelServer.TabIndex = 16;
            this.labelServer.Text = "Server ID :";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(530, 41);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(175, 20);
            this.textBoxServer.TabIndex = 15;
            this.textBoxServer.Text = "14";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 662);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.labelSleep);
            this.Controls.Add(this.textBoxSleep);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.labelURL);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonOK);
            this.Name = "Form1";
            this.Text = "Xtrance stuff";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.TextBox textBoxSleep;
        private System.Windows.Forms.Label labelSleep;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.TextBox textBoxServer;
    }
}

