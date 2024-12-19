<?php
include "conectar.inc.php";
$flujo=$_GET["flujo"];
$proceso=$_GET["proceso"];
$pantalla=$_GET["pantalla"];

include $pantalla."Up.inc.php";

if (isset($_GET["Siguiente"]))
{
	$sql="SELECT * from flujoproceso where flujo='$flujo' and proceso='$proceso'";
	$resultado=mysqli_query($con, $sql);
	$fila=mysqli_fetch_array($resultado);
	$siguiente=$fila["siguiente"];
}
if (isset($_GET["Anterior"]))
{
	$sql="SELECT * from flujoproceso where flujo='$flujo' and siguiente='$proceso'";
	$resultado=mysqli_query($con, $sql);
	$fila=mysqli_fetch_array($resultado);
	$siguiente=$fila["proceso"];
}
header("Location: flujo.php?flujo=$flujo&proceso=$siguiente");
?>