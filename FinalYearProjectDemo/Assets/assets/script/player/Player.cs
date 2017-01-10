using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class Player : MonoBehaviour {
		public GestureTracker gestureTracker;

		[SerializeField]
		private GameObject m_player;
		[SerializeField][Range(0.0f,0.2f)]
		private float m_moveSpeed;
		private List<ActionBase> m_actions = new List<ActionBase>();

		public float MoveSpeed {
			set { m_moveSpeed = value; }
			get { return m_moveSpeed; }
		}

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void FixedUpdate() {
			foreach (ActionBase action in m_actions) {
				bool completed = action.Update ();
				if (!completed) {
					m_actions.Remove (action);
				}
			}
		}

		// Update is called once per frame
		void Update () {
		}

		public void AddAction (ActionBase action) {
			m_actions.Add (action);
			action.Start ();
		}
	}
}