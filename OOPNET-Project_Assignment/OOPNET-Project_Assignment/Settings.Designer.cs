namespace OOPNET_Project_Assignment
{
    partial class Settings
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
            this.lblChooseGender = new System.Windows.Forms.Label();
            this.lblChooseLanguage = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.cbReadFromFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblChooseGender
            // 
            this.lblChooseGender.AutoSize = true;
            this.lblChooseGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChooseGender.Location = new System.Drawing.Point(16, 35);
            this.lblChooseGender.Name = "lblChooseGender";
            this.lblChooseGender.Size = new System.Drawing.Size(147, 24);
            this.lblChooseGender.TabIndex = 0;
            this.lblChooseGender.Text = "Choose gender:";
            // 
            // lblChooseLanguage
            // 
            this.lblChooseLanguage.AutoSize = true;
            this.lblChooseLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChooseLanguage.Location = new System.Drawing.Point(16, 79);
            this.lblChooseLanguage.Name = "lblChooseLanguage";
            this.lblChooseLanguage.Size = new System.Drawing.Size(165, 24);
            this.lblChooseLanguage.TabIndex = 1;
            this.lblChooseLanguage.Text = "Choose language:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSubmit.Location = new System.Drawing.Point(132, 161);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(84, 28);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cbGender
            // 
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cbGender.Location = new System.Drawing.Point(206, 40);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(121, 21);
            this.cbGender.TabIndex = 0;
            
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Items.AddRange(new object[] {
            "English",
            "Croatian"});
            this.cbLanguage.Location = new System.Drawing.Point(206, 82);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 0;
            
            // 
            // cbReadFromFile
            // 
            this.cbReadFromFile.AutoSize = true;
            this.cbReadFromFile.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbReadFromFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cbReadFromFile.Location = new System.Drawing.Point(20, 117);
            this.cbReadFromFile.Name = "cbReadFromFile";
            this.cbReadFromFile.Size = new System.Drawing.Size(144, 28);
            this.cbReadFromFile.TabIndex = 4;
            this.cbReadFromFile.Text = "Read from file";
            this.cbReadFromFile.UseVisualStyleBackColor = true;
            
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 201);
            this.Controls.Add(this.cbReadFromFile);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblChooseLanguage);
            this.Controls.Add(this.lblChooseGender);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChooseGender;
        private System.Windows.Forms.Label lblChooseLanguage;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cbGender;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.CheckBox cbReadFromFile;
    }
}

