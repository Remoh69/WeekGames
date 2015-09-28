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

	void Awake(){

		m_cnScore.enabled = false;
		m_cnGagne.enabled = false;

		m_txFin [0].enabled = false;
		m_txFin [1].enabled = false;
		m_txFin [2].enabled = false;
		
		m_ImgP2.enabled = false;

		m_txHvsH.enabled = false;
		m_txHvsC.enabled = false;

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



	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (Plateau[0,0] + "," + Plateau[0,1] + "," + Plateau[0,2] + " | " + Plateau[1,0] + "," + Plateau[1,1] + "," + Plateau[1,2] + " | " + Plateau[2,0] + "," + Plateau[2,1] + "," + Plateau[2,2]);

		if (Input.GetKeyDown (KeyCode.Space) && !m_bLance) {

			m_nMode = 1;
			m_cnMenu.enabled = false;
			m_cnScore.enabled = true;
			m_bLance = true;

		}

		if ((Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) && !m_bLance) {
			
			m_nMode = 2;
			m_cnMenu.enabled = false;
			m_cnScore.enabled = true;
			m_bLance = true;
			
		}

		if (Input.GetKeyDown (KeyCode.Escape) && m_bLance) {

			m_cnScore.enabled = false;
			m_bLance = false;

			m_cnGagne.enabled = false;
			
			m_txFin [0].enabled = false;
			m_txFin [1].enabled = false;
			m_txFin [2].enabled = false;
			
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
					
				}
				
			}

			if(Input.GetKeyDown(KeyCode.Space) && m_bGagne){

				m_cnGagne.enabled = false;

				m_txFin [0].enabled = false;
				m_txFin [1].enabled = false;
				m_txFin [2].enabled = false;

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

					m_nClic_X = Random.Range(0,3);
					m_nClic_Y = Random.Range(0,3);

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
				m_txFin [1].enabled = true;

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


	public void Click_Human(){

		m_nMode = 1;
		m_cnMenu.enabled = false;
		m_cnScore.enabled = true;
		m_bLance = true;

	}

	public void Click_Computer(){

		m_nMode = 2;
		m_cnMenu.enabled = false;
		m_cnScore.enabled = true;
		m_bLance = true;

	}


}
