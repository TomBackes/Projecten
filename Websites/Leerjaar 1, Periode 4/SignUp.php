<?php
session_start();
unset($_SESSION['Username']);
session_destroy();
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

function validate($data){
trim($data);
stripslashes($data);
htmlspecialchars($data);
return $data;
}
?>
<html>
    <head>
        <title>Gilde DevOps Groep 4 | Home</title>
        <link rel="stylesheet" href="SignUp.css">        
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
            <h1 class="page_name"><b>Account Aanmaken</b></h1>
        </div>

        <div class="Introductory_Message">
            <!--h1 style="font-size:72px;color:#c40000"><b>&#x26A0; Error &#x26A0;</b></h1>
            <p style="font-size:32px;">Deze pagina is momenteel niet functioneel, probeer het later opnieuw.</p-->
        </div>

        <div class="form">
            <h2 id="FormNaam">Account Aanmaken</h2>
            <form method="post" action="">
                <label>Gebruikers Naam: </label>
                <input id="InputInfo" type="text" name="Username" value=""></input>
                <br/><br/>
                <label>Wachtwoord: </label>
                <input id="InputInfo" type="password" name="Password" value=""></input>
                <br/><br/>
                <label>E-mail: </label>
                <input id="InputInfo" type="email" name="Email" value=""></input>
                <br/><br/>
                <input id="Submit" type="submit" name="submit" value="Creëer Account!"></input>
            </form>

        
        
        <?php
            if($_SERVER["REQUEST_METHOD"] == "POST"){
                $Username = validate($_POST["Username"]);
                $Password = validate($_POST["Password"]);
                $Email = validate($_POST["Email"]);
                $HashedPassword = password_hash($Password, PASSWORD_DEFAULT);
                $v = "SELECT Email FROM login";
                $e = $conn->prepare($v);
                $e->execute();
                while ($f = $e -> fetch()){
                    if ($Email == $f['Email']){
                        echo '<p class="error">Er bestaat al een account met dit email adres. Voer een ander email adres in.</p>';
                        exit;
                    }
                }
                $sql = "INSERT INTO login(Email, GebruikersNaam, Wachtwoord) VALUES ('$Email','$Username','$HashedPassword')";
                $q = $conn->prepare($sql);
                $q->execute();
                $_SESSION["Username"] = $Username;
                if(!empty($_SESSION)){
                    header("location: Home.php");
                    exit;
                }
                
            }
        ?>
    </body>
</html>