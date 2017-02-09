using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionRaycasting : ActionBase {
#region attributes
		private int m_curInstanceId = -1;
		private const float m_maxDistance = 10.0f;
		private Ray m_ray;
		private RaycastHit m_hit;
		private Player component = null;
#endregion

#region override methods
		public override bool Update() {
			m_ray.origin = m_owner.transform.position;
			if (Physics.Raycast(m_ray, out m_hit, m_maxDistance)) {
				if (m_curInstanceId == m_hit.transform.gameObject.GetInstanceID())
					return true;

				// The following code will be executed once for each collider
				switch (m_hit.collider.tag) {
					case "PathNodeRotateLeft":
						HandleRaycastPathNodeRotateLeft();
						break;
					case "PathNodeRotateRight":
						HandleRaycastPathNodeRotateRight();
						break;
					case "PathNodeSpeedup":
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
			// Create the ray facing downwards
			m_ray = new Ray (m_owner.transform.position, Vector3.down);
			component = m_owner.GetComponent<Player> ();
		}

		private void HandleRaycastPathNodeRotateLeft () {
			component.AddAction (ActionFactory.CreateActionRotate (m_owner, 90.0f, ActionRotate.ROTATE_DIRECTION.ROTATE_left));
		}

		private void HandleRaycastPathNodeRotateRight () {
			component.AddAction (ActionFactory.CreateActionRotate (m_owner, 90.0f, ActionRotate.ROTATE_DIRECTION.ROTATE_right));
		}

		private void HandleRaycastPathNodeSeaweed () {
			Transform child_to_delete = m_hit.transform.Find("Seaweed");
			if (child_to_delete != null) {
				iTween.Pause ();
				// TODO: going to track some gesture here
				// TODO: could add the eat seaweed action here and add it to the action list of the player.
				Object.Destroy(child_to_delete.gameObject);
				iTween.Resume ();
			}
		}
#endregion
	}
}