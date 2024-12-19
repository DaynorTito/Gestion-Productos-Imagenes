<?php
include "conexion.inc.php";

$con = new conexion();
$pdo = $con->obtieneConexion();

$sql = "select * from persona";
$entradas = []
if (isset( $_GET["ci"])) {
    $ci = $_GET["ci"];
    $sql = "where ci=:ci";
    $entradas[":ci"] = $_GET["ci"];
}


// como evitar
// que tanto mejora en este caso
$stmt = $pdo->prepare($sql);
//$stmt->execute();
$stmt->execute($entradas);
$stmt->setFetchMode(PDO::FETCH_ASSOC);
echo json_encode($stmt->fetchAll());
?>