using UnityEngine;
using System.Collections;

public class Controle_Player : MonoBehaviour {

	public float m_rVitesse_Dep = 8;
	public float m_rVitesse_Rot = 250;
	public float m_rAngle;

	public int m_nDirection;

	bool bGagne;

	Animator anPlayer;
	Jeu m_scpJeu;

	int [] m_tabTouche = {0,0,0,0};

	// Use this for initialization
	void Start () {
		m_rAngle = 0;
		m_nDirection = 2;
		bGagne = false;

		anPlayer = GetComponent<Animator>();
		m_scpJeu = Camera.main.GetComponent<Jeu> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		//Touches Relachées

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			m_tabTouche[0] = 0;
		}
		
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			m_tabTouche[1] = 0;
		}
		
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			m_tabTouche[2] = 0;
		}
		
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			m_tabTouche[3] = 0;
		}


		//Touches Enfoncées

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			m_tabTouche[0] = Recup_Ind_Touche();
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			m_tabTouche[1] = Recup_Ind_Touche();
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			m_tabTouche[2] = Recup_Ind_Touche();
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			m_tabTouche[3] = Recup_Ind_Touche();
		}

		m_nDirection = Recup_Direction();

		if(m_nDirection == -1)
			anPlayer.SetBool("bMarche",false);
		else
			anPlayer.SetBool("bMarche",true);

		switch (m_nDirection) {

			case 1:

				transform.eulerAngles =  new Vector3(0,0,0);

				break;

			case 2:
				
				transform.eulerAngles = (new Vector3(0,180,0));

				break;

			case 3:
				
				transform.eulerAngles = (new Vector3(0,270,0));

				break;

			case 4:
				
				transform.eulerAngles = (new Vector3(0,90,0));

				break;

		}

		if (m_nDirection != -1) {

			transform.Translate (Vector3.forward * Time.deltaTime * m_rVitesse_Dep);

		}

		m_rAngle = transform.rotation.eulerAngles.y;

		//Debug.Log (m_nDirection + " | " + m_tabTouche[0] + " " + m_tabTouche[1] + " " + m_tabTouche[2] + " " + m_tabTouche[3]);

	}

	int Recup_Ind_Touche(){

		int nInd = 1;

		foreach (int nTouche in m_tabTouche) {

			if(nTouche != 0 && nInd <= nTouche)
				nInd = nTouche + 1;

		}

		return nInd;

	}

	int Recup_Direction(){


		int nInd = 0;
		int nDir = -1;
		int i = 1;
		
		foreach (int nTouche in m_tabTouche) {
			
			if(nTouche > nInd && nTouche != 0){

				nInd = nTouche;
				nDir = i;

			}

			i++;
			
		}

		return nDir;

	}

	public void Pousse(){

		anPlayer.SetBool("bPousse",true);

	}

	public void Fin_Pousse(){

		anPlayer.SetBool("bPousse",false);
		
	}

	void Gagne(bool bOnOff){
		
		anPlayer.SetBool("bGagne",bOnOff);
		
	}


	void OnTriggerEnter(Collider col){
		
		if (col.gameObject.tag == "Door") {
			
			Gagne(true);

			if(!bGagne){

				bGagne = true;
				StartCoroutine(m_scpJeu.Level_Up());

			}
			
		}
		
	}

	void OnTriggerExit(Collider col){
		
		if (col.gameObject.tag == "Door") {
			
			Gagne(false);
			
		}
		
	}
}
