// File: /Vales.Infraestructura/AppSession.cs  (C# 7 compatible)
using System;
using Vales.Domain;

namespace Vales.Infraestructura
{
    /// <summary>
    /// Guarda el estado de sesión y la connection string periférica.
    /// En C# 7 las referencias pueden ser null; proveemos helpers para validar.
    /// </summary>
    public static class AppSession
    {
        // En C# 7, las referencias son anulables por defecto (sin ?).
        public static Empresa EmpresaSeleccionada { get; private set; }
        public static Usuario UsuarioActual { get; private set; }
        public static string PeripheralConnectionString { get; private set; }

        public static bool TieneSesionActiva
        {
            get
            {
                return EmpresaSeleccionada != null
                       && UsuarioActual != null
                       && !string.IsNullOrEmpty(PeripheralConnectionString);
            }
        }

        public static void SetSession(Empresa empresa, Usuario usuario, string connStr)
        {
            if (empresa == null) throw new ArgumentNullException(nameof(empresa));
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            if (string.IsNullOrEmpty(connStr)) throw new ArgumentException("ConnectionString vacía.", nameof(connStr));

            EmpresaSeleccionada = empresa;
            UsuarioActual = usuario;
            PeripheralConnectionString = connStr;
        }

        public static void Clear()
        {
            EmpresaSeleccionada = null;
            UsuarioActual = null;
            PeripheralConnectionString = null;
        }

        /// <summary>
        /// Lanza excepción si no hay sesión válida. Úsalo antes de consumir la conexión.
        /// </summary>
        public static void EnsureSesionActiva()
        {
            if (!TieneSesionActiva)
                throw new InvalidOperationException("No hay sesión activa. Inicie sesión primero.");
        }
    }
}

// Ejemplo de uso C# 7 seguro en MainForm (ajuste rápido):
// var emp = AppSession.EmpresaSeleccionada; var usr = AppSession.UsuarioActual;
// if (emp == null || usr == null) { MessageBox.Show("Sesión no inicializada"); Close(); return; }
// lblInfo.Text = $"Empresa: {emp.Nombre} ({emp.Alias})\nUsuario: {usr.NombreUsuario} - {usr.NombreCompleto}";
