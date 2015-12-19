<?PHP
	include("Connection.php");
	
	if(isset($_POST["Plus"])){
	
		$query = 'SELECT * from Like_Dislike where Adresse_Ip = "' . $_SERVER['REMOTE_ADDR'] . '"';
		$result = mysql_query($query) or die(mysql_error());
			
		$num_results = mysql_num_rows($result);
		
		if($num_results == 0)
			$sql = 'INSERT INTO Like_Dislike(Nom,Nb_Like,Nb_Dislike,Adresse_Ip) VALUES ("oxxo","1","0","' . $_SERVER['REMOTE_ADDR'] .'")';
		else
			$sql = 'UPDATE Like_Dislike SET Nb_Like="1", Nb_Dislike="0" where Nom = "oxxo" and Adresse_Ip = "' . $_SERVER['REMOTE_ADDR'] . '"';
			
		mysql_query($sql) or die(mysql_error());
	
	}else if(isset($_POST["Moins"])){
	
		$query = 'SELECT * from Like_Dislike where Adresse_Ip = "' . $_SERVER['REMOTE_ADDR'] .'"';
		$result = mysql_query($query) or die(mysql_error());
			
		$num_results = mysql_num_rows($result);
		
		if($num_results == 0)
			$sql = 'INSERT INTO Like_Dislike(Nom,Nb_Like,Nb_Dislike,Adresse_Ip) VALUES ("oxxo","0","1","' . $_SERVER['REMOTE_ADDR'] .'")';
		else
			$sql = 'UPDATE Like_Dislike SET Nb_Like="0", Nb_Dislike="1" where Nom = "oxxo" and Adresse_Ip = "' . $_SERVER['REMOTE_ADDR'] . '"';
			
		mysql_query($sql) or die(mysql_error());		
	
	}
	
	$Like = 0;
	$DisLike = 0;
	
	$query = 'SELECT * from Like_Dislike where Nom = "oxxo"';
	$result = mysql_query($query) or die(mysql_error());

	$num_results = mysql_num_rows($result);
	
	for($i = 0; $i < $num_results; $i++)
	{
		
		$row = mysql_fetch_object($result) or die(mysql_error());
		
		if($row->Nb_Like == 1)
			$Like++;
			
		if($row->Nb_Dislike == 1)
			$DisLike++;
		
	}
	
	
	
	
?>

<form action='http://www.polygonsnake.com/oxxo.php' method='post' name='Like_DisLike'>

	<?php
		
		echo "<input type='hidden' name='Like' value='" . $Like . "'>";
		echo "<input type='hidden' name='DisLike' value='" . $DisLike . "'>";
		
	?>

</form>

<script language="JavaScript">
	document.Like_DisLike.submit();
</script>

