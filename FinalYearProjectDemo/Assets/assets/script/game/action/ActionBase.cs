using UnityEngine;
using System.Collections;

namespace GameLogic {
	abstract public class ActionBase {
		public GameObject m_owner = null;
		protected ACTION_TYPE m_actionType;
		protected ACTION_CONDITION m_actionCondition;

		public GameObject Owner {
			set { m_owner = value; }
			get { return m_owner; }
		}

		public ACTION_TYPE ActionType {
			set { m_actionType = value; }
			get { return m_actionType; }
		}

		public ACTION_CONDITION ActionCondition {
			set { m_actionCondition = value; }
			get { return m_actionCondition; }
		}

		public abstract void Init ();
		public abstract void Start();
		public abstract bool Update();
		public abstract void End();

		public enum ACTION_TYPE {
			ACTION_none,
			ACTION_collisionDetection,
			ACTION_jump,
			ACTION_moveForward,
			ACTION_moveLeft
		};

		public enum ACTION_CONDITION {
			ACTION_instant,
			ACTION_permanent
		}
	}
}