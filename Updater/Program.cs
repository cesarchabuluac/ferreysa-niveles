using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Niveles - Actualizador";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   NIVELES - ACTUALIZADOR AUTOMÁTICO   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            try
            {
                // Validar argumentos
                if (args.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Argumentos insuficientes.");
                    Console.WriteLine("Uso: Updater.exe <ruta_zip> <ruta_instalacion> <ejecutable>");
                    Console.ResetColor();
                    Console.WriteLine("\nPresione cualquier tecla para salir...");
                    Console.ReadKey();
                    return;
                }

                string zipPath = args[0];
                string installPath = args[1];
                string exeName = args[2];

                Console.WriteLine($"Archivo de actualización: {Path.GetFileName(zipPath)}");
                Console.WriteLine($"Directorio de instalación: {installPath}");
                Console.WriteLine($"Ejecutable: {exeName}");
                Console.WriteLine();

                // Validar que existe el ZIP
                if (!File.Exists(zipPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: No se encontró el archivo {zipPath}");
                    Console.ResetColor();
                    Console.WriteLine("\nPresione cualquier tecla para salir...");
                    Console.ReadKey();
                    return;
                }

                // Esperar a que se cierre la aplicación principal
                Console.WriteLine("Esperando a que se cierre la aplicación principal...");
                WaitForProcessToClose(exeName);
                Thread.Sleep(2000); // Espera adicional para asegurar que se liberaron los archivos

                // Crear backup
                Console.WriteLine("\n[1/4] Creando respaldo de seguridad...");
                string backupPath = CreateBackup(installPath);
                if (!string.IsNullOrEmpty(backupPath))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ Respaldo creado: {backupPath}");
                    Console.ResetColor();
                }

                // Extraer actualización
                Console.WriteLine("\n[2/4] Extrayendo archivos de actualización...");
                ExtractUpdate(zipPath, installPath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Archivos extraídos correctamente");
                Console.ResetColor();

                // Limpiar archivos temporales
                Console.WriteLine("\n[3/4] Limpiando archivos temporales...");
                CleanupTempFiles(zipPath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Limpieza completada");
                Console.ResetColor();

                // Reiniciar aplicación
                Console.WriteLine("\n[4/4] Reiniciando aplicación...");
                string exePath = Path.Combine(installPath, exeName);
                
                if (File.Exists(exePath))
                {
                    Process.Start(exePath);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✓ Aplicación reiniciada correctamente");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"⚠ No se pudo reiniciar automáticamente. Ejecute manualmente: {exePath}");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("════════════════════════════════════════");
                Console.WriteLine("  ¡ACTUALIZACIÓN COMPLETADA CON ÉXITO!");
                Console.WriteLine("════════════════════════════════════════");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Esta ventana se cerrará en 3 segundos...");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("════════════════════════════════════════");
                Console.WriteLine("  ERROR DURANTE LA ACTUALIZACIÓN");
                Console.WriteLine("════════════════════════════════════════");
                Console.WriteLine($"\n{ex.Message}");
                Console.WriteLine($"\nDetalles técnicos:\n{ex.StackTrace}");
                Console.ResetColor();
                Console.WriteLine("\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Espera a que se cierre el proceso de la aplicación principal
        /// </summary>
        static void WaitForProcessToClose(string processName)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(processName);
            int attempts = 0;
            int maxAttempts = 30; // 30 segundos máximo

            while (attempts < maxAttempts)
            {
                Process[] processes = Process.GetProcessesByName(nameWithoutExtension);
                if (processes.Length == 0)
                {
                    return; // Proceso cerrado
                }

                Console.Write(".");
                Thread.Sleep(1000);
                attempts++;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠ Tiempo de espera agotado. Intentando continuar...");
            Console.ResetColor();
        }

        /// <summary>
        /// Crea un respaldo de la instalación actual
        /// </summary>
        static string CreateBackup(string installPath)
        {
            try
            {
                string backupDir = Path.Combine(Path.GetTempPath(), "NivelesBackup");
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupPath = Path.Combine(backupDir, $"Backup_{timestamp}.zip");

                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                // Crear ZIP del directorio actual (excluyendo ciertos archivos)
                if (Directory.Exists(installPath))
                {
                    ZipFile.CreateFromDirectory(installPath, backupPath, CompressionLevel.Fastest, false);
                    return backupPath;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠ No se pudo crear respaldo: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }

        /// <summary>
        /// Extrae el archivo ZIP de actualización
        /// </summary>
        static void ExtractUpdate(string zipPath, string installPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                int totalFiles = archive.Entries.Count;
                int currentFile = 0;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    currentFile++;
                    
                    // Mostrar progreso
                    int percentage = (currentFile * 100) / totalFiles;
                    Console.Write($"\rProgreso: {percentage}% ({currentFile}/{totalFiles}) - {entry.Name}".PadRight(80));

                    // Saltar directorios vacíos
                    if (string.IsNullOrEmpty(entry.Name))
                        continue;

                    string destinationPath = Path.Combine(installPath, entry.FullName);
                    string destinationDir = Path.GetDirectoryName(destinationPath);

                    // Crear directorio si no existe
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);

                    // Extraer archivo (sobrescribir si existe)
                    try
                    {
                        entry.ExtractToFile(destinationPath, true);
                    }
                    catch (IOException)
                    {
                        // Si el archivo está en uso, intentar una vez más después de un breve delay
                        Thread.Sleep(500);
                        try
                        {
                            entry.ExtractToFile(destinationPath, true);
                        }
                        catch
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"⚠ No se pudo actualizar: {entry.Name} (archivo en uso)");
                            Console.ResetColor();
                        }
                    }
                }

                Console.WriteLine(); // Nueva línea después del progreso
            }
        }

        /// <summary>
        /// Limpia archivos temporales de actualización
        /// </summary>
        static void CleanupTempFiles(string zipPath)
        {
            try
            {
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                // Limpiar directorio temporal si está vacío
                string tempDir = Path.GetDirectoryName(zipPath);
                if (Directory.Exists(tempDir) && Directory.GetFiles(tempDir).Length == 0)
                {
                    Directory.Delete(tempDir);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠ No se pudieron limpiar archivos temporales: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
