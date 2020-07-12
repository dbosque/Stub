CREATE DATABASE  IF NOT EXISTS `stubeditor` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `stubeditor`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: stubeditor
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `combination`
--

DROP TABLE IF EXISTS `combination`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `combination` (
  `CombinationID` int(11) NOT NULL AUTO_INCREMENT,
  `MessageTypeID` int(11) NOT NULL,
  `TemplateID` int(11) NOT NULL,
  `ResponseID` int(11) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`CombinationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `combinationxpath`
--

DROP TABLE IF EXISTS `combinationxpath`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `combinationxpath` (
  `CombinationXpathID` int(11) NOT NULL AUTO_INCREMENT,
  `XpathID` int(11) NOT NULL,
  `CombinationID` int(11) NOT NULL,
  `XpathValue` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`CombinationXpathID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `messagetype`
--

DROP TABLE IF EXISTS `messagetype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `messagetype` (
  `MessageTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Namespace` varchar(250) NOT NULL,
  `Rootnode` varchar(250) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `PassthroughEnabled` int(11) DEFAULT NULL,
  `PassthroughUrl` varchar(250) DEFAULT NULL,
  `Sample` longtext,
  PRIMARY KEY (`MessageTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `request`
--

DROP TABLE IF EXISTS `request`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request` (
  `RequestID` int(11) NOT NULL AUTO_INCREMENT,
  `Request` longtext NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`RequestID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `requestthumbprint`
--

DROP TABLE IF EXISTS `requestthumbprint`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `requestthumbprint` (
  `RequestThumbPrintID` int(11) NOT NULL AUTO_INCREMENT,
  `ResponseID` int(11) NOT NULL,
  `RequestID` int(11) NOT NULL,
  `Thumbprint` char(40) NOT NULL,
  PRIMARY KEY (`RequestThumbPrintID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `response`
--

DROP TABLE IF EXISTS `response`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `response` (
  `ResponseID` int(11) NOT NULL AUTO_INCREMENT,
  `Response` longtext NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `StatusCode` int(11) DEFAULT NULL,
  `ContentType` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`ResponseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `settings`
--

DROP TABLE IF EXISTS `settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TenantId` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stublog`
--

DROP TABLE IF EXISTS `stublog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stublog` (
  `StubLogID` int(11) NOT NULL AUTO_INCREMENT,
  `CombinationID` int(11) DEFAULT NULL,
  `ResponseDatumTijd` datetime(3) NOT NULL,
  `TenantID` int(11) DEFAULT NULL,
  `Request` longtext NOT NULL,
  `Uri` longtext,
  PRIMARY KEY (`StubLogID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `template`
--

DROP TABLE IF EXISTS `template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template` (
  `TemplateID` int(11) NOT NULL AUTO_INCREMENT,
  `MessageTypeID` int(11) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`TemplateID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `templatexpath`
--

DROP TABLE IF EXISTS `templatexpath`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `templatexpath` (
  `TemplateXpathID` int(11) NOT NULL AUTO_INCREMENT,
  `TemplateID` int(11) NOT NULL,
  `XpathID` int(11) NOT NULL,
  PRIMARY KEY (`TemplateXpathID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tenant`
--

DROP TABLE IF EXISTS `tenant`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tenant` (
  `TenantId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Connectionstring` longtext,
  `Active` tinyint(4) NOT NULL,
  PRIMARY KEY (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tenantsecurity`
--

DROP TABLE IF EXISTS `tenantsecurity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tenantsecurity` (
  `TenantSecurityId` int(11) NOT NULL AUTO_INCREMENT,
  `TenantId` int(11) NOT NULL,
  `SecurityCode` char(36) NOT NULL,
  `active` tinyint(4) NOT NULL,
  PRIMARY KEY (`TenantSecurityId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `xpath`
--

DROP TABLE IF EXISTS `xpath`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `xpath` (
  `XpathID` int(11) NOT NULL AUTO_INCREMENT,
  `Expression` varchar(1000) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  PRIMARY KEY (`XpathID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-12 10:16:35
