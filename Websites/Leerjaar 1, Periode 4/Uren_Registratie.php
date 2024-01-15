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
     "Connected successfully <br/>";
} catch(PDOException $e) {
     "Connection failed: " . $e->getMessage();
}
?>
<html>
    <head>
        <title>Gilde DevOps Groep 4 | Home</title>
        <link rel="stylesheet" href="Uren_Registratie.css">        
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
                echo '<h5 class="Success">U bent ingelogd als: </h5>';
                echo '<h5 class="User">'.$_SESSION["Username"].'</h5>';
                echo '<a href="LogOut.php" class="LogOut">Log Uit</a>';
              }
              else{
                echo '<a href="LogIn.php" class="LogIn">Log In</a>';
                echo '<a href="SignUp.php" class="SignUp">Registreer</a>';
              }
              ?>
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
            <h1 class="page_name"><b>Uren Registratie</b></h1>
          </div>

          <div class="form">
            <h2 id="FormNaam">Uren Registratie</h2>
            <form method="post" action="">
              Naam: 
              <select id="Medewerker" name="Medewerkers" value="">
                <option value="1">Tom Backes</option>
                <option value="2">Sjoerd van Veen</option>
                <option value="3">Lars van den Hoef</option>
                <option value="4">Mike Zhang</option>
              </select>
              <br/><br/>
                
              Project Naam: 
              <select id="Projecten" name="Project" value="">
                  <option value="1">ERP-Systeem</option>
                  <option value="2">Professionele infrastructuur</option>
                  <option value="3">Toolkit</option>
              </select>
              <br/><br/>
                
              Gewerkte Uren: 
                <input id="GewerkteUren" type="double" name="GewerkteUren" value=""></input>
              <br/><br/>

              Datum: 
                <input id="Datum" type="date" name="Datum"></input>
              <br/><br/>

              <input id="Submit" type="submit" name="submit" value="Toevoegen"></input>
            </form>
            <?php
            if ($_SERVER["REQUEST_METHOD"] == "POST") {
              $Naam = $_POST["Medewerkers"];
              $Project = $_POST["Project"];
              $GewerkteUren = $_POST["GewerkteUren"]; 
              $Datum = ($_POST["Datum"]);              
              // Prepare the INSERT query
              $q = $conn->prepare("INSERT INTO urenoverzicht (MedewerkerID, ProjectID, GewerkteUren, Datum) VALUES ('$Naam', '$Project', '$GewerkteUren', '$Datum')");
              //execute the statement
              $exe = $q->execute();
              if ($exe == true){
                header("Location: Uren_Overzicht.php");
                exit();
              }
              else{
                echo"<p class=\"error\">ERROR | Er is iets fout gegaan, controleer de invoer waardes en probeer het opnieuw.<br/> blijft dit zich voordoen, neem contact op met een developer</p>";
              }
            }
            
            ?>
    </body>
</html>
