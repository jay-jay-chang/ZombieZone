using UnityEngine;
using System.Collections;
using Spine.Unity;

namespace Spine.Unity.Examples {
	public class zmmView : MonoBehaviour {

		#region Inspector
		[Header("Components")]
		public zmmModel model;
		public SkeletonAnimation skeletonAnimation;

		[SpineAnimation] public string run, idle, fire;
//		[SpineEvent] public string footstepEventName;

//		[Header("Audio")]
//		public float footstepPitchOffset = 0.2f;
//		public float gunsoundPitchOffset = 0.13f;
//		public AudioSource footstepSource, gunSource, jumpSource;

//		[Header("Effects")]
//		public ParticleSystem gunParticles;
		#endregion

		zmmBodyState previousViewState;

		void Start () {
			if (skeletonAnimation == null) return;
			model.ShootEvent += PlayShoot;
//			skeletonAnimation.AnimationState.Event += HandleEvent;
		}

//		void HandleEvent (Spine.TrackEntry trackEntry, Spine.Event e) {
//			if (e.Data.Name == footstepEventName)
//				PlayFootstepSound();
//		}

		void Update () {
			if (skeletonAnimation == null) return;
			if (model == null) return;

			if (skeletonAnimation.skeleton.FlipX != model.facingLeft) {	// Detect changes in model.facingLeft
				Turn(model.facingLeft);
			}

			// Detect changes in model.state
			var currentModelState = model.state;

			if (previousViewState != currentModelState) {
				PlayNewStableAnimation();
			}

			previousViewState = currentModelState;
		}

		void PlayNewStableAnimation () {
			var newModelState = model.state;
			string nextAnimation;

			// Add conditionals to not interrupt transient animations.

//			if (previousViewState == SpineBeginnerBodyState.Jumping && newModelState != SpineBeginnerBodyState.Jumping) {
//				PlayFootstepSound();
//			}

//			if (newModelState == SpineBeginnerBodyState.Jumping) {
//				jumpSource.Play();
//				nextAnimation = jump;
//			} else {
				if (newModelState == zmmBodyState.Running) {
					nextAnimation = run;
				} else {
					nextAnimation = idle;
				}
//			}

			skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
		}

//		void PlayFootstepSound () {
//			footstepSource.Play();
//			footstepSource.pitch = GetRandomPitch(footstepPitchOffset);
//		}

		[ContextMenu("Check Tracks")]
		void CheckTracks () {
			var state = skeletonAnimation.AnimationState;
			Debug.Log(state.GetCurrent(0));
			Debug.Log(state.GetCurrent(1));
		}

		#region Transient Actions
		public void PlayShoot () {
			// Play the shoot animation on track 1.
			var track = skeletonAnimation.AnimationState.SetAnimation(1, fire, false);
			track.AttachmentThreshold = 1f;
			track.MixDuration = 0f;
			var empty = skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 0.1f);
			empty.AttachmentThreshold = 1f;
//			gunSource.pitch = GetRandomPitch(gunsoundPitchOffset);
//			gunSource.Play();
//			//gunParticles.randomSeed = (uint)Random.Range(0, 100);
//			gunParticles.Play();
		}

		public void Turn (bool facingLeft) {
			skeletonAnimation.Skeleton.FlipX = facingLeft;
			// Maybe play a transient turning animation too, then call ChangeStableAnimation.
		}
		#endregion

		#region Utility
		public float GetRandomPitch (float maxPitchOffset) {
			return 1f + Random.Range(-maxPitchOffset, maxPitchOffset);
		}
		#endregion
	}

}
