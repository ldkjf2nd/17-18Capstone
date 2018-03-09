using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


// Purpose of LogicTests class is to test classes that re short but contain logic operations.
public class LogicTests {

	public string PlayerTag = "player"; //set dummy player tag

	//BossTrigger variables
	public bool bossIntro = false; //set dummy boss intro boolean trigger
	public bool triggerDialogue = false; //set dummy dialogue trigger

	//CameraScript variables
	public bool inDialogue = false; //set dummy in dialogue flag

	//DiaglogueTrigger variables
	public bool loadlevel = false; //set dummy load trigger

	//DialogueManager variables
	public bool playerControl = false;
	public string bgm = "";
	public bool canvasActive = true;
	public bool bossStart = false;
	public int sentenceCount = 1; 
	public bool endDialogue = false;




	[Test]// APplicationManager.Quit()
	public void Quit (){
		bool AppIsPlaying = false;
		bool quit;
		#if AppIsPlaying
		#else
		quit = true;
		#endif
		// Testing 'quit' method logic

		Assert.AreEqual(quit, true);

	}

	// ArmourTrigger.OnTriggerEnter2D()
	public bool AmrOTE2D (string tag){
		bool buyTrigger = false;
		if (tag == "player"){
			buyTrigger = true;
		}
		return buyTrigger;
		// 'OnTriggerEnter2D Method Logic'
	}

	[Test]
	public void AmrOTE2DTest (){

		// Testing 'OnTriggerEnter2D' method logic
		bool buy = AmrOTE2D(PlayerTag);
		Assert.IsTrue(buy);// Test if trigger when tag is player
		buy = AmrOTE2D("notPlayer");
		Assert.IsTrue(!buy);// Test if not trigger when tag is not player

	}

	// AttackDetection.OnTriggerEnter2D()
	public bool AttOTE2D (string tag){
		if (tag == "player"){
			return true;
		}
		return false;
		// 'OnTriggerEnter2D Method Logic'
	}

	[Test]
	public void Att0TE2DTest (){

		// Testing 'OnTriggerEnter2D' method logic
		bool isPlayerNear = AttOTE2D(PlayerTag);
		Assert.IsTrue(isPlayerNear);// Test if trigger when tag is player
		isPlayerNear = !AttOTE2D("notPlayer");
		Assert.IsTrue(isPlayerNear);// Test if not trigger when tag is not player

	}

	// BossDoor.FixedUpdate()
	public Vector3 BDFixedUpdate(bool isOpen, Vector3 position, float deltaTime){
		if(isOpen){
			position = position + new Vector3(0,1,0)*5*deltaTime;
		}
		return position;
		// 'FixedUpdate' method logic

	}

	[Test]
	public void BDFixedUpdateTest (){
		Vector3 position = new Vector3(0,0,0);
		bool isOpen = true;
		float deltaTime = 1f;

		// Testing results of logic
		position = BDFixedUpdate(isOpen, position, deltaTime);
		Assert.AreEqual( position , new Vector3(0,5,0));//expect change in position
		isOpen = false;//expect no change.
		position = position = BDFixedUpdate(isOpen, position, deltaTime);
		Assert.AreEqual( position , new Vector3(0,5,0));

	}


	// BossDoor.OnCollisionEnter2D
	public bool BDOnCollisionEnter2D (string tag) {
		bool isOpen = false;
		if(tag == "player") {
			isOpen = true;
		}
		return isOpen;
	}

	[Test]
	public void BDOnCollisionEnter2DTest (){
		bool isOpen = BDOnCollisionEnter2D (PlayerTag);

		// Testing 'OnTriggerEnter2D' method logic
		Assert.IsTrue(isOpen);//Expect true for isOpen
		isOpen = !BDOnCollisionEnter2D ("notPlayer");//Expect not true for isOpen
		Assert.IsTrue(isOpen);
	}

