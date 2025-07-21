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
            grpTeamRules = new System.Windows.Forms.GroupBox();
            grpPokemonOptions = new System.Windows.Forms.GroupBox();
            grpOutput = new System.Windows.Forms.GroupBox();
            btnHelp = new System.Windows.Forms.Button();
            chkLimit = new System.Windows.Forms.CheckBox();
            cboGeneration = new System.Windows.Forms.ComboBox();
            cboStarter = new System.Windows.Forms.CheckedListBox();
            lblGeneration = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            chkBalanced = new System.Windows.Forms.CheckBox();
            chkMustEvolve = new System.Windows.Forms.CheckBox();
            chkLegendaries = new System.Windows.Forms.CheckBox();
            chkRegionalForms = new System.Windows.Forms.CheckBox();
            lblTeamSize = new System.Windows.Forms.Label();
            sldTeamSize = new System.Windows.Forms.TrackBar();
            lblTeamSizeValue = new System.Windows.Forms.Label();
            chkSecret = new System.Windows.Forms.CheckBox();
            chkEggs = new System.Windows.Forms.CheckBox();
            Generate = new System.Windows.Forms.Button();
            cboPreset = new System.Windows.Forms.ComboBox();
            lblPreset = new System.Windows.Forms.Label();
            chkStatLimit = new System.Windows.Forms.CheckBox();
            numMinStatTotal = new System.Windows.Forms.NumericUpDown();
            numMaxStatTotal = new System.Windows.Forms.NumericUpDown();
            lblMinStatTotal = new System.Windows.Forms.Label();
            lblMaxStatTotal = new System.Windows.Forms.Label();
            chkMaxIVs = new System.Windows.Forms.CheckBox();
            grpOptions.SuspendLayout();
            grpTeamRules.SuspendLayout();
            grpPokemonOptions.SuspendLayout();
            grpOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sldTeamSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinStatTotal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxStatTotal).BeginInit();
            SuspendLayout();
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(grpOutput);
            grpOptions.Controls.Add(grpPokemonOptions);
            grpOptions.Controls.Add(grpTeamRules);
            grpOptions.Controls.Add(cboGeneration);
            grpOptions.Controls.Add(lblGeneration);
            grpOptions.Controls.Add(chkLimit);
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
            grpOptions.Size = new System.Drawing.Size(560, 500);
            grpOptions.TabIndex = 0;
            grpOptions.TabStop = false;
            grpOptions.Text = "Team Generator Options";
            // 
            // grpTeamRules
            // 
            grpTeamRules.Controls.Add(chkBalanced);
            grpTeamRules.Controls.Add(chkMustEvolve);
            grpTeamRules.Location = new System.Drawing.Point(10, 240);
            grpTeamRules.Name = "grpTeamRules";
            grpTeamRules.Size = new System.Drawing.Size(260, 80);
            grpTeamRules.TabIndex = 20;
            grpTeamRules.TabStop = false;
            grpTeamRules.Text = "Team Rules";
            // 
            // grpPokemonOptions
            // 
            grpPokemonOptions.Controls.Add(chkLegendaries);
            grpPokemonOptions.Controls.Add(chkRegionalForms);
            grpPokemonOptions.Controls.Add(chkMaxIVs);
            grpPokemonOptions.Controls.Add(chkStatLimit);
            grpPokemonOptions.Controls.Add(lblMinStatTotal);
            grpPokemonOptions.Controls.Add(numMinStatTotal);
            grpPokemonOptions.Controls.Add(lblMaxStatTotal);
            grpPokemonOptions.Controls.Add(numMaxStatTotal);
            grpPokemonOptions.Location = new System.Drawing.Point(280, 240);
            grpPokemonOptions.Name = "grpPokemonOptions";
            grpPokemonOptions.Size = new System.Drawing.Size(260, 140);
            grpPokemonOptions.TabIndex = 21;
            grpPokemonOptions.TabStop = false;
            grpPokemonOptions.Text = "Pokémon Options";
            // 
            // grpOutput
            // 
            grpOutput.Controls.Add(chkSecret);
            grpOutput.Controls.Add(chkEggs);
            grpOutput.Location = new System.Drawing.Point(10, 390);
            grpOutput.Name = "grpOutput";
            grpOutput.Size = new System.Drawing.Size(260, 80);
            grpOutput.TabIndex = 22;
            grpOutput.TabStop = false;
            grpOutput.Text = "Output Options";
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
            cboStarter.Size = new System.Drawing.Size(455, 76);
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
            chkBalanced.Location = new System.Drawing.Point(10, 25);
            chkBalanced.Name = "chkBalanced";
            chkBalanced.Size = new System.Drawing.Size(79, 19);
            chkBalanced.TabIndex = 9;
            chkBalanced.Text = "Balanced";
            chkBalanced.UseVisualStyleBackColor = true;
            // 
            // chkMustEvolve
            // 
            chkMustEvolve.AutoSize = true;
            chkMustEvolve.Location = new System.Drawing.Point(10, 50);
            chkMustEvolve.Name = "chkMustEvolve";
            chkMustEvolve.Size = new System.Drawing.Size(95, 19);
            chkMustEvolve.TabIndex = 8;
            chkMustEvolve.Text = "Must Evolve";
            chkMustEvolve.UseVisualStyleBackColor = true;
            // 
            // chkLegendaries
            // 
            chkLegendaries.AutoSize = true;
            chkLegendaries.Location = new System.Drawing.Point(10, 25);
            chkLegendaries.Name = "chkLegendaries";
            chkLegendaries.Size = new System.Drawing.Size(94, 19);
            chkLegendaries.TabIndex = 6;
            chkLegendaries.Text = "Legendaries";
            chkLegendaries.UseVisualStyleBackColor = true;
            // 
            // chkRegionalForms
            // 
            chkRegionalForms.AutoSize = true;
            chkRegionalForms.Location = new System.Drawing.Point(10, 50);
            chkRegionalForms.Name = "chkRegionalForms";
            chkRegionalForms.Size = new System.Drawing.Size(110, 19);
            chkRegionalForms.TabIndex = 17;
            chkRegionalForms.Text = "Regional Forms";
            chkRegionalForms.UseVisualStyleBackColor = true;
            // 
            // chkMaxIVs
            // 
            chkMaxIVs.AutoSize = true;
            chkMaxIVs.Location = new System.Drawing.Point(130, 50);
            chkMaxIVs.Name = "chkMaxIVs";
            chkMaxIVs.Size = new System.Drawing.Size(78, 19);
            chkMaxIVs.TabIndex = 23;
            chkMaxIVs.Text = "Max IVs";
            chkMaxIVs.UseVisualStyleBackColor = true;
            // 
            // chkStatLimit
            // 
            chkStatLimit.AutoSize = true;
            chkStatLimit.Location = new System.Drawing.Point(10, 75);
            chkStatLimit.Name = "chkStatLimit";
            chkStatLimit.Size = new System.Drawing.Size(108, 19);
            chkStatLimit.TabIndex = 18;
            chkStatLimit.Text = "Limit Stat Total";
            chkStatLimit.UseVisualStyleBackColor = true;
            chkStatLimit.CheckedChanged += chkStatLimit_CheckedChanged;
            // 
            // lblMinStatTotal
            // 
            lblMinStatTotal.AutoSize = true;
            lblMinStatTotal.Location = new System.Drawing.Point(10, 100);
            lblMinStatTotal.Name = "lblMinStatTotal";
            lblMinStatTotal.Size = new System.Drawing.Size(31, 15);
            lblMinStatTotal.TabIndex = 19;
            lblMinStatTotal.Text = "Min:";
            lblMinStatTotal.Enabled = false;
            // 
            // numMinStatTotal
            // 
            numMinStatTotal.Location = new System.Drawing.Point(47, 98);
            numMinStatTotal.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            numMinStatTotal.Minimum = new decimal(new int[] { 180, 0, 0, 0 });
            numMinStatTotal.Name = "numMinStatTotal";
            numMinStatTotal.Size = new System.Drawing.Size(60, 23);
            numMinStatTotal.TabIndex = 20;
            numMinStatTotal.Value = new decimal(new int[] { 200, 0, 0, 0 });
            numMinStatTotal.Enabled = false;
            // 
            // lblMaxStatTotal
            // 
            lblMaxStatTotal.AutoSize = true;
            lblMaxStatTotal.Location = new System.Drawing.Point(113, 100);
            lblMaxStatTotal.Name = "lblMaxStatTotal";
            lblMaxStatTotal.Size = new System.Drawing.Size(34, 15);
            lblMaxStatTotal.TabIndex = 21;
            lblMaxStatTotal.Text = "Max:";
            lblMaxStatTotal.Enabled = false;
            // 
            // numMaxStatTotal
            // 
            numMaxStatTotal.Location = new System.Drawing.Point(153, 98);
            numMaxStatTotal.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            numMaxStatTotal.Minimum = new decimal(new int[] { 180, 0, 0, 0 });
            numMaxStatTotal.Name = "numMaxStatTotal";
            numMaxStatTotal.Size = new System.Drawing.Size(60, 23);
            numMaxStatTotal.TabIndex = 22;
            numMaxStatTotal.Value = new decimal(new int[] { 600, 0, 0, 0 });
            numMaxStatTotal.Enabled = false;
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
            // chkSecret
            // 
            chkSecret.AutoSize = true;
            chkSecret.Location = new System.Drawing.Point(10, 25);
            chkSecret.Name = "chkSecret";
            chkSecret.Size = new System.Drawing.Size(94, 19);
            chkSecret.TabIndex = 5;
            chkSecret.Text = "Silent Mode";
            chkSecret.UseVisualStyleBackColor = true;
            // 
            // chkEggs
            // 
            chkEggs.AutoSize = true;
            chkEggs.Location = new System.Drawing.Point(10, 50);
            chkEggs.Name = "chkEggs";
            chkEggs.Size = new System.Drawing.Size(78, 19);
            chkEggs.TabIndex = 4;
            chkEggs.Text = "As Eggs";
            chkEggs.UseVisualStyleBackColor = true;
            // 
            // cboPreset
            // 
            cboPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboPreset.FormattingEnabled = true;
            cboPreset.Location = new System.Drawing.Point(85, 181);
            cboPreset.Name = "cboPreset";
            cboPreset.Size = new System.Drawing.Size(175, 23);
            cboPreset.TabIndex = 15;
            cboPreset.SelectedIndexChanged += new System.EventHandler(cboPreset_SelectedIndexChanged);
            // 
            // lblPreset
            // 
            lblPreset.AutoSize = true;
            lblPreset.Location = new System.Drawing.Point(30, 184);
            lblPreset.Name = "lblPreset";
            lblPreset.Size = new System.Drawing.Size(39, 15);
            lblPreset.TabIndex = 16;
            lblPreset.Text = "Preset";
            // 
            // Generate
            // 
            Generate.Location = new System.Drawing.Point(280, 420);
            Generate.Name = "Generate";
            Generate.Size = new System.Drawing.Size(100, 30);
            Generate.TabIndex = 3;
            Generate.Text = "Generate Team";
            Generate.UseVisualStyleBackColor = true;
            Generate.Click += Generate_Click;
            // 
            // btnHelp
            // 
            btnHelp.Location = new System.Drawing.Point(400, 420);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new System.Drawing.Size(75, 30);
            btnHelp.TabIndex = 23;
            btnHelp.Text = "Help";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // TeamGenerator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 525);
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
            grpTeamRules.ResumeLayout(false);
            grpTeamRules.PerformLayout();
            grpPokemonOptions.ResumeLayout(false);
            grpPokemonOptions.PerformLayout();
            grpOutput.ResumeLayout(false);
            grpOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sldTeamSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinStatTotal).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxStatTotal).EndInit();
            ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cboGeneration;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.CheckBox chkLimit;
        private System.Windows.Forms.ComboBox cboPreset;
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.CheckBox chkStatLimit;
        private System.Windows.Forms.NumericUpDown numMinStatTotal;
        private System.Windows.Forms.NumericUpDown numMaxStatTotal;
        private System.Windows.Forms.Label lblMinStatTotal;
        private System.Windows.Forms.Label lblMaxStatTotal;
        private System.Windows.Forms.CheckBox chkMaxIVs;
    }
}
