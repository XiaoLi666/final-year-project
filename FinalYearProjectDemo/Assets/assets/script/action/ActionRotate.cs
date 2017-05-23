using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionRotate : ActionBase {
		#region attributes
		public enum ROTATE_DIRECTION {
			ROTATE_none,
			ROTATE_right,
			ROTATE_left
		};
		private float m_angle = 0.0f;
		private float m_angleCounter = 0.0f;
		private const float m_rotateSpeed = 90.0f;
		private ROTATE_DIRECTION m_direction = ROTATE_DIRECTION.ROTATE_none;
		#endregion

		#region override methods
		public ActionRotate(GameObject owner, float angle, ROTATE_DIRECTION direction) : base (owner) {
			m_actionType = ActionBase.ACTION_TYPE.ACTION_rotate;
			m_angle = angle;
			m_direction = direction;
			if (m_direction == ROTATE_DIRECTION.ROTATE_left)
				m_angle *= -1.0f;
		}

		public override bool Update(bool pause = false) {
			if (pause) {
				return true;
			}

			if (m_direction == ROTATE_DIRECTION.ROTATE_none) {
				Debug.LogError ("Invalid rotation!");
				return false;
			}

			switch (m_direction) {
			case ROTATE_DIRECTION.ROTATE_left:
				if (m_angleCounter > m_angle) {
					float rotation_value = -Time.deltaTime * m_rotateSpeed;
					m_owner.transform.Rotate (new Vector3 (0.0f, rotation_value, 0.0f));
					m_angleCounter += rotation_value;
				} else {
					return false;
				}
				break;
			case ROTATE_DIRECTION.ROTATE_right:
				if (m_angleCounter < m_angle) {
					float rotation_value = Time.deltaTime * m_rotateSpeed;
					m_owner.transform.Rotate (new Vector3 (0.0f, rotation_value, 0.0f));
					m_angleCounter += rotation_value;
				} else {
					return false;
				}
				break;
			}

			return true;
		}
		#endregion
	}
}