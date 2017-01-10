using UnityEngine;
using System.Collections;

namespace GameLogic {
	abstract public class ActionBase {
		public GameObject m_owner;
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

		// TODO: going to remove this definition, it seems most of the action are instant action
		public ACTION_CONDITION ActionCondition {
			set { m_actionCondition = value; }
			get { return m_actionCondition; }
		}

		// TODO: this constructor without parameter is useless, going to remove it
		public ActionBase () {
		}

		public ActionBase (GameObject owner) {
			m_owner = owner;
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