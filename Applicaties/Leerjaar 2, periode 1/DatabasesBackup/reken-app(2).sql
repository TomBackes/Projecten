-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Gegenereerd op: 10 okt 2023 om 07:11
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
-- Database: `reken-app`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `account`
--

DROP TABLE IF EXISTS `account`;
CREATE TABLE IF NOT EXISTS `account` (
  `AccountID` int NOT NULL AUTO_INCREMENT,
  `RolID` int NOT NULL,
  `Gebruikersnaam` varchar(100) NOT NULL,
  `wachtwoord` varchar(1000) NOT NULL,
  `email` varchar(100) NOT NULL,
  `voornaam` varchar(100) NOT NULL,
  `achternaam` varchar(100) NOT NULL,
  PRIMARY KEY (`AccountID`),
  KEY `RolID` (`RolID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=ascii;

--
-- Gegevens worden geëxporteerd voor tabel `account`
--

INSERT INTO `account` (`AccountID`, `RolID`, `Gebruikersnaam`, `wachtwoord`, `email`, `voornaam`, `achternaam`) VALUES
(2, 3, 'test', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 'bing@chil.ling', 'jasper', 'test'),
(3, 3, 'bim', 'bd66796784e4bb256fb9b0f4a9e1a13330e9122b261baf72e443ee97968d5fbe', 'bim', 'bing', 'chilling');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `koppelscore`
--

DROP TABLE IF EXISTS `koppelscore`;
CREATE TABLE IF NOT EXISTS `koppelscore` (
  `KoppelScore` int NOT NULL AUTO_INCREMENT,
  `ScoreID` int NOT NULL,
  `VakID` int NOT NULL,
  PRIMARY KEY (`KoppelScore`),
  KEY `ScoreID` (`ScoreID`),
  KEY `VakID` (`VakID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `rol`
--

DROP TABLE IF EXISTS `rol`;
CREATE TABLE IF NOT EXISTS `rol` (
  `RolID` int NOT NULL AUTO_INCREMENT,
  `Rol` varchar(20) NOT NULL,
  PRIMARY KEY (`RolID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=ascii;

--
-- Gegevens worden geëxporteerd voor tabel `rol`
--

INSERT INTO `rol` (`RolID`, `Rol`) VALUES
(1, 'admin'),
(2, 'docent'),
(3, 'leerling');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `score`
--

DROP TABLE IF EXISTS `score`;
CREATE TABLE IF NOT EXISTS `score` (
  `ScoreID` int NOT NULL AUTO_INCREMENT,
  `AccountID` int NOT NULL,
  `score` varchar(30) NOT NULL,
  PRIMARY KEY (`ScoreID`),
  KEY `AccountID` (`AccountID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=ascii;

--
-- Gegevens worden geëxporteerd voor tabel `score`
--

INSERT INTO `score` (`ScoreID`, `AccountID`, `score`) VALUES
(8, 2, '0');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `vak`
--

DROP TABLE IF EXISTS `vak`;
CREATE TABLE IF NOT EXISTS `vak` (
  `VakID` int NOT NULL AUTO_INCREMENT,
  `AccountID` int NOT NULL,
  `VakSoortID` int NOT NULL,
  PRIMARY KEY (`VakID`),
  KEY `AccountID` (`AccountID`),
  KEY `VakSoortID` (`VakSoortID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `vaksoort`
--

DROP TABLE IF EXISTS `vaksoort`;
CREATE TABLE IF NOT EXISTS `vaksoort` (
  `VakSoortID` int NOT NULL AUTO_INCREMENT,
  `VakSoortNaam` text NOT NULL,
  PRIMARY KEY (`VakSoortID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `account`
--
ALTER TABLE `account`
  ADD CONSTRAINT `account_ibfk_1` FOREIGN KEY (`RolID`) REFERENCES `rol` (`RolID`);

--
-- Beperkingen voor tabel `koppelscore`
--
ALTER TABLE `koppelscore`
  ADD CONSTRAINT `koppelscore_ibfk_1` FOREIGN KEY (`ScoreID`) REFERENCES `score` (`ScoreID`),
  ADD CONSTRAINT `koppelscore_ibfk_2` FOREIGN KEY (`VakID`) REFERENCES `vak` (`VakID`);

--
-- Beperkingen voor tabel `score`
--
ALTER TABLE `score`
  ADD CONSTRAINT `score_ibfk_1` FOREIGN KEY (`AccountID`) REFERENCES `account` (`AccountID`);

--
-- Beperkingen voor tabel `vak`
--
ALTER TABLE `vak`
  ADD CONSTRAINT `vak_ibfk_1` FOREIGN KEY (`AccountID`) REFERENCES `account` (`AccountID`),
  ADD CONSTRAINT `vak_ibfk_2` FOREIGN KEY (`VakSoortID`) REFERENCES `vaksoort` (`VakSoortID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
