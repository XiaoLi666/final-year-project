using UnityEngine;

namespace GameLogic {
	public class ActionRaycasting : ActionBase {
		#region attributes
		// private
		private const float m_maxDistance = 10.0f;
		private int m_curInstanceId = -1;
		private Ray m_ray;
		private RaycastHit m_hit;
		private Player m_playerClass = null;
		//private GestureAnalyser m_gestureAnalyser = null;
		//private AnimationAnalyser m_animationAnalyser = null;

		// public
		public RaycastHit Hit { get { return m_hit; } }
		#endregion

		#region override methods
		public override bool Update(bool pause = false) {
			m_ray.origin = m_owner.transform.position;
			if (Physics.Raycast(m_ray, out m_hit, m_maxDistance)) {
				// To make sure that each object will only be ray casted once
				if (m_curInstanceId == m_hit.transform.gameObject.GetInstanceID()) {
					return true;
				}

				// save the tag of the collided component in player class
				m_playerClass.RaycastingTag = m_hit.collider.tag;
				m_curInstanceId = m_hit.transform.gameObject.GetInstanceID();
			}
			return true;
		}
		#endregion

		#region custom methods
		public ActionRaycasting(GameObject owner) : base(owner) {
			m_actionType = ACTION_TYPE.ACTION_raycasting;
			m_ray = new Ray(m_owner.transform.position, Vector3.down);
			m_playerClass = m_owner.GetComponent<Player>();

			//m_gestureAnalyser = m_playerClass.GestureAnalyser;
			//m_animationAnalyser = m_playerClass.AnimationAnalyser;
			//m_gestureAnalyser.m_playerClass = m_playerClass;
			//m_gestureAnalyser.m_gameWorld = m_playerClass.GameWorld;
		}
		#endregion
	}
}