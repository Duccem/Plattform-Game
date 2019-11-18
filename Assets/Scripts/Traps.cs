using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

	void Start () {
		
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			col.SendMessage ("EnemyNockBack", transform.position.x);
		} else if (col.gameObject.tag == "Enemie") {
			col.SendMessage ("Direccion");
		}
	}
}
