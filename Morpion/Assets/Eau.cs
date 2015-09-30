using UnityEngine;
using System.Collections;

public class Eau : MonoBehaviour {

	public Material m_matEau;
	private float m_rOffset;

	private float m_rOffset_Vague;


	// Use this for initialization
	void Start () {

		m_rOffset = 0;

	}
	
	// Update is called once per frame
	void Update () {

		m_matEau.SetTextureOffset("_MainTex",new Vector2 (m_rOffset,m_rOffset));

		m_rOffset += 0.0008f;

		m_rOffset_Vague = Mathf.Sin(Time.time) / 100;
		this.transform.Translate(new Vector3(0,m_rOffset_Vague,0));

	}
}
