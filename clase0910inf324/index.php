<?php
include "conexion.inc.php";

$con = new conexion();
$pdo = $con->obtieneConexion();

echo $_SERVER['REQUEST_METHOD']; 

if ($_SERVER['REQUEST_METHOD'] == "GET") {
    $sql = "select * from persona";
    $entradas = [];

    if (isset($_GET["ci"])) {
        $sql = "where ci =:ci";
        $entradas[":ci"] = $_GET["ci"];
    }
    $stmt = $pdo->prepare($sql);
    // $stmt->execute();
    $stmt->execute($entradas);
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    header("HTTP/1.1 200 ok");
    echo json_encode($stmt->fetchAll());
    exit;

}

if ($_SERVER["REQUEST_METHOD"] =="POST") {
    header("HTTP/1.1 200 ok");
    echo post;
    exit;
}
?>