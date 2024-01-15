<?php
session_start();
?>
<html>
    <head>
        <title>Gilde DevOps Groep 4 | Home</title>
        <link rel="stylesheet" href="Home.css">        
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
                document.getElementById("mySidebar").style.width = "14%";
                document.getElementById("Menu").style.marginRight = "250px";
            }

            /* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
            function closeNav() {
                document.getElementById("mySidebar").style.width = "0";
                document.getElementById("Menu").style.marginRight = "0";
            } 
        </script>

        <div class="top_bar">
            <h1 class="page_name"><b>Home</b></h1>
            <img src="LogoP4.png" class="logo"></img>
        </div>

          
        <div class="Introductory_Message">
            <h1 style="font-size:72px;"><b>Welkom</b></h1>
            <p style="font-size:32px;">op onze website. Wij zijn Lars, Mike, Sjoerd en Tom. Op deze site kun je inloggen, jouw uren registreren en inzien</p>
        </div>
    </body>
</html>