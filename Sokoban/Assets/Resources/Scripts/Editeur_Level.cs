using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;


public class Editeur_Level : MonoBehaviour {

	public Sprite[] tabCase;
	public Image[] tabCase_Sel;

	int[,] tabCase_Level = {{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}};
	string m_sNumero_Level;
	string sLevelURL = "http://g.grousson38540.free.fr/Jeu_3D/Sokoban/PHP/Level.php";

	int m_nImage;

	WWW wwwLevelURL;

	// Use this for initialization
	void Start () {

		m_nImage = 0;
		m_sNumero_Level = "";

		for (int i = 0; i < tabCase_Sel.Length; i++) {
			
			tabCase_Sel[i].enabled = false;
			
		}
		
		tabCase_Sel[m_nImage].enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	public void Set_Image(int nImg){

		m_nImage = nImg;

		for (int i = 0; i < tabCase_Sel.Length; i++) {

			tabCase_Sel[i].enabled = false;

		}

		tabCase_Sel[nImg].enabled = true;

	}

	public void Click_Image(Image imgMyImg){

		imgMyImg.sprite = tabCase[m_nImage];

	}

	public void Click_ImageXY(string sXY){

		string [] split = sXY.Split ('_');

		tabCase_Level[int.Parse (split[0]),int.Parse (split[1])] = m_nImage;
		
	}

	public void Save_Numero_Level(string sLevel){
		
		m_sNumero_Level = sLevel;
		
	}

	public void Save_Level(){

		if (m_sNumero_Level != "") {
	
			StartCoroutine (Save_LevelSQL());


	
		}
	
	}

	IEnumerator Save_LevelSQL(){

		int nLevel;
		
		nLevel = int.Parse (m_sNumero_Level);
		
		int i = 0;
		int j = 0;
		string sLigne;
		sLigne = "";

		while(i < 10){

			j = 0;
			
			while(j < 10){
				
				sLigne += tabCase_Level[i,j];
				
				j++;
			}

			sLigne += "*";

			i++;
			
		}



		wwwLevelURL = new WWW (sLevelURL + "?Requete=Add_Level&Level=" + nLevel + "&TAB_Level=" + sLigne );
		yield return wwwLevelURL;
		
		if (wwwLevelURL.error != null) {
			Debug.Log ("Erreur " + wwwLevelURL.error);
		}
		else{

			if(wwwLevelURL.text.ToString() == "ok"){



			}
			
		}
		
	}




}
