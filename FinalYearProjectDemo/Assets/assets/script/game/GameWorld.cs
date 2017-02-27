using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class GameWorld : MonoBehaviour {
		#region attributes
		[SerializeField] private GameObject m_player;
		// [SerializeField] private GameObject m_playerModel;
		private Player component = null;
		private float m_timer;
		#endregion

		#region methods
		void Start () {
			// Initialization for player
			component = m_player.GetComponent<Player> ();
			ActionBase action_raycasting = ActionFactory.CreateActionRaycasting (m_player);
			component.AddAction (action_raycasting);
		}

		private void FixedUpdate() {
			m_timer += Time.deltaTime;
		}

		static public void StartGame() {
			ResumeGame ();
		}

		static public void PauseGame() {
			Time.timeScale = 0;
		}

		static public void ResumeGame() {
			Time.timeScale = 1;
		}
		#endregion
	}
}