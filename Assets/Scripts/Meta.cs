using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meta : MonoBehaviour {
	private GameObject GameController;
	public GameObject win;
	void Start(){
		GameController = GameObject.Find ("GameController");
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			win.SetActive (true);

			Invoke ("reiniciar", 3f);
		}
	}
	void reiniciar(){
		GameController.SendMessage ("reiniciar");
	}
}
