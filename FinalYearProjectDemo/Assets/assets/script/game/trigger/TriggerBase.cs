using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class TriggerBase {
		private GameObject m_user;
		public GameObject User {
			set { this.m_user = value; }
			get { return this.m_user; }
		}

		public TriggerBase(TriggerBase.TRIGGER_TYPE trigger_type) {
			m_type = trigger_type;
		}

		public virtual void Execute() {
		}

		public enum TRIGGER_TYPE {
			TRIGGER_none,
			TRIGGER_moveLeft,
			TRIGGER_moveRight,
			TRIGGER_moveUp,
			TRIGGER_moveDown,
			TRIGGER_moveforward,
			TRIGGER_rotateRight,
			TRIGGER_rotateLeft,
			TRIGGER_gameStart,
			TRIGGER_gameEnd,
			TRIGGER_speedup,
			TRIGGER_eatSeaweed
		};

		TRIGGER_TYPE m_type = TRIGGER_TYPE.TRIGGER_none;
	}
}