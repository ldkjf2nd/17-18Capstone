using System;
using NUnit.Framework;

namespace Capstone
{
	[TestFixture]
	public class HealthComponentTests
	{
		[Test]
		public void TakeDamage_PositiveAmount_HealthUpdated() 
		{
			HealthComponent health = new HealthComponent ();
			health.playerHealth = 100f;

			health.TakeDamage (10f);
			Assert.AreEqual (90f, health.playerHealth);
		}
	}
}

