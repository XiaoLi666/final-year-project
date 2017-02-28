using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameLogic {
	public class GameWorld : MonoBehaviour {
		#region attributes
		[SerializeField] private GameObject m_player;
        [SerializeField] private Text m_timerLabel;
        [SerializeField] private Text m_countDown;
		private Player component;
		private float m_timer;
        private bool m_start = false;
        private Transform[] m_waypointsTransformList;
		#endregion

		#region override methods
		void Start () {
			component = m_player.GetComponent<Player> ();
			ActionBase action_raycasting = ActionFactory.CreateActionRaycasting (m_player);
			component.AddAction (action_raycasting);
            m_waypointsTransformList = gameObject.GetComponent<PathNodeGenerator>().WaypointsTransformList;
            StartCoroutine(GetReady());
        }

        private void FixedUpdate() {
            if (m_start == false) return;
			m_timer += Time.deltaTime;
            m_timerLabel.text = "Timer: " + m_timer.ToString("0.00") + "s";
        }
        #endregion

        #region custom methods
        IEnumerator GetReady() {
            m_countDown.text = "3";
            yield return new WaitForSeconds(1.5f);
            m_countDown.text = "2";
            yield return new WaitForSeconds(1.5f);
            m_countDown.text = "1";
            yield return new WaitForSeconds(1.5f);
            m_countDown.text = "GO!";
            yield return new WaitForSeconds(1.5f);
            m_countDown.text = "";
            Go();
        }

        private void Go() {
            iTween.MoveTo(m_player, iTween.Hash("path", m_waypointsTransformList, "speed", 2.0f, "easetype", "linear"));
            m_start = true;
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