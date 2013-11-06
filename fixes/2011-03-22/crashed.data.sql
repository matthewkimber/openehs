DROP DATABASE IF EXISTS `openehs_database`;
CREATE DATABASE `openehs_database`;

USE `openehs_database`;


-- ------------------------------------------------------
-- ------------------- BEGIN DUMP -----------------------
-- ------------------------------------------------------


-- MySQL dump 10.13  Distrib 5.5.9, for Win64 (x86)
--
-- Host: localhost    Database: openehs_database
-- ------------------------------------------------------
-- Server version	5.5.9

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
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `AddressID` int(11) NOT NULL AUTO_INCREMENT,
  `Street1` varchar(50) NOT NULL,
  `Street2` varchar(50) DEFAULT NULL,
  `City` varchar(30) NOT NULL,
  `Region` varchar(30) NOT NULL,
  `Country` int(11) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`AddressID`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (2,'123 Main St.','','Accra','Accra',104,''),(4,'123 Main St.','','Accra','Greater accra',82,'\0'),(6,'box mp 1485','','Accra','Greater accra',82,''),(7,'box an 16684 accra-Ghana','','Accra','Accra',82,''),(8,'GUGGISBERG AVE.',' ','Accra','GA',82,''),(9,'SPINTEX,','','Accra','Greater accra',82,'\0'),(10,'SPINTEX,','','Accra','Greater accra',82,'\0'),(11,'block 3 korle-bu','','Accra','Greater accra',82,''),(26,'ADENTA','','Accra','Gt. accra',82,''),(27,'gbawe','','Koforidua','Eastern',82,'\0'),(28,'gbawe','','Koforidua','Eastern',82,'\0'),(29,'p.o.box 77','accra','Accra','G/a',82,''),(30,'ADENTA','ACCRA','Greater accra','Gt. accra',82,''),(31,'MANGOSTREET',NULL,'Accra','Greater accra',82,''),(32,'p.o.box 345322','sdjkdffjffl;g','Accra','Greater accra',82,'\0'),(33,'p.o.box 453434',NULL,'Accra','Eastern',82,'\0'),(34,'abossey okai',NULL,'Accra','Greater accra',82,'\0'),(35,'abossey okai',NULL,'Accra','Greater accra',82,'\0'),(36,'abossey okai',NULL,'Accra','Greater accra',82,'\0'),(37,'abossey okai',NULL,'Accra','Greater accra',82,'\0'),(38,'abossey okai',NULL,'Accra','Greater accra',82,'\0'),(39,'abossey okai',NULL,'Accra','Greater accra',82,'\0'),(40,'P.O.BOX 77','P.O.BOX 77','Accra','Greater accra',82,'\0'),(41,'P.O.BOX 77','','Accra','Greater accra',82,'\0'),(42,'ADENTA','ACCRA','Accra','G/a',82,''),(43,'P.O.BOX 3087','P.O.BOX 56733','Accra','Greater accra',82,'\0'),(44,'P.O.BOX 2005343',NULL,'Accra','Eastern',82,'\0'),(45,'box 77 korle bu',NULL,'Accra','Greater accra',82,''),(46,'Box  125584',NULL,'Keta','Volta region',82,''),(47,'65 Kpakpo Brown Street',NULL,'Accra','Greater accra',82,'');
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER tr_AutoCorrectFormatCityRegionCountryInAddress
BEFORE INSERT ON Address

FOR EACH ROW

BEGIN
    SET NEW.City = CONCAT(UCASE(substr(NEW.City,1,1)), (LCASE(substr(NEW.City,2))));
    SET NEW.Region = CONCAT(UCASE(substr(NEW.Region,1,1)), (LCASE(substr(NEW.Region,2))));
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `allergy`
--

