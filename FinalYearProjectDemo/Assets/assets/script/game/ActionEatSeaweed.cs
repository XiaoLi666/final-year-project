using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionEatSeaweed : ActionBase {
		public ActionEatSeaweed(GameObject owner) : base(owner) {
			m_actionType = ActionBase.ACTION_TYPE.ACTION_eatSeaweed;
		}

		public override bool Update () {
			return true;
		}
	}
}