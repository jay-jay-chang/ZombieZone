/******************************************************************************
 * Spine Runtimes Software License v2.5
 *
 * Copyright (c) 2013-2016, Esoteric Software
 * All rights reserved.
 *
 * You are granted a perpetual, non-exclusive, non-sublicensable, and
 * non-transferable license to use, install, execute, and perform the Spine
 * Runtimes software and derivative works solely for personal or internal
 * use. Without the written permission of Esoteric Software (see Section 2 of
 * the Spine Software License Agreement), you may not (a) modify, translate,
 * adapt, or develop new applications using the Spine Runtimes or otherwise
 * create derivative works or improvements of the Spine Runtimes or (b) remove,
 * delete, alter, or obscure any trademarks or any copyright, trademark, patent,
 * or other intellectual property or proprietary rights notices on or in the
 * Software, including any copy thereof. Redistributions in binary or source
 * form must include this license and terms.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS INTERRUPTION, OR LOSS OF
 * USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;
using System.Collections;

namespace Spine.Unity.Examples {
	public class SpineboyBeginnerInput : MonoBehaviour {
		#region Inspector
		public string horizontalAxis = "Horizontal";
		public string attackButton = "Fire1";
		public string jumpButton = "Jump";

		float current_x;

		//public SpineboyBeginnerModel model;
		public zmmModel model;

		void OnValidate () {
			if (model == null)
				model = GetComponent<zmmModel>();
		}
		#endregion

		void Start()
		{
			current_x = model.gameObject.transform.position.x;
		}

		void Update () {
			if (model == null) return;

			float dis = current_x - model.gameObject.transform.position.x;
			//Debug.Log (model.gameObject.transform.position.x);
			if (Mathf.Abs(dis) > 0.0001f) 
			{
				if (dis > 0) 
				{
					model.TryMove (1);
				} 
				else 
				{
					model.TryMove (-1);
				}
			}
			else 
			{
				model.TryMove (0);
			}
			current_x = model.gameObject.transform.position.x;

			//float currentHorizontal = Input.GetAxisRaw(horizontalAxis);
			//model.TryMove(currentHorizontal);

			if (Input.GetButton(attackButton))
				model.TryShoot();

//			if (Input.GetButtonDown(jumpButton))
//				model.TryJump();
		}
	}

}