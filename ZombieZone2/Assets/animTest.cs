using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules;

public class animTest : MonoBehaviour {
	public SkeletonAnimation skeletonAnimation;
	public AtlasRegionAttacher attacher;

	[SpineAnimation] public string run, idle, fire;

	// Use this for initialization
	void Start () {
		skeletonAnimation.AnimationState.SetAnimation (0, run, true);
		skeletonAnimation.AnimationState.SetAnimation (1, idle, true);

		var skeletonRenderer = GetComponent<SkeletonRenderer>();
		attacher.Apply (skeletonRenderer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
