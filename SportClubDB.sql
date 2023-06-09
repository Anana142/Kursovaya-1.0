﻿--
-- Script was generated by Devart dbForge Studio 2022 for MySQL, Version 9.1.21.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 04.05.2023 9:32:05
-- Server version: 8.0.32
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE sportclub;

--
-- Drop procedure `POMELO_AFTER_ADD_PRIMARY_KEY`
--
DROP PROCEDURE IF EXISTS POMELO_AFTER_ADD_PRIMARY_KEY;

--
-- Drop procedure `POMELO_BEFORE_DROP_PRIMARY_KEY`
--
DROP PROCEDURE IF EXISTS POMELO_BEFORE_DROP_PRIMARY_KEY;

--
-- Drop table `attendance`
--
DROP TABLE IF EXISTS attendance;

--
-- Drop table `sale`
--
DROP TABLE IF EXISTS sale;

--
-- Drop table `subscriptionservice`
--
DROP TABLE IF EXISTS subscriptionservice;

--
-- Drop table `subscription`
--
DROP TABLE IF EXISTS subscription;

--
-- Drop table `client`
--
DROP TABLE IF EXISTS client;

--
-- Drop table `period`
--
DROP TABLE IF EXISTS period;

--
-- Drop table `serviceworkersgraph`
--
DROP TABLE IF EXISTS serviceworkersgraph;

--
-- Drop table `graph`
--
DROP TABLE IF EXISTS graph;

--
-- Drop table `service`
--
DROP TABLE IF EXISTS service;

--
-- Drop table `workers`
--
DROP TABLE IF EXISTS workers;

--
-- Drop table `posts`
--
DROP TABLE IF EXISTS posts;

--
-- Set default database
--
USE sportclub;

