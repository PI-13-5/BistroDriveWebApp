/*
Navicat MySQL Data Transfer

Source Server         : local
Source Server Version : 50525
Source Host           : localhost:3306
Source Database       : users

Target Server Type    : MYSQL
Target Server Version : 50525
File Encoding         : 65001

Date: 2015-11-01 05:20:52
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for aspnetroles
-- ----------------------------
DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE `aspnetroles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for aspnetuserclaims
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId` (`UserId`),
  CONSTRAINT `FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for aspnetuserlogins
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserlogins`;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId` (`UserId`),
  CONSTRAINT `FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for aspnetuserroles
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for aspnetusers
-- ----------------------------
DROP TABLE IF EXISTS `aspnetusers`;
CREATE TABLE `aspnetusers` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(4) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(4) NOT NULL,
  `TwoFactorEnabled` tinyint(4) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(4) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(128) NOT NULL,
  `FristName` varchar(128) NOT NULL,
  `Surname` varchar(128) NOT NULL,
  `Address` varchar(256) DEFAULT NULL,
  `Avatar_Url` varchar(128) DEFAULT NULL,
  `LastOnlineTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for chatmessage
-- ----------------------------
DROP TABLE IF EXISTS `chatmessage`;
CREATE TABLE `chatmessage` (
  `Id_Message` int(11) NOT NULL AUTO_INCREMENT,
  `Id_Sender` varchar(128) NOT NULL,
  `Id_Reciver` varchar(128) NOT NULL,
  `Message` varchar(256) NOT NULL,
  `CreateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id_Message`),
  KEY `FK_ChatMessage_IdSender` (`Id_Sender`),
  KEY `FK_ChatMessage_IdReciver` (`Id_Reciver`),
  CONSTRAINT `FK_ChatMessage_IdReciver` FOREIGN KEY (`Id_Reciver`) REFERENCES `aspnetroles` (`Id`),
  CONSTRAINT `FK_ChatMessage_IdSender` FOREIGN KEY (`Id_Sender`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for dish
-- ----------------------------
DROP TABLE IF EXISTS `dish`;
CREATE TABLE `dish` (
  `Id_Dish` int(11) NOT NULL AUTO_INCREMENT,
  `Id_User` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Description` text,
  `Ingridient` text,
  `Price` decimal(10,2) NOT NULL,
  `PriceWithIngridient` decimal(10,2) NOT NULL,
  `ImageUrl` varchar(128) DEFAULT NULL,
  `Id_Type` int(11) NOT NULL,
  PRIMARY KEY (`Id_Dish`),
  KEY `FK_Dish_IdType` (`Id_Type`),
  KEY `FK_Dish_IdUser_aspnetusers_Id` (`Id_User`),
  CONSTRAINT `FK_Dish_IdType` FOREIGN KEY (`Id_Type`) REFERENCES `dishtype` (`Id_DishType`),
  CONSTRAINT `FK_Dish_IdUser_aspnetusers_Id` FOREIGN KEY (`Id_User`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for dishreview
-- ----------------------------
DROP TABLE IF EXISTS `dishreview`;
CREATE TABLE `dishreview` (
  `Id_Review` int(11) NOT NULL AUTO_INCREMENT,
  `Id_Owner` varchar(128) NOT NULL,
  `Id_Dish` int(11) NOT NULL,
  `Description` longtext,
  `Mark` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id_Review`),
  KEY `FK_DishReview_IdOwner` (`Id_Owner`),
  KEY `FK_DishReview_IdDish` (`Id_Dish`),
  CONSTRAINT `FK_DishReview_IdDish` FOREIGN KEY (`Id_Dish`) REFERENCES `dish` (`Id_Dish`) ON DELETE CASCADE,
  CONSTRAINT `FK_DishReview_IdOwner` FOREIGN KEY (`Id_Owner`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for dishtype
-- ----------------------------
DROP TABLE IF EXISTS `dishtype`;
CREATE TABLE `dishtype` (
  `Id_DishType` int(11) NOT NULL AUTO_INCREMENT,
  `Name` char(128) NOT NULL,
  `Descritpion` longtext,
  `Image_URL` char(128) DEFAULT NULL,
  PRIMARY KEY (`Id_DishType`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for order
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order` (
  `Id_Order` int(11) NOT NULL AUTO_INCREMENT,
  `Id_Cook` varchar(128) NOT NULL,
  `Id_Customer` varchar(128) DEFAULT NULL,
  `Id_Status` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `FinishTime` datetime DEFAULT NULL,
  `DeadLine` datetime NOT NULL,
  `Id_PaymentMethod` int(11) NOT NULL,
  `Id_ContactMethod` int(11) NOT NULL,
  `Id_Delivery` int(11) NOT NULL,
  `id_IngridientsBuyer` int(11) NOT NULL,
  `Phone` varchar(128) NOT NULL,
  `Email` varchar(128) NOT NULL,
  `FirstName` varchar(128) NOT NULL,
  `Surname` varchar(128) NOT NULL,
  `Address` varchar(128) NOT NULL,
  `Comment` varchar(512) DEFAULT NULL,
  `total` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id_Order`),
  KEY `FK_Order_IdStatus` (`Id_Status`),
  KEY `FK_Order_IdContactMethod` (`Id_ContactMethod`),
  KEY `FK_Order_IdDelivery` (`Id_Delivery`),
  KEY `FK_Order_IdPaymentMethod` (`Id_PaymentMethod`),
  KEY `FK_Order_IdCook` (`Id_Cook`),
  KEY `id_IngridientsBuyer` (`id_IngridientsBuyer`),
  CONSTRAINT `FK_Order_IdContactMethod` FOREIGN KEY (`Id_ContactMethod`) REFERENCES `ordercontactmethod` (`Id_ContactMethod`),
  CONSTRAINT `FK_Order_IdCook` FOREIGN KEY (`Id_Cook`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Order_IdDelivery` FOREIGN KEY (`Id_Delivery`) REFERENCES `orderdelivery` (`Id_Delivery`),
  CONSTRAINT `FK_Order_IdPaymentMethod` FOREIGN KEY (`Id_PaymentMethod`) REFERENCES `orderpaymentmethod` (`Id_PaymentMethod`),
  CONSTRAINT `FK_Order_IdStatus` FOREIGN KEY (`Id_Status`) REFERENCES `orderstatus` (`Id_Status`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`id_IngridientsBuyer`) REFERENCES `orderingridientbuyer` (`Id_IngridientBuyer`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for ordercontactmethod
-- ----------------------------
DROP TABLE IF EXISTS `ordercontactmethod`;
CREATE TABLE `ordercontactmethod` (
  `Id_ContactMethod` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id_ContactMethod`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for orderdelivery
-- ----------------------------
DROP TABLE IF EXISTS `orderdelivery`;
CREATE TABLE `orderdelivery` (
  `Id_Delivery` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id_Delivery`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for orderingridientbuyer
-- ----------------------------
DROP TABLE IF EXISTS `orderingridientbuyer`;
CREATE TABLE `orderingridientbuyer` (
  `Id_IngridientBuyer` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(128) NOT NULL,
  PRIMARY KEY (`Id_IngridientBuyer`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for orderpaymentmethod
-- ----------------------------
DROP TABLE IF EXISTS `orderpaymentmethod`;
CREATE TABLE `orderpaymentmethod` (
  `Id_PaymentMethod` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  `PaymentProvider` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`Id_PaymentMethod`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for orderproduct
-- ----------------------------
DROP TABLE IF EXISTS `orderproduct`;
CREATE TABLE `orderproduct` (
  `Id_Product` int(11) NOT NULL AUTO_INCREMENT,
  `Id_Dish` int(11) NOT NULL,
  `Id_Order` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `PriceWithIngridients` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`Id_Product`),
  KEY `FK_OrderProduct_IdDish` (`Id_Dish`),
  KEY `FK_OrderProduct_IdOrder` (`Id_Order`),
  CONSTRAINT `FK_OrderProduct_IdDish` FOREIGN KEY (`Id_Dish`) REFERENCES `dish` (`Id_Dish`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrderProduct_IdOrder` FOREIGN KEY (`Id_Order`) REFERENCES `order` (`Id_Order`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for orderstatus
-- ----------------------------
DROP TABLE IF EXISTS `orderstatus`;
CREATE TABLE `orderstatus` (
  `Id_Status` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id_Status`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for review
-- ----------------------------
DROP TABLE IF EXISTS `review`;
CREATE TABLE `review` (
  `Id_UserReview` int(11) NOT NULL AUTO_INCREMENT,
  `Id_User` varchar(128) NOT NULL,
  `Id_Owner` varchar(128) NOT NULL,
  `Text` longtext,
  `Mark` char(10) DEFAULT NULL,
  PRIMARY KEY (`Id_UserReview`),
  KEY `FK_Review_IdUser` (`Id_User`),
  KEY `FK_Review_IdOwner` (`Id_Owner`),
  CONSTRAINT `FK_Review_IdOwner` FOREIGN KEY (`Id_Owner`) REFERENCES `aspnetroles` (`Id`),
  CONSTRAINT `FK_Review_IdUser` FOREIGN KEY (`Id_User`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for userdescription
-- ----------------------------
DROP TABLE IF EXISTS `userdescription`;
CREATE TABLE `userdescription` (
  `Id_Description` int(4) NOT NULL AUTO_INCREMENT,
  `Id_User` varchar(128) NOT NULL,
  `Description` text,
  PRIMARY KEY (`Id_Description`),
  KEY `description_userid_to_aspnetuser_userid` (`Id_User`),
  CONSTRAINT `description_userid_to_aspnetuser_userid` FOREIGN KEY (`Id_User`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
