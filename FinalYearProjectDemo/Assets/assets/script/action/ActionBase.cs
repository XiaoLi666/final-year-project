using UnityEngine;

namespace GameLogic {
	abstract public class ActionBase {
		#region attributes
		protected enum ACTION_TYPE {
			ACTION_none,
			ACTION_raycasting,
			ACTION_eatSeaweed,
			ACTION_rotate,
			ACTION_playTutorialMode,
			ACTION_playCustomMode
		};
		protected GameObject m_owner;
		protected ACTION_TYPE m_actionType;
		#endregion

		#region methods
		public ActionBase (GameObject owner) {
			m_owner = owner;
		}

		public virtual bool Update(bool pause = false) {
			return true;
		}
		#endregion
	}
}