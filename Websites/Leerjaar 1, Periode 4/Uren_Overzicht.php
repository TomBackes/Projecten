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
} catch(PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}
?>
<html>
    <head>
        <title>Gilde DevOps Groep 4 | Uren Overzicht</title>
        <link rel="stylesheet" href="Uren_Overzicht.css">       
        <link rel="icon" type="img/x-icon" href="Site badge.png">
    </head>
    <body>
        <div id="mySidebar" class="sidebar">
            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
            <a href="Home.php">Home</a>
            <a href="Uren_Registratie.php">Uren Registratie</a>
            <a href="Uren_Overzicht.php">Uren Overzicht</a>
            <p>
              <?php
              if(count($_SESSION) > 0){
                echo '<h5 class="User"><span style="color:#DE006D">U bent ingelogd als: </span>'.$_SESSION["Username"].'</h5>';
                echo '<a href="LogOut.php" class="LogOut">Log Uit</a>';
              }
              else{
                echo '<a href="LogIn.php" class="LogIn">Log In</a>';
              }
              ?>
              <a href="SignUp.php" class="SignUp">Registreer</a>
            </p>
        </div>
          
        <div id="Menu">
            <button class="openbtn" onclick="openNav()">&#9776; Menu</button>
        </div>

        <script lang="Java">
            function openNav() {
                document.getElementById("mySidebar").style.width = "250px";
                document.getElementById("Menu").style.marginRight = "250px";
            }

            /* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
            function closeNav() {
                document.getElementById("mySidebar").style.width = "0";
                document.getElementById("Menu").style.marginRight = "0";
            } 
        </script>

        <div class="top_bar">
            <h1 class="page_name"><b>Uren Overzicht</b></h1>
        </div>

        <form method="Post">
            <select id="SortSelect" name="MedewerkerID" value="">
                <option value="">Medewerker</option>
                <option value="1">Tom Backes</option>
                <option value="2">Sjoerd van Veen</option>
                <option value="3">Lars van den Hoef</option>
                <option value="4">Mike Zhang</option>
            </select>
            <select id="SortSelect" name="ProjectID" value="">
                <option value="">Project</option>
                <option value="1">ERP-Systeem</option>
                <option value="2">Professionele infrastructuur</option>
                <option value="3">Toolkit</option>
            </select>
            <input id="SortSelect" type="date" name="Datum" value=""></input>
            <input id="Sort" type="submit" name="submit" value="Volgende"></input>
        </form>
        <button id="Sort" value="Reset" onClick="window.location.reload();"></button>
        <form method="Post">
            <input id="Excel" type="submit" name="Excel" value="Convert to PDF file"></input>
        </form>
        <?php
        if($_SERVER["REQUEST_METHOD"] == "POST"){
            $MedewerkerID = isset($_GET['MedewerkerID']) ? $_GET['MedewerkerID'] : '';
            $ProjectID = isset($_GET['ProjectID']) ? $_GET['MedewerkerID'] : '';
            $Datum = isset($_GET['MedewerkerID']) ? $_GET['MedewerkerID'] : '';
            try{
                    $sql = "SELECT uo.ID, uo.MedewerkerID, project.ProjectID, medewerkers.Voornaam, medewerkers.Achternaam, project.ProjectNaam, uo.GewerkteUren, uo.Datum FROM urenoverzicht AS uo
                    JOIN medewerkers ON medewerkers.MedewerkerID = uo.MedewerkerID
                    JOIN project ON project.ProjectID = uo.ProjectID
                    ORDER BY uo.ID ASC";
                    // prepare statement for execution
                    $q = $conn->prepare($sql);
                    $q->execute();
                    $q->setFetchMode(PDO::FETCH_ASSOC);
                    
                // print out the result set
                ob_start();
                echo "<table>";
                echo "<tr class=\"box\"><th>Id</th><th>Voornaam</th><th>Achternaam</th><th>Project</th><th>Gewerkte uren</th><th>Datum</th></tr>";
                while ($r = $q->fetch()) {
                    echo '<tr><td>';
                    echo sprintf($r['ID']);
                    echo '</td><td>';
                    echo sprintf($r['Voornaam']);
                    echo '</td><td>';
                    echo sprintf($r['Achternaam']);
                    echo '</td><td>';
                    echo sprintf($r['ProjectNaam']);
                    echo '</td><td>';
                    echo sprintf($r['GewerkteUren']);
                    echo '</td><td>';
                    echo sprintf($r['Datum']);
                    echo '</td><td>';
                    echo '<button class="edit"><a href="edit.php?ID=' . $r['ID'] . '&MedwerkerID=' . $r['MedewerkerID'] . '&ProjectID=' . $r['ProjectID'] . '&GewerkteUren=' . $r['GewerkteUren'] . '&Datum=' . $r['Datum'] . '" class="edit">Edit</a></button>';
                    echo '&nbsp;&nbsp;';
                    echo '<button class="delete"><a href="delete.php?id=' . $r['ID'] . '" class="delete">Delete</a></button>';
                    echo '</td></tr><br/>';    
                }
                echo "</table>";
                $PDF = ob_get_contents();
            } catch (PDOException $e) {
                die("Something went wrong. Reload the page. If this doesnt help, contact an Admin.<br/>" . $e->getMessage());
            }

        }
        
        
            //Convert to Excel file
            if (isset($_POST['Excel'])) {
                ob_start();
                require('fpdf185/fpdf.php');
                $pdf = new FPDF();
                $pdf->AddPage();
                // code for print Heading of tables
                $pdf->SetFont('Arial','B',12);
                $ret ="SELECT * FROM login";
                $query1 = $conn -> prepare($ret);
                $query1->execute();
                $header=$query1->fetchAll(PDO::FETCH_OBJ);
                $cnt=1;
                if($query1->rowCount() > 0)
                {
                foreach($header as $heading) {
                foreach($heading as $column_heading)
                $pdf->Cell(46,12,$column_heading,1);
                }}
                //code for print data
                $sql = "SELECT * from urenoverzicht ";
                $query = $conn -> prepare($sql);
                $query->execute();
                $results=$query->fetchAll(PDO::FETCH_OBJ);
                $cnt=1;
                if($query->rowCount() > 0)
                {
                foreach($results as $row) {
                $pdf->SetFont('Arial','',12);
                $pdf->Ln();
                foreach($row as $column)
                $pdf->Cell(46,12,$column,1);
                } }
                $pdf->Output();
                ob_end_flush();

            }
        

 
          
        ?>
    </body>
</html>