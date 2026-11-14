\# Sistema de Control de Servicios de Fotografía



Proyecto académico desarrollado en C# con ASP.NET Core Web API, Entity Framework Core y SQL Server.



\## Descripción



Sistema para gestionar clientes, fotógrafos, paquetes fotográficos, sesiones, pagos y entregas.



Incluye reglas de negocio para evitar clientes duplicados, conflictos de horarios y pagos superiores al saldo pendiente.



\## Estructura del proyecto



La solución contiene tres proyectos:



\- \*\*SistemaFotografia.API:\*\* controladores REST, DTOs y configuración de la API.

\- \*\*Fotografia.Domain:\*\* entidades e interfaces de repositorios.

\- \*\*Fotografia.Infrastructure:\*\* acceso a datos, repositorios, servicios y migraciones.



\## Funcionalidades



\- Registro de clientes y validación de duplicados.

\- Programación, reprogramación y cancelación de sesiones.

\- Validación de disponibilidad de fotógrafos.

\- Registro de anticipos y pagos parciales.

\- Consulta de saldos pendientes.

\- Programación y seguimiento de entregas.

\- Reportes de ingresos, sesiones por fotógrafo y pagos pendientes.



\## Tecnologías



\- C#

\- .NET 10

\- ASP.NET Core Web API

\- Entity Framework Core

\- SQL Server LocalDB

\- Swagger / OpenAPI

\- Git y GitHub



\## Requisitos



\- Visual Studio compatible con .NET 10.

\- SDK de .NET 10.

\- SQL Server LocalDB.

\- Herramientas de Entity Framework Core.



\## Abrir el proyecto



1\. Clonar el repositorio desde GitHub.

2\. Abrir el archivo `API/API/SistemaFotografia.slnx` en Visual Studio.

3\. Restaurar los paquetes NuGet.

4\. Configurar la cadena `FotografiaConnection` de `appsettings.json`.

5\. Crear o actualizar la base de datos mediante las migraciones de Entity Framework Core, una vez verificadas.

6\. Ejecutar `SistemaFotografia.API`.

7\. Abrir Swagger para probar los endpoints.



\## Base de datos



La base de datos utilizada es `FotografiaDB`.



Contiene las entidades:



\- Clientes

\- Fotografos

\- Paquetes

\- Sesiones

\- Pagos



La estructura de la base de datos se administra mediante migraciones de Entity Framework Core.



\## Estado



Las funcionalidades principales fueron probadas manualmente utilizando Swagger y SQL Server. Las pruebas unitarias automatizadas y la verificación de una instalación limpia quedan pendientes.