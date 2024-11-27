// Evento para manejar la carga del archivo
document.getElementById("uploadForm").addEventListener("submit", async function (event) {
    event.preventDefault();  // Evitar el envío tradicional del formulario

    const fileInput = document.getElementById("pointsFile");

    // Verificar si el archivo fue seleccionado
    if (fileInput.files.length > 0) {
        const file = fileInput.files[0];
        const reader = new FileReader();

        reader.onload = async function (e) {
            const fileContent = e.target.result;  // Obtener el contenido del archivo

            try {
                const pointsData = JSON.parse(fileContent);  // Convertir el contenido a JSON

                if (pointsData && pointsData.Points && Array.isArray(pointsData.Points)) {
                    // Enviar los puntos a la API para generar el modelo 3D
                    await sendPointsToApi(pointsData.Points);
                } else {
                    alert("El archivo JSON no tiene la estructura esperada.");
                }
            } catch (error) {
                alert("Error al procesar el archivo JSON: " + error.message);
            }
        };

        reader.readAsText(file);  // Leer el archivo como texto
    }
});

// Función para enviar los puntos al backend
async function sendPointsToApi(points) {
    try {
        // Enviar la solicitud POST con los puntos en formato JSON
        const response = await fetch('https://localhost:7298/api/pointcloud/upload', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ Points: points })  // Convertir los puntos en JSON
        });

        if (!response.ok) {
            throw new Error('Error al generar el modelo 3D');
        }

        // Obtener el archivo OBJ generado
        const blob = await response.blob();
        const objectURL = URL.createObjectURL(blob);
        displayModel(objectURL);  // Función para visualizar el modelo 3D
    } catch (error) {
        alert(error.message);
    }
}

// Función para visualizar el modelo 3D usando Three.js
function displayModel(fileUrl) {
    // Crear escena y cámara de Three.js
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(75, window.innerWidth / 500, 0.1, 1000);
    const renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, 500);
    document.getElementById("modelViewer").appendChild(renderer.domElement);

    // Cargar archivo OBJ usando OBJLoader
    const loader = new THREE.OBJLoader();
    loader.load(fileUrl, function (object) {
        scene.add(object);  // Añadir el objeto cargado a la escena
    }, undefined, function (error) {
        console.error(error);
    });

    // Agregar luz ambiental
    const light = new THREE.AmbientLight(0x404040);  // Luz suave
    scene.add(light);

    // Posicionar la cámara
    camera.position.z = 5;

    // Animar la escena
    function animate() {
        requestAnimationFrame(animate);
        renderer.render(scene, camera);  // Renderizar la escena
    }
    animate();  // Iniciar la animación
}
