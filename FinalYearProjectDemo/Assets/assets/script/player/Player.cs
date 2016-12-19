using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class Player : MonoBehaviour {
		// Public access
		public GameObject m_player;
		[Range(0.0f,0.2f)]
		public float m_moveSpeed;
		public GestureTracker gestureTracker;

		// Private access
		private List<ActionBase> m_actions;
		private PlayerMotion m_motion;
		private PlayerControl m_control;

		// Use this for initialization
		void Start () {
			m_motion = new PlayerMotion (m_player, m_moveSpeed);
			m_control = new PlayerControl (m_player);
		}

		// Update is called once per frame
		void FixedUpdate() {
		}

		// Update is called once per frame
		void Update () {
			foreach (ActionBase action in m_actions) {
				bool completed = action.Update ();
				if (!completed) {
					m_actions.Remove (action);
				}
			}

			m_motion.KeepMovingForward ();
			m_control.KeyboardControl ();
		}

		public void AddAction (ActionBase action) {
			m_actions.Add (action);
			action.Start ();
		}
	}
}