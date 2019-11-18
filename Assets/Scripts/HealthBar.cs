using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	float hp, maxHp = 100f;
	public Image health;
	private int vida= 3;
	private GameObject player;
	private GameObject GameController;
	void Start () {
		hp = maxHp;
		player = GameObject.Find ("Player");
		GameController = GameObject.Find ("GameController");
	}
	void FixedUpdate(){
		if (hp == 0) {
			GameController.SendMessage ("reiniciar");
			hp = 100;
			health.transform.localScale = new Vector2 (hp / maxHp, 1);
		}
	}
	public void TakeDamage(float amount){
		hp = Mathf.Clamp (hp - amount, 0f, maxHp);
		health.transform.localScale = new Vector2 (hp / maxHp, 1);
	}
	public void Curar(float amount){
		hp = Mathf.Clamp (hp + amount, 0f, maxHp);
		health.transform.localScale = new Vector2 (hp / maxHp, 1);
	}
}
