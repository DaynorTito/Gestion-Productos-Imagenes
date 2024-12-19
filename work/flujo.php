<?php
include "conectar.inc.php";
$flujo=$_GET["flujo"];
$proceso=$_GET["proceso"];
$sql="SELECT * from flujoproceso where flujo='$flujo' and proceso='$proceso'";
$resultado=mysqli_query($con, $sql);
$fila=mysqli_fetch_array($resultado);
$pantalla=$fila["pantalla"];
?>
<html>
	<head>
	</head>
	<body>
		<form action="controlador.php" method="get">
			<?php
			include $pantalla.".inc.php";
			?>
			<br/>
			<input type="hidden" value="<?php echo $pantalla;?>" name="pantalla"/>
			<input type="hidden" value="<?php echo $flujo;?>" name="flujo"/>
			<input type="hidden" value="<?php echo $proceso;?>" name="proceso"/>
			<br/>
			<input type="submit" value="Anterior" name="Anterior"/>
			<input type="submit" value="Siguiente" name="Siguiente"/>
		</form>
	</body>
<html>