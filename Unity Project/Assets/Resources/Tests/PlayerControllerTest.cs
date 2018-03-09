using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerTest {

	[Test]
	public void PlayerTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PlayerJumpForceTest() {
		GameObject gw = GameObject.FindWithTag ("Player");
		PlayerController gwc = gw.GetComponent<PlayerController> ();
		Assert.AreEqual(500f, gwc.jumpForce);
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}


}
