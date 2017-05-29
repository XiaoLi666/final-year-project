using UnityEngine;
using GameUI;

namespace GameLogic {
	public class GameWorld : MonoBehaviour {
		#region attributes
		// private
		[SerializeField] private GameObject m_playerGameObject;
		[SerializeField] private GameObject m_HUDManagerGameObject;
        private bool m_start = false;
		private bool m_gameEnd = false;
		private float m_timer;
		private Player m_player;
		private HUDManager m_HUDManager;
		private Transform[] m_waypointsTransformList;

		// public
		static public GAME_MODE m_mode;
		static public float m_speed;
		public float Timer {
			get { return m_timer; }
		}
		public enum GAME_MODE {
			GAME_MODE_tutorial,
			GAME_MODE_custom
		}
		#endregion

		#region override methods
		void Start () {
			m_HUDManager = m_HUDManagerGameObject.GetComponent<HUDManager>();
			m_HUDManager.GameWorld = this;

			m_player = m_playerGameObject.GetComponent<Player> ();
			m_player.GameWorld = this;
			
			ActionBase action_raycasting = ActionFactory.CreateActionRaycasting (m_playerGameObject);
			m_player.AddAction (action_raycasting);

			if (m_mode == GAME_MODE.GAME_MODE_tutorial) {
				ActionBase action_play_tutorial = ActionFactory.CreateActionPlayTutorialMode(m_playerGameObject);
				m_player.AddAction(action_play_tutorial);
			} else if (m_mode == GAME_MODE.GAME_MODE_custom) {
				ActionBase action_play_custom = ActionFactory.CreateActionPlayCustomMode(m_playerGameObject);
				m_player.AddAction(action_play_custom);
			}

			m_waypointsTransformList = gameObject.GetComponent<PathNodeGenerator>().WaypointsTransformList;
			StartCoroutine(m_HUDManager.GetReady());
        }

        private void FixedUpdate() {
            if (m_start == false || m_gameEnd == true) return;
			m_timer += Time.deltaTime;
			m_HUDManager.UpdateTimer(m_timer);
        }
        #endregion

        #region custom methods
        public void Go() {
            iTween.MoveTo(
				m_playerGameObject, 
				iTween.Hash(
					"path", m_waypointsTransformList, 
					"speed", m_player.MoveSpeed * m_speed, 
					"easetype", 
					"linear"));
            m_start = true;
		}

		public void GameEnd() {
			m_gameEnd = true;
			switch (m_mode) {
				case GAME_MODE.GAME_MODE_tutorial:
					m_HUDManager.ShowTutorialModeCompletionPanel();
					break;
				case GAME_MODE.GAME_MODE_custom:
					m_HUDManager.ShowResultPanel();
					break;
			}
		}
		
		public void PauseGame() {
			Time.timeScale = 0;
			iTween.Pause();
		}

		public void ResumeGame() {
			Time.timeScale = 1;
			iTween.Resume();
		}

		public void ShowHUDComplete() {
			m_HUDManager.ShowComplete();
		}

		public void ShowHUDMiss() {
			m_HUDManager.ShowMiss();
		}

		public void DisplayTutorial(string tag) {
			m_HUDManager.DisplayTutorialByTag(tag);
		}

		public void UndisplayTutorial() {
			m_HUDManager.UndisplayTutorial();
		}
		#endregion
	}
}