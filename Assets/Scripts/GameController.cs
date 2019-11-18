using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public  class GameController : MonoBehaviour {

	public GameObject uiIdle;
	public bool iniciado = false;
	private AudioSource music;
	void Start () {
		music = GetComponent<AudioSource> ();
		Time.timeScale = 0;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if(Input.GetKeyDown(KeyCode.W)){
			if(Time.timeScale == 1){    //si la velocidad es 1
				Time.timeScale = 0; 

				uiIdle.SetActive (true);//que la velocidad del juego sea 0
			} else if(Time.timeScale == 0) {   // si la velocidad es 0
				Time.timeScale = 1;
				music.Play ();
				uiIdle.SetActive (false);// que la velocidad del juego regrese a 1
			}
		}
	}
	void reiniciar(){
		SceneManager.LoadScene ("Mundo1");
	}
}
