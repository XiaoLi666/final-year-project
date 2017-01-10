using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionMoveForward : ActionBase {
		private float m_moveSpeed;
		private Rigidbody m_rb;

		public float MoveSpeed {
			set { m_moveSpeed = value; }
			get { return m_moveSpeed; }
		}

		public ActionMoveForward(ACTION_CONDITION action_condition, GameObject owner, float move_speed) : base(owner) {
			m_actionType = ACTION_TYPE.ACTION_moveForward;
			m_actionCondition = action_condition;
			m_moveSpeed = move_speed;
			Init ();
		}

		public override void Init() {
			m_actionType = ActionBase.ACTION_TYPE.ACTION_moveForward;
			m_rb = m_owner.transform.GetChild(0).GetComponent<Rigidbody>();
		}

		public override void Start() {
			
		}

		public override bool Update() {
			m_owner.transform.position += new Vector3 (m_moveSpeed, 0.0f);
			return true;
		}

		public override void End() {
			
		}
	}
}