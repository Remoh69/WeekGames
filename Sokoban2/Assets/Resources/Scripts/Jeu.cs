using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

struct STCase{
	public int x;
	public int y;
}

public class Jeu : MonoBehaviour {

	public Canvas m_cnEditor;
	public Canvas m_cnMenu;

	public Material m_matCaisse;
	public Material m_matArrivee;
	public Material m_matArriveeOk;

	public GameObject m_cnMenu_Princ;
	public GameObject m_cnLogin_Compte;
	public GameObject m_cnCreer_Compte;
	public GameObject m_cnErreur_Login;
	public GameObject m_cnErreur_Compte;
	public GameObject m_cnOk_Compte;

	public GameObject m_goNouveau;
	public GameObject m_goRetour;
	public GameObject m_goJeu_3D;
	
	bool m_bAffiche_Menu;
	bool m_bLance_Partie;
	bool m_bLance_Editeur;

	string m_sLevelURL = "http://g.grousson38540.free.fr/Jeu_3D/Sokoban/PHP/Level.php";
	public InputField m_txtLogin;
	public InputField m_txtPass;

	public InputField m_txtSurnom_Creer;
	public InputField m_txtLogin_Creer;
	public InputField m_txtPass_Creer;

	int m_nLevel;
	public Text m_sPseudo;

	bool bOuvre_Porte;
	bool bSave_EtatPorte;

	GameObject m_goPorte;


	string [] tabLoad = {"Prefab/Mur","Prefab/Sol","Prefab/Caisse","Prefab/Sol","Prefab/Mur_Porte","Prefab/Sol"};

	int[,] tabCase_Level = {{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}};

	List<STCase> m_tabSTCase;
	List<GameObject> m_tabGameObject;

	WWW wwwLevelURL;

