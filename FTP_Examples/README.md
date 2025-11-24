# Archivos de Ejemplo para Servidor FTP

Esta carpeta contiene archivos de ejemplo que debes subir a tu servidor FTP de HostGator.

## Estructura en el Servidor

Crea la siguiente estructura en tu FTP:

```
/public_html/
└── niveles/
    └── updates/
        ├── version.txt      ← Sube este archivo
        ├── update.zip       ← Crea este archivo con tus binarios
        └── changelog.txt    ← Sube este archivo (opcional)
```

## Archivos

### version.txt

- Contiene solo el número de versión actual
- Formato: `1.0.0` (sin espacios ni saltos de línea adicionales)
- Actualiza este archivo cada vez que publiques una nueva versión

### changelog.txt

- Archivo opcional con el historial de cambios
- Útil para que los usuarios sepan qué cambió
- Puedes mostrarlo en la aplicación si lo deseas

### update.zip

- NO está incluido aquí porque contiene tus binarios compilados
- Debes crearlo tú mismo con los archivos de tu aplicación
- Ver AUTO_UPDATE_GUIDE.md para instrucciones

## Cómo Subir

1. Conecta a tu FTP usando FileZilla o similar
2. Navega a `/public_html/`
3. Crea la carpeta `niveles/updates/`
4. Sube `version.txt` y `changelog.txt`
5. Crea y sube `update.zip` con tus binarios

## Permisos

Asegúrate de que los archivos tengan permisos de lectura (644):

- `version.txt`: 644
- `changelog.txt`: 644
- `update.zip`: 644

La carpeta `updates/` debe tener permisos 755.
