using UnityEngine;
using System;
using System.Collections;

public class Touche_Caisse : MonoBehaviour {

	Controle_Player m_scpCtrlPlay;
	Jeu m_scpJeu;

	public GameObject m_goPlayer;

	bool m_bDeplacement;
	bool m_bControle;
	bool m_bCol;
	bool m_bThis;
	int m_nDirection;

	float m_rPosX;
	float m_rPosY;


	// Use this for initialization
	void Start () {

		m_bDeplacement = false;
		m_bControle = false;
		m_bCol = false;
		m_bThis = false;
		m_nDirection = 0;

		m_scpJeu = Camera.main.GetComponent<Jeu> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C) && m_bThis) {

			m_scpCtrlPlay.Shoot ();
			if(!m_bDeplacement && m_bCol)
				StartCoroutine(Test_Deplacement ());
		} //else if (Input.GetKeyUp (KeyCode.C)) {
			//m_scpCtrlPlay.Fin_Shoot ();
		//}

		if (Input.GetKey (KeyCode.C) && m_bCol && !m_bDeplacement) {
			//Test_Deplacement ();
			//if(m_bDeplacement)
		}

		if (m_bDeplacement) {

			Vector3 vPos;

			vPos = transform.position;

			if(vPos.x / 2 > m_rPosX)
				vPos.x -= 0.1f; 
			else if (vPos.x / 2 < m_rPosX)
				vPos.x += 0.1f; 

			if(9 - (vPos.z / 2) < m_rPosY)
				vPos.z -= 0.1f; 
			else if (9 - (vPos.z / 2) > m_rPosY)
				vPos.z += 0.1f; 

			if(Math.Abs((vPos.x / 2) - m_rPosX) <= 0.1f && Math.Abs((9 - (vPos.z / 2)) - m_rPosY) <= 0.1f){


				m_bDeplacement = false;

				vPos.x = m_rPosX * 2;
				vPos.z = (9 - m_rPosY) * 2;

				transform.position = vPos;
				//if(m_bCol)
				//	Test_Deplacement();

			}
			else
				transform.position = vPos;

		}

	}

	void OnCollisionEnter(Collision MyColl) {

		if (MyColl.gameObject.tag == "Player" && !m_bDeplacement && !m_bControle) {

			m_bThis = true;

			m_bCol = true;

			m_scpCtrlPlay.Pousse();

			//Test_Deplacement();

		}

	}

	void OnCollisionExit(Collision MyColl) {

		if (MyColl.gameObject.tag == "Player") {

			m_bCol = false;
			m_bThis = false;

			m_scpCtrlPlay.Fin_Pousse();

		}
	}

	public IEnumerator Test_Deplacement(){

		yield return new WaitForSeconds(0.5f);

		m_bControle = true;
		
		if(m_scpCtrlPlay.m_rAngle >= 315 || m_scpCtrlPlay.m_rAngle < 45){
			m_nDirection = 1;
		}else if(m_scpCtrlPlay.m_rAngle >= 135 && m_scpCtrlPlay.m_rAngle < 225){
			m_nDirection = 2;
		}else if(m_scpCtrlPlay.m_rAngle >= 225 && m_scpCtrlPlay.m_rAngle < 315){
			m_nDirection = 3;
		}else if(m_scpCtrlPlay.m_rAngle >= 45 && m_scpCtrlPlay.m_rAngle < 135){
			m_nDirection = 4;
		}
		
		m_bDeplacement = false;
		
		switch (m_nDirection){
			
		case 1:
			
			if(m_scpJeu.Controle_Case(int.Parse (Math.Round(m_rPosX) + ""),int.Parse (Math.Round(m_rPosY) + ""),int.Parse (Math.Round(m_rPosX) + ""),int.Parse (Math.Round(m_rPosY) - 1 + "")) == true){
				
				m_bDeplacement = true;
				m_rPosY--;
				
			}
			
			break;
			
		case 2:
			
			if(m_scpJeu.Controle_Case(int.Parse (Math.Round(m_rPosX) + ""),int.Parse (Math.Round(m_rPosY) + ""),int.Parse (Math.Round(m_rPosX) + ""),int.Parse (Math.Round(m_rPosY) + 1 + "")) == true){
				
				m_bDeplacement = true;
				m_rPosY++;
				
			}
			
			break;
			
		case 3:
			
			if(m_scpJeu.Controle_Case(int.Parse (Math.Round(m_rPosX) + ""),int.Parse (Math.Round(m_rPosY) + ""),int.Parse (Math.Round(m_rPosX) - 1 + ""),int.Parse (Math.Round(m_rPosY) + "")) == true){
				
				m_bDeplacement = true;
				m_rPosX--;
				
			}
			
			break;
			
		case 4:
			
			if(m_scpJeu.Controle_Case(int.Parse (Math.Round(m_rPosX) + ""),int.Parse (Math.Round(m_rPosY) + ""),int.Parse (Math.Round(m_rPosX) + 1 + ""),int.Parse (Math.Round(m_rPosY) + "")) == true){
				
				m_bDeplacement = true;
				m_rPosX++;
				
			}
			
			break;
			
		}
		
		m_bControle = false;

	}


	public void Init_Aff_Player(){

		m_scpCtrlPlay = m_goPlayer.GetComponent<Controle_Player> ();

	}

	public void Set_Case(float x, float y){

		m_rPosX = x;
		m_rPosY = y;

	}

}
