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

function validate($data){
trim($data);
stripslashes($data);
htmlspecialchars($data);
return $data;
}
?>
<html>
    <head>
        <title>Gilde DevOps Groep 4 | Log In</title>
        <link rel="stylesheet" href="Login.css">        
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
            <h1 class="page_name"><b>Log In</b></h1>
        </div>

        <form id="form" method="post" action="">
            <label id="SplashTxt">Log In</label><br/><br/>
            <label>Email: </label><input id="Email" type="email" name="email" value=""></input><br/><br/>
            <label>Password: </label><input id="Password" type="password" name="password" value=""></input><br/><br/>
            <input id="submit" type="submit" action="submit" value="Log In">
        </form>
        <?php
        if ($_SERVER["REQUEST_METHOD"] == "POST") {
            $Email = validate($_POST["email"]);
            $Password = validate($_POST["password"]);
            $sql = "SELECT GebruikersNaam, Email, Wachtwoord FROM login WHERE Email = :email";
            $q = $conn->prepare($sql);
            $q->bindParam(":email",$Email);
            $q->execute();
            $r = $q->fetch();
            if($q->rowCount() == 1){
                $Hash = $r['Wachtwoord'];
                if(password_verify($Password, $Hash)){
                    $Username = $r['GebruikersNaam'];
                    $_SESSION["Username"] = $Username;
                    if(!empty($_SESSION)){
                        header("location: Home.php");
                        exit;
                    }
                    else{
                        header("location: Login.php");
                        exit;
                    }
                }
            }
            else{
               echo '<p class="error">Incorrecte Email/Wachtwoord Combinatie. Probeer het opnieuw</p>';
            }
        }
        ?>
    </body>
</html>