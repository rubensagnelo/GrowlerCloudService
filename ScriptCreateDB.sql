﻿/*

CREATE DATABASE `GROWLERDB`;


DROP table growler;

CREATE TABLE `growler` (
`IDGROWLER` varchar(50) DEFAULT NULL,
`VLRTMPIDEGROWLER` decimal(10,2) DEFAULT NULL,
`IDCALATMP` int(11) DEFAULT NULL,
`IDCMON` int(11) DEFAULT NULL
) 
ENGINE=InnoDB DEFAULT CHARSET=latin1;



drop table growler_log;

CREATE TABLE `growler_log` (
`IDGROWLER` varchar(50) DEFAULT NULL,
`TMPGROWLER` decimal(10,2) DEFAULT NULL,
`BATGROWLER` decimal(10,2) DEFAULT NULL,  
`DTALOGGROWLER` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) 
ENGINE=InnoDB DEFAULT CHARSET=latin1;
 commit;

*/

/*
INSERT INTO GROWLER(IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES('1', '2.51', '1', '1');
INSERT INTO GROWLER(IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES('2', '2.00', '1', '1');
INSERT INTO GROWLER(IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES('3', '4.20', '1', '1');
INSERT INTO GROWLER(IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES('7', '1.02', '1', '1');


INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('2', '5.32', '95.00', '2016-11-29 20:30:57');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('3', '4.32', '87.00', '2016-11-29 20:31:22');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('1', '4.32', '96.00', '2016-11-29 20:32:08');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('5', '4.32', '96.00', '2016-11-30 06:23:14');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('2', '2.32', '88.00', '2016-11-30 06:50:14');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('2', '1.90', '80.00', '2016-12-01 00:37:24');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('2', '1.80', '77.00', '2016-12-01 00:39:31');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('12', '11.90', '40.00', '2016-12-01 20:32:44');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-01 20:33:27');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:03:05');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:04:59');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:05:20');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:05:40');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:06:01');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:06:22');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:06:43');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:07:03');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('11', '11.90', '40.00', '2016-12-02 00:07:23');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('13', '-127.00', '2800.00', '2016-12-02 00:08:22');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('13', '-127.00', '2800.00', '2016-12-02 00:08:42');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('13', '-127.00', '2802.00', '2016-12-02 00:09:43');
INSERT INTO growler_log (IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) VALUES('13', '-127.00', '2818.00', '2016-12-02 00:10:03');

*/