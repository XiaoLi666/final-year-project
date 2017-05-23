using UnityEngine;

namespace GameLogic {
	public class ActionEatSeaweed : ActionBase {
		#region methods
		public ActionEatSeaweed(GameObject owner) : base(owner) {
			m_actionType = ACTION_TYPE.ACTION_eatSeaweed;
		}

		public override bool Update (bool pause = false) {
			return true;
		}
		#endregion
	}
}