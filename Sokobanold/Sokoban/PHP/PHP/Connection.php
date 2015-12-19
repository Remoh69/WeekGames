<?php

	$server = '127.0.0.1';
	$login='g_grousson38540';
	$pass='mmo2015';
	$db='g_grousson38540';

	$sConn = mysql_connect($server, $login, $pass) or die ('Erreur de connection serveur SQL' );
	mysql_select_db($db);

?>