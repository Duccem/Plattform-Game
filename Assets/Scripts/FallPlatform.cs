using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	private PolygonCollider2D pcd2;
	private Vector3 start;
	public float fallDelay = 1f;
	public float respawnDelay = 5f;
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		pcd2 = GetComponent<PolygonCollider2D> ();
		start = transform.position;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player")) {
			Invoke ("Fall", fallDelay);
			Invoke ("Respawn", respawnDelay + fallDelay);
		}
	}

	void Fall(){
		rb2d.isKinematic = false;
		pcd2.isTrigger = true;
	}
	void Respawn(){
		transform.position = start;
		rb2d.isKinematic = true;
		rb2d.velocity = Vector3.zero;
		pcd2.isTrigger = false;
	}
}
