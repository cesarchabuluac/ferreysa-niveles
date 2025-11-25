using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niveles.Helpers
{
    public static class ThemeManager
    {
        public static void ApplyTheme(Control root)
        {
            root.BackColor = ThemeColors.Background;
            root.ForeColor = ThemeColors.Foreground;

            foreach (Control ctrl in root.Controls)
            {
                ApplyControlTheme(ctrl);
            }
        }

        private static void ApplyControlTheme(Control ctrl)
        {
            // Colores de fondo por tipo de control
            if (ctrl is Panel || ctrl is GroupBox)
                ctrl.BackColor = ThemeColors.PanelBackground;

            if (ctrl is Label)
                ctrl.ForeColor = ThemeColors.Foreground;

            if (ctrl is TextBox txt)
            {
                txt.BackColor = Color.White;
                txt.ForeColor = ThemeColors.Foreground;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }

            if (ctrl is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = ThemeColors.Primary;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = ThemeColors.Border;
                btn.FlatAppearance.MouseOverBackColor = ThemeColors.Secondary;
            }

            if (ctrl is StatusStrip strip)
            {
                strip.BackColor = ThemeColors.StripBackground;
                strip.ForeColor = ThemeColors.Foreground;
            }

            if (ctrl is MenuStrip menu)
            {
                menu.BackColor = ThemeColors.StripBackground;
                menu.ForeColor = ThemeColors.Foreground;
            }

            if (ctrl is TabControl tab)
            {
                tab.BackColor = ThemeColors.PanelBackground;
            }

            // Recursividad -> aplica a los hijos
            foreach (Control child in ctrl.Controls)
                ApplyControlTheme(child);
        }
    }
}
