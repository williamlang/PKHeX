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
            cboGeneration = new System.Windows.Forms.ComboBox();
            lblGeneration = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cboStarter = new System.Windows.Forms.CheckedListBox();
            chkBalanced = new System.Windows.Forms.CheckBox();
            chkMustEvolve = new System.Windows.Forms.CheckBox();
            chkLegendaries = new System.Windows.Forms.CheckBox();
            lblTeamSize = new System.Windows.Forms.Label();
            numTeamSize = new System.Windows.Forms.NumericUpDown();
            chkSecret = new System.Windows.Forms.CheckBox();
            chkEggs = new System.Windows.Forms.CheckBox();
            Generate = new System.Windows.Forms.Button();
            txtDebug = new System.Windows.Forms.TextBox();
            chkLimit = new System.Windows.Forms.CheckBox();
            grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTeamSize).BeginInit();
            SuspendLayout();
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(chkLimit);
            grpOptions.Controls.Add(cboGeneration);
            grpOptions.Controls.Add(lblGeneration);
            grpOptions.Controls.Add(label1);
            grpOptions.Controls.Add(cboStarter);
            grpOptions.Controls.Add(chkBalanced);
            grpOptions.Controls.Add(chkMustEvolve);
            grpOptions.Controls.Add(chkLegendaries);
            grpOptions.Controls.Add(lblTeamSize);
            grpOptions.Controls.Add(numTeamSize);
            grpOptions.Controls.Add(chkSecret);
            grpOptions.Controls.Add(chkEggs);
            grpOptions.Controls.Add(Generate);
            grpOptions.Location = new System.Drawing.Point(12, 12);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new System.Drawing.Size(275, 335);
            grpOptions.TabIndex = 0;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // cboGeneration
            // 
            cboGeneration.FormattingEnabled = true;
            cboGeneration.Location = new System.Drawing.Point(85, 152);
            cboGeneration.Name = "cboGeneration";
            cboGeneration.Size = new System.Drawing.Size(111, 23);
            cboGeneration.TabIndex = 12;
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
            label1.Location = new System.Drawing.Point(23, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 15);
            label1.TabIndex = 10;
            label1.Text = "Starter";
            // 
            // cboStarter
            // 
            cboStarter.FormattingEnabled = true;
            cboStarter.Location = new System.Drawing.Point(85, 22);
            cboStarter.Name = "cboStarter";
            cboStarter.Size = new System.Drawing.Size(175, 94);
            cboStarter.TabIndex = 2;
            cboStarter.ItemCheck += cboStarter_ItemCheck;
            // 
            // chkBalanced
            // 
            chkBalanced.AutoSize = true;
            chkBalanced.Location = new System.Drawing.Point(13, 181);
            chkBalanced.Name = "chkBalanced";
            chkBalanced.Size = new System.Drawing.Size(79, 19);
            chkBalanced.TabIndex = 9;
            chkBalanced.Text = "Balanced?";
            chkBalanced.UseVisualStyleBackColor = true;
            // 
            // chkMustEvolve
            // 
            chkMustEvolve.AutoSize = true;
            chkMustEvolve.Location = new System.Drawing.Point(12, 232);
            chkMustEvolve.Name = "chkMustEvolve";
            chkMustEvolve.Size = new System.Drawing.Size(95, 19);
            chkMustEvolve.TabIndex = 8;
            chkMustEvolve.Text = "Must Evolve?";
            chkMustEvolve.UseVisualStyleBackColor = true;
            // 
            // chkLegendaries
            // 
            chkLegendaries.AutoSize = true;
            chkLegendaries.Location = new System.Drawing.Point(12, 207);
            chkLegendaries.Name = "chkLegendaries";
            chkLegendaries.Size = new System.Drawing.Size(94, 19);
            chkLegendaries.TabIndex = 6;
            chkLegendaries.Text = "Legendaries?";
            chkLegendaries.UseVisualStyleBackColor = true;
            // 
            // lblTeamSize
            // 
            lblTeamSize.AutoSize = true;
            lblTeamSize.Location = new System.Drawing.Point(12, 125);
            lblTeamSize.Name = "lblTeamSize";
            lblTeamSize.Size = new System.Drawing.Size(58, 15);
            lblTeamSize.TabIndex = 7;
            lblTeamSize.Text = "Team Size";
            // 
            // numTeamSize
            // 
            numTeamSize.Location = new System.Drawing.Point(85, 123);
            numTeamSize.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            numTeamSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTeamSize.Name = "numTeamSize";
            numTeamSize.Size = new System.Drawing.Size(175, 23);
            numTeamSize.TabIndex = 6;
            numTeamSize.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // chkSecret
            // 
            chkSecret.AutoSize = true;
            chkSecret.Location = new System.Drawing.Point(10, 257);
            chkSecret.Name = "chkSecret";
            chkSecret.Size = new System.Drawing.Size(63, 19);
            chkSecret.TabIndex = 5;
            chkSecret.Text = "Secret?";
            chkSecret.UseVisualStyleBackColor = true;
            // 
            // chkEggs
            // 
            chkEggs.AutoSize = true;
            chkEggs.Location = new System.Drawing.Point(10, 280);
            chkEggs.Name = "chkEggs";
            chkEggs.Size = new System.Drawing.Size(69, 19);
            chkEggs.TabIndex = 4;
            chkEggs.Text = "In Eggs?";
            chkEggs.UseVisualStyleBackColor = true;
            // 
            // Generate
            // 
            Generate.Location = new System.Drawing.Point(194, 306);
            Generate.Name = "Generate";
            Generate.Size = new System.Drawing.Size(75, 23);
            Generate.TabIndex = 3;
            Generate.Text = "Generate";
            Generate.UseVisualStyleBackColor = true;
            Generate.Click += Generate_Click;
            // 
            // txtDebug
            // 
            txtDebug.Location = new System.Drawing.Point(309, 32);
            txtDebug.Multiline = true;
            txtDebug.Name = "txtDebug";
            txtDebug.Size = new System.Drawing.Size(537, 309);
            txtDebug.TabIndex = 1;
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
            // 
            // TeamGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(858, 359);
            Controls.Add(txtDebug);
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
            ((System.ComponentModel.ISupportInitialize)numTeamSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.CheckBox chkEggs;
        private System.Windows.Forms.Label lblTeamSize;
        private System.Windows.Forms.NumericUpDown numTeamSize;
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
