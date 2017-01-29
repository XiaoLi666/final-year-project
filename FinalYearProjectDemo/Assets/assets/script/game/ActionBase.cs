using UnityEngine;
using System.Collections;

namespace GameLogic {
	abstract public class ActionBase {
#region attributes
		protected enum ACTION_TYPE {
			ACTION_none,
			ACTION_collisionDetection,
			ACTION_eatSeaweed,
			ACTION_jump,
			ACTION_moveForward,
			ACTION_moveLeft,
			Action_playerAbovePathNodeDetection
		};
		protected GameObject m_owner;
		protected ACTION_TYPE m_actionType;
#endregion

#region methods
		public ActionBase (GameObject owner) {
			m_owner = owner;
		}

		public virtual bool Update() {
			return true;
		}
#endregion
	}
}