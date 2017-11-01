using System;
using UnityEngine;
using System.Collections;

namespace Capstone
{
	public class MovementComponent
	{
		private Rigidbody2D rb2d;
		public float playerYPos;
		public float playerXPos;
		public float playerSpeed; 

		public void movePlayerHorizontally(float movementAmount)
		{
			//Store the current horizontal input in the float moveHorizontal.
			//float moveHorizontal = Input.GetAxis ("Horizontal");

			//Store the current vertical input in the float moveVertical.
			//float moveVertical = Input.GetAxis ("Vertical");

			//Use the two store floats to create a new Vector2 variable movement.
			//Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

			//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
			//rb2d.AddForce (movement * speed);
		}

		public void playerJump()
		{
			
		}
	}
}

