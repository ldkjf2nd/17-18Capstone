
using UnityEngine;

public class Controller
{
	Vector2 newVec;
	float nvx;
	// Use this for initialization
	public Controller ()
	{
	}

	public Vector2 horizontalMotion (float h, float vx, float vy, float max, float moveForce)
	{
		newVec = new Vector2 (vx, vy);
		if ((h * newVec.x) < max) {
			//Assert.IsTrue(velocX < max);
			newVec = Vector2.right * h * moveForce;
			return newVec;
		}
		return new Vector2 (0f, 0f);
	}

	public float horizontalMotionSpeedLimit (float vx, float max)
	{
		nvx = vx;
		if (Mathf.Abs (nvx) > max) {
			//Assert.IsTrue(velocX < max);
			nvx = Mathf.Sign (nvx) * max;
			return nvx;
		}
		return nvx;
	}

	public bool characterFlip (float h, bool facingright)
	{
		if (h > 0 && !facingright) {
			return true;
		} else if (h < 0 && facingright) {
			return true;
		}
		return false;
	}

	public bool characterJump (bool button, bool ground)
	{
		if (button && ground) {
			return true;
		}
		return false;
	}

	public bool basicAttack1 (bool button)
	{
		if (button) {
			return true;
		}
		return false;
	}

	public bool dash (float timeLimit, float delta, bool buttonPress)
	{
		if (timeLimit <= delta && buttonPress) {
			return true;
		}
		return false;
	}

	public bool checkIfDoubleAttack (float timeLimit, float delta, bool buttonPress, bool firstAttack)
	{
		if (timeLimit < delta && buttonPress && firstAttack) { 
			return true; 
		}
		return false;
	}

	public bool isDead (int health)
	{
		if (health <= 0) {
			return true;
		}
		return false;
	}

	public bool boostJump ()
	{
		return false;
	}

	public bool isCombo (float hitstun, float hitTime)
	{
		if (hitstun < hitTime) {
			return false;
		}
		return true; 
	}

	public bool canTripleAtk (float timeLimit, float delta, bool buttonPress, bool secondAttack)
	{
		if (delta < timeLimit && buttonPress && secondAttack) {
			return true;
		}
		return false;
	}
}
