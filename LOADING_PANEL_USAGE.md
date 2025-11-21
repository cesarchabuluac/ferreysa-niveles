# Sistema de Loading Transparente para Paneles

## Descripción
Este sistema permite mostrar un loading transparente en paneles específicos del formulario, en lugar de bloquear todo el formulario. Es útil cuando solo una parte de la interfaz está cargando datos.

## Componentes Creados

### 1. PanelLoadingOverlay
- **Ubicación**: `UI/Controls/PanelLoadingOverlay.cs`
- **Función**: Componente de loading transparente que puede ser mostrado en cualquier panel específico
- **Características**:
  - Fondo transparente con overlay semitransparente
  - Tarjeta central con sombra sutil
  - Soporte para título, subtítulo y botón de cancelar
  - Se centra automáticamente en el panel objetivo

### 2. Métodos en FormBaseConSesion
Se agregaron nuevos métodos para manejar loading en paneles específicos:

#### Métodos de Control Básico
```csharp
// Mostrar loading en un panel específico
protected void ShowPanelLoading(Control targetPanel, string title = "Cargando...", string subtitle = "Por favor espere", bool cancellable = false)

// Ocultar loading del panel
protected void HidePanelLoading()
```

#### Métodos de Ejecución con Loading
```csharp
// Para trabajo en background thread
public async Task RunWithPanelOverlayAsync(Control targetPanel, Action work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)

// Para trabajo en background thread con retorno de valor
public async Task<T> RunWithPanelOverlayAsync<T>(Control targetPanel, Func<T> work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)

// Para trabajo en UI thread (cuando se modifican controles)
public async Task RunUIWorkWithPanelOverlayAsync(Control targetPanel, Action work, string title = "Cargando...", string subtitle = "Esto puede tardar unos segundos", bool cancellable = false)
```

## Ejemplos de Uso

### Ejemplo 1: Loading en Panel Específico (UI Thread)
```csharp
private async void btnCargarDatos_Click(object sender, EventArgs e)
{
    try
    {
        // Mostrar loading solo en el panel derecho
        await RunUIWorkWithPanelOverlayAsync(
            splitContainer1.Panel2,  // Panel específico
            () => CargarDatosEnPanel(),  // Método que modifica controles
            title: "Cargando datos...",
            subtitle: "Actualizando información",
            cancellable: false
        );
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

### Ejemplo 2: Loading en Panel con Background Thread
```csharp
private async void btnProcesarDatos_Click(object sender, EventArgs e)
{
    try
    {
        var resultado = await RunWithPanelOverlayAsync(
            panelPrincipal,  // Panel donde mostrar el loading
            () => {
                // Trabajo pesado en background thread
                return ProcesarDatosComplejos();
            },
            title: "Procesando...",
            subtitle: "Esto puede tardar varios minutos",
            cancellable: true  // Permitir cancelación
        );
        
        // Usar el resultado
        MostrarResultado(resultado);
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

### Ejemplo 3: Loading Manual con Método Estático
```csharp
private void EjemploLoadingEstatico()
{
    // Crear y mostrar loading usando el método estático
    var loading = PanelLoadingOverlay.Create(miPanel, "Cargando...", "Por favor espere");
    
    try
    {
        // Hacer trabajo
        RealizarTrabajo();
        
        // Actualizar mensaje durante el proceso
        loading.UpdateMessage("Finalizando...", "Casi terminado");
    }
    finally
    {
        // Siempre ocultar loading
        loading.HideFromPanel();
    }
}
```

### Ejemplo 4: Loading Manual con Métodos de Instancia
```csharp
private void EjemploLoadingManual()
{
    // Mostrar loading
    ShowPanelLoading(miPanel, "Cargando...", "Por favor espere");
    
    try
    {
        // Hacer trabajo
        RealizarTrabajo();
    }
    finally
    {
        // Siempre ocultar loading
        HidePanelLoading();
    }
}
```

## Implementación en FormNiveles

### Casos de Uso Implementados

1. **btn_general_Click**: Loading en panel izquierdo al cargar artículos
2. **btnVistaValor_Click**: Loading en panel izquierdo al cargar por valor
3. **btnCargarValores_Click**: Loading en panel derecho al cargar valores
4. **cboAlmacen_SelectedIndexChanged**: Loading en panel derecho al cambiar almacén

### Ejemplo Específico del Proyecto
```csharp
private async void btn_general_Click(object sender, EventArgs e)
{
    try
    {
        var alm = cbo_almacenes.SelectedValue?.ToString() ?? "";
        // Mostrar loading solo en el panel izquierdo donde están los artículos
        await RunUIWorkWithPanelOverlayAsync(
            splitContainer1.Panel1,  // Panel específico donde están los datos
            () => Llenar_DGV(alm, ""),
            title: "Cargando artículos...",
            subtitle: "Calculando niveles y pintando filas",
            cancellable: false
        );
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

## Ventajas del Sistema

1. **Experiencia de Usuario Mejorada**: Solo bloquea la parte de la interfaz que está cargando
2. **Transparencia Visual**: El overlay es semitransparente y no oculta completamente el contenido
3. **Flexibilidad**: Puede usarse en cualquier panel o control contenedor
4. **Consistencia**: Mantiene el mismo estilo visual en toda la aplicación
5. **Facilidad de Uso**: API simple y consistente con el sistema existente

## Notas Técnicas

- El componente se agrega dinámicamente al panel objetivo cuando se muestra
- Se remueve automáticamente cuando se oculta
- Maneja correctamente el redimensionamiento del panel
- Bloquea eventos de mouse y teclado en el área del loading
- Compatible con el sistema de cancelación existente

## Migración desde el Sistema Anterior

Para migrar código existente:

**Antes:**
```csharp
await RunUIWorkWithOverlayAsync(() => MiMetodo(), "Título", "Subtítulo", false);
```

**Después:**
```csharp
await RunUIWorkWithPanelOverlayAsync(miPanel, () => MiMetodo(), "Título", "Subtítulo", false);
```

**O usando el método estático para casos simples:**
```csharp
var loading = PanelLoadingOverlay.Create(miPanel, "Título", "Subtítulo");
try { MiMetodo(); } finally { loading.HideFromPanel(); }
```

El sistema anterior sigue funcionando para casos donde se necesite bloquear todo el formulario.
