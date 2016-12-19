using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class PlayerControl {
		public GameObject m_player = null;
		public Rigidbody m_rb = null;

		public PlayerControl(GameObject player) {
			m_player = player;
			m_rb = m_player.transform.GetChild(0).GetComponent<Rigidbody>();
		}

		public void KeyboardControl() {
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				Debug.Log ("up arrow key was pressed");
				//m_player.transform.position += new Vector3 (0.05f, 0.0f);
				m_rb.AddForce (100.0f,0.0f,0.0f, ForceMode.VelocityChange);
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				Debug.Log ("left arrow key was pressed");
				m_rb.AddForce (0.0f,0.0f,3.0f, ForceMode.VelocityChange);
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow)) {
				Debug.Log ("right arrow key was pressed");
				m_rb.AddForce (0.0f,0.0f,-3.0f, ForceMode.VelocityChange);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow)) {
				Debug.Log ("down arrow key was pressed");
				m_player.transform.position += new Vector3 (-0.05f, 0.0f);
			}
		}
	}
}