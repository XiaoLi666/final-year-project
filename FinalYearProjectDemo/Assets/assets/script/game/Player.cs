using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class Player : MonoBehaviour {
#region attributes
		public GestureTracker gestureTracker;
		[SerializeField][Range(0.0f,0.2f)]
		private float m_moveSpeed;
		private List<ActionBase> m_actions = new List<ActionBase>();
#endregion

#region override methods
		void FixedUpdate() {
			for (int i = 0; i < m_actions.Count; ++ i) {
				bool completed = m_actions[i].Update ();
				if (!completed) {
					m_actions.Remove (m_actions[i]);
				}
			}
		}
#endregion

#region custom methods
		public void AddAction (ActionBase action) {
			if (action == null) {
				Debug.LogError ("Invalid action added in!");
			}

			m_actions.Add (action);
		}
#endregion
	}
}