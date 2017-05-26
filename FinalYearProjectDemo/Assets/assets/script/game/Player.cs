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
		private bool m_allowAnalyzeGesture;
		private bool m_allowAnalyzeAnimation;
		private string m_raycastingTag;
		private string m_prevRaycastingTag;
		private GameObject m_seaweedToEat = null;

		public GameObject SeaweedToEat { set; get; }
		public GestureAnalyser GestureAnalyser { get { return m_gestureAnalyser; } }
		public AnimationAnalyser AnimationAnalyser { get { return m_animationAnalyser; } }
		public float MoveSpeed { get { return m_moveSpeed; } }
		public bool PauseAction { set { m_pauseAction = value; } }
		public GameWorld GameWorld { set; get; }
		public bool AllowAnalyzeGesture { set; get; }
		public bool AllowAnalyzeAnimation { set; get; }
		public string RaycastingTag {
			set {
				PrevRaycastingTag = m_raycastingTag;
				m_raycastingTag = value;
				AllowAnalyzeGesture = true;
				AllowAnalyzeAnimation = true;
			}
			get {
				return m_raycastingTag;
			}
		}
		public string PrevRaycastingTag {
			set {
				m_prevRaycastingTag = value;
			}
			get {
				return m_prevRaycastingTag;
			}
		}

		// this is a low performance method (the actual performance may not be low)
		public ActionRaycasting GetRaycastAction() {
			foreach (ActionBase action in m_actions) {
				if (action.ActionType == ActionBase.ACTION_TYPE.ACTION_raycasting) {
					return action as ActionRaycasting;
				}
			}
			return null;
		}
		#endregion

		#region override methods
		private void Awake() {
			m_turtleAnimator = transform.FindChild("Turtle").GetComponent<Animator>();
			m_gestureAnalyser = new GestureAnalyser(m_gestureTracker);
			m_animationAnalyser = new AnimationAnalyser(m_turtleAnimator);

			m_pauseAction = false;
			m_allowAnalyzeGesture = false;
			m_allowAnalyzeAnimation = false;
		}

		private void Start() {
			m_gestureAnalyser.m_playerClass = this;
			m_gestureAnalyser.m_gameWorld = GameWorld;
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