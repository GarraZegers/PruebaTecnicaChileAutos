# PruebaTecnicaChileAutos
Prueba técnica solicitada por el área de reclutamiento para avanzar en el proceso de postulación al cargo de desarrollador de aplicaciones en ChileAutos.


# Rick and Morty App

Este proyecto consume datos desde una API propia que a su vez integra con la [API pública de Rick and Morty](https://rickandmortyapi.com/api). 
Está dividido en dos partes:

- **Frontend:** Angular 19
- **Backend:** ASP.NET 8 (con C#)

---

## Tecnologías usadas

- Angular 19 (con Lazy Loading y componentes standalone)
- .NET 8 (ASP.NET Minimal API)
- Docker (solo para el backend)
- CSS puro (sin frameworks)
- RxJS, HttpClient, etc.

---

## Cómo levantar el proyecto

### Requisitos

- Node.js 20+
- .NET 8 SDK
- Docker (opcional, solo para backend)

---

### Backend

Para levantar el backend:

```bash
cd backend
docker compose up --build
```


##Esto dejará el backend disponible en:

http://localhost:8080

##La documentación Swagger estará en:

http://localhost:8080/swagger

**En caso de no usar Docker, puedes abrir el backend con Visual Studio y ejecutarlo directamente.

## Frontend
Para levantar el frontend:

```
cd frontend/rick-and-morty-app --> se situa en la carpeta del proyecto
npm install --> instala las dependencias del proyecto
npm start --> ejecuta el script en package.json que a su vez hace un ng serve
```

Disponible en:

http://localhost:4200
Asegúrate de que el archivo src/environments/environment.ts tenga esta línea:

apiBaseUrl: 'http://localhost:8080/api'

## Funcionalidades
- Vista de episodios con paginación y filtros

- Vista de personajes con imagen y filtros (género, especie, estado)

- Vista de lugares con paginación

- Detalle de episodio con botón para cargar la imagen de los personajes

- Navegación entre secciones desde un menú común

- Estilos responsive y scroll limitado verticalmente

## Notas adicionales
El backend expone los datos de episodios, personajes y lugares mediante endpoints REST (todos con CORS configurado).
