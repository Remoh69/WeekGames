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

	public GameObject m_goErreur_Editeur;
	public Text m_txtErreur_Editeur;

	public GameObject m_goCanvasCaisse; 

	bool m_bTest_Level_Ok;
	bool m_bLance_Test;


	public Sprite[] tabCaisse;
	public Sprite[] tabArrCaisse;
	public GameObject m_goCase_Menu;
	public GameObject m_goArrCase_Menu;
	public Sprite[] tabImgBord_VertRouge;
	public Image[] tabImgBord;

	int m_nNumCaisse;

	public GameObject m_imgSim;

	int[,] tabCase_Level = {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
	int[,] tabCase_Test_Level;

	string m_sNumero_Level;
	string sLevelURL = "http://g.grousson38540.free.fr/Jeu_3D/Sokoban/PHP/Level.php";

	int m_nImage;
	int m_nImage_Caisse_ou_Arr;
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
		m_nImage_Caisse_ou_Arr = 0;
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

					if(tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest]) == 3 || tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] == 4){


						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = m_nSave_Case_Test;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest];
						tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest] = 5;

						m_nPosY_jrTest --;

						Dessiner_Test();

					}else if(Verif_Caisse(tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest]) == 2){

						if(m_nPosY_jrTest - 2 >= 0){

							if(tabCase_Test_Level[m_nPosY_jrTest - 2,m_nPosX_jrTest] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest - 2,m_nPosX_jrTest]) == 3){

								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest - 2,m_nPosX_jrTest] = tabCase_Test_Level[m_nPosY_jrTest - 1,m_nPosX_jrTest];
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
					
					if(tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest]) == 3 || tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] == 4){
						
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = m_nSave_Case_Test;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest];
						tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest] = 5;
						
						m_nPosY_jrTest ++;
						
						Dessiner_Test();
						
					}else if(Verif_Caisse(tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest]) == 2){
						
						if(m_nPosY_jrTest + 2 <= m_nHaut){
							
							if(tabCase_Test_Level[m_nPosY_jrTest + 2,m_nPosX_jrTest] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest + 2,m_nPosX_jrTest]) == 3){
								
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest + 2,m_nPosX_jrTest] = tabCase_Test_Level[m_nPosY_jrTest + 1,m_nPosX_jrTest];
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
					
					if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1]) == 3 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] == 4){
						
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1];
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1] = 5;
						
						m_nPosX_jrTest --;
						
						Dessiner_Test();
						
					}else if(Verif_Caisse(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1]) == 2){
						
						if(m_nPosX_jrTest - 2 >= 0){
							
							if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 2] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 2]) == 3){
								
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 2] = tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest - 1];
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
					
					if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1]) == 3 || tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] == 4){
						
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
						m_nSave_Case_Test = tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1];
						tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1] = 5;
						
						m_nPosX_jrTest ++;
						
						Dessiner_Test();
						
					}else if(Verif_Caisse(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1]) == 2){
						
						if(m_nPosX_jrTest + 2 <= m_nLarg){
							
							if(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 2] == 1 || Verif_Arrivee(tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 2]) == 3){
								
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest] = 1;
								tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 2] = tabCase_Test_Level[m_nPosY_jrTest,m_nPosX_jrTest + 1];
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

	int Verif_Arrivee(int nCase){
		
		if (nCase == 13 || nCase == 15 || nCase == 17 || nCase == 19 ||  nCase == 21)
			return 3;
		else
			return -1;
		
		
	}
	
	int Verif_Caisse(int nCase){
		
		if (nCase == 12 || nCase == 14 || nCase == 16 || nCase == 18 || nCase == 20)
			return 2;
		else
			return -1;
		
	}

	bool Verif_Case_Arrive_Reste(Sprite spCase){

		bool bTrouve = false;

		for (int i = 0; i < tabArrCaisse.Length; i++) {

			if(spCase == tabArrCaisse[i])
				bTrouve = true;

		}

		return bTrouve;

	}

	bool Verif_Case_Arr_Ok(){

		int i = 0;
		int j = 0;

		bool bGagne = true;

		while (i < m_nHaut) {
			
			j = 0;
			
			while (j < m_nLarg && bGagne == true) {
				
				if (tabCase_Test_Level [i, j] >= 12){

					if((tabCase_Level [i, j] - 1) !=  tabCase_Test_Level [i, j])
						bGagne = false;

				}
				
				j++;
			}
			
			i++;
			
		}

		return bGagne;

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

			if(Verif_Arrivee(tabCase_Level[y,x]) == 3 && tabCase_Test_Level[y,x] == 1)
				m_imgCase.sprite = tabCase[tabCase_Level[y,x]];
			else
				m_imgCase.sprite = tabCase[tabCase_Test_Level[y,x]];

			if(tabCase_Test_Level[y,x] == 4)
				goCaseExit = goCase;

			if(Verif_Case_Arrive_Reste(m_imgCase.sprite) || (tabCase_Test_Level[y,x] == 5 && Verif_Arrivee(tabCase_Level[y,x]) == 3))
				m_bGagne = false;


			
			x++;
			
			if(x > (m_nLarg - 1)){
				
				x = 0;
				y++;
				
			}
			
		}

		if (m_bGagne)
			m_bGagne = Verif_Case_Arr_Ok ();

		if (goCaseExit != null) {

			m_imgCase = goCaseExit.GetComponent<Image> ();

			if (m_bGagne == false){
				m_imgCase.sprite = tabCase [6];
			}
			else if (!m_bTest_Level_Ok) {
				m_bTest_Level_Ok = false;
				m_imgCase.sprite = tabCase [7];
			} else if (m_bTest_Level_Ok)
				m_imgCase.sprite = tabCase [8];
		
		} else if (m_bGagne) {

			m_bTest_Level_Ok = true;

		}


		
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

		if(nImg == 2){
			m_goCanvasCaisse.SetActive(true);
		}

		for (int i = 0; i < tabCase_Sel.Length; i++) {
			
			tabCase_Sel[i].enabled = false;
			
		}

		tabCase_Sel[nImg].enabled = true;

		if (nImg == 2 || nImg == 3) {
			m_nImage_Caisse_ou_Arr = nImg + 10;
			nImg = nImg + 10 + m_nNumCaisse * 2;
		}


		m_nImage = nImg;





	}

	public void Set_Caisse(int nCaisse){

		m_nNumCaisse = nCaisse;

		m_nImage = m_nImage_Caisse_ou_Arr + m_nNumCaisse * 2;
		
		Image m_imgCase;

		m_imgCase = m_goCase_Menu.GetComponent<Image>();
		m_imgCase.sprite = tabCaisse[nCaisse];

		m_imgCase = m_goArrCase_Menu.GetComponent<Image>();
		m_imgCase.sprite = tabArrCaisse[nCaisse];

		m_goCanvasCaisse.SetActive (false);

	}

	public void Click_Image(Image imgMyImg){

		m_imgCase_Temp = imgMyImg;

	}

	public void Click_ImageXY(GameObject goXY){
	
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
			bool bCaisseOk = true;
			bool bCaisse_Diff_Zero = false;
			
			int [] tabCaisse;

			tabCaisse = new int[10];

			while (i < m_nHaut) {
				
				j = 0;
				
				while (j < m_nLarg) {

					if (tabCase_Level [i, j] >= 12){
						tabCaisse[(tabCase_Level [i, j] - 12)] ++;
					}
					
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

			bCaisseOk = true;
			bCaisse_Diff_Zero = false;


			for (i = 1;i < 10;i += 2){

				if(tabCaisse[i] != tabCaisse[(i - 1)])
					bCaisseOk = false;
				else if(tabCaisse[i] != 0)
					bCaisse_Diff_Zero = true;

			}

			if (nNb_PosPlay == 1 && nNb_Sortie == 1 && bCaisseOk && bCaisse_Diff_Zero) {

				m_bLance_Test = true;
				m_bTest_Level_Ok = false;

				Dessiner_Test();

				m_imgSim.SetActive(true);

				tabImgBord[0].sprite = tabImgBord_VertRouge[1];
				tabImgBord[1].sprite = tabImgBord_VertRouge[1];
				tabImgBord[2].sprite = tabImgBord_VertRouge[1];
				tabImgBord[3].sprite = tabImgBord_VertRouge[1];

			}else{

				m_bLance_Test = false;
				m_bTest_Level_Ok = false;
				
				m_imgSim.SetActive(false);

				m_goErreur_Editeur.SetActive(true);

				if(nNb_PosPlay != 1)
					m_txtErreur_Editeur.text = "Il n'y a pas (ou trop) de position de départ du joueurs!";
				else if(bCaisseOk == false)
					m_txtErreur_Editeur.text = "Il n'y a un problème au niveau des caisses et des sorties!";
				else if(bCaisse_Diff_Zero == false)
					m_txtErreur_Editeur.text = "Il n'y a pas de caisses !";
				else if(nNb_Sortie != 1)
					m_txtErreur_Editeur.text = "Il n'y a pas (ou trop) de position de sortie!";
			
			}

		}

	}

	public void Cancel(){

		m_goErreur_Editeur.SetActive(false);

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

				m_goErreur_Editeur.SetActive(true);

				m_txtErreur_Editeur.text = "Vous devez tester votre level en rammenant toutes les caisses sur les cibles et ensuite atteindre la case de l'arrivée !";

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
		string sCase = "0123456789__ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		while(i < m_nHaut){

			j = 0;
			
			while(j < m_nLarg){

				if(tabCase_Level[i,j] == 0){

					int nCase = 9;

					if((i - 1) >= 0){

						if(tabCase_Level[(i - 1),j] != 0 && tabCase_Level[(i - 1),j] != 9)
							nCase = 0;

					}

					if((j - 1) >= 0){
						
						if(tabCase_Level[i,(j - 1)] != 0 && tabCase_Level[i,(j - 1)] != 9)
							nCase = 0;
						
					}

					if((i + 1) < m_nHaut){
						
						if(tabCase_Level[(i + 1),j] != 0 && tabCase_Level[(i + 1),j] != 9)
							nCase = 0;
						
					}

					if((j + 1) < m_nLarg){
						
						if(tabCase_Level[i,(j + 1)] != 0 && tabCase_Level[i,(j + 1)] != 9)
							nCase = 0;
						
					}

					tabCase_Level[i,j] = nCase;

				}



				sLigne += sCase[tabCase_Level[i,j]];
				
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
