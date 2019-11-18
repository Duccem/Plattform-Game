using UnityEngine;

public class PlayerController : MonoBehaviour {
	[Header("Player Stats")]
	public float speed = 5f;
	public float jumpPower = 6.25f;
	public float dashMaxTime  = 0.1f;
	public float dashSpeed = 30f;
	public AudioClip jumpClip;
	public AudioClip damageClip;
	[HideInInspector]
	public bool grounded;

	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer spr;
	private AudioSource music;
	private GameObject healbar;
	private GameObject game;

	private float dashTime = 0;
	private float axis;
	private bool isDashing;
	private bool jump;
	private bool jumpUp;
	private bool doubleJump;
	private bool movement = true;



	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		music = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		spr = GetComponent<SpriteRenderer> ();
		healbar = GameObject.Find ("Health_Bar");
		game = GameObject.Find ("GameController");
	}
	void Update(){
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		anim.SetBool ("Graunder", grounded);

		if (grounded) {
			doubleJump = true;
		}
		axis = Input.GetAxis ("Horizontal");
		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D))
			axis = 0;
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (grounded) {
				jump = true;
				doubleJump = true;
			}else if(doubleJump){
				jump = true;
				doubleJump = false;
			}
		} else if (Input.GetButtonUp ("Jump")) 
		{
			jumpUp = true;
		}
		if (Input.GetKeyDown (KeyCode.E))
			isDashing = true;

	}

	void FixedUpdate(){
		if (transform.position.y < -5.55f) {
			game.SendMessage ("reiniciar");
		}

		if (isDashing) {
			Dash (axis);
		} else {
			Walk (axis);
		}
		if (!movement) {
			axis = 0;
		}
		TurnAraund (axis);
		Jump ();
	}
		
	//MECANICAS 

	//Funcion WALK
	void Walk(float h){
		Vector3 fixedVelocity = rb2d.velocity;
		fixedVelocity.x *= 0.75f;
		if (grounded) {
			rb2d.velocity = fixedVelocity;
		}
		if (h < 0 || h > 0) {
			rb2d.velocity = new Vector2 (speed * h, rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}
	}

	//Habilidad DASH
	void Dash(float h){
		if (dashTime <= 0) {
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			dashTime = dashMaxTime;
			isDashing = false;
		} else {
			dashTime -= Time.deltaTime;	
			if (h > 0) {
				rb2d.velocity = Vector2.right * dashSpeed;

			} else if (h < 0) {
				rb2d.velocity = Vector2.left * dashSpeed;

			} else {
				rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}
		}
	}
	//Habilidad JUMP	
	void Jump(){
		if (jump) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpPower);
			music.clip = jumpClip;
			music.Play ();
			jump = false;
		} 
		if(jumpUp) {
			Vector3 fixedVelocity = rb2d.velocity;
			fixedVelocity.y *= 0.5f;
			if (rb2d.velocity.y > 0) {
				rb2d.velocity = fixedVelocity;
			}
			jumpUp = false;
		}
	} 



	//FLIP
	void TurnAraund(float h){
		bool flipSprite = (spr.flipX ? (h > 0) : (h < 0));
		if (flipSprite) 
		{
			spr.flipX = !spr.flipX;
		}
	}




	//EnemyJump
	public void EnemyJump(){
		jump = true;
	}

	public void EnemyNockBack(float enemyPosX){
		jump = true;
		float side = Mathf.Sign (enemyPosX - transform.position.x);
		rb2d.AddForce (Vector2.left * side * jumpPower, ForceMode2D.Impulse);
		movement = false;
		spr.color = Color.red;
		Invoke ("EnableMovement", 0.7f);
		healbar.SendMessage ("TakeDamage", 15f);
	}

	void EnableMovement(){
		movement = true;
		spr.color = Color.white;
	}

	void Curar(){
		healbar.SendMessage ("Curar",15f);
	}

}
