namespace OOPNET_Project_Assignment
{
    partial class Application
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
            this.btnSettings = new System.Windows.Forms.Button();
            this.cbTeam = new System.Windows.Forms.ComboBox();
            this.lblChooseNationalTeam = new System.Windows.Forms.Label();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.lvFavouritePlayers = new System.Windows.Forms.ListView();
            this.dgvRankingListPlayers = new System.Windows.Forms.DataGridView();
            this.dgvRankingListMatch = new System.Windows.Forms.DataGridView();
            this.btnCreateRListOfPlayers = new System.Windows.Forms.Button();
            this.btnCreateRListOfMatches = new System.Windows.Forms.Button();
            this.btnPrintRankingList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRankingListPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRankingListMatch)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(1258, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(121, 33);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Change settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // cbTeam
            // 
            this.cbTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeam.FormattingEnabled = true;
            this.cbTeam.Location = new System.Drawing.Point(297, 11);
            this.cbTeam.Name = "cbTeam";
            this.cbTeam.Size = new System.Drawing.Size(154, 21);
            this.cbTeam.TabIndex = 1;
            this.cbTeam.SelectedIndexChanged += new System.EventHandler(this.cbTeam_SelectedIndexChanged);
            // 
            // lblChooseNationalTeam
            // 
            this.lblChooseNationalTeam.AutoSize = true;
            this.lblChooseNationalTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChooseNationalTeam.Location = new System.Drawing.Point(12, 11);
            this.lblChooseNationalTeam.Name = "lblChooseNationalTeam";
            this.lblChooseNationalTeam.Size = new System.Drawing.Size(267, 20);
            this.lblChooseNationalTeam.TabIndex = 2;
            this.lblChooseNationalTeam.Text = "Choose your favourite national team:";
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPlayers.Location = new System.Drawing.Point(1348, 218);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(31, 16);
            this.lblPlayers.TabIndex = 5;
            this.lblPlayers.Text = "1 / 3";
            // 
            // lvPlayers
            // 
            this.lvPlayers.AllowDrop = true;
            this.lvPlayers.HideSelection = false;
            this.lvPlayers.Location = new System.Drawing.Point(12, 67);
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(662, 759);
            this.lvPlayers.TabIndex = 6;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            
            // 
            // lvFavouritePlayers
            // 
            this.lvFavouritePlayers.AllowDrop = true;
            this.lvFavouritePlayers.HideSelection = false;
            this.lvFavouritePlayers.Location = new System.Drawing.Point(717, 67);
            this.lvFavouritePlayers.Name = "lvFavouritePlayers";
            this.lvFavouritePlayers.Size = new System.Drawing.Size(661, 148);
            this.lvFavouritePlayers.TabIndex = 7;
            this.lvFavouritePlayers.UseCompatibleStateImageBehavior = false;
            // 
            // dgvRankingListPlayers
            // 
            this.dgvRankingListPlayers.AllowUserToAddRows = false;
            this.dgvRankingListPlayers.AllowUserToDeleteRows = false;
            this.dgvRankingListPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRankingListPlayers.Location = new System.Drawing.Point(818, 237);
            this.dgvRankingListPlayers.MultiSelect = false;
            this.dgvRankingListPlayers.Name = "dgvRankingListPlayers";
            this.dgvRankingListPlayers.ReadOnly = true;
            this.dgvRankingListPlayers.RowTemplate.Height = 50;
            this.dgvRankingListPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRankingListPlayers.Size = new System.Drawing.Size(560, 253);
            this.dgvRankingListPlayers.TabIndex = 8;
            // 
            // dgvRankingListMatch
            // 
            this.dgvRankingListMatch.AllowUserToAddRows = false;
            this.dgvRankingListMatch.AllowUserToDeleteRows = false;
            this.dgvRankingListMatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRankingListMatch.Location = new System.Drawing.Point(818, 540);
            this.dgvRankingListMatch.MultiSelect = false;
            this.dgvRankingListMatch.Name = "dgvRankingListMatch";
            this.dgvRankingListMatch.ReadOnly = true;
            this.dgvRankingListMatch.RowTemplate.Height = 50;
            this.dgvRankingListMatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRankingListMatch.Size = new System.Drawing.Size(560, 253);
            this.dgvRankingListMatch.TabIndex = 9;
            // 
            // btnCreateRListOfPlayers
            // 
            this.btnCreateRListOfPlayers.Location = new System.Drawing.Point(1203, 496);
            this.btnCreateRListOfPlayers.Name = "btnCreateRListOfPlayers";
            this.btnCreateRListOfPlayers.Size = new System.Drawing.Size(175, 38);
            this.btnCreateRListOfPlayers.TabIndex = 10;
            this.btnCreateRListOfPlayers.Text = "Create ranking list of players";
            this.btnCreateRListOfPlayers.UseVisualStyleBackColor = true;
            this.btnCreateRListOfPlayers.Click += new System.EventHandler(this.btnCreateRListOfPlayers_Click);
            // 
            // btnCreateRListOfMatches
            // 
            this.btnCreateRListOfMatches.Location = new System.Drawing.Point(1203, 800);
            this.btnCreateRListOfMatches.Name = "btnCreateRListOfMatches";
            this.btnCreateRListOfMatches.Size = new System.Drawing.Size(176, 38);
            this.btnCreateRListOfMatches.TabIndex = 11;
            this.btnCreateRListOfMatches.Text = "Create ranking list of matches";
            this.btnCreateRListOfMatches.UseVisualStyleBackColor = true;
            this.btnCreateRListOfMatches.Click += new System.EventHandler(this.btnCreateRListOfMatches_Click);
            // 
            // btnPrintRankingList
            // 
            this.btnPrintRankingList.Enabled = false;
            this.btnPrintRankingList.Location = new System.Drawing.Point(1113, 6);
            this.btnPrintRankingList.Name = "btnPrintRankingList";
            this.btnPrintRankingList.Size = new System.Drawing.Size(139, 33);
            this.btnPrintRankingList.TabIndex = 12;
            this.btnPrintRankingList.Text = "Print the ranking lists";
            this.btnPrintRankingList.UseVisualStyleBackColor = true;
            this.btnPrintRankingList.Click += new System.EventHandler(this.btnPrintRankingList_Click);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 850);
            this.Controls.Add(this.btnPrintRankingList);
            this.Controls.Add(this.btnCreateRListOfMatches);
            this.Controls.Add(this.btnCreateRListOfPlayers);
            this.Controls.Add(this.dgvRankingListMatch);
            this.Controls.Add(this.dgvRankingListPlayers);
            this.Controls.Add(this.lvFavouritePlayers);
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.lblChooseNationalTeam);
            this.Controls.Add(this.cbTeam);
            this.Controls.Add(this.btnSettings);
            this.Name = "Application";
            this.Text = "Application";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRankingListPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRankingListMatch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ComboBox cbTeam;
        private System.Windows.Forms.Label lblChooseNationalTeam;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.ListView lvPlayers;
        private System.Windows.Forms.ListView lvFavouritePlayers;
        private System.Windows.Forms.DataGridView dgvRankingListPlayers;
        private System.Windows.Forms.DataGridView dgvRankingListMatch;
        private System.Windows.Forms.Button btnCreateRListOfPlayers;
        private System.Windows.Forms.Button btnCreateRListOfMatches;
        private System.Windows.Forms.Button btnPrintRankingList;
    }
}