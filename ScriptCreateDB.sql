﻿ CREATE DATABASE `GROWLERDB`;

 CREATE TABLE `GROWLER_LOG` (
  `IDGROWLER` int(11) DEFAULT NULL,
  `TMPGROWLER` decimal(10,2) DEFAULT NULL,
  `BATGROWLER` decimal(10,2) DEFAULT NULL,
  `DTALOGGROWLER` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


CREATE TABLE `GROWLER` (
  `IDGROWLER` int(11) DEFAULT NULL,
  `VLRTMPIDEGROWLER` decimal(10,2) DEFAULT NULL,
  `IDCALATMP` int(11) DEFAULT NULL,
  `IDCMON` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;