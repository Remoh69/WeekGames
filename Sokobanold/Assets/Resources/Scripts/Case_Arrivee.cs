using UnityEngine;
using System.Collections;

public class Case_Arrivee : MonoBehaviour {

	Animation myAnim;

	// Use this for initialization
	void Start () {
	
		myAnim = GetComponent<Animation> ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider MyColl) {

		if (MyColl.gameObject.tag == "Caisse") {
			Debug.Log (MyColl.gameObject.name.Substring(MyColl.gameObject.name.Length - 4) + " " + this.gameObject.name.Substring(this.gameObject.name.Length - 4));
			if(MyColl.gameObject.name.Substring(MyColl.gameObject.name.Length - 4) == this.gameObject.name.Substring(this.gameObject.name.Length - 4))
				myAnim.Play("On_Ok");
			else
				myAnim.Play("On_Ko");

		}


	}

	void OnTriggerExit(Collider MyColl) {

		if (MyColl.gameObject.tag == "Caisse") {
			myAnim.Play ("Off");
		}
		
	}

}
