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
\par

\cf2\b TEAM RULES:\cf0\b0\par
\bullet \b Balanced:\b0 Ensures no duplicate types in your team (based on final evolution types)\par
\bullet \b Must Evolve:\b0 Only includes Pokémon that have evolution forms (excludes single-stage Pokémon)\par
  \tab - \cf3\b NOTE:\cf0\b0 When used with Legendaries option, legendary Pokémon are exempt from evolution requirement\par
\par

\cf2\b POKÉMON OPTIONS:\cf0\b0\par
\bullet \b Legendaries:\b0 Include legendary, mythical, and sub-legendary Pokémon in generation\par
  \tab - \cf3\b NOTE:\cf0\b0 When used with Must Evolve option, legendaries don't need to evolve\par
\bullet \b Regional Forms:\b0 Include regional variants such as:\par
  \tab - Alolan forms (Generation 7)\par
  \tab - Galarian forms (Generation 8)\par
  \tab - Paldean forms (Generation 9)\par
\bullet \b Max IVs:\b0 Generate Pokémon with maximum Individual Values (31 in all stats)\par
  \tab - When unchecked, Pokémon receive random IVs within legal ranges\par
\bullet \b Evolution Items:\b0 Automatically provide evolution items as held items\par
  \tab - Items like evolution stones for Eevee, trade items, etc.\par
  \tab - Makes it clear which evolution was intended\par
\bullet \b Limit Stat Total:\b0 Filter Pokémon by their base stat totals\par
  \tab - \b Min/Max:\b0 Set the stat total range (applies to final evolution forms)\par
  \tab - Useful for creating balanced teams or specific power levels\par
\par

\cf2\b OUTPUT OPTIONS:\cf0\b0\par
\bullet \b Silent Mode:\b0 Generate team without showing the results popup dialog\par
\bullet \b As Eggs:\b0 Generate Pokémon as eggs for hatching\par
  \tab - If team size is 6, the first Pokémon will be automatically hatched\par
  \tab - Eggs have step counters set for hatching simulation\par
  \tab - \b Hatch Rate:\b0 Controls how quickly eggs will hatch:\par
  \tab   • \i Fast:\i0 Lower step counter (hatches quickly)\par
  \tab   • \i Medium:\i0 Moderate step counter (default)\par
  \tab   • \i Slow:\i0 Higher step counter (takes longer to hatch)\par
  \tab - Each egg in the team gets progressively higher step counts\par
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
\bullet When both Legendaries and Must Evolve are enabled, legendary Pokémon are exempt from evolution requirements\par
\par

\cf3\b TIP:\b0 Experiment with different generation combinations and options to create unique and interesting teams!
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
