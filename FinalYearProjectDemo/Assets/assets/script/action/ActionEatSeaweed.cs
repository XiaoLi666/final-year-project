using UnityEngine;

namespace GameLogic {
	public class ActionEatSeaweed : ActionBase {
		#region attributes
		private GameObject m_seaweed;
		#endregion

		#region methods
		public ActionEatSeaweed(GameObject owner, GameObject seaweed) : base(owner) {
			m_seaweed = seaweed;
			m_actionType = ACTION_TYPE.ACTION_eatSeaweed;
		}

		public override bool Update (bool pause = false) {
			if (m_seaweed != null) {
				Object.Destroy(m_seaweed.gameObject);
				m_seaweed = null;
			}
			return false;
		}
		#endregion
	}
}