<!DOCTYPE html>
<html>
    <head>
        <title>Start Page</title>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    </head>
    <body>
    <center>
        <h1>Mi Ciclo For</h1>
    
        <form action="index.php" method="get">
            <h3><label for="num">Ingrese un número:  </label></h3>
            <input type="number" id="num" name="num" required><br><br>
            <input type="submit" value="Generar">
        </form>
        
        <h3>CICLO FOR</h3>
        <table border="1">
            <tr>
                <th>_Valor de i_</th>
            </tr>
            <?php
                if (isset($_GET['num'])) {
                    $num = intval($_GET['num']);
                    for ($i = 0; $i < $num; $i++) {
                        echo "<tr><td><center>" . $i . "</center></td></tr>";
                    }
                }
            ?>
        </table>
    </center>
    </body>
</html>
