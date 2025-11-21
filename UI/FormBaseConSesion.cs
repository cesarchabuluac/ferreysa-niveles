// File: /Vales.UI/FormBaseConSesion.cs  (NET 4.8, C# 7)
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Vales.Domain;
using Vales.Infraestructura;
using Vales.UI.Controls;

namespace Vales.UI
{
    public partial class FormBaseConSesion : Form
    {
        private LoadingOverlay _loading;
        private PanelLoadingOverlay _panelLoading;
        private CancellationTokenSource _cts;
        private System.Windows.Forms.Timer _slowTimer;

        protected bool IsDesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
                if (Site != null && Site.DesignMode) return true;
                return DesignMode;
            }
        }

        protected virtual bool RequireSession { get { return true; } }
        protected Empresa Empresa { get { return AppSession.EmpresaSeleccionada; } }
        protected Usuario Usuario { get { return AppSession.UsuarioActual; } }
        protected string PeripheralConnectionString { get { return AppSession.PeripheralConnectionString; } }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (IsDesignMode) return;

            _loading = new LoadingOverlay { Visible = false };
            _loading.CancelRequested += (s, ev) => { try { _cts?.Cancel(); } catch { } };
            Controls.Add(_loading);
            _loading.BringToFront();

            // Inicializar el loading para paneles específicos
            _panelLoading = new PanelLoadingOverlay { Visible = false };
            _panelLoading.CancelRequested += (s, ev) => { try { _cts?.Cancel(); } catch { } };

            _slowTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _slowTimer.Tick += (s, ev) =>
            {
                _slowTimer.Stop();
                if (_loading != null && _loading.Visible)
                    _loading.SetMessage("Trabajando...", "Esto puede tardar un poco más");
                if (_panelLoading != null && _panelLoading.IsShowingInPanel)
                    _panelLoading.SetMessage("Trabajando...", "Esto puede tardar un poco más");
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsDesignMode || !RequireSession) return;

            if (AppSession.EmpresaSeleccionada == null ||
                AppSession.UsuarioActual == null ||
                string.IsNullOrEmpty(AppSession.PeripheralConnectionString))
            {
                MessageBox.Show("No hay sesión activa. Inicie sesión.", "Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            OnSesionInicializada();
        }

        protected virtual void OnSesionInicializada() { }

        protected FbConnection CreatePeripheralConnection()
        {
            if (IsDesignMode) throw new InvalidOperationException("No disponible en diseñador.");
            if (string.IsNullOrEmpty(PeripheralConnectionString)) throw new InvalidOperationException("Sesión no activa.");
            return new FbConnection(PeripheralConnectionString);
        }

        protected void WithConnection(Action<FbConnection> action)
        {
            using (var conn = CreatePeripheralConnection())
            {
                conn.Open();
                action(conn);
            }
        }

        protected async Task WithConnectionAsync(Func<FbConnection, Task> action)
        {
            using (var conn = CreatePeripheralConnection())
            {
                await conn.OpenAsync();
                await action(conn);
            }
        }

        // ---------- Overlay ----------
        protected void ShowLoading(string title = "Cargando...", string subtitle = "Por favor espere", bool cancellable = false)
        {
            if (_loading == null) return;
            if (InvokeRequired) { BeginInvoke(new Action<string, string, bool>(ShowLoading), title, subtitle, cancellable); return; }

            _cts?.Dispose();
            _cts = new CancellationTokenSource();

            _loading.SetMessage(title, subtitle);
            _loading.SetCancelable(cancellable);
            _loading.Visible = true;
            _loading.BringToFront();
            UseWaitCursor = true;

            _slowTimer?.Stop();
            _slowTimer?.Start();
        }

        protected void HideLoading()
        {
            if (_loading == null) return;
            if (InvokeRequired) { BeginInvoke(new Action(HideLoading)); return; }

            _slowTimer?.Stop();
            _loading.Visible = false;
            UseWaitCursor = false;

            _cts?.Dispose();
            _cts = null;
        }

        // ---------- Panel Loading Methods ----------
        protected void ShowPanelLoading(Control targetPanel, string title = "Cargando...", string subtitle = "Por favor espere", bool cancellable = false)
        {
            if (_panelLoading == null || targetPanel == null) return;
            if (InvokeRequired) 
            { 
                BeginInvoke(new Action<Control, string, string, bool>(ShowPanelLoading), targetPanel, title, subtitle, cancellable); 
                return; 
            }

            _cts?.Dispose();
            _cts = new CancellationTokenSource();

            _panelLoading.ShowInPanel(targetPanel, title, subtitle, cancellable);
            UseWaitCursor = true;

            _slowTimer?.Stop();
            _slowTimer?.Start();
        }

        protected void HidePanelLoading()
        {
            if (_panelLoading == null) return;
            if (InvokeRequired) { BeginInvoke(new Action(HidePanelLoading)); return; }

            _slowTimer?.Stop();
            _panelLoading.HideFromPanel();
            UseWaitCursor = false;

            _cts?.Dispose();
            _cts = null;
        }

        public CancellationToken LoadingCancellationToken
        {
            get { return _cts != null ? _cts.Token : CancellationToken.None; }
        }


        // -------- Ejecutores (background) --------
        public async Task RunWithOverlayAsync(Action work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos")
        {
            ShowLoading(title, subtitle);
            try { await Task.Run(work); }
            finally { HideLoading(); }
        }

        public async Task RunWithOverlayAsync(Action work, string title = "Cargando...", string subtitle = "Por favor espere", bool cancellable = false)
        {
            ShowLoading(title, subtitle, cancellable);
            try { await Task.Run(work); }
            finally { HideLoading(); }
        }

        public async Task<T> RunWithOverlayAsync<T>(Func<T> work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
        {
            ShowLoading(title, subtitle, cancellable);
            try { return await Task.Run(work); }
            finally { HideLoading(); }
        }

        public async Task<T> RunWithOverlayAsync<T>(Func<Task<T>> work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
        {
            ShowLoading(title, subtitle, cancellable);
            try { return await work(); }
            finally { HideLoading(); }
        }

        private async Task EnsureOverlayPaintedAsync()
        {
            // Da tiempo al layout y repinta
            await Task.Delay(50);
            if (_loading != null)
            {
                _loading.Update();
                _loading.Refresh();
            }
            Application.DoEvents(); // asegura que el overlay se muestre antes del trabajo pesado
        }

        // -------- Ejecutor en UI (para código que toca controles) --------
        public async Task RunUIWorkWithOverlayAsync(Action work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
        {
            ShowLoading(title, subtitle, cancellable);
            await EnsureOverlayPaintedAsync();

            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();
            // Programa el trabajo después de que el overlay ya está en pantalla
            BeginInvoke(new Action(() =>
            {
                try { work(); tcs.SetResult(null); }
                catch (Exception ex) { tcs.SetException(ex); }
            }));

            try { await tcs.Task; }
            finally { HideLoading(); }
        }

        // -------- Ejecutores para paneles específicos --------
        public async Task RunWithPanelOverlayAsync(Control targetPanel, Action work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
        {
            ShowPanelLoading(targetPanel, title, subtitle, cancellable);
            try { await Task.Run(work); }
            finally { HidePanelLoading(); }
        }

        public async Task<T> RunWithPanelOverlayAsync<T>(Control targetPanel, Func<T> work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
        {
            ShowPanelLoading(targetPanel, title, subtitle, cancellable);
            try { return await Task.Run(work); }
            finally { HidePanelLoading(); }
        }

        public async Task RunUIWorkWithPanelOverlayAsync(Control targetPanel, Action work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
        {
            ShowPanelLoading(targetPanel, title, subtitle, cancellable);
            await EnsureOverlayPaintedAsync();

            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();
            // Programa el trabajo después de que el overlay ya está en pantalla
            BeginInvoke(new Action(() =>
            {
                try { work(); tcs.SetResult(null); }
                catch (Exception ex) { tcs.SetException(ex); }
            }));

            try { await tcs.Task; }
            finally { HidePanelLoading(); }
        }

        protected void MostrarError(string mensaje, Exception ex = null)
        {
            var txt = ex == null ? mensaje : (mensaje + Environment.NewLine + ex.Message);
            MessageBox.Show(txt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
