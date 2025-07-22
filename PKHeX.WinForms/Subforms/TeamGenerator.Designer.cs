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
            grpOptions = new System.Windows.Forms.GroupBox();
            grpOutput = new System.Windows.Forms.GroupBox();
            chkSecret = new System.Windows.Forms.CheckBox();
            chkEggs = new System.Windows.Forms.CheckBox();
            lblHatchRate = new System.Windows.Forms.Label();
            cboHatchRate = new System.Windows.Forms.ComboBox();
            grpPokemonOptions = new System.Windows.Forms.GroupBox();
            chkLegendaries = new System.Windows.Forms.CheckBox();
            chkRegionalForms = new System.Windows.Forms.CheckBox();
            chkMaxIVs = new System.Windows.Forms.CheckBox();
            chkEvoItems = new System.Windows.Forms.CheckBox();
            chkStatLimit = new System.Windows.Forms.CheckBox();
            lblMinStatTotal = new System.Windows.Forms.Label();
            numMinStatTotal = new System.Windows.Forms.NumericUpDown();
            lblMaxStatTotal = new System.Windows.Forms.Label();
            numMaxStatTotal = new System.Windows.Forms.NumericUpDown();
            grpTeamRules = new System.Windows.Forms.GroupBox();
            chkBalanced = new System.Windows.Forms.CheckBox();
            chkMustEvolve = new System.Windows.Forms.CheckBox();
            cboGeneration = new System.Windows.Forms.CheckedListBox();
            lblGeneration = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cboStarter = new System.Windows.Forms.CheckedListBox();
            lblTeamSize = new System.Windows.Forms.Label();
            sldTeamSize = new System.Windows.Forms.TrackBar();
            lblTeamSizeValue = new System.Windows.Forms.Label();
            cboPreset = new System.Windows.Forms.ComboBox();
            lblPreset = new System.Windows.Forms.Label();
            Generate = new System.Windows.Forms.Button();
            btnHelp = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            lblProgress = new System.Windows.Forms.Label();
            grpOptions.SuspendLayout();
            grpOutput.SuspendLayout();
            grpPokemonOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinStatTotal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxStatTotal).BeginInit();
            grpTeamRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sldTeamSize).BeginInit();
            SuspendLayout();
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(grpOutput);
            grpOptions.Controls.Add(grpPokemonOptions);
            grpOptions.Controls.Add(grpTeamRules);
            grpOptions.Controls.Add(cboGeneration);
            grpOptions.Controls.Add(lblGeneration);
            grpOptions.Controls.Add(label1);
            grpOptions.Controls.Add(cboStarter);
            grpOptions.Controls.Add(lblTeamSize);
            grpOptions.Controls.Add(sldTeamSize);
            grpOptions.Controls.Add(lblTeamSizeValue);
            grpOptions.Controls.Add(cboPreset);
            grpOptions.Controls.Add(lblPreset);
            grpOptions.Controls.Add(Generate);
            grpOptions.Controls.Add(btnHelp);
            grpOptions.Location = new System.Drawing.Point(12, 12);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new System.Drawing.Size(560, 578);
            grpOptions.TabIndex = 0;
            grpOptions.TabStop = false;
            grpOptions.Text = "Team Generator Options";
            // 
            // grpOutput
            // 
            grpOutput.Controls.Add(chkSecret);
            grpOutput.Controls.Add(chkEggs);
            grpOutput.Controls.Add(lblHatchRate);
            grpOutput.Controls.Add(cboHatchRate);
            grpOutput.Location = new System.Drawing.Point(10, 462);
            grpOutput.Name = "grpOutput";
            grpOutput.Size = new System.Drawing.Size(260, 110);
            grpOutput.TabIndex = 22;
            grpOutput.TabStop = false;
            grpOutput.Text = "Output Options";
            // 
            // chkSecret
            // 
            chkSecret.AutoSize = true;
            chkSecret.Location = new System.Drawing.Point(10, 25);
            chkSecret.Name = "chkSecret";
            chkSecret.Size = new System.Drawing.Size(89, 19);
            chkSecret.TabIndex = 5;
            chkSecret.Text = "Silent Mode";
            chkSecret.UseVisualStyleBackColor = true;
            // 
            // chkEggs
            // 
            chkEggs.AutoSize = true;
            chkEggs.Location = new System.Drawing.Point(10, 50);
            chkEggs.Name = "chkEggs";
            chkEggs.Size = new System.Drawing.Size(67, 19);
            chkEggs.TabIndex = 4;
            chkEggs.Text = "As Eggs";
            chkEggs.UseVisualStyleBackColor = true;
            // 
            // lblHatchRate
            // 
            lblHatchRate.AutoSize = true;
            lblHatchRate.Location = new System.Drawing.Point(10, 78);
            lblHatchRate.Name = "lblHatchRate";
            lblHatchRate.Size = new System.Drawing.Size(68, 15);
            lblHatchRate.TabIndex = 6;
            lblHatchRate.Text = "Hatch Rate:";
            // 
            // cboHatchRate
            // 
            cboHatchRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboHatchRate.FormattingEnabled = true;
            cboHatchRate.Location = new System.Drawing.Point(90, 75);
            cboHatchRate.Name = "cboHatchRate";
            cboHatchRate.Size = new System.Drawing.Size(75, 23);
            cboHatchRate.TabIndex = 7;
            // 
            // grpPokemonOptions
            // 
            grpPokemonOptions.Controls.Add(chkLegendaries);
            grpPokemonOptions.Controls.Add(chkRegionalForms);
            grpPokemonOptions.Controls.Add(chkMaxIVs);
            grpPokemonOptions.Controls.Add(chkEvoItems);
            grpPokemonOptions.Controls.Add(chkStatLimit);
            grpPokemonOptions.Controls.Add(lblMinStatTotal);
            grpPokemonOptions.Controls.Add(numMinStatTotal);
            grpPokemonOptions.Controls.Add(lblMaxStatTotal);
            grpPokemonOptions.Controls.Add(numMaxStatTotal);
            grpPokemonOptions.Location = new System.Drawing.Point(280, 376);
            grpPokemonOptions.Name = "grpPokemonOptions";
            grpPokemonOptions.Size = new System.Drawing.Size(260, 140);
            grpPokemonOptions.TabIndex = 21;
            grpPokemonOptions.TabStop = false;
            grpPokemonOptions.Text = "Pokémon Options";
            // 
            // chkLegendaries
            // 
            chkLegendaries.AutoSize = true;
            chkLegendaries.Location = new System.Drawing.Point(10, 25);
            chkLegendaries.Name = "chkLegendaries";
            chkLegendaries.Size = new System.Drawing.Size(89, 19);
            chkLegendaries.TabIndex = 6;
            chkLegendaries.Text = "Legendaries";
            chkLegendaries.UseVisualStyleBackColor = true;
            // 
            // chkRegionalForms
            // 
            chkRegionalForms.AutoSize = true;
            chkRegionalForms.Location = new System.Drawing.Point(10, 50);
            chkRegionalForms.Name = "chkRegionalForms";
            chkRegionalForms.Size = new System.Drawing.Size(108, 19);
            chkRegionalForms.TabIndex = 17;
            chkRegionalForms.Text = "Regional Forms";
            chkRegionalForms.UseVisualStyleBackColor = true;
            // 
            // chkMaxIVs
            // 
            chkMaxIVs.AutoSize = true;
            chkMaxIVs.Location = new System.Drawing.Point(130, 50);
            chkMaxIVs.Name = "chkMaxIVs";
            chkMaxIVs.Size = new System.Drawing.Size(66, 19);
            chkMaxIVs.TabIndex = 23;
            chkMaxIVs.Text = "Max IVs";
            chkMaxIVs.UseVisualStyleBackColor = true;
            // 
            // chkEvoItems
            // 
            chkEvoItems.AutoSize = true;
            chkEvoItems.Location = new System.Drawing.Point(130, 75);
            chkEvoItems.Name = "chkEvoItems";
            chkEvoItems.Size = new System.Drawing.Size(108, 19);
            chkEvoItems.TabIndex = 24;
            chkEvoItems.Text = "Evolution Items";
            chkEvoItems.UseVisualStyleBackColor = true;
            // 
            // chkStatLimit
            // 
            chkStatLimit.AutoSize = true;
            chkStatLimit.Location = new System.Drawing.Point(10, 75);
            chkStatLimit.Name = "chkStatLimit";
            chkStatLimit.Size = new System.Drawing.Size(105, 19);
            chkStatLimit.TabIndex = 18;
            chkStatLimit.Text = "Limit Stat Total";
            chkStatLimit.UseVisualStyleBackColor = true;
            chkStatLimit.CheckedChanged += chkStatLimit_CheckedChanged;
            // 
            // lblMinStatTotal
            // 
            lblMinStatTotal.AutoSize = true;
            lblMinStatTotal.Enabled = false;
            lblMinStatTotal.Location = new System.Drawing.Point(10, 100);
            lblMinStatTotal.Name = "lblMinStatTotal";
            lblMinStatTotal.Size = new System.Drawing.Size(31, 15);
            lblMinStatTotal.TabIndex = 19;
            lblMinStatTotal.Text = "Min:";
            // 
            // numMinStatTotal
            // 
            numMinStatTotal.Enabled = false;
            numMinStatTotal.Location = new System.Drawing.Point(47, 98);
            numMinStatTotal.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            numMinStatTotal.Minimum = new decimal(new int[] { 180, 0, 0, 0 });
            numMinStatTotal.Name = "numMinStatTotal";
            numMinStatTotal.Size = new System.Drawing.Size(60, 23);
            numMinStatTotal.TabIndex = 20;
            numMinStatTotal.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // lblMaxStatTotal
            // 
            lblMaxStatTotal.AutoSize = true;
            lblMaxStatTotal.Enabled = false;
            lblMaxStatTotal.Location = new System.Drawing.Point(113, 100);
            lblMaxStatTotal.Name = "lblMaxStatTotal";
            lblMaxStatTotal.Size = new System.Drawing.Size(32, 15);
            lblMaxStatTotal.TabIndex = 21;
            lblMaxStatTotal.Text = "Max:";
            // 
            // numMaxStatTotal
            // 
            numMaxStatTotal.Enabled = false;
            numMaxStatTotal.Location = new System.Drawing.Point(153, 98);
            numMaxStatTotal.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            numMaxStatTotal.Minimum = new decimal(new int[] { 180, 0, 0, 0 });
            numMaxStatTotal.Name = "numMaxStatTotal";
            numMaxStatTotal.Size = new System.Drawing.Size(60, 23);
            numMaxStatTotal.TabIndex = 22;
            numMaxStatTotal.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // grpTeamRules
            // 
            grpTeamRules.Controls.Add(chkBalanced);
            grpTeamRules.Controls.Add(chkMustEvolve);
            grpTeamRules.Location = new System.Drawing.Point(10, 376);
            grpTeamRules.Name = "grpTeamRules";
            grpTeamRules.Size = new System.Drawing.Size(260, 80);
            grpTeamRules.TabIndex = 20;
            grpTeamRules.TabStop = false;
            grpTeamRules.Text = "Team Rules";
            // 
            // chkBalanced
            // 
            chkBalanced.AutoSize = true;
            chkBalanced.Location = new System.Drawing.Point(10, 25);
            chkBalanced.Name = "chkBalanced";
            chkBalanced.Size = new System.Drawing.Size(74, 19);
            chkBalanced.TabIndex = 9;
            chkBalanced.Text = "Balanced";
            chkBalanced.UseVisualStyleBackColor = true;
            // 
            // chkMustEvolve
            // 
            chkMustEvolve.AutoSize = true;
            chkMustEvolve.Location = new System.Drawing.Point(10, 50);
            chkMustEvolve.Name = "chkMustEvolve";
            chkMustEvolve.Size = new System.Drawing.Size(90, 19);
            chkMustEvolve.TabIndex = 8;
            chkMustEvolve.Text = "Must Evolve";
            chkMustEvolve.UseVisualStyleBackColor = true;
            // 
            // cboGeneration
            // 
            cboGeneration.ColumnWidth = 50;
            cboGeneration.FormattingEnabled = true;
            cboGeneration.Location = new System.Drawing.Point(85, 142);
            cboGeneration.MultiColumn = true;
            cboGeneration.Name = "cboGeneration";
            cboGeneration.Size = new System.Drawing.Size(270, 58);
            cboGeneration.TabIndex = 12;
            cboGeneration.ItemCheck += cboGeneration_ItemCheck;
            // 
            // lblGeneration
            // 
            lblGeneration.AutoSize = true;
            lblGeneration.Location = new System.Drawing.Point(10, 142);
            lblGeneration.Name = "lblGeneration";
            lblGeneration.Size = new System.Drawing.Size(70, 15);
            lblGeneration.TabIndex = 11;
            lblGeneration.Text = "Generations";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(35, 60);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 15);
            label1.TabIndex = 10;
            label1.Text = "Starter";
            // 
            // cboStarter
            // 
            cboStarter.ColumnWidth = 120;
            cboStarter.FormattingEnabled = true;
            cboStarter.Location = new System.Drawing.Point(85, 60);
            cboStarter.MultiColumn = true;
            cboStarter.Name = "cboStarter";
            cboStarter.Size = new System.Drawing.Size(455, 76);
            cboStarter.TabIndex = 2;
            cboStarter.ItemCheck += cboStarter_ItemCheck;
            // 
            // lblTeamSize
            // 
            lblTeamSize.AutoSize = true;
            lblTeamSize.Location = new System.Drawing.Point(17, 25);
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
            sldTeamSize.Size = new System.Drawing.Size(415, 45);
            sldTeamSize.TabIndex = 6;
            sldTeamSize.Value = 6;
            sldTeamSize.ValueChanged += sldTeamSize_ValueChanged;
            // 
            // lblTeamSizeValue
            // 
            lblTeamSizeValue.AutoSize = true;
            lblTeamSizeValue.Location = new System.Drawing.Point(506, 25);
            lblTeamSizeValue.Name = "lblTeamSizeValue";
            lblTeamSizeValue.Size = new System.Drawing.Size(13, 15);
            lblTeamSizeValue.TabIndex = 14;
            lblTeamSizeValue.Text = "6";
            // 
            // cboPreset
            // 
            cboPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboPreset.FormattingEnabled = true;
            cboPreset.Location = new System.Drawing.Point(85, 206);
            cboPreset.Name = "cboPreset";
            cboPreset.Size = new System.Drawing.Size(175, 23);
            cboPreset.TabIndex = 15;
            cboPreset.SelectedIndexChanged += cboPreset_SelectedIndexChanged;
            // 
            // lblPreset
            // 
            lblPreset.AutoSize = true;
            lblPreset.Location = new System.Drawing.Point(37, 209);
            lblPreset.Name = "lblPreset";
            lblPreset.Size = new System.Drawing.Size(39, 15);
            lblPreset.TabIndex = 16;
            lblPreset.Text = "Preset";
            // 
            // Generate
            // 
            Generate.Location = new System.Drawing.Point(373, 542);
            Generate.Name = "Generate";
            Generate.Size = new System.Drawing.Size(100, 30);
            Generate.TabIndex = 3;
            Generate.Text = "Generate Team";
            Generate.UseVisualStyleBackColor = true;
            Generate.Click += Generate_Click;
            // 
            // btnHelp
            // 
            btnHelp.Location = new System.Drawing.Point(479, 542);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new System.Drawing.Size(75, 30);
            btnHelp.TabIndex = 23;
            btnHelp.Text = "Help";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(12, 596);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(560, 23);
            progressBar.TabIndex = 25;
            progressBar.Visible = false;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new System.Drawing.Point(480, 549);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new System.Drawing.Size(0, 15);
            lblProgress.TabIndex = 26;
            lblProgress.Visible = false;
            // 
            // TeamGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 631);
            Controls.Add(grpOptions);
            Controls.Add(progressBar);
            Controls.Add(lblProgress);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TeamGenerator";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Team Generator";
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            grpOutput.ResumeLayout(false);
            grpOutput.PerformLayout();
            grpPokemonOptions.ResumeLayout(false);
            grpPokemonOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinStatTotal).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxStatTotal).EndInit();
            grpTeamRules.ResumeLayout(false);
            grpTeamRules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sldTeamSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.GroupBox grpTeamRules;
        private System.Windows.Forms.GroupBox grpPokemonOptions;
        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox chkEggs;
        private System.Windows.Forms.Label lblTeamSize;
        private System.Windows.Forms.TrackBar sldTeamSize;
        private System.Windows.Forms.Label lblTeamSizeValue;
        private System.Windows.Forms.CheckBox chkSecret;
        private System.Windows.Forms.CheckBox chkLegendaries;
        private System.Windows.Forms.CheckBox chkMustEvolve;
        private System.Windows.Forms.CheckBox chkBalanced;
        private System.Windows.Forms.CheckBox chkRegionalForms;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox cboStarter;
        private System.Windows.Forms.CheckedListBox cboGeneration;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.ComboBox cboPreset;
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.CheckBox chkStatLimit;
        private System.Windows.Forms.NumericUpDown numMinStatTotal;
        private System.Windows.Forms.NumericUpDown numMaxStatTotal;
        private System.Windows.Forms.Label lblMinStatTotal;
        private System.Windows.Forms.Label lblMaxStatTotal;
        private System.Windows.Forms.CheckBox chkMaxIVs;
        private System.Windows.Forms.CheckBox chkEvoItems;
        private System.Windows.Forms.Label lblHatchRate;
        private System.Windows.Forms.ComboBox cboHatchRate;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
    }
}
