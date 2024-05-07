# Proyecto final - Programacion 3

**Integrante:** Brandon Gerardo Manzo Godoy - 0900-18-502

## Datos importantes

> **Nota:** Yo he trabajado en un dispositivo mac, por lo cual dejare un tutorial de como levantar la aplicacion por medio de Mac y otra a travez de windows con visual studio. Puede utilizar esta guia de [Como instalar .net en mac](https://learn.microsoft.com/es-es/dotnet/core/install/macos) para tener el ambiente configurado como en mi pc. De ante mano gracias!

## Guia de inicio

### Por medio de la terminal (Mac/Linux)

Esto esta enfocado para iniciar la aplicacion por medio de la terminal de mac y linux, para ello debe de cumplir con los requisitos que microsoft indica a travez de esta guia y haciendo una descarga directa de .net a travez del siguiente [link](https://dotnet.microsoft.com/es-es/download) indica para poder ejecutar un programa.

1. Abrir la terminal o consola.
2. Navegar por medio de la terminal hasta entrar en el proyecto y acceder a la carpeta "Proyecto1" ya que aqui se encuentra alojado el proyecto
3. Ejecutar el comando y esperar que finalice el proceso, esto dejará una consola abierta durante la ejecución:

```bash
dotnet run
```

4. Abrir un navegador.
5. Dirigirse a http://localhost:5263/swagger/index.html para revisar la documentacion.

### Utilizando Visual Studio Code (Mac/Linux/Windows)

> Nota: Si tiene instalado Visual Studio en su computadora Windows y la configuracion de .net8, ya esta todo configurado para iniciar.

> Nota 2: Para mac y linux, tiene que tener configurado el ambiente de desarrollo para visual studio code. Utilice esta [guia de instalacion visual studio code](https://learn.microsoft.com/es-es/dotnet/core/install/macos#install-alongside-visual-studio-code) para asegurarse que el entorno esta correctamente configurado y listo.

1. Abra Visual studio code y dirijase a **Archivo>Abrir** y busque el proyecto en su directorio.
2. Al abrirlo en VSCode, la extensión de C# detectará que es un proyecto de este tipo, asi que espere hasta que analice su codigo completamente.
3. En el explorador de archivos de VSCode, abra el archivo Program.cs (La ruta inicial del proyecto) para que pueda ejecutarlo.
4. En la parte superior derecha se encontrará un boton de "Play" (Triangulo con una araña en miniatura). Debe darle click y este levantará una terminal.
5. Durante el proceso abrirá una nueva pestaña en su navegador por defecto con la pagina de swagger con la documentacion del API.

### Utilizando Visual Studio (Windows)

1. Abra Visual Studio en su computadora windows
2. Abra el archivo "Proyecto1.sln" dentro de visual studio code.
3. Espere que su IDE lea todo el proyecto y estructura antes de comenzar.
4. Presione el boton de debug para iniciar el programa.
5. Iniciará el programa abriendo su navegador con una nueva pestaña directamente en la pagina de swagger.
