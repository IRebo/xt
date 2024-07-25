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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonOK = new System.Windows.Forms.Button();
            textBoxURL = new System.Windows.Forms.TextBox();
            textBoxUser = new System.Windows.Forms.TextBox();
            textBoxPassword = new System.Windows.Forms.TextBox();
            textBoxLog = new System.Windows.Forms.TextBox();
            labelURL = new System.Windows.Forms.Label();
            labelUser = new System.Windows.Forms.Label();
            labelPassword = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            textBoxFrom = new System.Windows.Forms.TextBox();
            textBoxTo = new System.Windows.Forms.TextBox();
            labelFrom = new System.Windows.Forms.Label();
            labelTo = new System.Windows.Forms.Label();
            textBoxSleep = new System.Windows.Forms.TextBox();
            labelSleep = new System.Windows.Forms.Label();
            labelServer = new System.Windows.Forms.Label();
            textBoxServer = new System.Windows.Forms.TextBox();
            labelUpdateText = new System.Windows.Forms.Label();
            labelUpdate = new System.Windows.Forms.Label();
            buttonUpdate = new System.Windows.Forms.Button();
            labelDirectory = new System.Windows.Forms.Label();
            textBoxDirectory = new System.Windows.Forms.TextBox();
            buttonDirectory = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // buttonOK
            // 
            buttonOK.Location = new System.Drawing.Point(52, 413);
            buttonOK.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(255, 63);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "Start";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // textBoxURL
            // 
            textBoxURL.Location = new System.Drawing.Point(208, 23);
            textBoxURL.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxURL.Name = "textBoxURL";
            textBoxURL.ReadOnly = true;
            textBoxURL.Size = new System.Drawing.Size(289, 31);
            textBoxURL.TabIndex = 1;
            textBoxURL.Text = "https://xtrance.info";
            // 
            // textBoxUser
            // 
            textBoxUser.Location = new System.Drawing.Point(208, 73);
            textBoxUser.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new System.Drawing.Size(289, 31);
            textBoxUser.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new System.Drawing.Point(208, 116);
            textBoxPassword.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new System.Drawing.Size(289, 31);
            textBoxPassword.TabIndex = 3;
            // 
            // textBoxLog
            // 
            textBoxLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxLog.Location = new System.Drawing.Point(52, 527);
            textBoxLog.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxLog.Size = new System.Drawing.Size(1150, 635);
            textBoxLog.TabIndex = 4;
            // 
            // labelURL
            // 
            labelURL.AutoSize = true;
            labelURL.Location = new System.Drawing.Point(140, 29);
            labelURL.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelURL.Name = "labelURL";
            labelURL.Size = new System.Drawing.Size(52, 25);
            labelURL.TabIndex = 5;
            labelURL.Text = "URL :";
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Location = new System.Drawing.Point(98, 82);
            labelUser.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelUser.Name = "labelUser";
            labelUser.Size = new System.Drawing.Size(100, 25);
            labelUser.TabIndex = 6;
            labelUser.Text = "Username :";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new System.Drawing.Point(102, 122);
            labelPassword.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new System.Drawing.Size(96, 25);
            labelPassword.TabIndex = 7;
            labelPassword.Text = "Password :";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new System.Drawing.Point(335, 413);
            buttonCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(255, 63);
            buttonCancel.TabIndex = 8;
            buttonCancel.Text = "Stop";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // textBoxFrom
            // 
            textBoxFrom.Location = new System.Drawing.Point(208, 223);
            textBoxFrom.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxFrom.Name = "textBoxFrom";
            textBoxFrom.Size = new System.Drawing.Size(289, 31);
            textBoxFrom.TabIndex = 9;
            textBoxFrom.Text = "1";
            // 
            // textBoxTo
            // 
            textBoxTo.Location = new System.Drawing.Point(208, 273);
            textBoxTo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxTo.Name = "textBoxTo";
            textBoxTo.Size = new System.Drawing.Size(289, 31);
            textBoxTo.TabIndex = 10;
            textBoxTo.Text = "1";
            // 
            // labelFrom
            // 
            labelFrom.AutoSize = true;
            labelFrom.Location = new System.Drawing.Point(138, 229);
            labelFrom.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new System.Drawing.Size(63, 25);
            labelFrom.TabIndex = 11;
            labelFrom.Text = "From :";
            labelFrom.Click += labelFrom_Click;
            // 
            // labelTo
            // 
            labelTo.AutoSize = true;
            labelTo.Location = new System.Drawing.Point(155, 279);
            labelTo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelTo.Name = "labelTo";
            labelTo.Size = new System.Drawing.Size(39, 25);
            labelTo.TabIndex = 12;
            labelTo.Text = "To :";
            // 
            // textBoxSleep
            // 
            textBoxSleep.Location = new System.Drawing.Point(883, 23);
            textBoxSleep.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxSleep.Name = "textBoxSleep";
            textBoxSleep.Size = new System.Drawing.Size(289, 31);
            textBoxSleep.TabIndex = 13;
            textBoxSleep.Text = "1";
            // 
            // labelSleep
            // 
            labelSleep.AutoSize = true;
            labelSleep.Location = new System.Drawing.Point(688, 29);
            labelSleep.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelSleep.Name = "labelSleep";
            labelSleep.Size = new System.Drawing.Size(184, 25);
            labelSleep.TabIndex = 14;
            labelSleep.Text = "Sleep time (seconds) :";
            // 
            // labelServer
            // 
            labelServer.AutoSize = true;
            labelServer.Location = new System.Drawing.Point(777, 85);
            labelServer.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelServer.Name = "labelServer";
            labelServer.Size = new System.Drawing.Size(93, 25);
            labelServer.TabIndex = 16;
            labelServer.Text = "Server ID :";
            // 
            // textBoxServer
            // 
            textBoxServer.Location = new System.Drawing.Point(883, 79);
            textBoxServer.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxServer.Name = "textBoxServer";
            textBoxServer.Size = new System.Drawing.Size(289, 31);
            textBoxServer.TabIndex = 15;
            textBoxServer.Text = "14";
            // 
            // labelUpdateText
            // 
            labelUpdateText.AutoSize = true;
            labelUpdateText.Location = new System.Drawing.Point(688, 171);
            labelUpdateText.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelUpdateText.Name = "labelUpdateText";
            labelUpdateText.Size = new System.Drawing.Size(136, 25);
            labelUpdateText.TabIndex = 17;
            labelUpdateText.Text = "Update status : ";
            // 
            // labelUpdate
            // 
            labelUpdate.AutoSize = true;
            labelUpdate.Location = new System.Drawing.Point(688, 196);
            labelUpdate.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelUpdate.Name = "labelUpdate";
            labelUpdate.Size = new System.Drawing.Size(96, 25);
            labelUpdate.TabIndex = 18;
            labelUpdate.Text = "Checking...";
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new System.Drawing.Point(688, 241);
            buttonUpdate.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new System.Drawing.Size(255, 63);
            buttonUpdate.TabIndex = 19;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Visible = false;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // labelDirectory
            // 
            labelDirectory.AutoSize = true;
            labelDirectory.Location = new System.Drawing.Point(60, 165);
            labelDirectory.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelDirectory.Name = "labelDirectory";
            labelDirectory.Size = new System.Drawing.Size(138, 25);
            labelDirectory.TabIndex = 20;
            labelDirectory.Text = "Write directory :";
            // 
            // textBoxDirectory
            // 
            textBoxDirectory.Location = new System.Drawing.Point(208, 159);
            textBoxDirectory.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            textBoxDirectory.Name = "textBoxDirectory";
            textBoxDirectory.Size = new System.Drawing.Size(289, 31);
            textBoxDirectory.TabIndex = 21;
            // 
            // buttonDirectory
            // 
            buttonDirectory.Location = new System.Drawing.Point(507, 165);
            buttonDirectory.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonDirectory.Name = "buttonDirectory";
            buttonDirectory.Size = new System.Drawing.Size(37, 31);
            buttonDirectory.TabIndex = 22;
            buttonDirectory.Text = "...";
            buttonDirectory.UseVisualStyleBackColor = true;
            buttonDirectory.Click += buttonDirectory_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1229, 1200);
            Controls.Add(buttonDirectory);
            Controls.Add(textBoxDirectory);
            Controls.Add(labelDirectory);
            Controls.Add(buttonUpdate);
            Controls.Add(labelUpdate);
            Controls.Add(labelUpdateText);
            Controls.Add(labelServer);
            Controls.Add(textBoxServer);
            Controls.Add(labelSleep);
            Controls.Add(textBoxSleep);
            Controls.Add(labelTo);
            Controls.Add(labelFrom);
            Controls.Add(textBoxTo);
            Controls.Add(textBoxFrom);
            Controls.Add(buttonCancel);
            Controls.Add(labelPassword);
            Controls.Add(labelUser);
            Controls.Add(labelURL);
            Controls.Add(textBoxLog);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxUser);
            Controls.Add(textBoxURL);
            Controls.Add(buttonOK);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name = "Form1";
            Text = "Xtrance stuff";
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label labelUpdateText;
        private System.Windows.Forms.Label labelUpdate;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label labelDirectory;
        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonDirectory;
    }
}

