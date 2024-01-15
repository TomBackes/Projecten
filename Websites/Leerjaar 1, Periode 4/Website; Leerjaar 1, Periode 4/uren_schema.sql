-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 15 jan 2024 om 11:42
-- Serverversie: 10.4.27-MariaDB
-- PHP-versie: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `uren_schema`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `login`
--

CREATE TABLE `login` (
  `ID` bigint(20) NOT NULL,
  `MedewerkerID` bigint(20) NOT NULL,
  `GebruikersNaam` text NOT NULL,
  `Wachtwoord` text NOT NULL,
  `SALT` text NOT NULL,
  `Email` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `login`
--

INSERT INTO `login` (`ID`, `MedewerkerID`, `GebruikersNaam`, `Wachtwoord`, `SALT`, `Email`) VALUES
(1, 1, 'Remiya Shirou', '4243 Tom', '356e29c4e70d5e1c02926333edcaa76199e73cd821ebfac5349dd5fe0d4e2400cc903840544831a19291086991bca94958e129f37304bc56daaace26273a41a8', 'tom.backes@student.gildeopleidingen.nl'),
(2, 0, 'Meh', '$2y$10$QooAIOz4mM2gAf/N0201ROMWfQhz6Zp9ClNZhUglh6.5GdzGVlWCu', '', 'meh@meh.meh'),
(3, 0, 'Tom', '$2y$10$qgI5tmZWFbK1neWLY5EvleAMS3zYanpRdLN0TJ5c9K0.PexBkifwq', '', '3@3.nl'),
(4, 0, 'Tom', '$2y$10$d6a0ABVQE/OxyfWG0PW0AOOOTbgnyuyJ8ex2qhQV.Kbvzr7Jz3Sfy', '', 'appel8638@gmail.com');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `medewerkers`
--

CREATE TABLE `medewerkers` (
  `MedewerkerID` bigint(20) NOT NULL,
  `Voornaam` varchar(100) NOT NULL,
  `Achternaam` varchar(100) NOT NULL,
  `Afdeling` varchar(30) NOT NULL,
  `GeboorteDatum` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `medewerkers`
--

INSERT INTO `medewerkers` (`MedewerkerID`, `Voornaam`, `Achternaam`, `Afdeling`, `GeboorteDatum`) VALUES
(1, 'Tom', 'Backes', 'Software Development', '2004-04-02'),
(2, 'Sjoerd', 'van Veen', 'Software Development', '2004-03-08'),
(3, 'Lars', 'van den Hoef', 'IT-Beheer', '0000-00-00'),
(4, 'Mike', 'Zhang', 'IT-Beheer', '0000-00-00');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `project`
--

CREATE TABLE `project` (
  `ProjectID` bigint(20) NOT NULL,
  `ProjectNaam` varchar(100) NOT NULL,
  `Beschrijving` text NOT NULL,
  `StartDatum` date NOT NULL,
  `EindDatum` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `project`
--

INSERT INTO `project` (`ProjectID`, `ProjectNaam`, `Beschrijving`, `StartDatum`, `EindDatum`) VALUES
(1, 'ERP-systeem', 'Als hoofd Development van Gilde DevOps solutions, wil ik een ingericht ERP-systeem, zodat ik in een handomdraai de gewerkte uren van medewerkers kan factureren naar klanten.', '2023-05-16', '0000-00-00'),
(2, 'Professionele infrastructuur', 'Als een Hoofd operation team van Gilde DevOps Solution Wil ik een Nieuwe testomgeving voor het hele netwerk Zodat het netwerk professioneel kan worden beheerd.', '2023-05-17', '0000-00-00'),
(3, 'Toolkit', 'Als afdelingshoofden van Development en Operations van Gilde DevOps Solutions, willen wij een testplan met bijbehorende tools, zodat de ontwikkelde software en ingerichte infrastructuur voldoen aan de kwaliteitseisen.', '2023-05-22', '0000-00-00');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `urenoverzicht`
--

CREATE TABLE `urenoverzicht` (
  `ID` bigint(20) NOT NULL,
  `MedewerkerID` bigint(20) NOT NULL,
  `ProjectID` bigint(20) NOT NULL,
  `GewerkteUren` float NOT NULL,
  `Datum` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `urenoverzicht`
--

INSERT INTO `urenoverzicht` (`ID`, `MedewerkerID`, `ProjectID`, `GewerkteUren`, `Datum`) VALUES
(1, 1, 1, 2.5, '2023-05-22'),
(2, 1, 1, 2, '2023-05-26'),
(4, 2, 3, 2.5, '2023-05-30'),
(6, 1, 3, 4, '2024-01-09');

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`ID`);

--
-- Indexen voor tabel `medewerkers`
--
ALTER TABLE `medewerkers`
  ADD PRIMARY KEY (`MedewerkerID`);

--
-- Indexen voor tabel `project`
--
ALTER TABLE `project`
  ADD PRIMARY KEY (`ProjectID`);

--
-- Indexen voor tabel `urenoverzicht`
--
ALTER TABLE `urenoverzicht`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `login`
--
ALTER TABLE `login`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT voor een tabel `medewerkers`
--
ALTER TABLE `medewerkers`
  MODIFY `MedewerkerID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT voor een tabel `project`
--
ALTER TABLE `project`
  MODIFY `ProjectID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT voor een tabel `urenoverzicht`
--
ALTER TABLE `urenoverzicht`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
