# Sci-All API Gateway

API Gateway desarrollado en .NET 8.0 para gestionar publicaciones utilizando una arquitectura limpia y modular.


## Tecnologías
- .NET 8.0
- Docker
- C#

## Requisitos
1. Tener **Docker** instalado en tu máquina. Puedes descargarlo desde [Docker Desktop](https://www.docker.com/products/docker-desktop/).
2. Editor de código como **Visual Studio Code** o **Visual Studio**.
3. Tener el **SDK de .NET 8.0** (si no usas Docker directamente). Descárgalo desde [aquí](https://dotnet.microsoft.com/).

## Cómo Iniciar el Proyecto
### Opción 1: Usando Docker

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/Noctis-Dev/sci-all-api-gateway.git
   cd sci-all-api-gateway
Construye la imagen de Docker:

bash
Copiar código
docker build -t sci-all-api-gateway .
Ejecuta el contenedor:

bash
Copiar código
docker run -p 8080:80 sci-all-api-gateway
Accede al API Gateway:
Abre http://localhost:8080 en tu navegador.
