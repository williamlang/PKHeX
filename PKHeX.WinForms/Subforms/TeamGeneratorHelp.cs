using System;
using System.Drawing;
using System.Windows.Forms;

namespace PKHeX.WinForms.Subforms
{
    public partial class TeamGeneratorHelp : Form
    {
        public TeamGeneratorHelp()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Team Generator Help";
            Size = new Size(600, 500);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            var rtbHelp = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = SystemColors.Window,
                Font = new Font("Segoe UI", 9),
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            rtbHelp.Rtf = @"{\rtf1\ansi\deff0 
{\fonttbl {\f0 Segoe UI;}}
{\colortbl ;\red0\green0\blue255;\red0\green128\blue0;\red255\green0\blue0;}

\f0\fs18
\cf1\b TEAM GENERATOR HELP\cf0\b0\par
\par

\cf2\b BASIC OPTIONS:\cf0\b0\par
\bullet \b Team Size:\b0 Set how many Pokémon to generate (1-6)\par
\bullet \b Starter:\b0 Select specific Pokémon to include in your team from the available list\par
\bullet \b Generation:\b0 Choose which Pokémon generation to draw from\par
\bullet \b Limit:\b0 When checked, only includes Pokémon from the selected generation (unchecked includes all generations up to selected)\par
\bullet \b Preset:\b0 Quick setup configurations:\par
  \tab - \i Legendary Focus:\i0 Generates a team focused on legendary Pokémon\par
  \tab - \i Starter Pokémon Only:\i0 Generates a team using only starter Pokémon\par
\par

\cf2\b TEAM RULES:\cf0\b0\par
\bullet \b Balanced:\b0 Ensures no duplicate types in your team (based on final evolution types)\par
\bullet \b Must Evolve:\b0 Only includes Pokémon that have evolution forms (excludes single-stage Pokémon)\par
\par

\cf2\b POKÉMON OPTIONS:\cf0\b0\par
\bullet \b Legendaries:\b0 Include legendary, mythical, and sub-legendary Pokémon in generation\par
\bullet \b Regional Forms:\b0 Include regional variants such as:\par
  \tab - Alolan forms (Generation 7)\par
  \tab - Galarian forms (Generation 8)\par
  \tab - Paldean forms (Generation 9)\par
\bullet \b Max IVs:\b0 Generate Pokémon with maximum Individual Values (31 in all stats)\par
  \tab - When unchecked, Pokémon receive random IVs within legal ranges\par
\bullet \b Limit Stat Total:\b0 Filter Pokémon by their base stat totals\par
  \tab - \b Min/Max:\b0 Set the stat total range (applies to final evolution forms)\par
  \tab - Useful for creating balanced teams or specific power levels\par
\par

\cf2\b OUTPUT OPTIONS:\cf0\b0\par
\bullet \b Silent Mode:\b0 Generate team without showing the results popup dialog\par
\bullet \b As Eggs:\b0 Generate Pokémon as eggs for hatching\par
  \tab - If team size is 6, the first Pokémon will be automatically hatched\par
  \tab - Eggs have step counters set for hatching simulation\par
\par

\cf2\b IMPORTANT NOTES:\cf0\b0\par
\bullet Selected starter Pokémon are used first, then random Pokémon fill remaining slots\par
\bullet All generated Pokémon receive:\par
  \tab - Random IVs and EVs within legal ranges\par
  \tab - Random natures and abilities\par
  \tab - Suggested movesets based on legality analysis\par
\bullet Evolution items are automatically provided when needed (e.g., stones for Eevee)\par
\bullet Regional forms are only available if your current save file supports them\par
\bullet The generator respects game-specific availability and legal move combinations\par
\par

\cf3\b TIP:\b0 Use presets as starting points, then customize the options to fine-tune your team generation!
}";

            var btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.OK,
                Size = new Size(75, 25),
                Location = new Point(510, 430),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            Controls.Add(rtbHelp);
            Controls.Add(btnClose);

            AcceptButton = btnClose;
        }
    }
}