	// BossTrigger.FixedUpdate()
	public bool BTFixedUpdate(bool isOpen, Vector3 position){
		if(position.x >= position.x -0.5 && position.y >= position.y){
			isOpen = false;
			bossIntro = true;
			triggerDialogue = true;
		}
		return isOpen;
		// 'FixedUpdate' method logic

	}

	[Test]
	public void BTFixedUpdateTest (){
		Vector3 position = new Vector3(0,0,0);
		bool isOpen = false;

		// Testing results of logic
		isOpen = BTFixedUpdate(isOpen, position);
		Assert.IsTrue(!isOpen);//expect false
		Assert.IsTrue(bossIntro);//expect true
		Assert.IsTrue (triggerDialogue);//expect true
	}

	// cameraScript.LateUpdate
	public Vector3 cSLateUpdate (Vector3 camPosition, Vector3 heroPosition, float deltaTime)
	{
		Vector3 offset = new Vector3 (0, 2, -10);
		Vector3 bossPosition = new Vector3 (0, 0, 10);
		if (!bossIntro) {
			camPosition = heroPosition + offset;
		}
		if (bossIntro) {
			camPosition = Vector3.MoveTowards (camPosition, bossPosition, 10 * deltaTime);
			if (camPosition.x == bossPosition.x && camPosition.y == bossPosition.y) {
				inDialogue = true;
			}
		}
		return camPosition;
	}

	[Test]
	public void cSLateUpdateTest () {
		float deltaTime = 1f;
		Vector3 camPosition = new Vector3 (0, 0, 0);
		Vector3 heroPosition = new Vector3 (0, 0, 0);

		bossIntro = false;
		Assert.AreEqual (new Vector3 (0, 2, -10), cSLateUpdate (camPosition, heroPosition, deltaTime)); // Test if correct offset when not in intro
		bossIntro = true;
		Assert.AreNotEqual (new Vector3 (0, 2, -10), cSLateUpdate (camPosition, heroPosition, deltaTime));// Test if correct position when in intro.
	}
		
	// DeathTrigger.OnTriggerEnter2D()
	public bool DTOTE2D (string tag)
	{
		bool setActive = true;
		loadlevel = false;
		if (tag == "player"){
			loadlevel = true;
		}
		else {
			setActive = false;
		}
		return setActive;
	}

	[Test]
	public void DTOTE2DTest (){

		// Testing 'OnTriggerEnter2D' method logic
		bool setActive = DTOTE2D(PlayerTag);
		Assert.IsTrue(setActive);// Test if trigger when tag is player
		Assert.IsTrue(loadlevel);
		setActive = DTOTE2D("notPlayer");
		Assert.IsFalse(setActive);// Test if not trigger when tag is not player
		Assert.IsFalse(loadlevel);
	}

	//DialogueManager.Update()
	void Update (bool InputDown)
	{
		if (InputDown && inDialogue) {
			DisplayNextSentence ();

		}
	}

	//DialogueManager.DisplayNextSentence()
	public void DisplayNextSentence ()
	{
		if (sentenceCount == 0) {

			endDialogue = true; //set up flag for testing purpose
			EndDialogue();
			return;
		}
		sentenceCount--;
	}

	//DialogueManager.EndDialogue()
	void EndDialogue ()
	{
		inDialogue = false;
		playerControl = true;
		bgm = "boss music";
		canvasActive = false; 
		bossIntro = false;
		bossStart = true;
	}

	[Test]
	public void DialogueManagerTest () {
		inDialogue = true;
		Update(true);
		Assert.IsFalse (endDialogue);	//Test dialogue has not yet end.
		Assert.AreEqual (sentenceCount, 0); 	//Test changes in sentenceCount
		Update(true);
		Assert.IsTrue(endDialogue);		// Test if triggering/initializing correct sequences.
		Assert.IsFalse (inDialogue);
		Assert.IsTrue (playerControl);
		Assert.AreEqual (bgm, "boss music");
		Assert.IsFalse (canvasActive);
		Assert.IsFalse (bossIntro);
		Assert.IsTrue (bossStart);
	}


}
