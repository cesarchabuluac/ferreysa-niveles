using System;
using System.Drawing;
using System.Windows.Forms;

namespace Niveles.UI.Controls
{
    /// <summary>
    /// Componente de loading transparente que puede ser usado en cualquier panel específico
    /// </summary>
    public partial class PanelLoadingOverlay : UserControl
    {
        private readonly Panel _card;
        private readonly Label _title;
        private readonly Label _subtitle;
        private readonly ProgressBar _bar;
        private readonly Button _btnCancel;
        private Control _targetPanel;

        public event EventHandler CancelRequested;

        public PanelLoadingOverlay()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(150, 255, 255, 255); // Más transparente
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

            // Tarjeta central con sombra sutil
            _card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(25),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Agregar sombra a la tarjeta
            _card.Paint += (s, e) =>
            {
                var rect = _card.ClientRectangle;
                // Sombra
                using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, rect.X + 3, rect.Y + 3, rect.Width, rect.Height);
                }
                // Fondo blanco
                using (var bgBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(bgBrush, rect);
                }
                // Borde sutil
                using (var borderPen = new Pen(Color.FromArgb(200, 200, 200)))
                {
                    e.Graphics.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                }
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
            layout.Controls.Add(Spacer(8), 0, 1);
            layout.Controls.Add(_subtitle, 0, 2);
            layout.Controls.Add(Spacer(15), 0, 3);
            layout.Controls.Add(_bar, 0, 4);
            layout.Controls.Add(Spacer(12), 0, 5);
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
            if (_card != null)
            {
                _card.Left = Math.Max(0, (ClientSize.Width - _card.Width) / 2);
                _card.Top = Math.Max(0, (ClientSize.Height - _card.Height) / 2);
                _card.BringToFront();
            }
        }

        /// <summary>
        /// Establece el mensaje del loading
        /// </summary>
        public void SetMessage(string title, string subtitle = null)
        {
            _title.Text = string.IsNullOrWhiteSpace(title) ? "Cargando..." : title;
            _subtitle.Text = string.IsNullOrWhiteSpace(subtitle) ? "" : subtitle;
            _subtitle.Visible = !string.IsNullOrEmpty(_subtitle.Text);
            CenterCard();
        }

        /// <summary>
        /// Establece si el loading puede ser cancelado
        /// </summary>
        public void SetCancelable(bool cancelable)
        {
            _btnCancel.Visible = cancelable;
            CenterCard();
        }

        /// <summary>
        /// Muestra el loading en el panel especificado
        /// </summary>
        public void ShowInPanel(Control targetPanel, string title = "Cargando...", string subtitle = "Por favor espere", bool cancellable = false)
        {
            if (targetPanel == null) return;

            _targetPanel = targetPanel;
            SetMessage(title, subtitle);
            SetCancelable(cancellable);

            // Remover de cualquier contenedor anterior
            Parent?.Controls.Remove(this);

            // Agregar al panel objetivo
            targetPanel.Controls.Add(this);
            BringToFront();
            Dock = DockStyle.Fill;
            Visible = true;

            // Asegurar que se centre correctamente
            CenterCard();
            
            // Forzar actualización visual
            Update();
            Refresh();
        }

        /// <summary>
        /// Oculta el loading del panel
        /// </summary>
        public void HideFromPanel()
        {
            if (Visible)
            {
                Visible = false;
                Parent?.Controls.Remove(this);
                _targetPanel = null;
            }
        }

        /// <summary>
        /// Verifica si el loading está actualmente visible en algún panel
        /// </summary>
        public bool IsShowingInPanel => Visible && Parent != null;

        /// <summary>
        /// Método estático para crear y mostrar un loading rápidamente en un panel
        /// </summary>
        /// <param name="targetPanel">Panel donde mostrar el loading</param>
        /// <param name="title">Título del loading</param>
        /// <param name="subtitle">Subtítulo del loading</param>
        /// <param name="cancellable">Si puede ser cancelado</param>
        /// <returns>Instancia del PanelLoadingOverlay para control manual</returns>
        public static PanelLoadingOverlay Create(Control targetPanel, string title = "Cargando...", string subtitle = "Por favor espere", bool cancellable = false)
        {
            var overlay = new PanelLoadingOverlay();
            overlay.ShowInPanel(targetPanel, title, subtitle, cancellable);
            return overlay;
        }

        /// <summary>
        /// Actualiza el mensaje del loading si está visible
        /// </summary>
        public void UpdateMessage(string title, string subtitle = null)
        {
            if (IsShowingInPanel)
            {
                SetMessage(title, subtitle);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) { /* bloquea clicks al fondo */ }
        protected override void OnKeyDown(KeyEventArgs e) { /* bloquea teclado al fondo */ }
    }
}
