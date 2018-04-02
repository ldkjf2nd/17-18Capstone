using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class octo : MonoBehaviour {
	private SpriteRenderer sp2d;
	public int ebdmg;
	public int ebspeed;
	public int speed;
	public int hSpeed;
	public int dmg;
	public Slider healthSlider;
	public GameObject health;
	public GameObject wall;
	public GameObject horizontal; 
	public GameObject vertical;
	public GameObject bullet;
	public GameObject bigBullet;
	public GameObject explosion;
	private Animator anim;
	public int[] moveOrder = {0,1};
	public int n;
	public float timeDuration;
	public float timeDuration2;
	public float timeDuration3;
	public float time;
	private Vector3 startPos;
	public float distance;


	public bool inMove;
	public bool inAttack3;
	public bool inAttack2;
	public bool inAttack1;
	public bool inAttack4;
	public bool inDeath;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		sp2d = GetComponent<SpriteRenderer> ();
		n = 0;
		time = 0;
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
	if (BossTrigger.bossStart) 
		{
			GameObject temp = GameObject.Find ("GW");
			PlayerController gw = temp.GetComponent<PlayerController> ();
			time += Time.deltaTime;
			/*
		Vector3 org = startPos; 
		org.x += distance * Mathf.Sin (Time.time * hSpeed);
		transform.position = org;
		*/
			if (time < timeDuration) {
				attack3 (gw);
			}  
			if (time > timeDuration && time < timeDuration2 && !inAttack1 && !inAttack3 && !inAttack2) {
				attack1 (6);
			}  
			if (time > timeDuration2 && time < timeDuration3 && !inAttack1) {
				attack2 (gw);
			} 
			if (time > timeDuration3) {
				time = 0;
			}  
		}
	}
	public void attack3(PlayerController gw )
	{
		float a =UnityEngine.Random.Range (1f, 3f);
		float b =UnityEngine.Random.Range (1f, 3f);
		if (!inAttack3 && !inMove && moveOrder [n % moveOrder.Length] == 0) {
			StartCoroutine (move (new Vector3 (gw.transform.position.x +a, gw.transform.position.y + b, gw.transform.position.z), 1f));
			n++;
		}
		if (!inAttack3 && !inMove && moveOrder [n % moveOrder.Length] == 1) {
			StartCoroutine (attack3 (1, 1.5f));
			n++;
		} 
	}

	public void attack2(PlayerController gw)
	{
		float a =UnityEngine.Random.Range (1f, 3f);
		float b =UnityEngine.Random.Range (4f, 7f);
		if (!inAttack3 && !inMove && moveOrder [n % moveOrder.Length] == 0) {
			StartCoroutine (move (new Vector3 (gw.transform.position.x, gw.transform.position.y + b, gw.transform.position.z), 1f));
			n++;
		}
		if (!inAttack3 && !inMove && moveOrder [n % moveOrder.Length] == 1) {
			StartCoroutine (attack2 (1, 2f,gw));
			n++;
		} 

	}


	public void attack1(int noAtk)
	{
		if (!inAttack1 && !inMove && moveOrder [n % moveOrder.Length] == 0) {
			StartCoroutine (move (new Vector3 (159.87f, -44.53f, 0f), 3f));
			n++;
		}
		if (!inAttack1 && !inMove && moveOrder [n % moveOrder.Length] == 1) {
			StartCoroutine (attack1 (noAtk, 1.5f));
			n++;
		} 
	}



	public void setHor(GameObject hor, int ebdmg, int ebspeed, GameObject bullet,int speed,float target, int dmg)
	{
		hor.GetComponent<horizontal> ().ebdmg = ebdmg;
		hor.GetComponent<horizontal> ().ebspeed = ebspeed;
		hor.GetComponent<horizontal> ().bullet = bullet;
		hor.GetComponent<horizontal> ().speed = speed;
		hor.GetComponent<horizontal> ().target = target;
		hor.GetComponent<horizontal> ().dmg = dmg;

	}
	public void setWall(GameObject wall,float speed, Vector3 target, int dmg){
		wall.GetComponent<electricwall> ().dmg = dmg;
		wall.GetComponent<electricwall> ().speed = speed;
		wall.GetComponent<electricwall> ().target = target;
	}

	public void wallShoot()
	{
		GameObject wl = (GameObject)Instantiate(wall, transform.position, transform.rotation);
		GameObject wr = (GameObject)Instantiate(wall, transform.position, transform.rotation);
		setWall (wl,5, new Vector3 (140f, transform.position.y, transform.position.z), 5);
		setWall (wr,5, new Vector3 (178f, transform.position.y, transform.position.z), 5);
	}
	public void bigShoot(PlayerController gw)
	{
		GameObject w = (GameObject)Instantiate(bigBullet, new Vector3(transform.position.x,transform.position.y, transform.position.z-1f), transform.rotation);
		float a =UnityEngine.Random.Range (0f, 2f);
		float b =UnityEngine.Random.Range (3f, 5f);
		w.transform.SetParent (null);
		w.GetComponent<elctrobullet> ().dmg = 5;
		w.GetComponent<elctrobullet> ().speed = 7;
		w.GetComponent<elctrobullet> ().target = new Vector3(gw.transform.position.x+a, gw.transform.position.y-b, transform.position.z);
	}

	public void qShoot()
	{
		GameObject[] q = new GameObject[4];
		float x = 3;
		for(int i = 0; i < q.Length; i++){
			q[i] =  (GameObject)Instantiate(bullet, new Vector3(transform.position.x,transform.position.y, transform.position.z), transform.rotation);
			setQ(q[i], 5, 5, new Vector3(145f+x,-47f,0));
			x += 10;
		}
	}
	public void qShoot2()
	{
		GameObject[] q = new GameObject[3];
		float x = 3;
		for(int i = 0; i < q.Length; i++){
			q[i] =  (GameObject)Instantiate(bullet, new Vector3(transform.position.x,transform.position.y, transform.position.z), transform.rotation);
			setQ(q[i], 5, 5, new Vector3(148f+x,-47f,0));
			x += 10;
		}
	}

	public void setQ(GameObject q, int dmg, int speed, Vector3 target){
		q.GetComponent<elctrobullet> ().dmg = 5;
		q.GetComponent<elctrobullet> ().speed = 7;
		q.GetComponent<elctrobullet> ().target = target;

	}

	public void shoot()
	{
		GameObject horL = (GameObject)Instantiate(horizontal, transform.position, transform.rotation);
		GameObject horR = (GameObject)Instantiate(horizontal, transform.position, transform.rotation);
		GameObject ver = (GameObject)Instantiate(vertical, transform.position, transform.rotation);
		setHor (horL, ebdmg, ebspeed, bullet, speed, 140f, dmg);
		setHor (horL, ebdmg, ebspeed, bullet, speed, 10f, dmg);
		setHor (horR, ebdmg, ebspeed, bullet, speed, 178f, dmg);
	}
	public IEnumerator attack3(int shots,float time)
	{
		inAttack3 = true; 
		for(int i =0; i<shots; i++ ){
			anim.Play ("attack3");
			shoot ();
			yield return new WaitForSeconds (time);
			//anim.SetBool ("attack3", false);
		}
		inAttack3 = false; 
	}

	public IEnumerator move(Vector3 target,float ltime)
	{
		inMove = true; 
		anim.SetBool ("hover", true);
		 float time = 0;
		while (time < ltime) {
			yield return null;
			time += Time.deltaTime;
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target, step);
		}
		anim.SetBool ("hover", false);
		inMove = false;
	}

	public IEnumerator attack1(int shots,float time)
	{
		inAttack1 = true; 
		for(int i =0; i<shots; i++ ){
			anim.Play ("attack1");
			wallShoot ();
			yield return new WaitForSeconds (time);
		}
		inAttack1 = false; 
	}



	public IEnumerator attack2(int shots,float time, PlayerController gw)
	{
		inAttack2 = true; 
		for(int i =0; i<shots; i++ ){
			anim.Play ("attack3");
			bigShoot (gw);
			yield return new WaitForSeconds (time);
		}
		inAttack2 = false; 
	}


	public IEnumerator attack4(int shots,float time)
	{
		inAttack4 = true; 
		for(int i =0; i<shots; i++ ){
			qShoot ();
			yield return new WaitForSeconds (time);
		}
		inAttack4 = false; 
	}

	IEnumerator blink(){
		sp2d.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		sp2d.color = Color.white;
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Attack")) {
			healthSlider.value -= 1;
			GameObject thePlayer2 = GameObject.Find ("GW");
			PlayerController playercontroller2 = thePlayer2.GetComponent<PlayerController> ();
			FindObjectOfType<GameManager> ().increaseEnergy (5);
			health.SetActive (true);
			StartCoroutine (blink ());
			if (healthSlider.value <= 0) {
				StopAllCoroutines ();
				StartCoroutine (death ());
			}
		}
	}
	IEnumerator death(){
		inDeath = true; 
		anim.Play ("hurt"); 
		Instantiate (explosion, this.transform);
		health.SetActive (false);
		this.GetComponent<Rigidbody2D> ().simulated = false;
		this.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds (3);
		FindObjectOfType<GameManager> ().resetRespawn ();
		FindObjectOfType<GameManager> ().nextLevel ();
		this.GetComponent<Rigidbody2D>().gameObject.SetActive (false);
		inDeath = false;
	}



}