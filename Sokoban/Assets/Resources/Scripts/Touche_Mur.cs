using UnityEngine;
using System.Collections;

public class Touche_Mur : MonoBehaviour {
	
	Controle_Player m_scpCtrlPlay;
	public GameObject m_goPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision MyColl) {
		
		if (MyColl.gameObject.tag == "Player") {
			
			m_scpCtrlPlay.Pousse();
			
		}
		
	}
	
	void OnCollisionExit(Collision MyColl) {
		
		if (MyColl.gameObject.tag == "Player") {
			
			m_scpCtrlPlay.Fin_Pousse();
			
		}
	}

	public void Init_Aff_Player(){
		
		m_scpCtrlPlay = m_goPlayer.GetComponent<Controle_Player> ();
		
	}
}
