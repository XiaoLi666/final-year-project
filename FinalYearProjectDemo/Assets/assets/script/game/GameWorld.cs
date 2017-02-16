using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class GameWorld : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_player;
		[SerializeField]
		private GameObject m_playerModel; // TODO: must fix this
		private Player component = null;
#endregion

#region methods
		void Start () {
			component = m_player.GetComponent<Player> ();
			ActionBase action_raycasting = ActionFactory.CreateActionRaycasting (m_player);
			component.AddAction (action_raycasting);
		}

		void Update () {
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