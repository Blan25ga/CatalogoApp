
## 📦 CatalogoApp - Sistema de Gestión de Inventario Comercial

[![.NET Framework](https://shields.io)](https://microsoft.com)
[![SQL Server](https://shields.io)](https://microsoft.com)
[![Language](https://shields.io)](https://microsoft.com)

**CatalogoApp** es una solución de escritorio genérica diseñada para la administración y persistencia de catálogos de artículos comerciales. El sistema funciona como un núcleo centralizado de carga de datos, estructurado de forma abstracta para que la información persistida pueda ser consumida externamente por servicios web, plataformas de e-commerce o aplicaciones móviles.

---

## 🚀 Características Principales & Alcance

La aplicación cubre el ciclo completo de administración de datos mediante una interfaz gráfica intuitiva, garantizando la integridad de la información técnica y comercial de los productos:

*   **Gestión Completa (CRUD):** Alta, baja, modificación y lectura detallada de artículos comerciales.
*   **Búsqueda Avanzada:** Motor de filtrado parametrizado por múltiples criterios combinados para optimizar la localización de registros.
*   **Asignación Dinámica:** Vinculación relacional de artículos con marcas y categorías mediante controles desplegables dinámicos alimentados desde la base de datos.
*   **Soporte Multimedia:** Visualización dinámica de imágenes de productos integrada en la interfaz de usuario.
*   **Consistencia Financiera:** Manejo preciso de datos numéricos y de precisión para el control de precios.

---

## 🏗️ Arquitectura del Sistema & Buenas Prácticas

El proyecto fue construido aplicando estándares profesionales de ingeniería de software para asegurar la escalabilidad, el mantenimiento y el desacoplamiento de componentes:

### 1. Arquitectura en Capas (N-Tier Architecture)
*   **Capa de Presentación (UI):** Formularios Windows Forms, encargados exclusivamente de la interacción con el usuario, captura de eventos y renderizado de componentes visuales.
*   **Capa de Negocio / Servicios:** Componentes lógicos que procesan las peticiones de la interfaz, ejecutan las reglas del negocio y coordinan la comunicación con la base de datos.
*   **Capa de Dominio / Modelos:** Clases que representan las entidades del mundo real (`Articulo`, `Marca`, `Categoria`) con tipado fuerte y Programación Orientada a Objetos (POO).

### 2. Robustez y Seguridad
*   **Manejo Global de Excepciones:** Bloques de contingencia estructurados para capturar fallos de conectividad o errores en tiempo de ejecución, evitando caídas inesperadas de la aplicación y ofreciendo feedback limpio al usuario.
*   **Capa de Validación:** Validaciones lógicas previas a la persistencia para asegurar que los campos requeridos (Códigos, Precios, Nombres) cumplan con los formatos y restricciones del modelo de datos.

---

## 🗄️ Modelo de Datos (Estructura del Objeto)

Cada artículo persistido en **SQL Server** está compuesto de forma estricta por los siguientes atributos:
*   **Código de Artículo:** Identificador alfanumérico único.
*   **Nombre:** Identificación comercial del producto.
*   **Descripción:** Detalle de características técnicas o comerciales.
*   **Marca & Categoría:** Entidades relacionales normalizadas en la base de datos.
*   **Imagen:** Puntero de recurso (URL/Ruta) para renderizado dinámico.
*   **Precio:** Valor monetario con precisión decimal.

---

## ⚙️ Instalación y Configuración

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/Blan25ga/CatalogoApp.git
   ```
2. **Preparar la Base de Datos:**
   * Ejecutar el script SQL adjunto en el proyecto dentro de tu instancia de SQL Server para levantar las tablas de artículos, marcas y categorías.
3. **Configurar la Conexión:**
   * Abrir el proyecto en Visual Studio.
   * Modificar el archivo `App.config` con los datos de tu servidor local:
     ```xml
     <connectionStrings>
         <add name="CadenaConexion" connectionString="Server=TU_SERVIDOR;Database=CATALOGO_DB;Trusted_Connection=True;" providerName="System.Data.SqlClient" />
     </connectionStrings>
     ```
4. **Compilar y Ejecutar:** Presionar `F5` en Visual Studio para iniciar el sistema.
