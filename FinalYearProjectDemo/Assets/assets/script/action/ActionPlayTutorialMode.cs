using UnityEngine;

namespace GameLogic {
	public class ActionPlayTutorialMode : ActionBase {
		#region methods
		public ActionPlayTutorialMode(GameObject owner) : base(owner) {
			m_actionType = ACTION_TYPE.ACTION_playTutorialMode;
		}

		public override bool Update(bool pause = false) {
			return true;
		}
		#endregion
	}
}
