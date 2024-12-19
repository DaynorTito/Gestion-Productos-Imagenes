Pantalla Nota
<br>
-------------
<br>
<?php
$sql="SELECT * from bddaynor.persona where ci=123";
$resultado2=mysqli_query($con, $sql);
$fila2=mysqli_fetch_array($resultado2);
?>
<label>Nombres</label>
<input type="text" value="<?php echo $fila2["nombre"];?>" name="nombres"/>
<br>
<label>Ap.Paterno</label>
<input type="text" value="<?php echo $fila2["paterno"];?>" name="apaterno"/>
