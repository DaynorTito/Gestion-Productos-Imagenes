// primer ejercicio con three.js
// crear una geometria teniendo en cuenta el orden de los vértices
var camera, scene, renderer;
var cameraControls;
var clock = new THREE.Clock();
var ambientLight, light;


function init() {
	var canvasWidth = window.innerWidth * 0.9;
	var canvasHeight = window.innerHeight * 0.9;

	// CAMERA

	camera = new THREE.PerspectiveCamera( 45, window.innerWidth / window.innerHeight, 1, 80000 );
	camera.position.set(-1,1,3);
	camera.lookAt(0,0,0);

	// LIGHTS

	light = new THREE.DirectionalLight( 0xFFFFFF, 0.7 );
	light.position.set( 1, 1, 1 );
	light.target.position.set(0, 0, 0);
	light.target.updateMatrixWorld()

	var ambientLight = new THREE.AmbientLight( 0x111111 );

	// RENDERER
	renderer = new THREE.WebGLRenderer( { antialias: true } );
	renderer.setSize( canvasWidth, canvasHeight );
	renderer.setClearColor( 0x00000, 1.0 );

	renderer.gammaInput = true;
	renderer.gammaOutput = true;

	// Add to DOM
	var container = document.getElementById('container');
	container.appendChild( renderer.domElement );

	// CONTROLS
	cameraControls = new THREE.OrbitControls( camera, renderer.domElement );
	cameraControls.target.set(0, 0, 0);

	// OBJECT
	
    
        var migeometria = new THREE.Geometry();
	migeometria.vertices.push( new THREE.Vector3( 0.0, 0.0, 0.0 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.3, 0.0, 0.0 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.3, 0.3, 0.0 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.0, 0.0, -0.3 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.0, 0.3, -0.3 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.0, 0.3, 0.0 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.3, 0.0, -0.3 ) );
	migeometria.vertices.push( new THREE.Vector3( 0.3, 0.0, -0.3 ) );

	// alta, baja, cambio, seleccion tablas parmetricas olo mostrar

	
	// migeometria.faces.push( new THREE.Face3( 0, 1, 2 ) );
	// migeometria.faces.push( new THREE.Face3( 0, 2, 5 ) );

	
	// migeometria.faces.push( new THREE.Face3( 3, 5, 4 ) );
	// migeometria.faces.push( new THREE.Face3( 0, 5, 3 ) );

	// migeometria.faces.push( new THREE.Face3( 2, 5, 0 ) );

	// migeometria.faces.push( new THREE.Face3( 0, 3, 6 ) );


	for (let i = 0; i < 7; i++) {
		for (let j = 0; j < 7; j++) {
			for (let k = 0; k < 7; k++) {
				migeometria.faces.push( new THREE.Face3( i, j, k ));
			}
		}
	}
	// migeometria.faces.push( new THREE.Face3( 0, 1, 6 ) );


	// migeometria.faces.push( new THREE.Face3( 3, 4, 6 ) );


		
    
    var material = new THREE.MeshBasicMaterial( { color: 0xF004481 } ); // Material de color rojo
    var miobjeto = new THREE.Mesh (migeometria, material); // Crea el objeto
	
	// SCENE

	scene = new THREE.Scene();
	scene.add( light );
	scene.add( ambientLight );

	scene.add( miobjeto );

}

function animate() {
	window.requestAnimationFrame( animate );
	render();
}

function render() {
	var delta = clock.getDelta();
	cameraControls.update(delta);
	renderer.render( scene, camera );
}

try {
	init();
	animate();
} catch(e) {
	var errorReport = "Your program encountered an unrecoverable error, can not draw on canvas. Error was:<br/><br/>";
	$('#container').append(errorReport+e);
}
