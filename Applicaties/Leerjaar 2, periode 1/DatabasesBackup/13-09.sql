-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Gegenereerd op: 13 sep 2023 om 08:40
-- Serverversie: 8.0.31
-- PHP-versie: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `weekend`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `klas`
--

DROP TABLE IF EXISTS `klas`;
CREATE TABLE IF NOT EXISTS `klas` (
  `KlasID` int NOT NULL AUTO_INCREMENT,
  `klas` int NOT NULL,
  `leeraar` int NOT NULL COMMENT 'is userID',
  PRIMARY KEY (`KlasID`),
  KEY `leeraar` (`leeraar`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `role` int NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `score`
--

DROP TABLE IF EXISTS `score`;
CREATE TABLE IF NOT EXISTS `score` (
  `ScoreID` int NOT NULL AUTO_INCREMENT,
  `userID` int NOT NULL,
  `score` int NOT NULL,
  `date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ScoreID`),
  KEY `userID` (`userID`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `RoleID` int NOT NULL,
  `KlasID` int DEFAULT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL,
  `name` text NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `RoleID` (`RoleID`),
  KEY `KlasID` (`KlasID`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `klas`
--
ALTER TABLE `klas`
  ADD CONSTRAINT `klas_ibfk_1` FOREIGN KEY (`leeraar`) REFERENCES `user` (`UserID`);

--
-- Beperkingen voor tabel `score`
--
ALTER TABLE `score`
  ADD CONSTRAINT `score_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `user` (`UserID`);

--
-- Beperkingen voor tabel `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`RoleID`) REFERENCES `role` (`RoleID`),
  ADD CONSTRAINT `user_ibfk_2` FOREIGN KEY (`KlasID`) REFERENCES `klas` (`KlasID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
