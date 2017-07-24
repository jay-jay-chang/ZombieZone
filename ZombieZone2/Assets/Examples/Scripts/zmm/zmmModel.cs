using UnityEngine;
using System.Collections;

namespace Spine.Unity.Examples {
	[SelectionBase]
	public class zmmModel : MonoBehaviour {

		#region Inspector
		[Header("Current State")]
		public zmmBodyState state;
		public bool facingLeft;
		[Range(-1f, 1f)]
		public float currentSpeed;

		[Header("Balance")]
		public float shootInterval = 0.12f;
		#endregion

		float lastShootTime;
		public event System.Action ShootEvent;	// Lets other scripts know when Spineboy is shooting. Check C# Documentation to learn more about events and delegates.

		#region API

		ZombieAI target;

		public void TryShoot () {
			float currentTime = Time.time;

			if (currentTime - lastShootTime > shootInterval) {
				lastShootTime = currentTime;
				if (ShootEvent != null) ShootEvent();	// Fire the "ShootEvent" event.
			}
		}

		public void TryShoot (ZombieAI ai) {
			float currentTime = Time.time;

			facingLeft = ai.transition > 0 ? true : false;

			if (currentTime - lastShootTime > shootInterval) {
				lastShootTime = currentTime;
				if (ShootEvent != null) {
					ShootEvent();	// Fire the "ShootEvent" event.
					ai.OnDamage(1);
				}
			}
		}

		public void TryMove (float speed) {
			currentSpeed = speed; // show the "speed" in the Inspector.

			if (speed != 0) {
				bool speedIsNegative = (speed < 0f);
				facingLeft = speedIsNegative; // Change facing direction whenever speed is not 0.
			}
				
			state = (speed == 0) ? zmmBodyState.Idle : zmmBodyState.Running;

		}
		#endregion
	}

	public enum zmmBodyState {
		Idle,
		Running,
	}
}
