<?php
session_start();
$host = "localhost";
$dbname = "uren_schema";
$username = "root";
$password = "";
try {
    $conn = new PDO("mysql:host=$host;dbname=$dbname", $username, $password);
    // set the PDO error mode to exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    echo "Connected successfully <br/>";

    if (isset($_GET['id'])) {
        $ID = $_GET['id'];
        try {
            $sql = "DELETE FROM urenoverzicht WHERE ID = :id";
            $q = $conn->prepare($sql);
            $q->bindValue(':id', $ID);
            $exe = $q->execute();
            if ($exe == true) {
                echo "Record deleted successfully <br/>";
                header("Location: Uren_Overzicht.php");
                exit();
            }
        } catch (PDOException $e) {
            echo "Er ging iets fout tijdens het verwijderen. Herlaad de pagina. Als dit zich blijft voordoen, neem dan contact op met een Admin.<br/>" . $e->getMessage();
        }
    } else {
        echo "Sql ID niet gespecificeerd. Voeg \"?id=[het ID van de rij die verwijderd moet]\" toe aan de URL. Blijft dit zich voordoen, neem dan contact op met een Admin.";
    }
} catch (PDOException $e) {
    die("Connection failed: " . $e->getMessage());
}
?>