--
-- Create table `posts`
--
CREATE TABLE posts (
  Id int NOT NULL AUTO_INCREMENT,
  Post varchar(255) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create table `workers`
--
CREATE TABLE workers (
  Id int NOT NULL AUTO_INCREMENT,
  Surname varchar(255) DEFAULT NULL,
  Name varchar(50) NOT NULL,
  Patronymic varchar(255) DEFAULT NULL,
  IdPost int NOT NULL,
  Birthday date DEFAULT NULL,
  Gender varchar(255) DEFAULT NULL,
  PassportDetails varchar(255) DEFAULT NULL,
  PhoneNumber varchar(255) DEFAULT NULL,
  Email varchar(50) DEFAULT NULL,
  Login varchar(255) DEFAULT NULL,
  Password varchar(255) DEFAULT NULL,
  Street varchar(255) DEFAULT NULL,
  HomeNumber varchar(255) DEFAULT NULL,
  FlatNumber int DEFAULT NULL,
  IsDeleted tinyint(1) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 29,
AVG_ROW_LENGTH = 2048,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create foreign key
--
ALTER TABLE workers
ADD CONSTRAINT FK_workers_IdPost2 FOREIGN KEY (IdPost)
REFERENCES posts (Id);

--
-- Create table `service`
--
CREATE TABLE service (
  Id int NOT NULL AUTO_INCREMENT,
  Title varchar(255) DEFAULT NULL,
  PricePerHour decimal(10, 2) DEFAULT NULL,
  NumberOfPersons int DEFAULT NULL,
  Description varchar(255) DEFAULT NULL,
  IsDeleted tinyint(1) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 34,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create table `graph`
--
CREATE TABLE graph (
  Id int NOT NULL AUTO_INCREMENT,
  DayOfWeek varchar(255) DEFAULT NULL,
  StartTime time DEFAULT NULL,
  EndTime time DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 42,
AVG_ROW_LENGTH = 420,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create table `serviceworkersgraph`
--
CREATE TABLE serviceworkersgraph (
  Id int NOT NULL AUTO_INCREMENT,
  IdService int DEFAULT NULL,
  IdWorker int DEFAULT NULL,
  IdGraph int DEFAULT NULL,
  IsDeleted tinyint(1) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 97,
AVG_ROW_LENGTH = 227,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create foreign key
--
ALTER TABLE serviceworkersgraph
ADD CONSTRAINT FK_ServiceWorkersGraph_IdGraph FOREIGN KEY (IdGraph)
REFERENCES graph (Id);

--
-- Create foreign key
--
ALTER TABLE serviceworkersgraph
ADD CONSTRAINT FK_serviceworkersgraph_IdService2 FOREIGN KEY (IdService)
REFERENCES service (Id);

--
-- Create foreign key
--
ALTER TABLE serviceworkersgraph
ADD CONSTRAINT FK_serviceworkersgraph_IdWorker2 FOREIGN KEY (IdWorker)
REFERENCES workers (Id);

--
-- Create table `period`
--
CREATE TABLE period (
  Id int NOT NULL AUTO_INCREMENT,
  Duration int NOT NULL,
  IsDeleted tinyint(1) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create table `client`
--
CREATE TABLE client (
  Id int NOT NULL AUTO_INCREMENT,
  SurName varchar(255) DEFAULT NULL,
  Name varchar(50) DEFAULT NULL,
  Patronymic varchar(255) DEFAULT NULL,
  Gender varchar(255) DEFAULT NULL,
  PhoneNumber varchar(255) DEFAULT NULL,
  Birthday date DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 54,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create table `subscription`
--
CREATE TABLE subscription (
  Id int NOT NULL AUTO_INCREMENT,
  Status varchar(255) DEFAULT NULL,
  StartDate date DEFAULT NULL,
  IdPeriod int DEFAULT NULL,
  IdClient int DEFAULT NULL,
  TotalVisits int NOT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 32,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create foreign key
--
ALTER TABLE subscription
ADD CONSTRAINT FK_subscription_IdClient FOREIGN KEY (IdClient)
REFERENCES client (Id);

--
-- Create foreign key
--
ALTER TABLE subscription
ADD CONSTRAINT FK_Subscription_IdPeriod FOREIGN KEY (IdPeriod)
REFERENCES period (Id);

--
-- Create table `subscriptionservice`
--
CREATE TABLE subscriptionservice (
  IdService int NOT NULL,
  IdSubscrirtion int NOT NULL,
  Id int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 55,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci;

--
-- Create index `IX_Subscriptionservices_IdService` on table `subscriptionservice`
--
ALTER TABLE subscriptionservice
ADD INDEX IX_Subscriptionservices_IdService (IdService);

--
-- Create index `IX_Subscriptionservices_IdSubscrirtion` on table `subscriptionservice`
--
ALTER TABLE subscriptionservice
ADD INDEX IX_Subscriptionservices_IdSubscrirtion (IdSubscrirtion);

--
-- Create foreign key
--
ALTER TABLE subscriptionservice
ADD CONSTRAINT FK_subscriptionservice_IdService2 FOREIGN KEY (IdService)
REFERENCES serviceworkersgraph (Id);

--
-- Create foreign key
--
ALTER TABLE subscriptionservice
ADD CONSTRAINT FK_subscriptionservice_IdSubscrirtion FOREIGN KEY (IdSubscrirtion)
REFERENCES subscription (Id);

--
-- Create table `sale`
--
CREATE TABLE sale (
  Id int NOT NULL AUTO_INCREMENT,
  IdSubscription int DEFAULT NULL,
  IdWorker int DEFAULT NULL,
  Sum decimal(10, 2) DEFAULT NULL,
  Date datetime DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 28,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create foreign key
--
ALTER TABLE sale
ADD CONSTRAINT FK_sale_IdSubscription FOREIGN KEY (IdSubscription)
REFERENCES subscription (Id);

--
-- Create foreign key
--
ALTER TABLE sale
ADD CONSTRAINT FK_sale_IdWorker2 FOREIGN KEY (IdWorker)
REFERENCES workers (Id);

--
-- Create table `attendance`
--
CREATE TABLE attendance (
  Id int NOT NULL AUTO_INCREMENT,
  IdSubscription int DEFAULT NULL,
  Date datetime DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 126,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_unicode_ci;

--
-- Create foreign key
--
ALTER TABLE attendance
ADD CONSTRAINT FK_attendance_IdSubscription FOREIGN KEY (IdSubscription)
REFERENCES subscription (Id);

DELIMITER $$

--
-- Create procedure `POMELO_BEFORE_DROP_PRIMARY_KEY`
--
CREATE
DEFINER = 'root'@'localhost'
PROCEDURE POMELO_BEFORE_DROP_PRIMARY_KEY (IN `SCHEMA_NAME_ARGUMENT` varchar(255), IN `TABLE_NAME_ARGUMENT` varchar(255))
BEGIN
  DECLARE HAS_AUTO_INCREMENT_ID tinyint(1);
  DECLARE PRIMARY_KEY_COLUMN_NAME varchar(255);
  DECLARE PRIMARY_KEY_TYPE varchar(255);
  DECLARE SQL_EXP varchar(1000);
  SELECT
    COUNT(*) INTO HAS_AUTO_INCREMENT_ID
  FROM `information_schema`.`COLUMNS`
  WHERE `TABLE_SCHEMA` = (SELECT
      IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `Extra` = 'auto_increment'
  AND `COLUMN_KEY` = 'PRI'
  LIMIT 1;
  IF HAS_AUTO_INCREMENT_ID THEN
    SELECT
      `COLUMN_TYPE` INTO PRIMARY_KEY_TYPE
    FROM `information_schema`.`COLUMNS`
    WHERE `TABLE_SCHEMA` = (SELECT
        IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
    AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
    AND `COLUMN_KEY` = 'PRI'
    LIMIT 1;
    SELECT
      `COLUMN_NAME` INTO PRIMARY_KEY_COLUMN_NAME
    FROM `information_schema`.`COLUMNS`
    WHERE `TABLE_SCHEMA` = (SELECT
        IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
    AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
    AND `COLUMN_KEY` = 'PRI'
    LIMIT 1;
    SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT
        IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
    SET @SQL_EXP = SQL_EXP;
    PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
    EXECUTE SQL_EXP_EXECUTE;
    DEALLOCATE PREPARE SQL_EXP_EXECUTE;
  END IF;
END
$$

--
-- Create procedure `POMELO_AFTER_ADD_PRIMARY_KEY`
--
CREATE
DEFINER = 'root'@'localhost'
PROCEDURE POMELO_AFTER_ADD_PRIMARY_KEY (IN `SCHEMA_NAME_ARGUMENT` varchar(255), IN `TABLE_NAME_ARGUMENT` varchar(255), IN `COLUMN_NAME_ARGUMENT` varchar(255))
BEGIN
  DECLARE HAS_AUTO_INCREMENT_ID int(11);
  DECLARE PRIMARY_KEY_COLUMN_NAME varchar(255);
  DECLARE PRIMARY_KEY_TYPE varchar(255);
  DECLARE SQL_EXP varchar(1000);
  SELECT
    COUNT(*) INTO HAS_AUTO_INCREMENT_ID
  FROM `information_schema`.`COLUMNS`
  WHERE `TABLE_SCHEMA` = (SELECT
      IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
  AND `COLUMN_TYPE` LIKE '%int%'
  AND `COLUMN_KEY` = 'PRI';
  IF HAS_AUTO_INCREMENT_ID THEN
    SELECT
      `COLUMN_TYPE` INTO PRIMARY_KEY_TYPE
    FROM `information_schema`.`COLUMNS`
    WHERE `TABLE_SCHEMA` = (SELECT
        IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
    AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
    AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
    AND `COLUMN_TYPE` LIKE '%int%'
    AND `COLUMN_KEY` = 'PRI';
    SELECT
      `COLUMN_NAME` INTO PRIMARY_KEY_COLUMN_NAME
    FROM `information_schema`.`COLUMNS`
    WHERE `TABLE_SCHEMA` = (SELECT
        IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
    AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
    AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
    AND `COLUMN_TYPE` LIKE '%int%'
    AND `COLUMN_KEY` = 'PRI';
    SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT
        IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
    SET @SQL_EXP = SQL_EXP;
    PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
    EXECUTE SQL_EXP_EXECUTE;
    DEALLOCATE PREPARE SQL_EXP_EXECUTE;
  END IF;
END
$$

DELIMITER ;

-- 
-- Dumping data for table posts
--
INSERT INTO posts VALUES
(1, 'Менеджерr'),
(2, 'Тренер');

-- 
-- Dumping data for table workers
--
INSERT INTO workers VALUES
(1, 'Ушакова', 'Анна', 'Вячеславовна', 1, '1990-03-10', 'ж', '12345 1234', '+792464290858', 'smthmail@mail.ru', '1', '1', 'ул.Арсеньева', '48', 4, 0),
(2, 'Синицина', 'Наталья', 'Владимировна', 2, '2000-07-12', 'ж', '125690 6789', '+79841473488', 'abcd@mail.ru', NULL, NULL, 'ул.Фадеева ', '76', 3, 0),
(3, 'Иванова ', 'Арина', 'Сергеевна', 2, '2000-01-12', 'женский', '123455 7890', '+79841286436', 'ashhjg@mail.ru', NULL, NULL, 'ул.Октябрьская ', '4', 45, 0),
(4, 'Романов', 'Данил', 'Александрович', 2, '1998-01-01', 'мужской', '345664 1285', '+79145689436', 'lkjuterc@mail.ru', NULL, NULL, 'ул.Советская ', '32a', 15, 0),
(5, 'Смирнов', 'Егор', 'Алексеевич', 2, '2001-04-16', 'мужской', '675433 5785', '+79246785123', 'fghfghse@mail.ru', NULL, NULL, 'ул.Солнечная', '2', 4, 0),
(6, 'Понаморева', 'Алина', 'Витальевна', 2, '2002-10-08', 'женский', '679890 5421', '+79241578653', 'zxcvbn@mail.ru\r\n', NULL, NULL, 'ул.Пионерская', '2', 42, 0),
(7, 'Русланова', 'Валентина', 'Игнатьевна', 2, '1999-09-20', 'женский', '875341 2345', '+79245678532', 'qpiuyt@mail.ru', NULL, NULL, 'ул.Индустриальная ', '5', 51, 0),
(28, 'Алексеева', 'Анна', 'Петровна', 1, '1990-08-30', NULL, NULL, '+79245978345', 'anna1990@mail.ru', NULL, NULL, 'Уличная', '23а', 2, NULL);

-- 
-- Dumping data for table service
--
INSERT INTO service VALUES
(2, 'Бассейн', 400.00, 1, 'Описание2', NULL),
(3, 'Тренажерный зал', 200.00, 1, 'Описание', NULL),
(5, 'Йога', 350.00, 10, 'Описание', NULL),
(6, 'Бассейн', 350.00, 10, 'Описание', NULL),
(8, 'Тренажерный зал', 150.00, 10, 'Описание', 0),
(33, 'Тренажер для жопы', 1000000.00, 1, 'Трррренажр для жооопыыыы!!!!', 1);

-- 
-- Dumping data for table graph
--
INSERT INTO graph VALUES
(1, 'Понедельник', '12:00:00', '13:00:00'),
(2, 'Понедельник', '13:00:00', '14:00:00'),
(3, 'Понедельник', '14:00:00', '15:00:00'),
(4, 'Понедельник', '15:00:00', '16:00:00'),
(5, 'Понедельник', '16:00:00', '17:00:00'),
(6, 'Понедельник', '17:00:00', '18:00:00'),
(7, 'Понедельник', '18:00:00', '19:00:00'),
(8, 'Понедельник', '19:00:00', '20:00:00'),
(9, 'Вторник', '12:00:00', '13:00:00'),
(10, 'Вторник', '13:00:00', '14:00:00'),
(11, 'Вторник', '14:00:00', '15:00:00'),
(12, 'Вторник', '15:00:00', '16:00:00'),
(13, 'Вторник', '16:00:00', '17:00:00'),
(14, 'Вторник', '17:00:00', '18:00:00'),
(15, 'Вторник', '18:00:00', '19:00:00'),
(16, 'Вторник', '19:00:00', '20:00:00'),
(17, 'Среда', '12:00:00', '13:00:00'),
(18, 'Среда ', '13:00:00', '14:00:00'),
(19, 'Среда', '14:00:00', '15:00:00'),
(20, 'Среда', '15:00:00', '16:00:00'),
(21, 'Среда', '16:00:00', '17:00:00'),
(22, 'Среда', '17:00:00', '18:00:00'),
(23, 'Среда', '18:00:00', '19:00:00'),
(24, 'Среда', '19:00:00', '20:00:00'),
(25, 'Четверг', '12:00:00', '13:00:00'),
(26, 'Четверг', '13:00:00', '14:00:00'),
(27, 'Четверг', '14:00:00', '15:00:00'),
(28, 'Четверг', '15:00:00', '16:00:00'),
(29, 'Четверг', '16:00:00', '17:00:00'),
(30, 'Четверг', '17:00:00', '18:00:00'),
(31, 'Четверг', '18:00:00', '19:00:00'),
(32, 'Четверг', '19:00:00', '20:00:00'),
(33, 'Пятница', '12:00:00', '13:00:00'),
(34, 'Пятница', '13:00:00', '14:00:00'),
(35, 'Пятница', '14:00:00', '15:00:00'),
(36, 'Пятница', '15:00:00', '16:00:00'),
(37, 'Пятница', '16:00:00', '17:00:00'),
(38, 'Пятница', '17:00:00', '18:00:00'),
(39, 'Пятница', '18:00:00', '19:00:00'),
(40, 'Пятница', '19:00:00', '20:00:00'),
(41, 'Суббота', '08:00:00', '09:00:00');

-- 
-- Dumping data for table period
--
INSERT INTO period VALUES
(1, 1, NULL),
(2, 14, NULL);

-- 
-- Dumping data for table client
--
INSERT INTO client VALUES
(46, 'Юзвенко', 'Сергей', 'Алексеевич', 'мужской', '+79246535787', '2001-12-09'),
(47, 'Демиденко', 'Алина', 'Евгеньевна', 'женский', '+79841956797', '2000-09-12'),
(48, 'Петров', 'Сергей', 'Витальевич', 'мужской', '+7924157964', '2001-04-05'),
(52, 'Петров', 'Сергей', 'Витальевич', 'мужской', '+7984195634', '2001-03-12'),
(53, 'Юров', 'Александр', 'Алексеевич', 'мужской', '+793645829456', '2000-05-08');

-- 
-- Dumping data for table serviceworkersgraph
--
INSERT INTO serviceworkersgraph VALUES
(1, 8, 7, 5, NULL),
(2, 8, 7, 6, NULL),
(3, 8, 7, 7, NULL),
(4, 8, 7, 8, NULL),
(5, 3, 4, 5, NULL),
(6, 3, 4, 6, NULL),
(7, 3, 4, 7, NULL),
(8, 3, 4, 8, NULL),
(9, 8, 4, 9, NULL),
(10, 8, 4, 10, NULL),
(11, 8, 4, 11, NULL),
(12, 8, 4, 12, NULL),
(13, 3, 7, 13, NULL),
(14, 3, 7, 14, NULL),
(15, 3, 7, 15, NULL),
(16, 3, 7, 16, NULL),
(17, 3, 7, 17, NULL),
(18, 3, 7, 18, NULL),
(19, 3, 7, 19, NULL),
(20, 3, 7, 20, NULL),
(21, 8, 4, 21, NULL),
(22, 8, 4, 22, NULL),
(23, 8, 4, 23, NULL),
(24, 8, 4, 24, NULL),
(25, 8, 4, 25, NULL),
(26, 8, 4, 26, NULL),
(27, 8, 4, 27, NULL),
(28, 8, 4, 28, NULL),
(29, 3, 7, 29, NULL),
(30, 3, 7, 30, NULL),
(31, 3, 7, 31, NULL),
(32, 3, 7, 32, NULL),
(33, 3, 7, 33, NULL),
(34, 3, 7, 34, NULL),
(35, 3, 7, 35, NULL),
(36, 3, 7, 36, NULL),
(37, 8, 4, 37, NULL),
(38, 8, 4, 38, NULL),
(39, 8, 4, 39, NULL),
(41, 5, 3, 1, NULL),
(42, 5, 6, 2, NULL),
(45, 5, 3, 9, NULL),
(46, 5, 6, 10, NULL),
(49, 5, 3, 17, NULL),
(50, 5, 6, 18, NULL),
(53, 5, 3, 25, NULL),
(54, 5, 6, 26, NULL),
(57, 5, 3, 33, NULL),
(58, 5, 6, 34, NULL),
(61, 6, 2, 1, NULL),
(62, 6, 5, 2, NULL),
(65, 2, 2, 5, NULL),
(66, 2, 5, 6, NULL),
(67, 6, 2, 9, NULL),
(68, 6, 5, 10, NULL),
(71, 2, 2, 13, NULL),
(72, 2, 5, 14, NULL),
(73, 6, 2, 17, NULL),
(74, 6, 5, 18, NULL),
(77, 2, 2, 21, NULL),
(78, 2, 5, 22, NULL),
(80, 6, 5, 26, NULL),
(83, 2, 2, 29, NULL),
(84, 2, 5, 30, NULL),
(85, 6, 2, 33, NULL),
(86, 6, 5, 34, NULL),
(89, 2, 2, 37, NULL),
(90, 2, 5, 38, NULL),
(93, 3, 3, 2, NULL),
(94, 2, 2, 15, NULL),
(96, 3, 3, 14, NULL);

-- 
-- Dumping data for table subscription
--
INSERT INTO subscription VALUES
(24, 'Неактивен', '2023-04-30', 2, 46, 2),
(26, 'Неактивен', '2023-04-30', 2, 48, 2),
(27, 'Неактивен', '2023-04-30', 1, 47, 1);

-- 
-- Dumping data for table subscriptionservice
--
INSERT INTO subscriptionservice VALUES
(85, 24, 39),
(54, 26, 41),
(58, 27, 42);

-- 
-- Dumping data for table sale
--
INSERT INTO sale VALUES
(20, 24, 1, 900.00, '2023-04-30 14:12:41'),
(22, 26, 1, 900.00, '2023-04-30 14:13:07'),
(23, 27, 1, 450.00, '2023-04-30 14:13:20');

-- 
-- Dumping data for table attendance
--
INSERT INTO attendance VALUES
(121, 27, '2023-04-30 22:50:58'),
(122, 26, '2023-04-30 22:51:01'),
(123, 26, '2023-04-30 22:51:02'),
(124, 24, '2023-05-03 18:03:38'),
(125, 24, '2023-05-03 18:03:39');

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;