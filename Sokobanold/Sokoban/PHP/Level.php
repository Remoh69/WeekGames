<?PHP

	include("Connection.php");
	
	if($_GET["Requete"] == "Add_Level"){
	
		$query = "Delete FROM Sokoban where Level = '" . $_GET["Level"] . "' Order By Num_Check";
		$result = mysql_query($query) or die('Query failed: ' . mysql_error());
		
		$Level_Sok  = $_GET["TAB_Level"];
		$tabLigne = explode("*", $Level_Sok);
		
		$i = 1;
		
		foreach ($tabPorte as $Ligne) {
		
			$sql = 'INSERT INTO Sokoban (Level,Ligne,Valeur) VALUES ("' . $_GET["Level"] . '","' . $i . '","' . $Ligne . '")';
			
			mysql_query($sql);	
			echo mysql_error();
			
			$i++;
			
		}
		
		$i = 0;
		
		echo "ok";		
	}
	else if($_GET["Requete"] == "Get_Level"){
	
		$i = 0;
		$Level = "";
		
		for($i = 0;$i < 10;$i++){
	
			$querySokoban = 'SELECT * from Sokoban Where Level ="' . $_GET["Level"] . '" and Ligne = "' . ($i + 1) . '"';				
			$resultSokoban = mysql_query($querySokoban) or die(mysql_error());
		
			$rowSokoban = mysql_fetch_object($resultSokoban);

			$Level .= $rowSokoban->Valeur . "*";
			
		}	

		echo $Level;
	
	}
	
?>