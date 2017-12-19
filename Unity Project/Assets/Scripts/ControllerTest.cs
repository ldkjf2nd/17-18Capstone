using UnityEngine;
using UnityEditor;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ControllerTest
{   //tests for player controller logic
	Controller controller = new Controller();
    [Test]
    public void AddPlayerHorizontalMovement()
    {
        float xpos = 0f;
        float ypos = 0f;
        float input = 1f;
        float maxSpeed = 5f;
        float moveforce = 365f;
        Assert.IsTrue(controller.horizontalMotion(input, xpos, ypos, maxSpeed, moveforce).x > 0f); //positive movement result in positive direction
        input = 0.5f;
        Assert.IsTrue(controller.horizontalMotion(input, xpos, ypos, maxSpeed, moveforce).x > 0f); //positive movement result in positive direction
        input = -0.5f;
        Assert.IsTrue(controller.horizontalMotion(input, xpos, ypos, maxSpeed, moveforce).x < 0f); //positive movement result in positive direction
        input = -1f;
        Assert.IsTrue(controller.horizontalMotion(input, xpos, ypos, maxSpeed, moveforce).x < 0f); //negative movement result in negative direction
    }

    [Test]
    public void AddPlayerHorizontalMovementBoundaryTest()
    {
        float input = 99999999999999999999999999999999999999f;
        Assert.IsTrue((controller.horizontalMotion(input, 0f, 0f, 5f, 365f).x > 0f)); //if system behaves towards input overflow
        input *= -1;
        Assert.IsTrue((controller.horizontalMotion(input, 0f, 0f, 5f, 365f).x < 0f)); //if system behaves towards input overflow
    }

    [Test]
    public void AddPlayerHorizontalMovementSpeedLimitTest()
    {
        float VelocityInXAxis = 5f;
        float maxSpeed = 5f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) == VelocityInXAxis); //velocity within limit remains unchanged.
        VelocityInXAxis = 2.5f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) == VelocityInXAxis); //velocity within limit remains unchanged.
        VelocityInXAxis = 0f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) == VelocityInXAxis); //velocity within limit remains unchanged.
        VelocityInXAxis = -2.5f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) == VelocityInXAxis); //velocity within limit remains unchanged.
        VelocityInXAxis = -5f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) == VelocityInXAxis); //velocity within limit remains unchanged.
    }

    [Test]
    public void AddPlayerHorizontalMovementOverSpeedLimitTest()
    {
        float VelocityInXAxis = 10f;
        float maxSpeed = 5f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) < VelocityInXAxis); //velocity within limit remains unchanged.
        VelocityInXAxis = -10f;
        Assert.IsTrue(condition: controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) > VelocityInXAxis); //velocity within limit remains unchanged.
    }

    [Test]
    public void AddPlayerHorizontalMovementSpeedLimitBoundaryTest()
    {
        float VelocityInXAxis = 99999999999999999999999999999999999999f;
        float maxSpeed = 5f;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) < VelocityInXAxis); //velocity within limit remains unchanged.
        VelocityInXAxis = -99999999999999999999999999999999999999f; ;
        Assert.IsTrue(controller.horizontalMotionSpeedLimit(VelocityInXAxis, maxSpeed) > VelocityInXAxis); //velocity within limit remains unchanged.
    }

    [Test]
    public void PlayerFlipInitiate()
    {
        float Xinput = 1f;
        bool facingRight = true;
        Assert.IsFalse(controller.characterFlip(Xinput, facingRight));  // should not flip if input is positive right direction and facing right.
        Xinput = -1f;
        facingRight = false;
        Assert.IsFalse(condition: controller.characterFlip(Xinput, facingRight));  // should not flip if input is negative right(left) direction and facing left.
        facingRight = true;
        Assert.IsTrue(controller.characterFlip(Xinput, facingRight)); // should flip if input and face direction are different.
        Xinput = 1f;
        facingRight = false;
        Assert.IsTrue(controller.characterFlip(Xinput, facingRight)); // should flip if input and face direction are different.
    }

    [Test]
    public void characterJumpInitiate()
    {
        bool buttonPressed = true;
        bool grounded = true;
        Assert.IsTrue(controller.characterJump(buttonPressed, grounded)); // should jump if button is pressed and avatar is on the ground
        grounded = false;
        Assert.IsFalse(controller.characterJump(buttonPressed, grounded)); // should not jump if button is pressed and avatar is not on the ground
        buttonPressed = false;
        Assert.IsFalse(controller.characterJump(buttonPressed, grounded)); // should not jump if button is not pressed and avatar is not on the ground
        grounded = true;
        Assert.IsFalse(controller.characterJump(buttonPressed, grounded)); // should not jump if button is not pressed and avatar is on the ground
    }

	[Test]
	public void dashTest()
	// test if player presses a horizontal direction twice within a time limit 
	{

		Assert.IsTrue (controller.dash(0.5f, 0.2f,true));
		Assert.IsTrue (controller.dash(0.5f, 0.4f,true));
		Assert.IsTrue (controller.dash(0.5f, 0.1f,true));
		Assert.IsTrue (controller.dash(0.6f, 0.3f,true));
		Assert.IsFalse (controller.dash (0.5f, 2.0f, true));
		Assert.IsFalse (controller.dash (0.5f, 2.0f, false));
		Assert.IsFalse (controller.dash (1f, 0f, true));
		Assert.IsFalse (controller.dash (-1f, 0.4f, true));
		Assert.IsFalse (controller.dash (0.5f, 0.4f, false));
	}
	[Test]
	public void checkIfDoubleAttack()
	{
		// { timeLimit < delta && buttonPress && firstAttack} 
		Assert.IsTrue(controller.checkIfDoubleAttack(1f,0.5f, true, true));
		Assert.IsTrue(controller.checkIfDoubleAttack(1f,0.4f, true, true));
		Assert.IsTrue(controller.checkIfDoubleAttack(1f,0.2f, true, true));
		Assert.IsTrue(controller.checkIfDoubleAttack(1f,0.5f, true, true));

		Assert.IsFalse(controller.checkIfDoubleAttack(1f,0.5f, false, false));
		Assert.IsFalse(controller.checkIfDoubleAttack(0.3f,5f, true, false));
		Assert.IsFalse(controller.checkIfDoubleAttack(0.6f,1f, false, true));
		Assert.IsFalse(controller.checkIfDoubleAttack(0.2f,7f, false, false));
	}
	[Test]
	public void isDead(){

		Assert.IsFalse (controller.isDead(90));
		Assert.IsFalse (controller.isDead (40));
		Assert.IsTrue (controller.isDead (0));
		Assert.IsTrue (controller.isDead (-10));
	}
	[Test]
	public void boostJump(){
		Assert.Fail();
	}
	[Test]
	public void isCombo(){
		//(hitstun < hitTime) 
		Assert.IsTrue (controller.isCombo(2f,3f));
		Assert.IsFalse(controller.isCombo(5f,3f));

	}
	[Test]
	public void canTripleAtk(){
		//{timeLimit < delta && buttonPress && secondAttack}
		Assert.IsTrue (controller.canTripleAtk(2f,3f,true,true));
		Assert.IsFalse (controller.canTripleAtk(2f,5f,true,true));
		Assert.IsFalse (controller.canTripleAtk(2f,3f,false,true));
		Assert.IsFalse (controller.canTripleAtk(2f,3f,false,false));
	}



}