using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
public class PlayerTest {


	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PlayerJumpAnimTest() {
		GameObject gw = GameObject.FindWithTag ("Player");
		PlayerController gwc = gw.GetComponent<PlayerController>();
		gwc.playerJump (); 
		Assert.AreEqual ("Jump", gwc.anim);
		yield return null;
	
	}
	[UnityTest]
	public IEnumerator PlayerJumpForceTest() {
		GameObject gw = GameObject.FindWithTag ("Player");
		PlayerController gwc = gw.GetComponent<PlayerController>();
		gwc.playerJump ();
		Assert.AreEqual(500f, gwc.jumpForce);
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}

}
