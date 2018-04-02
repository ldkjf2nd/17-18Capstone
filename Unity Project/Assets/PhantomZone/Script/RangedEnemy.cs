using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour {
	private GameObject targetPlayer;
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool inMove;
	private float deltaX;
	private float deltaY;
	private bool inAttack1;
	private bool faceRight = true;
	public bool isDead;
	public GameObject bullet;
	public float attackRange;
	public float moveUp;
	private Vector3 moveLocation;
	private float d;
	private Vector3 org;
	public int hp;

	void Awake () {
		targetPlayer = GameObject.Find ("GW");
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		moveLocation = new Vector3(transform.position.x, transform.position.y + moveUp, transform.position.y);
		org = transform.position; 
		d = 1;
	}
	// Update is called once per frame
	void Update () {
		targetPlayer = GameObject.Find ("GW");
		deltaX = targetPlayer.transform.position.x-transform.position.x;
		deltaY = targetPlayer.transform.position.y - transform.position.y;
		transform.Translate (new Vector3 (0f, 1f*d, 0f) * Time.deltaTime);

		if (transform.position.y > moveLocation.y) {
			d = -1;
		}
		if (transform.position.y < org.y ) {
			d = 1;
		}

		if (!inAttack1 && Mathf.Abs(deltaX) < attackRange && Mathf.Abs(deltaY)< attackRange) {
			StartCoroutine (attack1 (targetPlayer, 3, 5f));
		}
		if (deltaX > 0 && !faceRight) {
			faceRight = !faceRight;
			flip ();
		}
		if (deltaX < 0 && faceRight) {
			faceRight = !faceRight;
			flip ();
		}
	}
		
	void flip() {
		Vector3 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
	}
		
	public void homShoot(GameObject targetPlayer)
	{
		GameObject w = (GameObject)Instantiate(bullet, new Vector3(transform.position.x,transform.position.y, transform.position.z-1f), transform.rotation);
		float a =UnityEngine.Random.Range (0f, 2f);
		float b =UnityEngine.Random.Range (3f, 5f);
		w.transform.SetParent (null);
		w.GetComponent<elctrobullet> ().dmg = 5;
		w.GetComponent<elctrobullet> ().speed = 7;
		w.GetComponent<elctrobullet> ().target = new Vector3(targetPlayer.transform.position.x+a, targetPlayer.transform.position.y-b, transform.position.z);
	}

	public IEnumerator attack1(GameObject targetPlayer, int shots,float time)
	{
		inAttack1 = true; 
		for(int i =0; i<shots; i++ ){
			anim.Play ("RangedAttack");
			homShoot (targetPlayer);
			yield return new WaitForSeconds (time);
		}
		inAttack1 = false; 
	}

	IEnumerator death(){
		anim.Play ("RangedDeath");
		this.GetComponent<BoxCollider2D> ().enabled = false;
		//FindObjectOfType<SoundManagerScript> ().PlaySound ("enemyDeath");
		yield return new WaitForSeconds (1);
		FindObjectOfType<GameManager> ().increaseScrap (63);
		rb2d.gameObject.SetActive (false);
		isDead = true;
	}

	IEnumerator blink(){
		this.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.2f);
		this.GetComponent<SpriteRenderer>().color = Color.white;
	}
	 
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Attack")| other.gameObject.CompareTag ("Attack2")| other.gameObject.CompareTag ("Launcher") ){
			hp -= 1;
			StartCoroutine (blink ());
			FindObjectOfType<GameManager> ().increaseEnergy (5);
			if (hp < 0) {
				StartCoroutine (death ());
			}
		}
	}

}