	// Use this for initialization
	void Start () {
		m_bAffiche_Menu = false;
		m_cnEditor.enabled = false;

		m_goJeu_3D.SetActive(false);

		m_bLance_Partie = false;
		m_bLance_Editeur = false;

		bOuvre_Porte = false;
		bSave_EtatPorte = false;

		m_nLevel = 1;
		m_sPseudo.text = "New Player";

		m_goRetour.SetActive(false);
		m_goNouveau.SetActive(true);

		m_tabSTCase = new List <STCase>();
		m_tabGameObject = new List <GameObject>();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape) && (m_bLance_Partie || m_bLance_Editeur)) {

			m_bAffiche_Menu = !m_bAffiche_Menu;

			if(!m_bAffiche_Menu && m_bLance_Editeur)
				m_cnEditor.enabled = true;
			else
				m_cnEditor.enabled = false;

			if(!m_bAffiche_Menu && m_bLance_Partie)
				m_goJeu_3D.SetActive(true);
			else if(m_bAffiche_Menu && m_bLance_Partie){
				m_goRetour.SetActive(true);
				m_goNouveau.SetActive(false);
				m_goJeu_3D.SetActive(false);
			}else{

				m_goJeu_3D.SetActive(false);

			}

			m_cnMenu.enabled = m_bAffiche_Menu;

		}

	}

	void Charge_Level(){

		StartCoroutine (Charge_LevelSQL());

	}

	IEnumerator Charge_LevelSQL(){
		
		GameObject m_goCase_Sol;
		GameObject m_goCase_Player;
		Renderer rndSol;

		m_tabSTCase.Clear ();
		
		m_goCase_Player = Instantiate(Resources.Load ("Prefab/Chibis"),new Vector3(0,0,0),Quaternion.identity) as GameObject;
		m_tabGameObject.Add (m_goCase_Player);

		m_goCase_Player.SetActive (false);
		
		m_goCase_Player.transform.parent = m_goJeu_3D.transform;

		
		string sLigne;
		
		int i = 0;
		int j = 0;


		wwwLevelURL = new WWW (m_sLevelURL + "?Requete=Get_Level&Level=" + m_nLevel);
		yield return wwwLevelURL;
		
		if (wwwLevelURL.error != null) {
			Debug.Log ("Erreur " + wwwLevelURL.error);
		}
		else{

			string[] m_tabsSQLLevel;
			m_tabsSQLLevel = wwwLevelURL.text.ToString().Split("*"[0]);

			while(i <= m_tabsSQLLevel.Length){

				sLigne = m_tabsSQLLevel[i];

				j = 0;
				
				foreach (char cCase in sLigne) {
					
					tabCase_Level[j,i] = int.Parse (cCase + "");

					//if(cCase != '0'){
					
						m_goCase_Sol = Instantiate(Resources.Load (tabLoad[int.Parse (cCase + "")]),new Vector3(j * 2,0,(9 - i) * 2),Quaternion.identity) as GameObject;
						m_tabGameObject.Add (m_goCase_Sol);

						m_goCase_Sol.transform.parent = m_goJeu_3D.transform;
						
						if(cCase == '5'){
							
							m_goCase_Player.transform.Translate(new Vector3(j * 2,0,(9 - i)  * 2));

							
						}else if(cCase == '2'){
							
							Touche_Caisse scpTouche_Caisse;
							scpTouche_Caisse = m_goCase_Sol.GetComponent<Touche_Caisse> ();
							
							scpTouche_Caisse.m_goPlayer = m_goCase_Player;
							scpTouche_Caisse.Init_Aff_Player();
							scpTouche_Caisse.Set_Case(j, i);
							
							m_goCase_Sol = Instantiate(Resources.Load (tabLoad[1]),new Vector3(j * 2,0,(9 - i) * 2),Quaternion.identity) as GameObject;
							m_tabGameObject.Add (m_goCase_Sol);

							m_goCase_Sol.transform.parent = m_goJeu_3D.transform;

							/*m_goCase_Sol = Instantiate(Resources.Load (tabLoad[1]),new Vector3(j * 2,0.02f,(9 - i) * 2),Quaternion.identity) as GameObject;
							m_tabGameObject.Add (m_goCase_Sol);

							rndSol = m_goCase_Sol.GetComponent<Renderer> ();

							rndSol.material = m_matCaisse;

							m_goCase_Sol.transform.parent = m_goJeu_3D.transform;*/
							
						}else if(cCase == '3'){
							
							m_goCase_Sol = Instantiate(Resources.Load (tabLoad[1]),new Vector3(j * 2,0.02f,(9 - i) * 2),Quaternion.identity) as GameObject;
							m_tabGameObject.Add (m_goCase_Sol);

							rndSol = m_goCase_Sol.GetComponent<Renderer> ();

							rndSol.material = m_matArrivee;

							m_goCase_Sol.transform.parent = m_goJeu_3D.transform;
							
							STCase stCase;

							stCase.x = j;
							stCase.y = i;

							m_tabSTCase.Add (stCase);

							
							
						}else if(cCase == '0'){
							
							Touche_Mur scpTouche_Mur;
							scpTouche_Mur = m_goCase_Sol.GetComponent<Touche_Mur> ();
							
							scpTouche_Mur.m_goPlayer = m_goCase_Player;
							scpTouche_Mur.Init_Aff_Player();
							
						}else if(cCase == '4'){

							m_goPorte = m_goCase_Sol;

						}
					
					//}

					j++;
				}

				i++;
				
			}



			m_goCase_Player.SetActive (true);

		}

	}

	IEnumerator Creer_compte_LevelSQL(){

		wwwLevelURL = new WWW (m_sLevelURL + "?Requete=Creer_Compte&Login=" + m_txtSurnom_Creer.text + "&Mail=" + m_txtLogin_Creer.text + "&Pass=" + m_txtPass_Creer.text);
		yield return wwwLevelURL;
		
		if (wwwLevelURL.error != null) {
			Debug.Log ("Erreur " + wwwLevelURL.error);
		} else {
			
			string sResult;
			sResult = wwwLevelURL.text.ToString ();
			
			if (sResult != "Doublons") {
				
				/*m_bLance_Partie = true;
				m_bAffiche_Menu = false;
				m_cnMenu.enabled = false;
				m_goJeu_3D.SetActive (true);

				m_sPseudo.text = m_txtSurnom_Creer.text;
				m_nLevel = 1;

				Charge_Level ();*/

				m_cnOk_Compte.SetActive (true);
				
			} else {

				m_cnErreur_Compte.SetActive (true);
				
			}
			
		}
	}

	IEnumerator Connection_compte_LevelSQL(){
		
		wwwLevelURL = new WWW (m_sLevelURL + "?Requete=Connection&Mail=" + m_txtLogin.text + "&Pass=" + m_txtPass.text);
		yield return wwwLevelURL;
		
		if (wwwLevelURL.error != null) {
			Debug.Log ("Erreur " + wwwLevelURL.error);
		} else {

			string[] m_tabsSQLLevel;
			m_tabsSQLLevel = wwwLevelURL.text.ToString().Split("_"[0]);
			
			if (m_tabsSQLLevel[0] == "Ok") {

				m_bLance_Partie = true;
				m_bAffiche_Menu = false;
				m_cnMenu.enabled = false;
				m_goJeu_3D.SetActive (true);

				m_sPseudo.text = m_tabsSQLLevel[1];
				m_nLevel = int.Parse (m_tabsSQLLevel[2]);

				Raz_Level();

				Charge_Level ();
				
			} else {

				m_cnErreur_Login.SetActive (true);

			}
			
		}

	}


	public void Sel_Menu(int nMenu){

		m_bLance_Editeur = false;
		m_bLance_Partie = false;

		if (nMenu == 0) {

			m_cnMenu_Princ.SetActive (false);
			m_cnLogin_Compte.SetActive (true);

		} else if (nMenu == 1) {

			m_bLance_Partie = true;
			m_bAffiche_Menu = false;
			m_cnMenu.enabled = false;
			m_goJeu_3D.SetActive (true);

			m_sPseudo.text = "New Player";
			m_nLevel = 10;

			Raz_Level();

			Charge_Level ();

		} else if (nMenu == 2) {
			
			m_bLance_Editeur = true;
			m_bAffiche_Menu = false;
			m_cnMenu.enabled = false;
			m_cnMenu_Princ.SetActive (false);
			m_cnEditor.enabled = true;
			
		} else if (nMenu == 3) {

			m_cnMenu_Princ.SetActive (false);
			m_cnCreer_Compte.SetActive (true);


		} else if (nMenu == 4) {

			StartCoroutine (Creer_compte_LevelSQL ());
		
		} else if (nMenu == 5) {

			StartCoroutine (Connection_compte_LevelSQL ());

		} else if (nMenu == 6) {

			m_cnLogin_Compte.SetActive (false);
			m_cnCreer_Compte.SetActive (false);
			m_cnMenu_Princ.SetActive (true);

		} else if (nMenu == 7) {

			m_cnErreur_Login.SetActive (false);

		}
		else if (nMenu == 8) {
			
			m_cnErreur_Compte.SetActive (false);
			
		}
		else if (nMenu == 9) {
			
			m_cnOk_Compte.SetActive (false);
			m_cnLogin_Compte.SetActive (false);
			m_cnCreer_Compte.SetActive (false);
			m_cnMenu_Princ.SetActive (true);
			
		}else if (nMenu == 10) {

			m_cnEditor.enabled = false;
			m_bLance_Editeur = false;
			m_bAffiche_Menu = true;
			m_cnMenu.enabled = true;
			m_cnMenu_Princ.SetActive (true);
			
		}

	}

	public bool Controle_Case(int xOld,int yOld,int x, int y ){

		if (tabCase_Level [x, y] == 1 || tabCase_Level [x, y] == 3 || tabCase_Level [x, y] == 5) {

			tabCase_Level [x, y] = 2;
			tabCase_Level [xOld, yOld] = 1;

			Controle_Gagne();

			return true;

		}
		else
			return false;

	}

	void Controle_Gagne(){

		bOuvre_Porte = true;

		foreach(STCase stC in m_tabSTCase){

			if(tabCase_Level [stC.x, stC.y] != 2)
				bOuvre_Porte = false;

		}

		if (bOuvre_Porte != bSave_EtatPorte) {

			if(bOuvre_Porte){
				Collider colPorte;
				colPorte = m_goPorte.GetComponent<Collider>();
				colPorte.enabled = false;

			} else {

				Collider colPorte;
				colPorte = m_goPorte.GetComponent<Collider>();
				colPorte.enabled = true;
			}


			Animator anPorte;

			anPorte = m_goPorte.GetComponent<Animator>();

			anPorte.SetBool("bOuvre",bOuvre_Porte);

			bSave_EtatPorte = bOuvre_Porte;

		}


	}

	public IEnumerator Level_Up(){

		m_nLevel++;

		yield return new WaitForSeconds(3);
		
		Raz_Level();

		Charge_Level ();

	}

	void Raz_Level(){

		foreach(GameObject goObj in m_tabGameObject){
			
			Destroy(goObj);
			
		}
		
		m_tabGameObject.Clear ();

	}

}
