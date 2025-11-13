using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vales.UI.Controls
{
    public partial class LoadingOverlay : UserControl
    {
        private readonly Panel _card;
        private readonly Label _title;
        private readonly Label _subtitle;
        private readonly ProgressBar _bar;
        private readonly Button _btnCancel;

        public event EventHandler CancelRequested;

        public LoadingOverlay()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(120, 255, 255, 255); // velo claro semitransparente
            //BackColor = Color.Transparent; // Nunca usar semitransparente aquí
            Visible = false;

            _title = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(32, 32, 32),
                Text = "Cargando...",
                TextAlign = ContentAlignment.MiddleCenter
            };

            _subtitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(90, 90, 90),
                Text = "Esto puede tardar unos segundos",
                TextAlign = ContentAlignment.MiddleCenter
            };

            _bar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 28,
                Width = 280,
                Height = 16
            };

            _btnCancel = new Button
            {
                Text = "Cancelar",
                AutoSize = true,
                Padding = new Padding(12, 4, 12, 4),
                FlatStyle = FlatStyle.System,
                Visible = false
            };
            _btnCancel.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);

            // Tarjeta central (tema claro)
            _card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Layout vertical limpio
            var layout = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 7,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            layout.Controls.Add(_title, 0, 0);
            layout.Controls.Add(Spacer(6), 0, 1);
            layout.Controls.Add(_subtitle, 0, 2);
            layout.Controls.Add(Spacer(12), 0, 3);
            layout.Controls.Add(_bar, 0, 4);
            layout.Controls.Add(Spacer(10), 0, 5);
            layout.Controls.Add(_btnCancel, 0, 6);

            _title.Anchor = AnchorStyles.Top;
            _subtitle.Anchor = AnchorStyles.Top;
            _bar.Anchor = AnchorStyles.Top;
            _btnCancel.Anchor = AnchorStyles.Top;

            _card.Controls.Add(layout);
            Controls.Add(_card);

            Resize += (s, e) => CenterCard();
            Load += (s, e) => CenterCard();
        }

        private Control Spacer(int h) => new Panel { Height = h, Width = 1, Dock = DockStyle.Top };

        private void CenterCard()
        {
            _card.Left = Math.Max(0, (ClientSize.Width - _card.Width) / 2);
            _card.Top = Math.Max(0, (ClientSize.Height - _card.Height) / 2);
            _card.BringToFront();
        }

        public void SetMessage(string title, string subtitle = null)
        {
            _title.Text = string.IsNullOrWhiteSpace(title) ? "Cargando..." : title;
            _subtitle.Text = string.IsNullOrWhiteSpace(subtitle) ? "" : subtitle;
            _subtitle.Visible = !string.IsNullOrEmpty(_subtitle.Text);
            CenterCard();
        }

        public void SetCancelable(bool cancelable)
        {
            _btnCancel.Visible = cancelable;
            CenterCard();
        }

        protected override void OnMouseDown(MouseEventArgs e) { /* bloquea clicks al fondo */ }
        protected override void OnKeyDown(KeyEventArgs e) { /* bloquea teclado al fondo */ }
    }
}