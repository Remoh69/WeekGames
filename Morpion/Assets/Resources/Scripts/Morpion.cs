using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Morpion : MonoBehaviour {

	private bool m_bLance;
	private bool m_bGagne;
	private int m_nMode;
	private int m_nPlayJr;

	private GameObject m_goCroix;
	private GameObject m_goRond;

	private int m_nClic_X;
	private int m_nClic_Y;

	private int m_nPt1;
	private int m_nPt2;

	private int [,]Plateau = {{0,0,0},{0,0,0},{0,0,0}};
	private List<GameObject> m_goPiece1;
	private List<GameObject> m_goPiece2;

	private int m_nMode_Comp;

	private bool m_bOnoff_Sound;




	public Sprite m_txSound_On;
	public Sprite m_txSound_Off;

	public Canvas m_cnMenu;
	public Canvas m_cnScore;
	public Canvas m_cnGagne;
	public Text m_txSc1;
	public Text m_txSc2;
	public Image []m_txFin;
	public Text m_ImgP1;
	public Text m_ImgP2;

	public Text m_txHvsH;
	public Text m_txHvsC;

	public Canvas m_cnSet_Comp;
	public Text m_txSet_Comp;

	public AudioSource m_audSound;

	public Button m_btnMenuSound;
	public Button m_btnJeuSound;

	public Material m_matComp;

	public Texture []m_txComp;

	void Awake(){

		m_cnScore.enabled = false;
		m_cnGagne.enabled = false;

		m_txFin [0].enabled = false;
		m_txFin [1].enabled = false;
		m_txFin [2].enabled = false;
		m_txFin [3].enabled = false;
		
		m_ImgP2.enabled = false;

		m_txHvsH.enabled = false;
		m_txHvsC.enabled = false;
		m_cnSet_Comp.enabled = false;

		m_nMode_Comp = 0;
		m_bOnoff_Sound = true;

	}

	// Use this for initialization
	void Start () {
	

		m_ImgP2.enabled = false;

		m_bLance = false;
		m_bGagne = false;
		m_nMode = 1;
		m_nPlayJr = 1;

		m_nClic_X = -1;
		m_nClic_Y = -1;
		m_nPt1 = 0;
		m_nPt2 = 0;


		m_goPiece1 = new List <GameObject>();
		m_goPiece2 = new List <GameObject>();

		m_goCroix = Resources.Load ("Models 3D/Croix_Player") as GameObject;
		m_goRond = Resources.Load ("Models 3D/Rond_Player") as GameObject;

		//m_txSound_On = Resources.Load("Textures/SonOk") as Sprite;
		//m_txSound_Off = Resources.Load("Textures/SonNon") as Sprite;



	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (Plateau[0,0] + "," + Plateau[0,1] + "," + Plateau[0,2] + " | " + Plateau[1,0] + "," + Plateau[1,1] + "," + Plateau[1,2] + " | " + Plateau[2,0] + "," + Plateau[2,1] + "," + Plateau[2,2]);

		if(m_nMode_Comp == 0 && m_nMode == 2){
			m_txSet_Comp.text = "Computer level:\n[X]\n[  ]\n[  ]";
		}else if(m_nMode_Comp == 1 && m_nMode == 2){
			m_txSet_Comp.text = "Computer level:\n[  ]\n[X]\n[  ]";
		}else if(m_nMode_Comp == 2 && m_nMode == 2){
			m_txSet_Comp.text = "Computer level:\n[  ]\n[  ]\n[X]";
		}

		if (Input.GetKeyDown (KeyCode.Keypad1) && m_nMode == 2) {

			m_nMode_Comp = 0;

		}

		if (Input.GetKeyDown (KeyCode.Keypad2) && m_nMode == 2) {
			
			m_nMode_Comp = 1;
			
		}

		if (Input.GetKeyDown (KeyCode.Keypad3) && m_nMode == 2) {
			
			m_nMode_Comp = 2;
			
		}

		if (m_bLance) {

			if(m_nMode == 1){
				m_txHvsH.enabled = true;
				m_txHvsC.enabled = false;
			}else{
				m_txHvsH.enabled = false;
				m_txHvsC.enabled = true;
			}


			if (Input.GetMouseButtonDown (0) && !m_bGagne && (m_nMode == 1 || m_nPlayJr == 1)) {
				
				Vector3 m_vPos_RecupPosition;
				Plane m_pPlane_RecupPosition;
				Ray m_ryRay_Recuposition;
				float m_fHitdist_Recuposition;
				
				m_vPos_RecupPosition = transform.position;
				m_vPos_RecupPosition.y = 0;
				
				m_pPlane_RecupPosition = new Plane (Vector3.up, m_vPos_RecupPosition);
				m_ryRay_Recuposition = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				m_fHitdist_Recuposition = 0;
				
				if (m_pPlane_RecupPosition.Raycast (m_ryRay_Recuposition, out m_fHitdist_Recuposition)) {
					
					Vector3 targetPoint = m_ryRay_Recuposition.GetPoint (m_fHitdist_Recuposition);

					m_nClic_X = (int)targetPoint.x;
					m_nClic_Y = (int)targetPoint.z;

					if(targetPoint.x < 0 && m_nClic_X == 0)
						m_nClic_X = -1;

					if(targetPoint.z < 0 && m_nClic_Y == 0)
						m_nClic_Y = -1;

				}
				
			}

			if(m_nMode == 1 && m_bGagne == false){

				if(m_nPlayJr == 1){

					m_ImgP1.enabled = true;
					m_ImgP2.enabled = false;

					if((m_nClic_X >= 0 && m_nClic_X < 3) && (m_nClic_Y >= 0 && m_nClic_Y < 3)){

						if(Plateau[m_nClic_X,m_nClic_Y] == 0){

							Plateau[m_nClic_X,m_nClic_Y] = 1;

							GameObject goPiece;

							goPiece = Instantiate(m_goRond,new Vector3(((float)m_nClic_X) + 0.5f,0,((float)m_nClic_Y) + 0.5f),Quaternion.identity) as GameObject;

							m_goPiece1.Add (goPiece);

							m_nClic_X = -1;
							m_nClic_Y = -1;

							if( PasGagne(m_nPlayJr) == false){
								m_nPlayJr = 2;
							}

						}

					}


				}else if(m_nPlayJr == 2){

					m_ImgP1.enabled = false;
					m_ImgP2.enabled = true;

					if((m_nClic_X >= 0 && m_nClic_X < 3) && (m_nClic_Y >= 0 && m_nClic_Y < 3)){

						if(Plateau[m_nClic_X,m_nClic_Y] == 0){
							
							Plateau[m_nClic_X,m_nClic_Y] = 2;

							GameObject goPiece;

							goPiece = Instantiate(m_goCroix,new Vector3(((float)m_nClic_X) + 0.5f,0,((float)m_nClic_Y) + 0.5f),Quaternion.identity) as GameObject;

							m_goPiece2.Add (goPiece);

							m_nClic_X = -1;
							m_nClic_Y = -1;

							if( PasGagne(m_nPlayJr) == false){

								m_nPlayJr = 1;

							}

						}
						
					}

				}

			}else if(m_nMode == 2 && m_bGagne == false){

				if(m_nPlayJr == 1){

					m_ImgP1.enabled = true;
					m_ImgP2.enabled = false;
					
					if((m_nClic_X >= 0 && m_nClic_X < 3) && (m_nClic_Y >= 0 && m_nClic_Y < 3)){
						
						if(Plateau[m_nClic_X,m_nClic_Y] == 0){
							
							Plateau[m_nClic_X,m_nClic_Y] = 1;
							
							GameObject goPiece;
							
							goPiece = Instantiate(m_goRond,new Vector3(((float)m_nClic_X) + 0.5f,0,((float)m_nClic_Y) + 0.5f),Quaternion.identity) as GameObject;
							
							m_goPiece1.Add (goPiece);
							
							m_nClic_X = -1;
							m_nClic_Y = -1;
							
							if( PasGagne(m_nPlayJr) == false){
								m_nPlayJr = 2;
							}
							
						}
						
					}

				}else if(m_nPlayJr == 2){

					if(m_nMode_Comp == 0){
						m_nClic_X = Random.Range(0,3);
						m_nClic_Y = Random.Range(0,3);
					}else if(m_nMode_Comp == 1){

						if(Recherche_PasPerdre() == false){
							m_nClic_X = Random.Range(0,3);
							m_nClic_Y = Random.Range(0,3);
						}



					}else if(m_nMode_Comp == 2){
						
						if(Recherche_Gagne() == false){

							if(Recherche_PasPerdre() == false){
								m_nClic_X = Random.Range(0,3);
								m_nClic_Y = Random.Range(0,3);
							}
						}
						
					}

					m_ImgP1.enabled = false;
					m_ImgP2.enabled = true;
					
					if((m_nClic_X >= 0 && m_nClic_X < 3) && (m_nClic_Y >= 0 && m_nClic_Y < 3)){

						if(Plateau[m_nClic_X,m_nClic_Y] == 0){

							Plateau[m_nClic_X,m_nClic_Y] = 2;

							StartCoroutine(Temporisation(0.5f,m_nClic_X,m_nClic_Y));

							m_nClic_X = -1;
							m_nClic_Y = -1;

							m_nPlayJr = 0;

						}
						
					}
				}

			}

		}

	}

	public void New_Round(){
		
		m_cnGagne.enabled = false;
		
		m_txFin [0].enabled = false;
		m_txFin [1].enabled = false;
		m_txFin [2].enabled = false;
		m_txFin [3].enabled = false;
		
		int i;
		
		i = 0;
		
		while(i < m_goPiece1.Count){
			
			Destroy(m_goPiece1[i]);
			
			i++;
			
		}
		
		m_goPiece1.Clear();
		
		i = 0;
		
		while(i < m_goPiece2.Count){
			
			Destroy(m_goPiece2[i]);
			
			i++;
			
		}
		
		m_goPiece2.Clear();
		
		m_bGagne = false;
		
	}
	public void Reset_Game() {
		
		m_cnScore.enabled = false;
		m_bLance = false;
		
		m_cnGagne.enabled = false;
		m_cnSet_Comp.enabled = false;
		
		m_txFin [0].enabled = false;
		m_txFin [1].enabled = false;
		m_txFin [2].enabled = false;
		m_txFin [3].enabled = false;
		
		int i;
		
		i = 0;
		
		while(i < m_goPiece1.Count){
			
			Destroy(m_goPiece1[i]);
			
			i++;
			
		}
		
		m_goPiece1.Clear();
		
		i = 0;
		
		while(i < m_goPiece2.Count){
			
			Destroy(m_goPiece2[i]);
			
			i++;
			
		}
		
		m_goPiece2.Clear();
		
		i = 0;
		
		while (i <= 2) {
			
			Plateau [0, i] = 0;
			Plateau [1, i] = 0;
			Plateau [2, i] = 0;
			
			i++;
		}
		
		m_nPlayJr = 1;
		
		m_bGagne = false;
		
		m_cnMenu.enabled = true;
		
		
	}

	public void Lance_Partie(int nMode){



		if (nMode == 1) {

			m_matComp.mainTexture = m_txComp[1];
			m_nMode_Comp = 0;

			m_nMode = 1;
			m_cnSet_Comp.enabled = false;
			m_cnMenu.enabled = false;
			m_cnScore.enabled = true;
			m_bLance = true;
			m_nPt1 = 0;
			m_nPt2 = 0;

			m_txSc1.text = "" + m_nPt1;
			m_txSc2.text = "" + m_nPt2;
			
		}else if (nMode == 2) {

			m_matComp.mainTexture = m_txComp[0];
			m_nMode_Comp = 0;

			m_nMode = 2;
			m_cnMenu.enabled = false;
			m_cnScore.enabled = true;
			m_bLance = true;
			m_cnSet_Comp.enabled = true;
			m_nPt1 = 0;
			m_nPt2 = 0;

			m_txSc1.text = "" + m_nPt1;
			m_txSc2.text = "" + m_nPt2;

		}
	}

	public void Set_Computer(int nLevel){

		m_nMode_Comp = nLevel;

		m_matComp.mainTexture = m_txComp[nLevel];

	}
	
	IEnumerator Temporisation(float NbSeonde,int x,int y) {

		GameObject goPiece;

		yield return new WaitForSeconds(NbSeonde);

		goPiece = Instantiate(m_goCroix,new Vector3(((float)x) + 0.5f,0,((float)y) + 0.5f),Quaternion.identity) as GameObject;
		
		m_goPiece2.Add (goPiece);

		if (PasGagne (m_nPlayJr) == false) {
			
			m_nPlayJr = 1;
			
		} else

			m_nPlayJr = 2;

	}


	bool Recherche_PasPerdre(){

		int i = 0;
		bool bStop = false;
		
		while(i <= 2 && bStop == false) {
			
			if(Plateau[i,0] == Plateau[i,1] && Plateau[i,0] == 1 && Plateau[i,2] == 0){
				bStop = true;
				m_nClic_X = i;
				m_nClic_Y = 2;
			}
			else if(Plateau[i,0] == Plateau[i,2] && Plateau[i,0] == 1 && Plateau[i,1] == 0){
				bStop = true;
				m_nClic_X = i;
				m_nClic_Y = 1;
			}else if(Plateau[i,1] == Plateau[i,2] && Plateau[i,1] == 1 && Plateau[i,0] == 0){
				bStop = true;
				m_nClic_X = i;
				m_nClic_Y = 0;
			}
			else
				i++;
			
		}

		i = 0;
		
		while(i <= 2 && bStop == false) {
			
			if(Plateau[0,i] == Plateau[1,i] && Plateau[0,i] == 1 && Plateau[2,i] == 0){
				bStop = true;
				m_nClic_X = 2;
				m_nClic_Y = i;
			}
			else if(Plateau[0,i] == Plateau[2,i] && Plateau[0,i] == 1 && Plateau[1,i] == 0){
				bStop = true;
				m_nClic_X = 1;
				m_nClic_Y = i;
			}else if(Plateau[1,i] == Plateau[2,i] && Plateau[1,i] == 1 && Plateau[0,i] == 0){
				bStop = true;
				m_nClic_X = 0;
				m_nClic_Y = i;
			}
			else
				i++;
			
		}

		if (bStop == false) {
			
			if(Plateau[0,0] == Plateau[1,1] && Plateau[0,0] == 1 && Plateau[2,2] == 0){
				bStop = true;
				m_nClic_X = 2;
				m_nClic_Y = 2;
			}
			else if(Plateau[0,0] == Plateau[2,2] && Plateau[0,0] == 1 && Plateau[1,1] == 0){
				bStop = true;
				m_nClic_X = 1;
				m_nClic_Y = 1;
			}else if(Plateau[1,1] == Plateau[2,2] && Plateau[1,1] == 1 && Plateau[0,0] == 0){
				bStop = true;
				m_nClic_X = 0;
				m_nClic_Y = 0;
			}

			if(Plateau[2,0] == Plateau[1,1] && Plateau[2,0] == 1 && Plateau[0,2] == 0){
				bStop = true;
				m_nClic_X = 0;
				m_nClic_Y = 2;
			}
			else if(Plateau[2,0] == Plateau[0,2] && Plateau[2,0] == 1 && Plateau[1,1] == 0){
				bStop = true;
				m_nClic_X = 1;
				m_nClic_Y = 1;
			}else if(Plateau[1,1] == Plateau[0,2] && Plateau[1,1] == 1 && Plateau[2,0] == 0){
				bStop = true;
				m_nClic_X = 2;
				m_nClic_Y = 0;
			}
			
		}

		return bStop;

	}

	bool Recherche_Gagne(){

		int i = 0;
		bool bStop = false;
		
		while(i <= 2 && bStop == false) {
			
			if(Plateau[i,0] == Plateau[i,1] && Plateau[i,0] == 2 && Plateau[i,2] == 0){
				bStop = true;
				m_nClic_X = i;
				m_nClic_Y = 2;
			}
			else if(Plateau[i,0] == Plateau[i,2] && Plateau[i,0] == 2 && Plateau[i,1] == 0){
				bStop = true;
				m_nClic_X = i;
				m_nClic_Y = 1;
			}else if(Plateau[i,1] == Plateau[i,2] && Plateau[i,1] == 2 && Plateau[i,0] == 0){
				bStop = true;
				m_nClic_X = i;
				m_nClic_Y = 0;
			}
			else
				i++;
			
		}
		
		i = 0;
		
		while(i <= 2 && bStop == false) {
			
			if(Plateau[0,i] == Plateau[1,i] && Plateau[0,i] == 2 && Plateau[2,i] == 0){
				bStop = true;
				m_nClic_X = 2;
				m_nClic_Y = i;
			}
			else if(Plateau[0,i] == Plateau[2,i] && Plateau[0,i] == 2 && Plateau[1,i] == 0){
				bStop = true;
				m_nClic_X = 1;
				m_nClic_Y = i;
			}else if(Plateau[1,i] == Plateau[2,i] && Plateau[1,i] == 2 && Plateau[0,i] == 0){
				bStop = true;
				m_nClic_X = 0;
				m_nClic_Y = i;
			}
			else
				i++;
			
		}
		
		if (bStop == false) {
			
			if(Plateau[0,0] == Plateau[1,1] && Plateau[0,0] == 2 && Plateau[2,2] == 0){
				bStop = true;
				m_nClic_X = 2;
				m_nClic_Y = 2;
			}
			else if(Plateau[0,0] == Plateau[2,2] && Plateau[0,0] == 2 && Plateau[1,1] == 0){
				bStop = true;
				m_nClic_X = 1;
				m_nClic_Y = 1;
			}else if(Plateau[1,1] == Plateau[2,2] && Plateau[1,1] == 2 && Plateau[0,0] == 0){
				bStop = true;
				m_nClic_X = 0;
				m_nClic_Y = 0;
			}
			
			if(Plateau[2,0] == Plateau[1,1] && Plateau[2,0] == 2 && Plateau[0,2] == 0){
				bStop = true;
				m_nClic_X = 0;
				m_nClic_Y = 2;
			}
			else if(Plateau[2,0] == Plateau[0,2] && Plateau[2,0] == 2 && Plateau[1,1] == 0){
				bStop = true;
				m_nClic_X = 1;
				m_nClic_Y = 1;
			}else if(Plateau[1,1] == Plateau[0,2] && Plateau[1,1] == 2 && Plateau[2,0] == 0){
				bStop = true;
				m_nClic_X = 2;
				m_nClic_Y = 0;
			}
			
		}
		
		return bStop;

	}

	bool PasGagne(int Jr){

		int i = 0;
		bool bGagne = false;

		while(i <= 2 && bGagne == false) {

			if(Plateau[i,0] == Plateau[i,1] && Plateau[i,0] == Plateau[i,2] && Plateau[i,0] != 0)
				bGagne = true;
			else
				i++;

		}

		if (bGagne == false) {

			i = 0;

			while(i <= 2 && bGagne == false) {
				
				if(Plateau[0,i] == Plateau[1,i] && Plateau[0,i] == Plateau[2,i] && Plateau[0,i] != 0)
					bGagne = true;
				else
					i++;
				
			}

		}


		if (bGagne == false) {

			if(Plateau[0,0] == Plateau[1,1] && Plateau[0,0] == Plateau[2,2] && Plateau[0,0] != 0)
				bGagne = true;

			if(Plateau[2,0] == Plateau[1,1] && Plateau[2,0] == Plateau[0,2] && Plateau[2,0] != 0)
				bGagne = true;

		}


		if (bGagne == true) {

			i = 0;

			while (i <= 2) {

				Plateau [0, i] = 0;
				Plateau [1, i] = 0;
				Plateau [2, i] = 0;

				i++;
			}

			Animator anGagnePerdu;

			if (Jr == 1){
				m_txFin [0].enabled = true;

				i = 0;
								
				while(i < m_goPiece1.Count){
					
					anGagnePerdu = m_goPiece1[i].GetComponentInChildren<Animator>();
					anGagnePerdu.SetBool("bGagne",true);
					
					i++;
					
				}
				
				
				i = 0;
				
				while(i < m_goPiece2.Count){
					
					anGagnePerdu = m_goPiece2[i].GetComponentInChildren<Animator>();
					anGagnePerdu.SetBool("bPerdu",true);
					
					i++;
					
				}

				m_nPt1++;
			}
			else{
				if(m_nMode == 1)
					m_txFin [1].enabled = true;
				else
					m_txFin [3].enabled = true;

				i = 0;
				
				while(i < m_goPiece1.Count){
					
					anGagnePerdu = m_goPiece1[i].GetComponentInChildren<Animator>();
					anGagnePerdu.SetBool("bPerdu",true);
					
					i++;
					
				}
				
				
				i = 0;
				
				while(i < m_goPiece2.Count){
					
					anGagnePerdu = m_goPiece2[i].GetComponentInChildren<Animator>();
					anGagnePerdu.SetBool("bGagne",true);
					
					i++;
					
				}

				m_nPt2++;
			}


			//m_txGagne.text = "JOUEUR n°" + Jr + " A GAGNE !!!\npress space to continue !";






			m_cnGagne.enabled = true;

			m_txSc1.text = "" + m_nPt1;
			m_txSc2.text = "" + m_nPt2;
			


		} else {

			bool Partie_Nule = true;

			i = 0;
			
			while (i <= 2 && Partie_Nule == true) {
				
				if (Plateau [0, i] == 0)
					Partie_Nule = false;

				if (Plateau [1, i] == 0)
					Partie_Nule = false;

				if (Plateau [2, i] == 0)
					Partie_Nule = false;
				
				i++;
			}


			if(Partie_Nule){

				bGagne = Partie_Nule;

				i = 0;
				
				while (i <= 2) {
					
					Plateau [0, i] = 0;
					Plateau [1, i] = 0;
					Plateau [2, i] = 0;
					
					i++;
				}

				//m_txFin.text = "PARTIE NULE !!!\npress space to continue !";
				m_txFin [2].enabled = true;

				m_cnGagne.enabled = true;

			}

		}

		m_bGagne = bGagne;

		return bGagne;


	}




	public void Sound_ONOFF(){

		m_bOnoff_Sound = !m_bOnoff_Sound;

		if (m_bOnoff_Sound) {

			m_btnMenuSound.image.sprite = m_txSound_On;
			m_btnJeuSound.image.sprite = m_txSound_On;
			m_audSound.mute = false;
		
		} else {

			m_btnMenuSound.image.sprite = m_txSound_Off;
			m_btnJeuSound.image.sprite = m_txSound_Off;
			m_audSound.mute = true;
		}

	}


}
