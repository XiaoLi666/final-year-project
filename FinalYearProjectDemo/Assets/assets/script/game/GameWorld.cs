using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UI;

namespace GameLogic {
	public class GameWorld : MonoBehaviour {
		#region attributes
		[SerializeField] private GameObject m_playerGameObject;
		[SerializeField] private GameObject m_HUDManagerGameObject;
        private bool m_start = false;
		private bool m_gameEnd = false;
		private float m_timer;
		private Player m_player;
		private HUDManager m_HUDManager;
		private Transform[] m_waypointsTransformList;

		public float Timer { get { return m_timer; } }
		#endregion

		#region override methods
		void Start () {
			m_HUDManager = m_HUDManagerGameObject.GetComponent<HUDManager>();
			m_HUDManager.GameWorld = this;
			m_player = m_playerGameObject.GetComponent<Player> ();
			m_player.GameWorld = this;
			ActionBase action_raycasting = ActionFactory.CreateActionRaycasting (m_playerGameObject);
			m_player.AddAction (action_raycasting);
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
					"speed", m_player.MoveSpeed, 
					"easetype", 
					"linear"));
            m_start = true;
		}

		public void GameEnd() {
			m_gameEnd = true;
			m_HUDManager.ShowResultPanel();
		}
		
		public void PauseGame() {
			Time.timeScale = 0;
			iTween.Pause();
		}

		public void ResumeGame() {
			Time.timeScale = 1;
			iTween.Resume();
		}
		#endregion
	}
}