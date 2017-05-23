using UnityEngine;
using System.Collections.Generic;

namespace GameLogic {
	public class Player : MonoBehaviour {
		#region attributes
		[SerializeField][Range(0.0f,5.0f)] private float m_moveSpeed;
		[SerializeField] private GestureTracker m_gestureTracker;

		private Animator m_turtleAnimator;
		private AnimationAnalyser m_animationAnalyser = null;
		private GestureAnalyser m_gestureAnalyser = null;
		private List<ActionBase> m_actions = new List<ActionBase>();
		private GameWorld m_gameWorld;
		private bool m_pauseAction;

		public GestureAnalyser GestureAnalyser { get { return m_gestureAnalyser; } }
		public AnimationAnalyser AnimationAnalyser { get { return m_animationAnalyser; } }
		public GameWorld GameWorld { set { m_gameWorld = value; } get { return m_gameWorld; } }
		public float MoveSpeed { get { return m_moveSpeed; } }
		public bool PauseAction { set { m_pauseAction = value; } }
		#endregion

		#region override methods
		private void Awake() {
			m_turtleAnimator = transform.FindChild("Turtle").GetComponent<Animator>();
			m_gestureAnalyser = new GestureAnalyser(m_gestureTracker);
			m_animationAnalyser = new AnimationAnalyser(m_turtleAnimator);
			m_pauseAction = false;
		}

		void FixedUpdate() {
			for (int i = 0; i < m_actions.Count; ++ i) {
				bool completed = m_actions[i].Update (m_pauseAction);
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