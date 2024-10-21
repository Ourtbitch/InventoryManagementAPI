

```
# Backend

## Descripción
Este proyecto es un sistema de gestión de inventarios y productos, centrado principalmente en la gestión de cervezas. Está construido utilizando **ASP.NET Core** como framework backend, con **Entity Framework Core** para la interacción con la base de datos, y se integra con **procedimientos almacenados** para optimizar ciertas consultas.

## Características
- API RESTful para la gestión de productos y cervezas.
- Soporte para procedimientos almacenados en la base de datos.
- Arquitectura por capas que separa la lógica de negocio (servicios) del acceso a datos (repositorios).
- Implementación de validaciones con **FluentValidation**.
- Conexión a bases de datos **SQL Server**.

## Tecnologías utilizadas
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **FluentValidation**
- **AutoMapper**
- **Git** para control de versiones.

## Requisitos previos
Antes de ejecutar este proyecto, asegúrate de tener instalados los siguientes programas:
- **Visual Studio 2019/2022** o posterior.
- **SQL Server**.
- **.NET Core SDK 6.0** o posterior.

## Instalación y configuración
1. Clona este repositorio en tu máquina local:
   ```bash
   git clone https://github.com/YourUsername/YourRepositoryName.git
   ```

2. Configura la cadena de conexión en el archivo `appsettings.json` para que apunte a tu servidor de base de datos local:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server_name;Database=Store;User Id=your_username;Password=your_password;"
     }
   }
   ```

3. Aplica las migraciones de la base de datos para crear las tablas:
   ```bash
   Update-Database
   ```

4. Ejecuta la aplicación desde Visual Studio o con el siguiente comando:
   ```bash
   dotnet run
   ```

## Endpoints principales

### Obtener todas las cervezas
```http
GET /api/beer
```

### Obtener una cerveza por ID
```http
GET /api/beer/{id}
```

### Buscar cervezas por nombre (con o sin procedimiento almacenado)
```http
GET /api/beer/search?name=corona&useStoredProcedure=false
```

### Crear una nueva cerveza
```http
POST /api/beer
```
```json
{
  "name": "Corona",
  "brandId": 1,
  "alcohol": 4.5
}
```

### Actualizar una cerveza
```http
PUT /api/beer/{id}
```

### Eliminar una cerveza
```http
DELETE /api/beer/{id}
```

## Contribuciones
Las contribuciones a este proyecto son bienvenidas. Si tienes alguna sugerencia o encuentras un error, por favor abre un *issue* o envía un *pull request*.

## Licencia
Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.
```
