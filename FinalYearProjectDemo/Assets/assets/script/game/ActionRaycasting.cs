using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionRaycasting : ActionBase {
#region attributes
		private int m_curInstanceId = -1;
		private const float m_maxDistance = 10.0f;
		private Ray m_ray;
		private RaycastHit m_hit;
		private Player m_component = null;
		private GestureAnalyser m_gestureAnalyser = null;
#endregion

#region override methods
		public override bool Update() {
			m_ray.origin = m_owner.transform.position;
			if (Physics.Raycast(m_ray, out m_hit, m_maxDistance)) {
				if (m_curInstanceId == m_hit.transform.gameObject.GetInstanceID()) {
					m_gestureAnalyser.Analysis("null", m_hit);
					return true;
				}

				m_gestureAnalyser.Analysis(m_hit.collider.tag, m_hit);

				// The following code will be executed once for each collider
				switch (m_hit.collider.tag) {
					case "PathNodeRotateLeft":
						HandleRaycastPathNodeRotateLeft();
						break;
					case "PathNodeRotateRight":
						HandleRaycastPathNodeRotateRight();
						break;
					case "PathNodeSeaweed":
						HandleRaycastPathNodeSeaweed();
						break;
				}

				m_curInstanceId = m_hit.transform.gameObject.GetInstanceID();
			}
			return true;
		}
#endregion

#region custom methods
		public ActionRaycasting(GameObject owner) : base(owner) {
			m_actionType = ActionBase.ACTION_TYPE.ACTION_collisionDetection;
			m_ray = new Ray(m_owner.transform.position, Vector3.down);
			m_component = m_owner.GetComponent<Player>();
			m_gestureAnalyser = new GestureAnalyser(m_component.Gesture);
		}

		private void HandleRaycastPathNodeRotateLeft () {
			m_component.AddAction (ActionFactory.CreateActionRotate (m_owner, 90.0f, ActionRotate.ROTATE_DIRECTION.ROTATE_left));
		}

		private void HandleRaycastPathNodeRotateRight () {
			m_component.AddAction (ActionFactory.CreateActionRotate (m_owner, 90.0f, ActionRotate.ROTATE_DIRECTION.ROTATE_right));
		}

		private void HandleRaycastPathNodeSeaweed () {
			Transform child_to_delete = m_hit.transform.Find("Seaweed");
			if (child_to_delete != null) {
				iTween.Pause ();
				Object.Destroy(child_to_delete.gameObject);
				iTween.Resume ();
			}
		}
#endregion
	}
}