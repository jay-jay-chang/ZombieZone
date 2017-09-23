using UnityEngine;
using System.Collections;

namespace Spine.Unity.Examples {
	[SelectionBase]
	public class zmmModel : MonoBehaviour {

		#region Inspector
		[Header("Current State")]
		public zmmBodyState state = zmmBodyState.Idle;
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
		public float moveStep = 1f;
		float move;

		public bool auto = true;
		public float endPoint = 0;

		public Vector3 InitialLocalPos;

		public System.Action OnArrivalDel;

		public void MoveForward(float dis){
			move = dis;
			state = zmmBodyState.Running;
		}

		public void Reset(){
			this.gameObject.transform.localPosition = InitialLocalPos;
		}

		void Start(){
			InitialLocalPos = this.gameObject.transform.localPosition;
		}

		void Update(){
			if (state == zmmBodyState.Firing) {
			} else if (state == zmmBodyState.Idle) {
				if (auto) {
					float dis = endPoint - this.gameObject.transform.localPosition.x;
					if (Mathf.Abs (dis) >= moveStep){
						MoveForward (dis);
					}
				}
			} else if (state == zmmBodyState.Running) {
				float dis = Mathf.Abs (move);
				if (dis > moveStep) {
					float factor = (move > 0) ? moveStep : -moveStep;
					TryMove (-factor);
					this.gameObject.transform.localPosition += new Vector3 (factor, 0, 0);
					move -= factor;
				} else {
//					if (auto) {
//						float d = endPoint - this.gameObject.transform.localPosition.x;
//						if (Mathf.Abs (d) < moveStep) {
//						} else {
//							MoveForward (endPoint - this.gameObject.transform.localPosition.x);
//						}
//					} else {
						move = 0;
						TryMove (0);
					//}
					if (OnArrivalDel != null) {
						OnArrivalDel();
					}
				}
			}
		}

		public void TryShoot () {
			float currentTime = Time.time;

			if (currentTime - lastShootTime > shootInterval) {
				lastShootTime = currentTime;
				if (ShootEvent != null) ShootEvent();	// Fire the "ShootEvent" event.
			}
		}

		public void TryShoot (ZombieAI ai) {
			float currentTime = Time.time;


			MoveForward (0);
			state = zmmBodyState.Firing;

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

		void OnTriggerEnter2D(Collider2D other) {
			//Debug.Log (other.gameObject.name +" Enter!");
		}

		void OnTriggerStay2D(Collider2D other) {
			if (auto) {
				ZombieAI ai = other.gameObject.GetComponent<ZombieAI> ();
				if (ai != null) {
					TryShoot (ai);
				}
			}
		}

		void OnTriggerExit2D(Collider2D other) {
			//Debug.Log (other.gameObject.name +" Exit!");
			if (auto) {
				state = zmmBodyState.Idle;
			}
		}

	}



	public enum zmmBodyState {
		Idle,
		Running,
		Firing,
	}
}
