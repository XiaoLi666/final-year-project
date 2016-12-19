using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class PlayerMotion {
		public GameObject m_player = null;
		public float m_moveSpeed = 0.0f;

		public Rigidbody m_rb = null;

		public PlayerMotion(GameObject player, float move_speed) {
			m_player = player;
			m_moveSpeed = move_speed;

			m_rb = m_player.transform.GetChild(0).GetComponent<Rigidbody>();
		}

		public void KeepMovingForward() {
			m_player.transform.position += new Vector3 (m_moveSpeed, 0.0f);

			// TODO: check center alignment
			if (m_rb.transform.position.x != 1) {
			}
		}
	}
}