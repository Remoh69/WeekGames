using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;


public class Editeur_Level : MonoBehaviour {

	public Sprite[] tabCase;
	public Image[] tabCase_Sel;

	int[,] tabCase_Level = {{0,0,0,0,0,0,0,0,0,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,0},{0,0,0,0,0,0,0,0,0,0}};
	string m_sNumero_Level;
	string sLevelURL = "http://g.grousson38540.free.fr/Jeu_3D/Sokoban/PHP/Level.php";

	int m_nImage;
	Image m_imgCase_Temp;

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

		m_imgCase_Temp = imgMyImg;

	}

	public void Click_ImageXY(string sXY){

		int x = 0;
		int y = 0;

		string [] split = sXY.Split ('_');

		x = int.Parse (split [0]);
		y = int.Parse (split [1]);

		if (m_nImage == 4) {

			if (x != y && (x == 0 || y == 0 || x == 9 || y == 9) && !(x == 9 && y == 0) && !(x == 0 && y == 9)) {

				tabCase_Level [x, y] = m_nImage;
			
				if (y == 0 || y == 9) {

					Vector3 vAngle = m_imgCase_Temp.transform.rotation.eulerAngles;

					vAngle.z = 90.0f;

					m_imgCase_Temp.transform.rotation = Quaternion.Euler (vAngle);

					//m_imgCase_Temp.transform.Rotate(new Vector3(0,0,90));
				
				}
						

				m_imgCase_Temp.sprite = tabCase [m_nImage];
			

			}

		} else if (m_nImage > 0) {

			if(x != 0 && y != 0 && x != 9 && y != 9){

				tabCase_Level [x, y] = m_nImage;
				m_imgCase_Temp.sprite = tabCase[m_nImage];
			
			}

		}else {

			tabCase_Level [x, y] = m_nImage;
			m_imgCase_Temp.sprite = tabCase[m_nImage];


		}


	}

	public void Save_Numero_Level(string sLevel){
		
		m_sNumero_Level = sLevel;
		
	}

	public void Save_Level(){

		if (m_sNumero_Level != "") {
	
			int i = 0;
			int j = 0;

			int nNb_Sortie = 0;
			int nNb_PosPlay = 0;
			int nNbCaisse = 0;
			int nNbEmp_Caisse = 0;

			
			while(i < 10){
				
				j = 0;
				
				while(j < 10){
					
					if(tabCase_Level[i,j] == 2)
						nNbCaisse++;

					if(tabCase_Level[i,j] == 3)
						nNbEmp_Caisse++;

					if(tabCase_Level[i,j] == 4)
						nNb_Sortie++;

					if(tabCase_Level[i,j] == 5)
						nNb_PosPlay++;
					
					j++;
				}
				
				i++;
				
			}

			if(nNb_PosPlay == 1 && nNb_Sortie == 1 && nNbEmp_Caisse != 0 && nNbEmp_Caisse == nNbCaisse){

				StartCoroutine (Save_LevelSQL());

			}else{
				Debug.Log ("ko" + nNb_PosPlay + "  " + nNb_Sortie + "  " + nNbEmp_Caisse + "  " + nNbCaisse);
			}
				
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

			}else{

				Debug.Log (wwwLevelURL.text.ToString());

			}
			
		}
		
	}




}
