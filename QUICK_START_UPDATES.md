# 🚀 QUICK START - Auto-Update System

## ⚡ Pasos Rápidos para Activar

### 1️⃣ Instalar FluentFTP (REQUERIDO)

Abre la **Consola del Administrador de Paquetes** en Visual Studio y ejecuta:

```powershell
Install-Package FluentFTP
```

### 2️⃣ Configurar Credenciales FTP

Edita `UpdateConfig.json` con tus datos de HostGator:

```json
{
  "FtpSettings": {
    "Host": "ftp.tudominio.com",
    "Username": "usuario@tudominio.com",
    "Password": "tu_password",
    "UpdatesPath": "/public_html/niveles/updates/"
  }
}
```

### 3️⃣ Crear Estructura en FTP

Usando FileZilla o tu cliente FTP:

1. Crea la carpeta: `/public_html/niveles/updates/`
2. Sube `FTP_Examples/version.txt`
3. Sube `FTP_Examples/changelog.txt` (opcional)

### 4️⃣ Compilar y Probar

1. Compila el proyecto en **Release**
2. Verifica que `Updater.exe` esté en la carpeta raíz
3. Ejecuta `Niveles.exe` para probar

## 📚 Documentación Completa

Ver [AUTO_UPDATE_GUIDE.md](AUTO_UPDATE_GUIDE.md) para instrucciones detalladas.

## ✅ Checklist Mínimo

- [ ] FluentFTP instalado
- [ ] UpdateConfig.json configurado
- [ ] Estructura FTP creada
- [ ] Updater.exe en carpeta raíz
- [ ] Probado localmente

## 🆘 Problemas Comunes

**Error: "No se encontró UpdateConfig.json"**
→ Copia el archivo a la carpeta donde está Niveles.exe

**Error: "No se puede conectar al FTP"**
→ Verifica credenciales y prueba con FileZilla primero

**No detecta actualizaciones**
→ Asegúrate que version.txt en FTP tenga un número mayor (ej: 1.0.1)

---

**¿Listo?** Instala FluentFTP y configura tu FTP. ¡El sistema está completo!
