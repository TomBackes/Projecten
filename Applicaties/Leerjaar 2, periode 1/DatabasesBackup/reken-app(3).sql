-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 30, 2023 at 10:09 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

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
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `AccountID` int(11) NOT NULL,
  `RolID` int(11) NOT NULL,
  `Gebruikersnaam` varchar(100) NOT NULL,
  `wachtwoord` varchar(1000) NOT NULL,
  `email` varchar(100) NOT NULL,
  `voornaam` varchar(100) NOT NULL,
  `achternaam` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_general_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`AccountID`, `RolID`, `Gebruikersnaam`, `wachtwoord`, `email`, `voornaam`, `achternaam`) VALUES
(1, 3, 'pietje', 'eb27916877972e848123e36a8323d542435a6fefef51b0f024025f7cb31f9501', 'pietje', 'pietje', 'pietje');

-- --------------------------------------------------------

--
-- Table structure for table `rol`
--

CREATE TABLE `rol` (
  `RolID` int(11) NOT NULL,
  `Rol` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_general_ci;

--
-- Dumping data for table `rol`
--

INSERT INTO `rol` (`RolID`, `Rol`) VALUES
(1, 'admin'),
(2, 'docent'),
(3, 'leerling');

-- --------------------------------------------------------

--
-- Table structure for table `score`
--

CREATE TABLE `score` (
  `ScoreID` int(11) NOT NULL,
  `AccountID` int(11) NOT NULL,
  `Score` varchar(30) NOT NULL,
  `SpelID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_general_ci;

--
-- Dumping data for table `score`
--

INSERT INTO `score` (`ScoreID`, `AccountID`, `Score`, `SpelID`) VALUES
(1, 1, '7534', 2),
(11, 1, '12020', 2),
(12, 1, '0', 2);

-- --------------------------------------------------------

--
-- Table structure for table `spel`
--

CREATE TABLE `spel` (
  `Spel` varchar(20) NOT NULL,
  `SpelID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `spel`
--

INSERT INTO `spel` (`Spel`, `SpelID`) VALUES
('FlappyBird', 1),
('Whack-A-Mole', 2),
('Snake', 3);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`AccountID`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `Gebruikersnaam` (`Gebruikersnaam`),
  ADD KEY `RolID` (`RolID`);

--
-- Indexes for table `rol`
--
ALTER TABLE `rol`
  ADD PRIMARY KEY (`RolID`);

--
-- Indexes for table `score`
--
ALTER TABLE `score`
  ADD PRIMARY KEY (`ScoreID`),
  ADD KEY `SpelID` (`SpelID`),
  ADD KEY `AccountID` (`AccountID`) USING BTREE;

--
-- Indexes for table `spel`
--
ALTER TABLE `spel`
  ADD PRIMARY KEY (`SpelID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `AccountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `rol`
--
ALTER TABLE `rol`
  MODIFY `RolID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `score`
--
ALTER TABLE `score`
  MODIFY `ScoreID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `spel`
--
ALTER TABLE `spel`
  MODIFY `SpelID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `account`
--
ALTER TABLE `account`
  ADD CONSTRAINT `account_ibfk_1` FOREIGN KEY (`RolID`) REFERENCES `rol` (`RolID`);

--
-- Constraints for table `score`
--
ALTER TABLE `score`
  ADD CONSTRAINT `score_ibfk_2` FOREIGN KEY (`SpelID`) REFERENCES `spel` (`SpelID`),
  ADD CONSTRAINT `score_ibfk_3` FOREIGN KEY (`AccountID`) REFERENCES `account` (`AccountID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
