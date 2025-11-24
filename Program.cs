using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Niveles.Helpers;
using Niveles.UI;


namespace Niveles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ═══════════════════════════════════════════════════════════
            // VERIFICAR ACTUALIZACIONES AL INICIAR
            // ═══════════════════════════════════════════════════════════
            try
            {
                var updateManager = new UpdateManager();
                // Ejecutar verificación en background thread para no bloquear
                Task.Run(() => updateManager.CheckAndUpdate());
            }
            catch (Exception ex)
            {
                // Si falla la verificación de actualizaciones, continuar normalmente
                System.Diagnostics.Debug.WriteLine($"Error al verificar actualizaciones: {ex.Message}");
            }

            using (var login = new FormAccess())
            {
                var result = login.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Application.Run(new FormNiveles());
                }
            }

            //Application.Run(new FormAccess());

            ////////ClaseConn inicia = new ClaseConn();

            ////////inicia.MetodoConn("192.168.1.1", "C:\\Microsip datos\\FERREY.FDB", "SYSDBA", "masterkey190652"); //laptop
            //////////inicia.MetodoConn("localhost", "C:\\Microsip datos\\COPIAFERREYSAFEB2019.FDB", "SYSDBA", "masterkey"); //oficina
            //////////inicia.MetodoConn("216.238.69.160", "C:\\DBCENTRAL\\CENTRAL.FDB", "sysdba", "masterkey190652");

            ////////if (inicia.Validacion())
            ////////{
            ////////    MessageBox.Show("CONEXION EXITOSA: MATRIZ");
            ////////    Application.Run(new FormNiveles());
            ////////    //Application.Run(new FormCotizacion());
            ////////}
            ////////else
            ////////{
            ////////    inicia.MetodoConn("10.1.1.1", "C:\\Microsip datos\\Ferrey Omar.fdb", "sysdba", "masterkey");
            ////////    if (inicia.Validacion())
            ////////    {
            ////////        ClaseConn.es_papalote = true;

            ////////        MessageBox.Show("CONEXION EXITOSA: PAPALOTE");
            ////////        Application.Run(new FormNiveles());
            ////////    }
            ////////    else
            ////////    {
            ////////        MessageBox.Show("NO HAY CONNEXION");
            ////////    }

            ////////}

            //TABLAS O DATOS NUEVOS QUE TUVE QUE HACER PARA PROGRAMA:


        }
    }
}
