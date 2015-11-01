/*
Navicat MySQL Data Transfer

Source Server         : local
Source Server Version : 50525
Source Host           : localhost:3306
Source Database       : users

Target Server Type    : MYSQL
Target Server Version : 50525
File Encoding         : 65001

Date: 2015-11-01 17:31:02
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
-- Records of aspnetroles
-- ----------------------------

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
-- Records of aspnetuserclaims
-- ----------------------------

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
-- Records of aspnetuserlogins
-- ----------------------------

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
-- Records of aspnetuserroles
-- ----------------------------

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
-- Records of aspnetusers
-- ----------------------------
INSERT INTO `aspnetusers` VALUES ('772ce1c9-d3cf-4270-8063-6b8a02e99bb8', 'lexa1805@gmail.com', '0', 'AJ+Bn3MboeFtQbvRVyHcoAUOvUH7q/cDxG3gIaB8DXw39eVQXsNcUgHUXnTH7JEdrg==', '511e32b8-d144-4591-a197-970fe2ea04b1', '23123213123', '0', '0', null, '1', '0', 'Akrex', 'Алексей', 'Крючков', 'eqweqweq', '/Uploads/avatars/772ce1c9-d3cf-4270-8063-6b8a02e99bb8BistroDrive.sql', null);

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
-- Records of chatmessage
-- ----------------------------

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
-- Records of dish
-- ----------------------------
INSERT INTO `dish` VALUES ('16', '772ce1c9-d3cf-4270-8063-6b8a02e99bb8', 'Лапша братишка', 'Очень вкусная лапша', 'Много лапши', '21.00', '40.00', '/Uploads/dish/3d9893a16e538eba3f52f5e3c791e8e6.jpeg', '1');
INSERT INTO `dish` VALUES ('32', '772ce1c9-d3cf-4270-8063-6b8a02e99bb8', 'testtest1', 'testtesttesttest123', 'ewtwetwetwe', '213.32', '3213123.00', '/Uploads/dish/89684377c1706840355555da2e8d9028.png', '4');

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
-- Records of dishreview
-- ----------------------------

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
-- Records of dishtype
-- ----------------------------
INSERT INTO `dishtype` VALUES ('1', 'Первое блюдо', null, null);
INSERT INTO `dishtype` VALUES ('2', 'Второе блюбо', null, null);
INSERT INTO `dishtype` VALUES ('3', 'Десерты', null, null);
INSERT INTO `dishtype` VALUES ('4', 'Завтраки', null, null);
INSERT INTO `dishtype` VALUES ('5', 'Напитки', null, null);
INSERT INTO `dishtype` VALUES ('6', 'Китайская кухня', null, null);
INSERT INTO `dishtype` VALUES ('7', 'Восточная кухня', null, null);

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
-- Records of order
-- ----------------------------
INSERT INTO `order` VALUES ('3', '772ce1c9-d3cf-4270-8063-6b8a02e99bb8', null, '3', '2015-11-01 04:41:45', '2015-11-01 16:25:26', '2015-11-01 06:41:12', '2', '2', '2', '2', '911911', 'petya@pp.ppa', 'Петя', 'Пететкевич', 'daleko daleko', 'rewrw', '15.00');
INSERT INTO `order` VALUES ('4', '772ce1c9-d3cf-4270-8063-6b8a02e99bb8', null, '1', '2015-11-01 04:47:42', null, '2015-11-01 06:47:27', '1', '1', '1', '2', '911911', 'test@test.te', 'Алексей', 'asdasda', 'daleko daleko', null, '40.00');

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
-- Records of ordercontactmethod
-- ----------------------------
INSERT INTO `ordercontactmethod` VALUES ('1', 'Телефон', null);
INSERT INTO `ordercontactmethod` VALUES ('2', 'Email', null);

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
-- Records of orderdelivery
-- ----------------------------
INSERT INTO `orderdelivery` VALUES ('1', 'Самовывоз', null);
INSERT INTO `orderdelivery` VALUES ('2', 'Доставка курьером', null);

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
-- Records of orderingridientbuyer
-- ----------------------------
INSERT INTO `orderingridientbuyer` VALUES ('1', 'Повар');
INSERT INTO `orderingridientbuyer` VALUES ('2', 'Покупатель');

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
-- Records of orderpaymentmethod
-- ----------------------------
INSERT INTO `orderpaymentmethod` VALUES ('1', 'Наличный', null, null);
INSERT INTO `orderpaymentmethod` VALUES ('2', 'Visa', null, null);
INSERT INTO `orderpaymentmethod` VALUES ('3', 'Перевозчик', null, null);

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
-- Records of orderproduct
-- ----------------------------
INSERT INTO `orderproduct` VALUES ('2', '16', '3', '21.00', '40.00');
INSERT INTO `orderproduct` VALUES ('3', '16', '4', '21.00', '40.00');

-- ----------------------------
-- Table structure for orderstatus
-- ----------------------------
DROP TABLE IF EXISTS `orderstatus`;
CREATE TABLE `orderstatus` (
  `Id_Status` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id_Status`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of orderstatus
-- ----------------------------
INSERT INTO `orderstatus` VALUES ('1', 'Новый', null);
INSERT INTO `orderstatus` VALUES ('2', 'Отправлен', null);
INSERT INTO `orderstatus` VALUES ('3', 'Выполнен', null);
INSERT INTO `orderstatus` VALUES ('4', 'Завершена', null);

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
-- Records of review
-- ----------------------------

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

-- ----------------------------
-- Records of userdescription
-- ----------------------------
INSERT INTO `userdescription` VALUES ('1', '772ce1c9-d3cf-4270-8063-6b8a02e99bb8', null);
