<?PHP

	include("Connection.php");
	
	if($_GET["Requete"] == "Add_Level"){
	
		$query = "Delete FROM Sokoban where Level = '" . $_GET["Level"] . "' Order By Num_Check";
		$result = mysql_query($query) or die('Query failed: ' . mysql_error());
		
		$Level_Sok  = $_GET["Save"];
		$tabLigne = explode("*", $Level_Sok);
		
		foreach ($tabPorte as $Ligne) {
		
			$sql = 'INSERT INTO Sokoban (Level,Ligne,Valeur) VALUES ("' . $_GET["Level"] . '","' . $i . '","' . $Ligne . '")';
			mysql_query($sql);	
			echo mysql_error();
			
			$i++;
			
		}
		
		$i = 0;
			
	}
	else if($_GET["Requete"] == "Get_Level"){
	
		$i = 0;
		$Level = "";
		
		for($i = 0;$i < 10;$i++){
	
			$querySokoban = 'SELECT * from Sokoban Where Level ="' . $_GET["Level"] . '" and Ligne = "' . $i . '"';				
			$resultSokoban = mysql_query($querySokoban) or die(mysql_error());
		
			$rowSokoban = mysql_fetch_object($resultSokoban);

			$Level .= $rowSokoban->Valeur;
			
		}	

		echo $Level;
	
	}
	else if($_GET["Requete"] == "Creer_Compte"){
	
		$query = 'SELECT count(*) from Utilisateurs_Sokoban where Login="' . $_GET["Login"] . '"';
		$result = mysql_query($query) or die(mysql_error());
		
		$Existe = mysql_fetch_array($result);
		
		if ($Existe[0] == 0){
		
			$query = 'SELECT count(*) from Utilisateurs_Sokoban where Email="' . $_GET["Mail"] . '"';
			$result = mysql_query($query) or die(mysql_error());
			
			$Existe = mysql_fetch_array($result);
			
			if ($Existe[0] == 0){
			
				$query = 'INSERT INTO Utilisateurs_Sokoban (Login,Password,Email) VALUES ("' . $_GET["Login"] . '","' . $_GET["Pass"] . '","' . $_GET["Mail"] . '")';

				mysql_query($query) or die(mysql_error());
				
				//$Id = mysql_insert_id();
				
				echo 'Ok';
				
			}
			else{
				
				echo 'Doublons';
				
		}
		else{
			
			echo 'Doublons';
			
		}
	
	}
	else if ($_GET["Requete"] == "Connection"){
		
		$query = 'SELECT count(*) from Utilisateurs_Sokoban where Email="' . $_GET["Mail"] . '"';
		$result = mysql_query($query) or die(mysql_error());
		
		$Existe = mysql_fetch_array($result);
		
		if ($Existe[0] == 1){
		
			$query = 'SELECT * from Utilisateurs_Sokoban where Email="' . $_GET["Mail"] . '"';
			$result = mysql_query($query) or die(mysql_error());
			
			$row = mysql_fetch_object($result);
			
			if($row->Password == $_GET["Pass"])
			{
						
				echo 'Ok_' . $row->Login;
				
			}
			else
				echo 'Erreur_Login ou Password incorrect !';
		
		}else{
		
			echo 'Erreur_Login ou Password incorrect !';
		
		}
		
	}
	
	
	
?>