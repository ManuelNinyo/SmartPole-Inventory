1. **Descargar y añadir modelo YOLOv8n ONNX:**
   - Descargaré un modelo preentrenado de YOLOv8n en formato ONNX.
   - Lo añadiré a la carpeta `Resources/Raw` de `SmartPole.Inventory.App` para que esté disponible en la aplicación móvil.

2. **Añadir dependencias necesarias:**
   - Añadiré paquetes como `Microsoft.ML.OnnxRuntime` u otros necesarios para ejecutar modelos ONNX en la aplicación móvil (.NET MAUI).

3. **Crear el servicio de detección YOLO (`IYoloDetectionService` y `YoloDetectionService`):**
   - En `SmartPole.Inventory.MobileCore/Services`, crearé un servicio para cargar el modelo ONNX, preprocesar las imágenes tomadas por el usuario, realizar la inferencia y postprocesar las cajas delimitadoras (bounding boxes) para devolver los resultados (etiquetas como cables, transformadores, etc., y confidencias).

4. **Crear una nueva vista y ViewModel para la Inspección de Inventario:**
   - En `SmartPole.Inventory.App/Views`, crearé `InventoryInspectionPage.xaml`.
   - En `SmartPole.Inventory.MobileCore/ViewModels`, crearé `InventoryInspectionViewModel.cs`.
   - La vista permitirá abrir la cámara o seleccionar una foto, mostrar la imagen, llamar al servicio YOLO y mostrar los resultados (elementos detectados).
   - Añadiré navegación a esta nueva página desde `MainPage` o `AppShell`.

5. **Actualizar el dominio y modelos de persistencia:**
   - En `SmartPole.Inventory.Domain/Entities/Entities.cs`, añadiré una entidad `InventoryItem` asociada a `SmartPole` o `Inspection` para representar los elementos detectados (Cables, Transformadores).
   - Actualizaré los modelos equivalentes en `SmartPole.Inventory.MobileCore/Models` y la lógica de base de datos local (SQLite) en `SmartPole.Inventory.MobileCore/Persistence` para almacenar estos inventarios.

6. **Actualizar el backend (WebAPI y Application):**
   - Crearé/actualizaré los Commands en el backend (ej. `AddInventoryItemsCommand`) para recibir los elementos detectados (y opcionalmente la imagen) y guardarlos en PostgreSQL/MongoDB.
   - Añadiré un endpoint en `SmartPoleController` o `SyncController` para sincronizar estos elementos.

7. **Sincronización:**
   - En el ViewModel, añadiré lógica para guardar localmente y luego intentar enviar los datos de inventario y la imagen al backend.

8. **Pre commit steps:**
   - Completar los pasos de pre-commit para asegurar que el código compila, los tests pasan (si existen o si es necesario crear básicos), y no hay problemas de estilo.

9. **Submit:**
   - Enviar los cambios con un mensaje claro.
