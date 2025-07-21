namespace PKHeX.WinForms.Subforms
{
    partial class TeamGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamGenerator));
            grpOptions = new System.Windows.Forms.GroupBox();
            chkLimit = new System.Windows.Forms.CheckBox();
            cboGeneration = new System.Windows.Forms.ComboBox();
            cboStarter = new System.Windows.Forms.CheckedListBox();
            lblGeneration = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            chkBalanced = new System.Windows.Forms.CheckBox();
            chkMustEvolve = new System.Windows.Forms.CheckBox();
            chkLegendaries = new System.Windows.Forms.CheckBox();
            lblTeamSize = new System.Windows.Forms.Label();
            sldTeamSize = new System.Windows.Forms.TrackBar();
            lblTeamSizeValue = new System.Windows.Forms.Label();
            chkSecret = new System.Windows.Forms.CheckBox();
            chkEggs = new System.Windows.Forms.CheckBox();
            Generate = new System.Windows.Forms.Button();
            grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sldTeamSize).BeginInit();
            SuspendLayout();
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(chkLimit);
            grpOptions.Controls.Add(cboGeneration);
            grpOptions.Controls.Add(cboStarter);
            grpOptions.Controls.Add(lblGeneration);
            grpOptions.Controls.Add(label1);
            grpOptions.Controls.Add(chkBalanced);
            grpOptions.Controls.Add(chkMustEvolve);
            grpOptions.Controls.Add(chkLegendaries);
            grpOptions.Controls.Add(lblTeamSize);
            grpOptions.Controls.Add(sldTeamSize);
            grpOptions.Controls.Add(lblTeamSizeValue);
            grpOptions.Controls.Add(chkSecret);
            grpOptions.Controls.Add(chkEggs);
            grpOptions.Controls.Add(Generate);
            grpOptions.Location = new System.Drawing.Point(12, 12);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new System.Drawing.Size(420, 277);
            grpOptions.TabIndex = 0;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // chkLimit
            // 
            chkLimit.AutoSize = true;
            chkLimit.Location = new System.Drawing.Point(202, 154);
            chkLimit.Name = "chkLimit";
            chkLimit.Size = new System.Drawing.Size(58, 19);
            chkLimit.TabIndex = 13;
            chkLimit.Text = "Limit?";
            chkLimit.UseVisualStyleBackColor = true;
            chkLimit.CheckedChanged += chkLimit_CheckedChanged;
            // 
            // cboGeneration
            // 
            cboGeneration.FormattingEnabled = true;
            cboGeneration.Location = new System.Drawing.Point(85, 152);
            cboGeneration.Name = "cboGeneration";
            cboGeneration.Size = new System.Drawing.Size(111, 23);
            cboGeneration.TabIndex = 12;
            cboGeneration.SelectedIndexChanged += cboGeneration_SelectedIndexChanged;
            // 
            // cboStarter
            // 
            cboStarter.ColumnWidth = 120;
            cboStarter.FormattingEnabled = true;
            cboStarter.Location = new System.Drawing.Point(85, 60);
            cboStarter.MultiColumn = true;
            cboStarter.Name = "cboStarter";
            cboStarter.Size = new System.Drawing.Size(320, 76);
            cboStarter.TabIndex = 2;
            cboStarter.ItemCheck += cboStarter_ItemCheck;
            // 
            // lblGeneration
            // 
            lblGeneration.AutoSize = true;
            lblGeneration.Location = new System.Drawing.Point(5, 155);
            lblGeneration.Name = "lblGeneration";
            lblGeneration.Size = new System.Drawing.Size(65, 15);
            lblGeneration.TabIndex = 11;
            lblGeneration.Text = "Generation";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 60);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 15);
            label1.TabIndex = 10;
            label1.Text = "Starter";
            // 
            // chkBalanced
            // 
            chkBalanced.AutoSize = true;
            chkBalanced.Location = new System.Drawing.Point(10, 201);
            chkBalanced.Name = "chkBalanced";
            chkBalanced.Size = new System.Drawing.Size(79, 19);
            chkBalanced.TabIndex = 9;
            chkBalanced.Text = "Balanced?";
            chkBalanced.UseVisualStyleBackColor = true;
            // 
            // chkMustEvolve
            // 
            chkMustEvolve.AutoSize = true;
            chkMustEvolve.Location = new System.Drawing.Point(165, 201);
            chkMustEvolve.Name = "chkMustEvolve";
            chkMustEvolve.Size = new System.Drawing.Size(95, 19);
            chkMustEvolve.TabIndex = 8;
            chkMustEvolve.Text = "Must Evolve?";
            chkMustEvolve.UseVisualStyleBackColor = true;
            // 
            // chkLegendaries
            // 
            chkLegendaries.AutoSize = true;
            chkLegendaries.Location = new System.Drawing.Point(320, 201);
            chkLegendaries.Name = "chkLegendaries";
            chkLegendaries.Size = new System.Drawing.Size(94, 19);
            chkLegendaries.TabIndex = 6;
            chkLegendaries.Text = "Legendaries?";
            chkLegendaries.UseVisualStyleBackColor = true;
            // 
            // lblTeamSize
            // 
            lblTeamSize.AutoSize = true;
            lblTeamSize.Location = new System.Drawing.Point(6, 25);
            lblTeamSize.Name = "lblTeamSize";
            lblTeamSize.Size = new System.Drawing.Size(59, 15);
            lblTeamSize.TabIndex = 7;
            lblTeamSize.Text = "Team Size";
            // 
            // sldTeamSize
            // 
            sldTeamSize.Location = new System.Drawing.Point(85, 25);
            sldTeamSize.Maximum = 6;
            sldTeamSize.Minimum = 1;
            sldTeamSize.Name = "sldTeamSize";
            sldTeamSize.Size = new System.Drawing.Size(301, 45);
            sldTeamSize.TabIndex = 6;
            sldTeamSize.Value = 6;
            sldTeamSize.ValueChanged += sldTeamSize_ValueChanged;
            // 
            // lblTeamSizeValue
            // 
            lblTeamSizeValue.AutoSize = true;
            lblTeamSizeValue.Location = new System.Drawing.Point(392, 25);
            lblTeamSizeValue.Name = "lblTeamSizeValue";
            lblTeamSizeValue.Size = new System.Drawing.Size(13, 15);
            lblTeamSizeValue.TabIndex = 14;
            lblTeamSizeValue.Text = "6";
            // 
            // chkSecret
            // 
            chkSecret.AutoSize = true;
            chkSecret.Location = new System.Drawing.Point(10, 226);
            chkSecret.Name = "chkSecret";
            chkSecret.Size = new System.Drawing.Size(63, 19);
            chkSecret.TabIndex = 5;
            chkSecret.Text = "Secret?";
            chkSecret.UseVisualStyleBackColor = true;
            // 
            // chkEggs
            // 
            chkEggs.AutoSize = true;
            chkEggs.Location = new System.Drawing.Point(165, 226);
            chkEggs.Name = "chkEggs";
            chkEggs.Size = new System.Drawing.Size(69, 19);
            chkEggs.TabIndex = 4;
            chkEggs.Text = "In Eggs?";
            chkEggs.UseVisualStyleBackColor = true;
            // 
            // Generate
            // 
            Generate.Location = new System.Drawing.Point(165, 253);
            Generate.Name = "Generate";
            Generate.Size = new System.Drawing.Size(75, 23);
            Generate.TabIndex = 3;
            Generate.Text = "Generate";
            Generate.UseVisualStyleBackColor = true;
            Generate.Click += Generate_Click;
            // 
            // TeamGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(444, 301);
            Controls.Add(grpOptions);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TeamGenerator";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Team Generator";
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sldTeamSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.CheckBox chkEggs;
        private System.Windows.Forms.Label lblTeamSize;
        private System.Windows.Forms.TrackBar sldTeamSize;
        private System.Windows.Forms.Label lblTeamSizeValue;
        private System.Windows.Forms.CheckBox chkSecret;
        private System.Windows.Forms.CheckBox chkLegendaries;
        private System.Windows.Forms.CheckBox chkMustEvolve;
        private System.Windows.Forms.CheckBox chkBalanced;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox cboStarter;
        private System.Windows.Forms.ComboBox cboGeneration;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.CheckBox chkLimit;
    }
}
