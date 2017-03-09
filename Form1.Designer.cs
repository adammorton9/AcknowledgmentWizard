namespace hrmg_ackowledgements
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Success = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menufile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuquit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuoptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menufont = new System.Windows.Forms.ToolStripMenuItem();
            this.menupreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.menuhelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuabout = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmreButton = new System.Windows.Forms.RadioButton();
            this.hrmgButton = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(167, 125);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(365, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path of Source File:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(443, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(167, 170);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(270, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Directory to Save:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(79, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(402, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "HRMG Acknowledgment Wizard";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(142, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Drag and Drop Source File to Import";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(419, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 42);
            this.button2.TabIndex = 7;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(164, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 15);
            this.label5.TabIndex = 8;
            // 
            // Success
            // 
            this.Success.AutoSize = true;
            this.Success.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Success.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Success.Location = new System.Drawing.Point(296, 228);
            this.Success.Name = "Success";
            this.Success.Size = new System.Drawing.Size(0, 16);
            this.Success.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menufile,
            this.menuoptions,
            this.menuhelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(568, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menufile
            // 
            this.menufile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuquit});
            this.menufile.Name = "menufile";
            this.menufile.Size = new System.Drawing.Size(37, 20);
            this.menufile.Text = "File";
            // 
            // menuquit
            // 
            this.menuquit.Name = "menuquit";
            this.menuquit.Size = new System.Drawing.Size(152, 22);
            this.menuquit.Text = "Quit";
            this.menuquit.Click += new System.EventHandler(this.menuquit_Click);
            // 
            // menuoptions
            // 
            this.menuoptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menufont,
            this.menupreferences});
            this.menuoptions.Name = "menuoptions";
            this.menuoptions.Size = new System.Drawing.Size(61, 20);
            this.menuoptions.Text = "Options";
            // 
            // menufont
            // 
            this.menufont.Name = "menufont";
            this.menufont.Size = new System.Drawing.Size(159, 22);
            this.menufont.Text = "Font";
            this.menufont.Click += new System.EventHandler(this.menufont_Click);
            // 
            // menupreferences
            // 
            this.menupreferences.Name = "menupreferences";
            this.menupreferences.Size = new System.Drawing.Size(159, 22);
            this.menupreferences.Text = "PDF Preferences";
            this.menupreferences.Click += new System.EventHandler(this.menupreferences_Click);
            // 
            // menuhelp
            // 
            this.menuhelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuabout});
            this.menuhelp.Name = "menuhelp";
            this.menuhelp.Size = new System.Drawing.Size(44, 20);
            this.menuhelp.Text = "Help";
            // 
            // menuabout
            // 
            this.menuabout.Name = "menuabout";
            this.menuabout.Size = new System.Drawing.Size(152, 22);
            this.menuabout.Text = "About";
            this.menuabout.Click += new System.EventHandler(this.menuabout_Click);
            // 
            // fontDialog1
            // 
            this.fontDialog1.AllowVerticalFonts = false;
            this.fontDialog1.FontMustExist = true;
            this.fontDialog1.ScriptsOnly = true;
            this.fontDialog1.ShowApply = true;
            this.fontDialog1.ShowEffects = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 224);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(281, 23);
            this.progressBar1.TabIndex = 11;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(446, 224);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 12;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmreButton);
            this.groupBox1.Controls.Add(this.hrmgButton);
            this.groupBox1.Location = new System.Drawing.Point(158, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 32);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // cmreButton
            // 
            this.cmreButton.AutoSize = true;
            this.cmreButton.Location = new System.Drawing.Point(131, 9);
            this.cmreButton.Name = "cmreButton";
            this.cmreButton.Size = new System.Drawing.Size(56, 17);
            this.cmreButton.TabIndex = 1;
            this.cmreButton.Text = "CMRE";
            this.cmreButton.UseVisualStyleBackColor = true;
            this.cmreButton.CheckedChanged += new System.EventHandler(this.cmreButton_CheckedChanged);
            // 
            // hrmgButton
            // 
            this.hrmgButton.AutoSize = true;
            this.hrmgButton.BackColor = System.Drawing.SystemColors.Control;
            this.hrmgButton.Checked = true;
            this.hrmgButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.hrmgButton.Location = new System.Drawing.Point(40, 9);
            this.hrmgButton.Name = "hrmgButton";
            this.hrmgButton.Size = new System.Drawing.Size(58, 17);
            this.hrmgButton.TabIndex = 0;
            this.hrmgButton.TabStop = true;
            this.hrmgButton.Text = "HRMG";
            this.hrmgButton.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 273);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Success);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Acknowledgment Wizard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Success;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menufile;
        private System.Windows.Forms.ToolStripMenuItem menuquit;
        private System.Windows.Forms.ToolStripMenuItem menuoptions;
        private System.Windows.Forms.ToolStripMenuItem menupreferences;
        private System.Windows.Forms.ToolStripMenuItem menuhelp;
        private System.Windows.Forms.ToolStripMenuItem menuabout;
        private System.Windows.Forms.ToolStripMenuItem menufont;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton cmreButton;
        private System.Windows.Forms.RadioButton hrmgButton;
    }
}

