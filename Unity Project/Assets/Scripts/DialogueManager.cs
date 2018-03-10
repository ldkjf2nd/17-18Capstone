using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	private Queue<string> sentences; 
	public Text nameText; 
	public Text dialogueText;
	public Image image;
	public GameObject dialogueCanvas; 
	public static bool inDialogue;
	// Use this for initialization
	void Update(){
		if (Input.GetKeyDown (KeyCode.J) && inDialogue) {
			DisplayNextSentence ();
			GameObject boss = GameObject.Find ("fireDino");
			fireDinoController fireDinoController = boss.GetComponent<fireDinoController> ();
			fireDinoController.anim.SetTrigger ("WallAttack");
			FindObjectOfType <SoundManagerScript> ().PlaySound ("nextDialogue");

		}
	} 
	void Start () {
		sentences = new Queue<string> ();
	}
	public void StartDialogue (Dialogue dialogue)
	{
		dialogueCanvas.SetActive (true);
		nameText.text = dialogue.name;
		dialogueText.text = dialogue.sentences [0];
		sentences.Clear ();
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue (sentence);
		}
		DisplayNextSentence ();	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue ();
		dialogueText.text = sentence;
	}
	void EndDialogue(){
		inDialogue = false;
		FindObjectOfType<GameManager> ().startPlayerControls ();
		FindObjectOfType<SoundManagerScript> ().PlayBGM ("level1Boss");
		dialogueCanvas.SetActive (false); 
		BossTrigger.bossIntro = false;
		BossTrigger.bossStart = true;
	}
}
