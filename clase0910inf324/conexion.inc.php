<?php 
class conexion
{
	private $pdo;
	
	public function __construct()
	{
		try {
			$this->pdo = new PDO("mysql:dbname=BDDaynor;host=127.0.0.1", "root", "");
			$this->pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
		}
		catch (PDOException $e) {
			echo $e->getMessage();
		}
	}
	
	public function obtieneConexion()
	{
		return $this->pdo;
	}
}
?>