DROP TABLE IF EXISTS `allergy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `allergy` (
  `AllergyID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`AllergyID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `allergy`
--

LOCK TABLES `allergy` WRITE;
/*!40000 ALTER TABLE `allergy` DISABLE KEYS */;
/*!40000 ALTER TABLE `allergy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `category` (
  `CategoryID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  `Description` text,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `country`
--

DROP TABLE IF EXISTS `country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `country` (
  `CountryID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`CountryID`)
) ENGINE=InnoDB AUTO_INCREMENT=247 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country`
--

LOCK TABLES `country` WRITE;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` VALUES (1,'Afghanistan'),(2,'Åland Islands'),(3,'Albania'),(4,'Algeria'),(5,'Samoa, USA'),(6,'Andorra'),(7,'Angola'),(8,'Anguilla'),(9,'Antarctica'),(10,'Antigua'),(11,'Argentina'),(12,'Armenia'),(13,'Aruba'),(14,'Australia'),(15,'Austria'),(16,'Azerbaijan'),(17,'Bahamas'),(18,'Bahrain'),(19,'Bangladesh'),(20,'Barbados'),(21,'Belarus'),(22,'Belgium'),(23,'Belize'),(24,'Benin'),(25,'Bermuda'),(26,'Bhutan'),(27,'Bolivia'),(28,'Bonaire'),(29,'Bosnia'),(30,'Botswana'),(31,'Bouvet Island'),(32,'Brazil'),(33,'British Indian Ocean Territory'),(34,'Brunei Darussalam'),(35,'Bulgaria'),(36,'Burkina Faso'),(37,'Burundi'),(38,'Cambodia'),(39,'Cameroon'),(40,'Canada'),(41,'Cape Verde'),(42,'Cayman Islands'),(43,'Central African Republic'),(44,'Chad'),(45,'Chile'),(46,'China'),(47,'Christmas Island'),(48,'Cocos'),(49,'Colombia'),(50,'Comoros'),(51,'Congo'),(52,'Congo'),(53,'Cook Islands'),(54,'Costa Rica'),(55,'Côte Divoire'),(56,'Croatia'),(57,'Cuba'),(58,'Curaçao'),(59,'Cyprus'),(60,'Czech Republic'),(61,'Denmark'),(62,'Djibouti'),(63,'Dominica'),(64,'Dominican Republic'),(65,'Ecuador'),(66,'Egypt'),(67,'El Salvador'),(68,'Equatorial Guinea'),(69,'Eritrea'),(70,'Estonia'),(71,'Ethiopia'),(72,'Falkland Islands'),(73,'Faroe Islands'),(74,'Fiji'),(75,'Finland'),(76,'France'),(77,'French Guiana'),(78,'French Polynesia'),(79,'French Southern Territories'),(80,'Gabon'),(81,'Gambia'),(82,'Georgia'),(83,'Germany'),(84,'Ghana'),(85,'Gibraltar'),(86,'Greece'),(87,'Greenland'),(88,'Grenada'),(89,'Guadeloupe'),(90,'Guam'),(91,'Guatemala'),(92,'Guernsey'),(93,'Guinea'),(94,'Guinea-Bissau'),(95,'Guyana'),(96,'Haiti'),(97,'Heard Island'),(98,'Vatican City'),(99,'Honduras'),(100,'Hong Kong'),(101,'Hungary'),(102,'Iceland'),(103,'India'),(104,'Indonesia'),(105,'Iran'),(106,'Iraq'),(107,'Ireland'),(108,'Isle Of Man'),(109,'Israel'),(110,'Italy'),(111,'Jamaica'),(112,'Japan'),(113,'Jersey'),(114,'Jordan'),(115,'Kazakhstan'),(116,'Kenya'),(117,'Kiribati'),(118,'Korea'),(119,'Korea, Republic Of'),(120,'Kuwait'),(121,'Kyrgyzstan'),(122,'Lao'),(123,'Latvia'),(124,'Lebanon'),(125,'Lesotho'),(126,'Liberia'),(127,'Libyan Arab Jamahiriya'),(128,'Liechtenstein'),(129,'Lithuania'),(130,'Luxembourg'),(131,'Macao'),(132,'Macedonia'),(133,'Madagascar'),(134,'Malawi'),(135,'Malaysia'),(136,'Maldives'),(137,'Mali'),(138,'Malta'),(139,'Marshall Islands'),(140,'Martinique'),(141,'Mauritania'),(142,'Mauritius'),(143,'Mayotte'),(144,'Mexico'),(145,'Micronesia'),(146,'Moldova'),(147,'Monaco'),(148,'Mongolia'),(149,'Montenegro'),(150,'Montserrat'),(151,'Morocco'),(152,'Mozambique'),(153,'Myanmar'),(154,'Namibia'),(155,'Nauru'),(156,'Nepal'),(157,'Netherlands'),(158,'New Caledonia'),(159,'New Zealand'),(160,'Nicaragua'),(161,'Niger'),(162,'Nigeria'),(163,'Niue'),(164,'Norfolk Island'),(165,'Northern Mariana Islands'),(166,'Norway'),(167,'Oman'),(168,'Pakistan'),(169,'Palau'),(170,'Palestinian Territory'),(171,'Panama'),(172,'Papua New Guinea'),(173,'Paraguay'),(174,'Peru'),(175,'Philippines'),(176,'Pitcairn'),(177,'Poland'),(178,'Portugal'),(179,'Puerto Rico'),(180,'Qatar'),(181,'Réunion'),(182,'Romania'),(183,'Russian Federation'),(184,'Rwanda'),(185,'Saint Barthélemy'),(186,'Saint Helena'),(187,'Saint Kitts'),(188,'Saint Lucia'),(189,'Saint Martin'),(190,'Saint Pierre'),(191,'Saint Vincent'),(192,'Samoa'),(193,'San Marino'),(194,'Sao Tome'),(195,'Saudi Arabia'),(196,'Senegal'),(197,'Serbia'),(198,'Seychelles'),(199,'Sierra Leone'),(200,'Singapore'),(201,'Sint Maarten'),(202,'Slovakia'),(203,'Slovenia'),(204,'Solomon Islands'),(205,'Somalia'),(206,'South Africa'),(207,'South Georgia'),(208,'Spain'),(209,'Sri Lanka'),(210,'Sudan'),(211,'Suriname'),(212,'Svalbard'),(213,'Swaziland'),(214,'Sweden'),(215,'Switzerland'),(216,'Syrian Arab Republic'),(217,'Taiwan'),(218,'Tajikistan'),(219,'Tanzania'),(220,'Thailand'),(221,'Timor-Leste'),(222,'Togo'),(223,'Tokelau'),(224,'Tonga'),(225,'Trinidad'),(226,'Tunisia'),(227,'Turkey'),(228,'Turkmenistan'),(229,'Turks'),(230,'Tuvalu'),(231,'Uganda'),(232,'Ukraine'),(233,'United Arab Emirates'),(234,'United Kingdom'),(235,'United States'),(236,'Uruguay'),(237,'Uzbekistan'),(238,'Vanuatu'),(239,'Venezuela'),(240,'Viet Nam'),(241,'Virgin Islands'),(242,'Wallis'),(243,'Western Sahara'),(244,'Yemen'),(245,'Zambia'),(246,'Zimbabwe');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emergencycontact`
--

DROP TABLE IF EXISTS `emergencycontact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `emergencycontact` (
  `EmergencyContactID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(30) NOT NULL,
  `LastName` varchar(30) NOT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `Relationship` int(11) NOT NULL,
  `AddressID` int(11) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`EmergencyContactID`),
  KEY `FK_EmergencyContactMustHaveAddressID` (`AddressID`),
  CONSTRAINT `FK_EmergencyContactMustHaveAddressID` FOREIGN KEY (`AddressID`) REFERENCES `address` (`AddressID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emergencycontact`
--

LOCK TABLES `emergencycontact` WRITE;
/*!40000 ALTER TABLE `emergencycontact` DISABLE KEYS */;
INSERT INTO `emergencycontact` VALUES (1,'Amber','Kimber','222-7654321',0,4,''),(2,'Adolphina','Ogbu','0248134770',0,10,''),(3,'Frederick ','Bekoe','0287015488',8,28,''),(4,'Ransford','Dickson','0277452314',0,41,''),(5,'Martey','Dickson','0207856434',3,44,'');
/*!40000 ALTER TABLE `emergencycontact` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER tr_AutoCorrectFormatEmergencyContactName
BEFORE INSERT ON EmergencyContact

FOR EACH ROW

BEGIN
    SET NEW.FirstName = CONCAT(UCASE(substr(NEW.FirstName,1,1)), (LCASE(substr(NEW.FirstName,2))));
    SET NEW.LastName = CONCAT(UCASE(substr(NEW.LastName,1,1)), (LCASE(substr(NEW.LastName,2))));
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `feedchart`
--

DROP TABLE IF EXISTS `feedchart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `feedchart` (
  `FeedChartID` int(11) NOT NULL AUTO_INCREMENT,
  `PatientCheckInID` int(11) NOT NULL,
  `FeedTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `FeedType` varchar(30) DEFAULT NULL,
  `AmountOffered` varchar(20) DEFAULT NULL,
  `AmountTaken` varchar(20) DEFAULT NULL,
  `Vomit` varchar(20) DEFAULT NULL,
  `Urine` varchar(20) DEFAULT NULL,
  `Stool` varchar(20) DEFAULT NULL,
  `Comments` text,
  PRIMARY KEY (`FeedChartID`),
  KEY `FK_FeedChartMustHavePatientCheckInID` (`PatientCheckInID`),
  CONSTRAINT `FK_FeedChartMustHavePatientCheckInID` FOREIGN KEY (`PatientCheckInID`) REFERENCES `patientcheckin` (`PatientCheckInID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feedchart`
--

LOCK TABLES `feedchart` WRITE;
/*!40000 ALTER TABLE `feedchart` DISABLE KEYS */;
/*!40000 ALTER TABLE `feedchart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `immunization`
--

DROP TABLE IF EXISTS `immunization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `immunization` (
  `ImmunizationID` int(11) NOT NULL AUTO_INCREMENT,
  `VaccineType` text NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`ImmunizationID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `immunization`
--

LOCK TABLES `immunization` WRITE;
/*!40000 ALTER TABLE `immunization` DISABLE KEYS */;
/*!40000 ALTER TABLE `immunization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `intakechart`
--

DROP TABLE IF EXISTS `intakechart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `intakechart` (
  `InTakeChartID` int(11) NOT NULL AUTO_INCREMENT,
  `ChartTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `KindOfFluid` varchar(30) DEFAULT NULL,
  `Amount` varchar(20) DEFAULT NULL,
  `PatientCheckInID` int(11) NOT NULL,
  PRIMARY KEY (`InTakeChartID`),
  KEY `FK_IntakeChartMustHavePatientCheckInID` (`PatientCheckInID`),
  CONSTRAINT `FK_IntakeChartMustHavePatientCheckInID` FOREIGN KEY (`PatientCheckInID`) REFERENCES `patientcheckin` (`PatientCheckInID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `intakechart`
--

LOCK TABLES `intakechart` WRITE;
/*!40000 ALTER TABLE `intakechart` DISABLE KEYS */;
/*!40000 ALTER TABLE `intakechart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS `invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoice` (
  `InvoiceID` int(11) NOT NULL AUTO_INCREMENT,
  `Total` decimal(6,2) NOT NULL DEFAULT '0.00',
  `Date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`InvoiceID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice`
--

LOCK TABLES `invoice` WRITE;
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoiceitem`
--

DROP TABLE IF EXISTS `invoiceitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `invoiceitem` (
  `InvoiceItemID` int(11) NOT NULL AUTO_INCREMENT,
  `InvoiceID` int(11) NOT NULL,
  `ProductID` int(11) DEFAULT NULL,
  `ServiceID` int(11) DEFAULT NULL,
  `Quantity` float DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`InvoiceItemID`),
  KEY `FK_InvoiceItemMustHaveInvoiceID` (`InvoiceID`),
  KEY `FK_InvoiceItemMustHaveProductID` (`ProductID`),
  KEY `FK_InvoiceItemMustHaveServiceID` (`ServiceID`),
  CONSTRAINT `FK_InvoiceItemMustHaveInvoiceID` FOREIGN KEY (`InvoiceID`) REFERENCES `invoice` (`InvoiceID`),
  CONSTRAINT `FK_InvoiceItemMustHaveProductID` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ProductID`),
  CONSTRAINT `FK_InvoiceItemMustHaveServiceID` FOREIGN KEY (`ServiceID`) REFERENCES `service` (`ServiceID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoiceitem`
--

LOCK TABLES `invoiceitem` WRITE;
/*!40000 ALTER TABLE `invoiceitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `invoiceitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `location`
--

DROP TABLE IF EXISTS `location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `location` (
  `LocationID` int(11) NOT NULL AUTO_INCREMENT,
  `Department` varchar(20) NOT NULL,
  `RoomNumber` varchar(15) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`LocationID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `location`
--

LOCK TABLES `location` WRITE;
/*!40000 ALTER TABLE `location` DISABLE KEYS */;
INSERT INTO `location` VALUES (1,'Eye Clinic','',''),(2,'ENT Clinic','',''),(3,'Physician Clinic','',''),(4,'Surgical Clinic','',''),(5,'Urology Clinic','',''),(6,'Neurology Clinic','',''),(7,'Anesthetic Clinic','',''),(8,'Renal Clinic','',''),(9,'Gastro Clinic','',''),(10,'Endocrine Clinic','',''),(11,'Orthopedic Clinic','',''),(12,'Dermatology Clinic','',''),(13,'Neuro Surgery Clinic','',''),(14,'Vascluar Clinic','','');
/*!40000 ALTER TABLE `location` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medication`
--

DROP TABLE IF EXISTS `medication`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `medication` (
  `MedicationID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `Description` text,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`MedicationID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medication`
--

LOCK TABLES `medication` WRITE;
/*!40000 ALTER TABLE `medication` DISABLE KEYS */;
/*!40000 ALTER TABLE `medication` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `note`
--

DROP TABLE IF EXISTS `note`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `note` (
  `NoteID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(30) DEFAULT NULL,
  `Type` varchar(20) NOT NULL,
  `Body` longtext NOT NULL,
  `DateCreated` datetime NOT NULL,
  `StaffID` int(11) NOT NULL,
  `NoteTemplateCategoryID` int(11) DEFAULT NULL,
  `PatientCheckInID` int(11) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`NoteID`),
  KEY `FK_NoteMustHaveStaffID` (`StaffID`),
  KEY `FK_NoteMustHavePatientCheckInID` (`PatientCheckInID`),
  CONSTRAINT `FK_NoteMustHavePatientCheckInID` FOREIGN KEY (`PatientCheckInID`) REFERENCES `patientcheckin` (`PatientCheckInID`),
  CONSTRAINT `FK_NoteMustHaveStaffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `note`
--

LOCK TABLES `note` WRITE;
/*!40000 ALTER TABLE `note` DISABLE KEYS */;
/*!40000 ALTER TABLE `note` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notetemplatecategory`
--

DROP TABLE IF EXISTS `notetemplatecategory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `notetemplatecategory` (
  `NoteTemplateCategoryID` int(11) NOT NULL AUTO_INCREMENT,
  `TemplateCategoryName` varchar(30) NOT NULL,
  PRIMARY KEY (`NoteTemplateCategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notetemplatecategory`
--

LOCK TABLES `notetemplatecategory` WRITE;
/*!40000 ALTER TABLE `notetemplatecategory` DISABLE KEYS */;
INSERT INTO `notetemplatecategory` VALUES (1,'General'),(2,'Surgery');
/*!40000 ALTER TABLE `notetemplatecategory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `outputchart`
--

DROP TABLE IF EXISTS `outputchart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `outputchart` (
  `OutputChartID` int(11) NOT NULL AUTO_INCREMENT,
  `ChartTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `NGSuctionAmount` varchar(20) DEFAULT NULL,
  `NGSuctionColor` varchar(30) DEFAULT NULL,
  `UrineAmount` varchar(20) DEFAULT NULL,
  `StoolAmount` varchar(20) DEFAULT NULL,
  `StoolColor` varchar(30) DEFAULT NULL,
  `PatientCheckInID` int(11) NOT NULL,
  PRIMARY KEY (`OutputChartID`),
  KEY `FK_OutputChartMustHavePatientCheckInID` (`PatientCheckInID`),
  CONSTRAINT `FK_OutputChartMustHavePatientCheckInID` FOREIGN KEY (`PatientCheckInID`) REFERENCES `patientcheckin` (`PatientCheckInID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `outputchart`
--

LOCK TABLES `outputchart` WRITE;
/*!40000 ALTER TABLE `outputchart` DISABLE KEYS */;
/*!40000 ALTER TABLE `outputchart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient`
--

DROP TABLE IF EXISTS `patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patient` (
  `PatientID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(30) NOT NULL,
  `MiddleName` varchar(30) DEFAULT NULL,
  `LastName` varchar(30) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Gender` int(11) NOT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `AddressID` int(11) NOT NULL,
  `BloodType` int(11) DEFAULT NULL,
  `Tribe` int(11) DEFAULT NULL,
  `Race` int(11) DEFAULT NULL,
  `Religion` int(11) DEFAULT NULL,
  `Occupation` varchar(30) DEFAULT NULL,
  `Education` int(11) DEFAULT NULL,
  `PatientNote` longtext,
  `OldPhysicalRecordNumb` varchar(50) DEFAULT NULL,
  `InsuranceNumber` varchar(20) DEFAULT NULL,
  `InsuranceExpiration` datetime DEFAULT NULL,
  `PlaceOfBirth` varchar(50) DEFAULT NULL,
  `MaritalStatus` tinyint(4) DEFAULT NULL,
  `EmergencyContactID` int(11) DEFAULT NULL,
  `DateOfDeath` datetime DEFAULT NULL,
  `CreationDate` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`PatientID`),
  KEY `FK_PatientMustHaveAddressID` (`AddressID`),
  KEY `FK_PatientMustHaveEmergencyContactID` (`EmergencyContactID`),
  CONSTRAINT `FK_PatientMustHaveAddressID` FOREIGN KEY (`AddressID`) REFERENCES `address` (`AddressID`),
  CONSTRAINT `FK_PatientMustHaveEmergencyContactID` FOREIGN KEY (`EmergencyContactID`) REFERENCES `emergencycontact` (`EmergencyContactID`)
) ENGINE=InnoDB AUTO_INCREMENT=100005 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient`
--

LOCK TABLES `patient` WRITE;
/*!40000 ALTER TABLE `patient` DISABLE KEYS */;
INSERT INTO `patient` VALUES (100001,'Sunny',' ','Ogbu','1973-03-17',0,'0248134770',9,0,11,0,2,'Businessman',0,'','358682','','0001-01-01 00:00:00','NIGERIA',1,2,'0001-01-01 00:00:00','2011-03-17 09:39:59',''),(100002,'Florence','','Anyomi','1951-03-18',1,'0287015488',27,0,2,0,2,'Rtd teacher',0,'',NULL,'','0001-01-01 00:00:00','mpraeso',1,3,'0001-01-01 00:00:00','2011-03-18 01:54:27',''),(100003,'Joseph','Martey','Dickson','1987-03-10',0,'020767543',40,0,0,0,0,'Network engineer',0,'','956342','546433455','2011-03-08 00:00:00','MAMPROBI',0,4,'0001-01-01 00:00:00','2011-03-18 02:56:42',''),(100004,'Ransford',NULL,'Dickson','1946-03-20',0,'0244605342',43,0,0,0,0,'Engineer ',0,NULL,'8944569','565755556577','2011-03-23 00:00:00','MAMPROBI',1,5,'0001-01-01 00:00:00','2011-03-21 01:52:46','');
/*!40000 ALTER TABLE `patient` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER tr_AutoCorrectFormatPatientInfo
BEFORE INSERT ON Patient

FOR EACH ROW

BEGIN
    SET NEW.FirstName = CONCAT(UCASE(substr(NEW.FirstName,1,1)), (LCASE(substr(NEW.FirstName,2))));
    SET NEW.MiddleName = CONCAT(UCASE(substr(NEW.MiddleName,1,1)), (LCASE(substr(NEW.MiddleName,2))));
    SET NEW.LastName = CONCAT(UCASE(substr(NEW.LastName,1,1)), (LCASE(substr(NEW.LastName,2))));
    SET NEW.Occupation = CONCAT(UCASE(substr(NEW.Occupation,1,1)), (LCASE(substr(NEW.Occupation,2))));
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `patientallergy`
--

DROP TABLE IF EXISTS `patientallergy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientallergy` (
  `PatientAllergyID` int(11) NOT NULL AUTO_INCREMENT,
  `PatientID` int(11) NOT NULL,
  `AllergyID` int(11) NOT NULL,
  PRIMARY KEY (`PatientAllergyID`),
  KEY `FK_PatientAllergyMustHavePatientID` (`PatientID`),
  KEY `FK_PatientAllergyMustHaveAllergyID` (`AllergyID`),
  CONSTRAINT `FK_PatientAllergyMustHaveAllergyID` FOREIGN KEY (`AllergyID`) REFERENCES `allergy` (`AllergyID`),
  CONSTRAINT `FK_PatientAllergyMustHavePatientID` FOREIGN KEY (`PatientID`) REFERENCES `patient` (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientallergy`
--

LOCK TABLES `patientallergy` WRITE;
/*!40000 ALTER TABLE `patientallergy` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientallergy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patientcheckin`
--

DROP TABLE IF EXISTS `patientcheckin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientcheckin` (
  `PatientCheckInID` int(11) NOT NULL AUTO_INCREMENT,
  `CheckinTime` datetime NOT NULL,
  `PatientType` tinyint(4) NOT NULL,
  `PatientID` int(11) NOT NULL,
  `InvoiceID` int(11) NOT NULL,
  `CheckOutTime` datetime DEFAULT NULL,
  `LocationID` int(11) NOT NULL,
  `StaffID` int(11) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`PatientCheckInID`),
  KEY `FK_PatientCheckInMustHavePatientID` (`PatientID`),
  KEY `FK_PatientCheckInMustHaveLocationID` (`LocationID`),
  KEY `FK_PatientCheckInMustHaveStaffID` (`StaffID`),
  CONSTRAINT `FK_PatientCheckInMustHaveLocationID` FOREIGN KEY (`LocationID`) REFERENCES `location` (`LocationID`),
  CONSTRAINT `FK_PatientCheckInMustHavePatientID` FOREIGN KEY (`PatientID`) REFERENCES `patient` (`PatientID`),
  CONSTRAINT `FK_PatientCheckInMustHaveStaffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientcheckin`
--

LOCK TABLES `patientcheckin` WRITE;
/*!40000 ALTER TABLE `patientcheckin` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientcheckin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patientimmunization`
--

DROP TABLE IF EXISTS `patientimmunization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientimmunization` (
  `PatientImmunizationID` int(11) NOT NULL AUTO_INCREMENT,
  `PatientID` int(11) NOT NULL,
  `DateAdministered` datetime NOT NULL,
  `ImmunizationID` int(11) NOT NULL,
  PRIMARY KEY (`PatientImmunizationID`),
  KEY `PatientImmunizationMustHavePatientID` (`PatientID`),
  KEY `PatientImmunizationMustHaveImmunizationID` (`ImmunizationID`),
  CONSTRAINT `PatientImmunizationMustHaveImmunizationID` FOREIGN KEY (`ImmunizationID`) REFERENCES `immunization` (`ImmunizationID`),
  CONSTRAINT `PatientImmunizationMustHavePatientID` FOREIGN KEY (`PatientID`) REFERENCES `patient` (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientimmunization`
--

LOCK TABLES `patientimmunization` WRITE;
/*!40000 ALTER TABLE `patientimmunization` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientimmunization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patientmedication`
--

DROP TABLE IF EXISTS `patientmedication`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientmedication` (
  `PatientMedicationID` int(11) NOT NULL AUTO_INCREMENT,
  `Instruction` text,
  `StartDate` datetime NOT NULL,
  `ExpDate` datetime NOT NULL,
  `Dose` varchar(15) NOT NULL,
  `Frequency` varchar(20) NOT NULL,
  `Administration` int(11) NOT NULL,
  `PatientID` int(11) NOT NULL,
  `MedicationID` int(11) NOT NULL,
  PRIMARY KEY (`PatientMedicationID`),
  KEY `PatientMedicationMustHavePatientID` (`PatientID`),
  KEY `PatientMedicationMustHaveMedicationID` (`MedicationID`),
  CONSTRAINT `PatientMedicationMustHaveMedicationID` FOREIGN KEY (`MedicationID`) REFERENCES `medication` (`MedicationID`),
  CONSTRAINT `PatientMedicationMustHavePatientID` FOREIGN KEY (`PatientID`) REFERENCES `patient` (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientmedication`
--

LOCK TABLES `patientmedication` WRITE;
/*!40000 ALTER TABLE `patientmedication` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientmedication` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patientproblem`
--

DROP TABLE IF EXISTS `patientproblem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientproblem` (
  `PatientProblemID` int(11) NOT NULL AUTO_INCREMENT,
  `PatientID` int(11) NOT NULL,
  `ProblemID` int(11) NOT NULL,
  PRIMARY KEY (`PatientProblemID`),
  KEY `FK_PatientProblemMustHavePatientID` (`PatientID`),
  KEY `FK_PatientProblemMustHaveProblemID` (`ProblemID`),
  CONSTRAINT `FK_PatientProblemMustHavePatientID` FOREIGN KEY (`PatientID`) REFERENCES `patient` (`PatientID`),
  CONSTRAINT `FK_PatientProblemMustHaveProblemID` FOREIGN KEY (`ProblemID`) REFERENCES `problem` (`ProblemID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientproblem`
--

LOCK TABLES `patientproblem` WRITE;
/*!40000 ALTER TABLE `patientproblem` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientproblem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment` (
  `PaymentID` int(11) NOT NULL AUTO_INCREMENT,
  `CashAmount` decimal(6,2) NOT NULL,
  `PaymentDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `InvoiceID` int(11) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`PaymentID`),
  KEY `FK_PaymentMustHaveInvoiceID` (`InvoiceID`),
  CONSTRAINT `FK_PaymentMustHaveInvoiceID` FOREIGN KEY (`InvoiceID`) REFERENCES `invoice` (`InvoiceID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `problem`
--

DROP TABLE IF EXISTS `problem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `problem` (
  `ProblemID` int(11) NOT NULL AUTO_INCREMENT,
  `ProblemName` varchar(30) NOT NULL,
  PRIMARY KEY (`ProblemID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `problem`
--

LOCK TABLES `problem` WRITE;
/*!40000 ALTER TABLE `problem` DISABLE KEYS */;
/*!40000 ALTER TABLE `problem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `ProductID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Unit` varchar(10) NOT NULL,
  `CategoryID` int(11) NOT NULL,
  `ProductCost` decimal(6,2) NOT NULL,
  `QuantityOnHand` int(11) NOT NULL,
  `Counter` int(11) NOT NULL DEFAULT '1',
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`ProductID`),
  KEY `ProductMustHaveCategoryID` (`CategoryID`),
  CONSTRAINT `ProductMustHaveCategoryID` FOREIGN KEY (`CategoryID`) REFERENCES `category` (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `RoleID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Administrators','The system administrator.','2011-02-21 08:00:00'),(2,'OPDClerks','OPD Clerks','2011-02-21 08:00:00'),(3,'OPDAdministrators','Administrator over OPD clerks.','2011-02-21 08:00:00');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `service`
--

DROP TABLE IF EXISTS `service`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `service` (
  `ServiceID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  `ServiceCost` decimal(6,2) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`ServiceID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service`
--

LOCK TABLES `service` WRITE;
/*!40000 ALTER TABLE `service` DISABLE KEYS */;
/*!40000 ALTER TABLE `service` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff`
--

DROP TABLE IF EXISTS `staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `staff` (
  `StaffID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(30) NOT NULL,
  `MiddleName` varchar(30) DEFAULT NULL,
  `LastName` varchar(30) NOT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `StaffType` tinyint(1) NOT NULL,
  `LicenseNumber` varchar(20) DEFAULT NULL,
  `AddressID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`StaffID`),
  KEY `FK_StaffMustHaveAddressID` (`AddressID`),
  KEY `FK_StaffMustHaveUserID` (`UserID`),
  CONSTRAINT `FK_StaffMustHaveAddressID` FOREIGN KEY (`AddressID`) REFERENCES `address` (`AddressID`),
  CONSTRAINT `FK_StaffMustHaveUserID` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` VALUES (1,'Korle Bu','','Administrator','',4,NULL,2,1,''),(3,'Teddy','Nii','Moore','0272756613',4,NULL,6,3,'\0'),(4,'Robert','Aidoo','Adu','0268452456',4,NULL,7,4,'\0'),(5,'Anthony','','Akolgo','0277445031',0,NULL,8,5,'\0'),(6,'Tahiru','','Ayobi','0244211853',0,NULL,11,6,'\0'),(7,'Richard','Korbla','Bright','0276066288',3,NULL,26,7,'\0'),(8,'Joseph','Martey','Dickson','0207535315',4,NULL,29,8,'\0'),(9,'Zagudas','Achosman','Korbla','0276066288',3,NULL,42,11,'\0'),(10,'Ruth ',NULL,'Mensah','0208803648',3,NULL,45,12,'\0'),(12,'Eugene','Nii amarkai`','Amarfio','233246370618',4,NULL,47,14,'\0');
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER tr_AutoCorrectFormatStaffName
BEFORE INSERT ON Staff

FOR EACH ROW

BEGIN
    SET NEW.FirstName = CONCAT(UCASE(substr(NEW.FirstName,1,1)), (LCASE(substr(NEW.FirstName,2))));
    SET NEW.MiddleName = CONCAT(UCASE(substr(NEW.MiddleName,1,1)), (LCASE(substr(NEW.MiddleName,2))));
    SET NEW.LastName = CONCAT(UCASE(substr(NEW.LastName,1,1)), (LCASE(substr(NEW.LastName,2))));
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `surgery`
--

DROP TABLE IF EXISTS `surgery`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `surgery` (
  `SurgeryID` int(11) NOT NULL AUTO_INCREMENT,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime DEFAULT NULL,
  `LocationID` int(11) DEFAULT NULL,
  `PatientCheckInID` int(11) NOT NULL,
  `CaseType` bit(1) NOT NULL,
  PRIMARY KEY (`SurgeryID`),
  KEY `FK_SurgeryMustHavePatientCheckInID` (`PatientCheckInID`),
  KEY `FK_SurgeryMustHaveLocationID` (`LocationID`),
  CONSTRAINT `FK_SurgeryMustHaveLocationID` FOREIGN KEY (`LocationID`) REFERENCES `location` (`LocationID`),
  CONSTRAINT `FK_SurgeryMustHavePatientCheckInID` FOREIGN KEY (`PatientCheckInID`) REFERENCES `patientcheckin` (`PatientCheckInID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgery`
--

LOCK TABLES `surgery` WRITE;
/*!40000 ALTER TABLE `surgery` DISABLE KEYS */;
/*!40000 ALTER TABLE `surgery` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgerystaff`
--

DROP TABLE IF EXISTS `surgerystaff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `surgerystaff` (
  `SurgeryStaffID` int(11) NOT NULL AUTO_INCREMENT,
  `StaffID` int(11) NOT NULL,
  `SurgeryID` int(11) NOT NULL,
  `Role` int(11) NOT NULL,
  PRIMARY KEY (`SurgeryStaffID`),
  KEY `FK_SurgeryStaffMustHaveStaffID` (`StaffID`),
  KEY `FK_SurgeryStaffMustHaveSurgeryID` (`SurgeryID`),
  CONSTRAINT `FK_SurgeryStaffMustHaveStaffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`),
  CONSTRAINT `FK_SurgeryStaffMustHaveSurgeryID` FOREIGN KEY (`SurgeryID`) REFERENCES `surgery` (`SurgeryID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgerystaff`
--

LOCK TABLES `surgerystaff` WRITE;
/*!40000 ALTER TABLE `surgerystaff` DISABLE KEYS */;
/*!40000 ALTER TABLE `surgerystaff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `template`
--

DROP TABLE IF EXISTS `template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `template` (
  `TemplateID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(150) NOT NULL,
  `TemplateBody` longtext NOT NULL,
  `NoteTemplateCategoryID` int(11) NOT NULL,
  `StaffID` int(11) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`TemplateID`),
  KEY `TemplateMustHaveStaffID` (`StaffID`),
  KEY `TemplateMustHaveNoteTemplateCategoryID` (`NoteTemplateCategoryID`),
  CONSTRAINT `TemplateMustHaveNoteTemplateCategoryID` FOREIGN KEY (`NoteTemplateCategoryID`) REFERENCES `notetemplatecategory` (`NoteTemplateCategoryID`),
  CONSTRAINT `TemplateMustHaveStaffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `template`
--

LOCK TABLES `template` WRITE;
/*!40000 ALTER TABLE `template` DISABLE KEYS */;
/*!40000 ALTER TABLE `template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(30) NOT NULL,
  `EmailAddress` varchar(50) DEFAULT NULL,
  `ApplicationName` varchar(30) DEFAULT NULL,
  `Password` varchar(30) NOT NULL,
  `PasswordQuestion` varchar(50) DEFAULT NULL,
  `PasswordAnswer` varchar(50) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastLogin` datetime DEFAULT NULL,
  `LastActivity` datetime DEFAULT NULL,
  `LastPasswordChange` datetime DEFAULT NULL,
  `IsOnline` bit(1) NOT NULL DEFAULT b'0',
  `IpAddress` varchar(20) DEFAULT NULL,
  `IsLockedOut` bit(1) NOT NULL DEFAULT b'0',
  `FailedPasswordAttemptCount` int(11) NOT NULL DEFAULT '0',
  `IsApproved` bit(1) NOT NULL DEFAULT b'0',
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'admin','',NULL,'KBTHCOPD',NULL,NULL,'2011-03-17 12:35:56','0001-01-01 00:00:00','0001-01-01 00:00:00','0001-01-01 00:00:00','\0',NULL,'\0',0,'',''),(3,'tmoore','t.aryee@yahoo.com','/','te1982','','','2011-03-17 12:53:21','2011-03-17 05:53:21','2011-03-17 05:53:21','0001-01-01 00:00:00','','::1','\0',0,'',''),(4,'radu','bobwiner@gmail.com','/','amadaa','','','2011-03-17 12:57:15','2011-03-17 05:57:15','2011-03-17 05:57:15','0001-01-01 00:00:00','','::1','\0',0,'',''),(5,'aakolgo','aakolgo@yahoo.com','/','197809G','','','2011-03-17 16:23:50','2011-03-17 09:23:50','2011-03-17 09:23:50','0001-01-01 00:00:00','','192.168.1.17','\0',0,'',''),(6,'tayobi','','/','quanda','','','2011-03-17 16:40:01','2011-03-17 09:40:01','2011-03-17 09:40:01','0001-01-01 00:00:00','','192.168.1.200','\0',0,'',''),(7,'rbright','richardbright@yahoo.com','/','ACHOSMAN','','','2011-03-18 08:48:35','2011-03-18 01:48:35','2011-03-18 01:48:35','0001-01-01 00:00:00','','192.168.1.17','\0',0,'',''),(8,'jdickson','','/','martey','','','2011-03-18 09:04:00','2011-03-18 02:04:00','2011-03-18 02:04:00','0001-01-01 00:00:00','','192.168.1.15','\0',0,'',''),(9,'brichard','richardbright@yahoo.com','/','ACHOSMAN','','','2011-03-18 09:24:23','2011-03-18 02:24:23','2011-03-18 02:24:23','0001-01-01 00:00:00','','192.168.1.29','\0',0,'\0',''),(10,'eblaber',NULL,'/','242021','','','2011-03-18 09:25:10','2011-03-18 02:25:10','2011-03-18 02:25:10','0001-01-01 00:00:00','','192.168.1.15','\0',0,'\0',''),(11,'kkorbla','richardbright@yahoo.com','/','zagudas','','','2011-03-18 10:00:17','2011-03-18 03:00:17','2011-03-18 03:00:17','0001-01-01 00:00:00','','192.168.1.29','\0',0,'\0',''),(12,'rmensah',NULL,'/','sylvia','','','2011-03-21 09:02:25','2011-03-21 02:02:25','2011-03-21 02:02:25','0001-01-01 00:00:00','','192.168.1.20','\0',0,'\0',''),(13,'bamarh','Nil','/','bamarh','','','2011-03-21 09:18:06','2011-03-21 02:18:06','2011-03-21 02:18:06','0001-01-01 00:00:00','','192.168.1.10','\0',0,'\0',''),(14,'eamarfio','skybelebele2020@gmail.com','/','SNOOPY','','','2011-03-21 09:31:37','2011-03-21 02:31:37','2011-03-21 02:31:37','0001-01-01 00:00:00','','::1','\0',0,'','');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userrole`
--

DROP TABLE IF EXISTS `userrole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userrole` (
  `UserRoleID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `RoleID` int(11) NOT NULL,
  PRIMARY KEY (`UserRoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userrole`
--

LOCK TABLES `userrole` WRITE;
/*!40000 ALTER TABLE `userrole` DISABLE KEYS */;
INSERT INTO `userrole` VALUES (1,1,1),(2,3,1),(3,4,1),(4,2,3),(5,5,2),(6,6,2),(7,8,1),(8,7,2);
/*!40000 ALTER TABLE `userrole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vitals`
--

DROP TABLE IF EXISTS `vitals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vitals` (
  `VitalsID` int(11) NOT NULL AUTO_INCREMENT,
  `Time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Type` bit(5) NOT NULL,
  `Height` float DEFAULT NULL,
  `Weight` float DEFAULT NULL,
  `HeartRate` int(11) DEFAULT NULL,
  `Temperature` decimal(4,1) DEFAULT NULL,
  `BPSystolic` int(11) DEFAULT NULL,
  `BPDiastolic` int(11) DEFAULT NULL,
  `RespiratoryRate` int(11) DEFAULT NULL,
  `PatientCheckInID` int(11) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`VitalsID`),
  KEY `FK_VitalsMustHavePatientCheckInID` (`PatientCheckInID`),
  CONSTRAINT `FK_VitalsMustHavePatientCheckInID` FOREIGN KEY (`PatientCheckInID`) REFERENCES `patientcheckin` (`PatientCheckInID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vitals`
--

LOCK TABLES `vitals` WRITE;
/*!40000 ALTER TABLE `vitals` DISABLE KEYS */;
/*!40000 ALTER TABLE `vitals` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2011-03-22  8:57:12
