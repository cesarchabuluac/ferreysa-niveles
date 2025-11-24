# Guía de Sistema de Actualización ClickOnce

## Problema Resuelto

El sistema de actualización no funcionaba correctamente cuando la aplicación se publicaba usando ClickOnce porque:

1. **UpdateConfig.json** no se incluía en el paquete de instalación
2. **Updater.exe** no se copiaba al directorio de instalación
3. Las rutas de archivos no funcionaban correctamente en ClickOnce

## Solución Implementada

### 1. Configuración del Proyecto (.csproj)

Se modificaron las siguientes entradas en el archivo `Niveles.csproj`:

```xml
<!-- UpdateConfig.json ahora se incluye como Content -->
<Content Include="UpdateConfig.json">
  <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  <IncludeInPackage>true</IncludeInPackage>
</Content>

<!-- Updater.exe ahora se incluye correctamente -->
<Content Include="Updater.exe">
  <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  <IncludeInPackage>true</IncludeInPackage>
</Content>
```

### 2. Mejoras en UpdateManager.cs

#### Detección de Tipo de Instalación
El sistema ahora detecta automáticamente si la aplicación está ejecutándose como:
- **ClickOnce**: Usa `ApplicationDeployment.IsNetworkDeployed`
- **Instalación Normal**: Usa el sistema FTP personalizado

#### Rutas Correctas
```csharp
string basePath;
if (ApplicationDeployment.IsNetworkDeployed)
{
    // Para ClickOnce, usar el directorio de datos de la aplicación
    basePath = ApplicationDeployment.CurrentDeployment.DataDirectory;
}
else
{
    // Para instalación normal, usar StartupPath
    basePath = Application.StartupPath;
}
```

#### Sistema Dual de Actualización
- **ClickOnce**: Usa el sistema nativo de ClickOnce (`deployment.Update()`)
- **Instalación Normal**: Usa el sistema FTP personalizado con Updater.exe

## Cómo Usar

### Para ClickOnce
```csharp
var updateManager = new UpdateManager();
updateManager.CheckAndUpdate(); // Automáticamente detecta ClickOnce
```

### Para Instalación Normal
```csharp
var updateManager = new UpdateManager();
updateManager.CheckAndUpdate(); // Usa el sistema FTP
```

## Archivos Necesarios

### Para ClickOnce
- `UpdateConfig.json` (opcional, para futuras extensiones)

### Para Instalación Normal
- `UpdateConfig.json` (requerido)
- `Updater.exe` (requerido)

## Configuración de Publicación

1. **Publicar la aplicación** usando Visual Studio
2. Los archivos `UpdateConfig.json` y `Updater.exe` se incluirán automáticamente
3. El sistema de actualización funcionará según el tipo de instalación

## Beneficios

1. **Compatibilidad Dual**: Funciona tanto con ClickOnce como con instalación normal
2. **Detección Automática**: No requiere configuración manual
3. **Archivos Incluidos**: Los archivos necesarios se copian automáticamente
4. **Rutas Correctas**: Maneja correctamente las rutas en ambos tipos de instalación

## Notas Importantes

- Para ClickOnce, las actualizaciones se manejan a través del sistema nativo
- Para instalación normal, se usa el sistema FTP personalizado
- Los archivos de configuración ahora se incluyen automáticamente en la publicación
- El sistema es retrocompatible con instalaciones existentes
