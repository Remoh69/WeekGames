using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;


public class Editeur_Level : MonoBehaviour {

	public Sprite[] tabCase;
	public Image[] tabCase_Sel;
	public GameObject m_goCase;
	public GameObject m_goGrille;

	bool m_bTest_Level_Ok;
	bool m_bLance_Test;

	public GameObject m_imgSim;

	int[,] tabCase_Level = {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
	int[,] tabCase_Test_Level;

	string m_sNumero_Level;
	string sLevelURL = "http://g.grousson38540.free.fr/Jeu_3D/Sokoban/PHP/Level.php";

	int m_nImage;
	int m_nSave_Case_Test = 1;

	Image m_imgCase_Temp;

	int m_nLarg = 10;
	int m_nHaut = 10;

	int m_nPosX_jrTest = 0;
	int m_nPosY_jrTest = 0;


	public Text m_txtLarg;
	public Text m_txtHaut;

	List<GameObject> m_tabCase_Obj;


	WWW wwwLevelURL;

	// Use this for initialization
	void Start () {

		m_bTest_Level_Ok = false;

		m_nImage = 0;
		m_sNumero_Level = "";

		for (int i = 0; i < tabCase_Sel.Length; i++) {
			
			tabCase_Sel[i].enabled = false;
			
		}

		tabCase_Sel[m_nImage].enabled = true;

		m_tabCase_Obj = new List <GameObject>();

		Init_Larg_Haut (10, 10);

		Dessiner_Grille ();


	}
	
	// Update is called once per frame
	void Update () {
	
		if (m_bLance_Test) {

			if(Input.GetKeyDown(KeyCode.UpArrow)){

				if(m_nPosY_jrTest - 1 >= 0){

					if(tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] == 1 || tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] == 3 || tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] == 4){


						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = m_nSave_Case_Test;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest];
						tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] = 5;

						m_nPosY_jrTest --;

						Dessiner_Test();

					}else if(tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] == 2){

						if(m_nPosY_jrTest - 2 >= 0){

							if(tabCase_Test_Level[m_nPosY_jrTest - 2,m_nPosX_jrTest] == 1 || tabCase_Test_Level[m_nPosY_jrTest - 2,m_nPosX_jrTest] == 3){

								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest - 2,m_nPosX_jrTest] = 2;
								tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] = 5;

								m_nPosY_jrTest --;
								
								Dessiner_Test();

							}

						}

					}


				}

			}

			if(Input.GetKeyDown(KeyCode.DownArrow)){
				
				if(m_nPosY_jrTest + 1 <= m_nHaut){
					
					if(tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] == 1 || tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] == 3 || tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] == 4){
						
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = m_nSave_Case_Test;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest];
						tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] = 5;
						
						m_nPosY_jrTest ++;
						
						Dessiner_Test();
						
					}else if(tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] == 2){
						
						if(m_nPosY_jrTest + 2 <= m_nHaut){
							
							if(tabCase_Test_Level[m_nPosY_jrTest + 2,m_nPosX_jrTest] == 1 || tabCase_Test_Level[m_nPosY_jrTest + 2,m_nPosX_jrTest] == 3){
								
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest + 2,m_nPosX_jrTest] = 2;
								tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] = 5;
								
								m_nPosY_jrTest ++;
								
								Dessiner_Test();
								
							}
							
						}
						
					}
					
				}
				
			}

			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				
				if(m_nPosX_jrTest - 1 >= 0){
					
					if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] == 1 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] == 3 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] == 4){
						
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1];
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] = 5;
						
						m_nPosX_jrTest --;
						
						Dessiner_Test();
						
					}else if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] == 2){
						
						if(m_nPosX_jrTest - 2 >= 0){
							
							if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 2] == 1 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 2] == 3){
								
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 2] = 2;
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] = 5;
								
								m_nPosX_jrTest --;
								
								Dessiner_Test();
								
							}
							
						}
						
					}
					
				}
				
			}

			if(Input.GetKeyDown(KeyCode.RightArrow)){
				
				if(m_nPosX_jrTest + 1 <= m_nLarg){
					
					if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] == 1 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] == 3 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] == 4){
						
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1];
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] = 5;
						
						m_nPosX_jrTest ++;
						
						Dessiner_Test();
						
					}else if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] == 2){
						
						if(m_nPosX_jrTest + 2 <= m_nLarg){
							
							if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 2] == 1 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 2] == 3){
								
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 2] = 2;
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] = 5;
								
								m_nPosX_jrTest ++;
								
								Dessiner_Test();
								
							}
							
						}
						
					}
					
				}
				
			}

		}

	}

	void Dessiner_Test(){
	
		GameObject goCase;
		GameObject goCaseExit;
		goCaseExit = null;

		Image m_imgCase;

		int x = 1;
		int y = 0;

		bool m_bGagne;
		m_bGagne = true;

		for (int i = 0; i < (m_nLarg * m_nHaut) - 1; i++) {
						
			goCase = m_tabCase_Obj[i];
			
			m_imgCase = goCase.GetComponent<Image>();

			if(tabCase_Level[y,x] == 3 && tabCase_Test_Level[y,x] == 1)
				m_imgCase.sprite = tabCase[tabCase_Level[y,x]];
			else
				m_imgCase.sprite = tabCase[tabCase_Test_Level[y,x]];

			if(tabCase_Test_Level[y,x] == 4)
				goCaseExit = goCase;

			if(m_imgCase.sprite == tabCase[3] || tabCase_Test_Level[y,x] == 5 && tabCase_Level[y,x] == 3)
				m_bGagne = false;

			
			x++;
			
			if(x > (m_nLarg - 1)){
				
				x = 0;
				y++;
				
			}
			
		}

		if (goCaseExit != null) {

			m_imgCase = goCaseExit.GetComponent<Image> ();

			if (m_bGagne == false || !m_bTest_Level_Ok){
				m_bTest_Level_Ok = false;
				m_imgCase.sprite = tabCase [6];
			}
			else if(m_bTest_Level_Ok)
				m_imgCase.sprite = tabCase [4];
		
		} else if(m_bGagne)
			m_bTest_Level_Ok = true;


		
	}

	void Init_Larg_Haut(int Larg,int Haut){

		m_nLarg = Larg;
		m_nHaut = Haut;

	}

	public void Plus_Largeur(){

		if (m_nLarg < 20) {

			m_nLarg++;

			Dessiner_Grille();

		}

	}

	public void Moins_Largeur(){
		
		if (m_nLarg > 6) {
			
			m_nLarg--;
			
			Dessiner_Grille();
			
		}
		
	}

	public void Plus_Hauteur(){
		
		if (m_nHaut < 20) {
			
			m_nHaut++;
			
			Dessiner_Grille();
			
		}
		
	}
	
	public void Moins_Hauteur(){
		
		if (m_nHaut > 6) {
			
			m_nHaut--;
			
			Dessiner_Grille();
			
		}
		
	}

	void Dessiner_Grille(){

		GameObject goCase;
		Image m_imgCase;
		
		int x = 1;
		int y = 0;

		m_txtLarg.text = "" + m_nLarg;
		m_txtHaut.text = "" + m_nHaut;


		foreach(GameObject goCaseDel in m_tabCase_Obj){

			Destroy(goCaseDel);

		}

		m_tabCase_Obj.Clear ();

		RectTransform rtRectGrille;
	
		rtRectGrille = m_goGrille.GetComponent<RectTransform> ();

		rtRectGrille.sizeDelta = new Vector2(m_nLarg * 20, m_nHaut * 20);

		tabCase_Level = new int[m_nHaut,m_nLarg];
		tabCase_Test_Level = new int[m_nHaut,m_nLarg];

		tabCase_Level[0,0] = 0;
		tabCase_Test_Level[0,0] = 0;



		for (int i = 1; i < m_nLarg * m_nHaut; i++) {
			
			goCase = Instantiate(m_goCase,Vector3.zero,Quaternion.identity) as GameObject;

			m_tabCase_Obj.Add (goCase);

			m_imgCase = goCase.GetComponent<Image>();
			
			if(x == 0 || x == (m_nLarg - 1) || y == 0 || y == (m_nHaut - 1)){
				tabCase_Level[y,x] = 0;
				tabCase_Test_Level[y,x] = 0;
				m_imgCase.sprite = tabCase[0];
			}
			else{
				tabCase_Level[y,x] = 1;
				tabCase_Test_Level[y,x] = 1;
				m_imgCase.sprite = tabCase[1];
			}
			
			goCase.transform.SetParent(m_goGrille.transform);
			
			goCase.name = x + "_" + y;
			
			x++;
			
			if(x > (m_nLarg - 1)){

				x = 0;
				y++;
				
			}
			
		}

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

	public void Click_ImageXY(GameObject goXY){

		Debug.Log ("name: " + goXY.name);

		int x = 0;
		int y = 0;

		string [] split = goXY.name.Split ('_');

		x = int.Parse (split [0]);
		y = int.Parse (split [1]);

		if (m_nImage == 4) {

			if (x != y && (x == 0 || y == 0 || x == (m_nLarg - 1) || y == (m_nHaut - 1)) && !(x == (m_nLarg - 1) && y == 0) && !(x == (m_nLarg - 1) && y == (m_nHaut - 1))) {

				tabCase_Level [y, x] = m_nImage;
				tabCase_Test_Level [y, x] = m_nImage;
			
				if (x == 0 || x == (m_nLarg - 1)) {

					Vector3 vAngle = m_imgCase_Temp.transform.rotation.eulerAngles;

					vAngle.z = 90.0f;

					m_imgCase_Temp.transform.rotation = Quaternion.Euler (vAngle);

					//m_imgCase_Temp.transform.Rotate(new Vector3(0,0,90));
				
				}
						

				m_imgCase_Temp.sprite = tabCase [m_nImage];
			

			}

		} else if (m_nImage > 0) {

			if(x != 0 && y != 0 && x != (m_nLarg - 1) && y != (m_nHaut - 1)){

				tabCase_Level [y, x] = m_nImage;
				tabCase_Test_Level [y, x] = m_nImage;
				m_imgCase_Temp.sprite = tabCase[m_nImage];
			
			}

		}else {

			tabCase_Level [y, x] = m_nImage;
			tabCase_Test_Level [y, x] = m_nImage;
			m_imgCase_Temp.sprite = tabCase[m_nImage];


		}


	}

	public void Save_Numero_Level(string sLevel){
		
		m_sNumero_Level = sLevel;
		
	}

	public void Reset_Level(){

		Dessiner_Grille ();

	}

	public void Lance_Test(){

		if (m_sNumero_Level != "") {
			
			int i = 0;
			int j = 0;
			
			int nNb_Sortie = 0;
			int nNb_PosPlay = 0;
			int nNbCaisse = 0;
			int nNbEmp_Caisse = 0;
			
			
			while (i < m_nHaut) {
				
				j = 0;
				
				while (j < m_nLarg) {
					
					if (tabCase_Level [i, j] == 2)
						nNbCaisse++;
					
					if (tabCase_Level [i, j] == 3)
						nNbEmp_Caisse++;
					
					if (tabCase_Level [i, j] == 4)
						nNb_Sortie++;
					
					if (tabCase_Level [i, j] == 5){

						m_nPosX_jrTest = j;
						m_nPosY_jrTest = i;
						nNb_PosPlay++;

					}
					
					j++;
				}
				
				i++;
				
			}
			
			if (nNb_PosPlay == 1 && nNb_Sortie == 1 && nNbEmp_Caisse != 0 && nNbEmp_Caisse == nNbCaisse) {

				m_bLance_Test = true;
				m_bTest_Level_Ok = false;

				Dessiner_Test();

				m_imgSim.SetActive(true);

			}else{

				m_bLance_Test = false;
				m_bTest_Level_Ok = false;
				
				m_imgSim.SetActive(false);


				Debug.Log ("ko " + nNb_PosPlay + "  " + nNb_Sortie + "  " + nNbEmp_Caisse + "  " + nNbCaisse);
			}

		}

	}

	public void Save_Level(){

		if (m_sNumero_Level != "") {
	
			/*int i = 0;
			int j = 0;

			int nNb_Sortie = 0;
			int nNb_PosPlay = 0;
			int nNbCaisse = 0;
			int nNbEmp_Caisse = 0;

			
			while(i < m_nHaut){
				
				j = 0;
				
				while(j < m_nLarg){
					
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

			if(nNb_PosPlay == 1 && nNb_Sortie == 1 && nNbEmp_Caisse != 0 && nNbEmp_Caisse == nNbCaisse){*/
			if(m_bTest_Level_Ok){
				StartCoroutine (Save_LevelSQL());

			}else{
				Debug.Log ("Erreur");
				//Debug.Log ("ko " + nNb_PosPlay + "  " + nNb_Sortie + "  " + nNbEmp_Caisse + "  " + nNbCaisse);
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

		while(i < m_nHaut){

			j = 0;
			
			while(j < m_nLarg){
				
				sLigne += tabCase_Level[i,j];
				
				j++;
			}

			if(i < (m_nHaut - 1))
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
