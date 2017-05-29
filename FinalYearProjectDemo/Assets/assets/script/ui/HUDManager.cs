using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using GameLogic;
using GameEvent;

namespace GameUI {
	public class HUDManager : MonoBehaviour {
#region attributes
		// private
		[SerializeField] private Text m_timerLabel;
		[SerializeField] private Text m_countDown;
		[SerializeField] private GameObject m_resultPanel;
		[SerializeField] private GameObject m_pausePanel;
		[SerializeField] private GameObject m_tutorialModeCompletionPanel;
		[SerializeField] private GameObject m_eventHandlerGame;
		[SerializeField] private Text m_complete;
		[SerializeField] private Text m_miss;
		[SerializeField] private List<Text> m_results;
		[SerializeField] private Image m_moveDownTutorial;
		[SerializeField] private Image m_moveUpTutorial;
		[SerializeField] private Image m_moveLeftTutorial;
		[SerializeField] private Image m_moveRightTutorial;
		[SerializeField] private Image m_turnLeftTutorial;
		[SerializeField] private Image m_turnRightTutorial;
		[SerializeField] private Image m_seaweedTutorial;
		[SerializeField] private Image m_speedupTutorial;
		[SerializeField] private Image m_currentDisplayedTutorial;

		private GameWorld m_gameWorldClass;
		private EventHandlerGame m_eventHandlerGameClass;
		private float m_timer;

		// public
		public GameWorld GameWorld {
			set { m_gameWorldClass = value; }
			get { return m_gameWorldClass; }
		}
#endregion

#region override methods
		private void Start() {
			m_eventHandlerGameClass = m_eventHandlerGame.GetComponent<EventHandlerGame>();
			if (GameWorld.m_mode == GameWorld.GAME_MODE.GAME_MODE_tutorial) {
				m_timerLabel.gameObject.SetActive(false);
			}
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				m_pausePanel.SetActive(true);
				m_gameWorldClass.PauseGame();
			}
		}
#endregion

#region custom methods
		public IEnumerator GetReady() {
			m_countDown.text = "3";
			yield return new WaitForSeconds(1.0f);
			m_countDown.text = "2";
			yield return new WaitForSeconds(1.0f);
			m_countDown.text = "1";
			yield return new WaitForSeconds(1.0f);
			m_countDown.text = "GO!";
			yield return new WaitForSeconds(1.0f);
			m_countDown.text = "";
			m_gameWorldClass.Go();
		}

		public void UpdateTimer(float timer) {
			if (GameWorld.m_mode == GameWorld.GAME_MODE.GAME_MODE_tutorial) {
				return;
			}

			m_timer = timer;
			m_timerLabel.text = "Timer: " + m_timer.ToString("0.00") + "s";
		}

		public void ShowResultPanel() {
			m_resultPanel.SetActive(true);
			m_results[0].text = m_timer.ToString("0.00") + "s";
			m_results[1].text = PlayingData.GetInstance().GetResultBy("PathNodeMoveLeft");
			m_results[2].text = PlayingData.GetInstance().GetResultBy("PathNodeMoveRight");
			m_results[3].text = PlayingData.GetInstance().GetResultBy("PathNodeMoveUp");
			m_results[4].text = PlayingData.GetInstance().GetResultBy("PathNodeMoveDown");
			m_results[5].text = PlayingData.GetInstance().GetResultBy("PathNodeRotateLeft");
			m_results[6].text = PlayingData.GetInstance().GetResultBy("PathNodeRotateRight");
			m_results[7].text = PlayingData.GetInstance().GetResultBy("PathNodeSeaweed");
			m_results[8].text = PlayingData.GetInstance().GetResultBy("PathNodeSpeedup");
		}

		public void ShowTutorialModeCompletionPanel() {
			m_tutorialModeCompletionPanel.SetActive(true);
		}

		public void TutorialModeCompletionPanelOKButtonClick(int id) {
			SceneManager.LoadScene(id);
		}

		public void ResultPanelOKButtonClick(int id) {
			string a = DataCollection.GetInstance().GetPlayerData();
			m_eventHandlerGameClass.EventUploadData(a);
			// DataCollection.GetInstance().SavePlayerData();
			DataCollection.GetInstance().SaveMapData(PlayingData.GetInstance().m_mapData);
			SceneManager.LoadScene(id);
		}

		public void PausePanelResumeButtonClick() {
			m_pausePanel.SetActive(false);
			m_gameWorldClass.ResumeGame();
		}

		public void PausePanelQuitButtonClick(int id) {
			m_gameWorldClass.ResumeGame();
			SceneManager.LoadScene(id);
		}

		public void ShowComplete() {
			// m_complete
			m_complete.gameObject.SetActive(true);
			StartCoroutine(ShowCompleteText());
		}

		public void ShowMiss() {
			// m_miss
			m_miss.gameObject.SetActive(true);
			StartCoroutine(ShowMissText());
		}

		private IEnumerator ShowCompleteText() {
			yield return new WaitForSeconds(1.0f);
			m_complete.gameObject.SetActive(false);
		}

		private IEnumerator ShowMissText() {
			yield return new WaitForSeconds(1.0f);
			m_miss.gameObject.SetActive(false);
		}

		public void DisplayTutorialByTag(string tag) {
			UndisplayTutorial();
			switch (tag) {
				case "PathNodeMoveLeft":
					m_moveLeftTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_moveLeftTutorial;
					break;
				case "PathNodeMoveRight":
					m_moveRightTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_moveRightTutorial;
					break;
				case "PathNodeMoveUp":
					m_moveUpTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_moveUpTutorial;
					break;
				case "PathNodeMoveDown":
					m_moveDownTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_moveDownTutorial;
					break;
				case "PathNodeRotateLeft":
					m_turnLeftTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_turnLeftTutorial;
					break;
				case "PathNodeRotateRight":
					m_turnRightTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_turnRightTutorial;
					break;
				case "PathNodeSeaweed":
					m_seaweedTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_seaweedTutorial;
					break;
				case "PathNodeSpeedup":
					m_speedupTutorial.gameObject.SetActive(true);
					m_currentDisplayedTutorial = m_speedupTutorial;
					break;
			}
		}

		public void UndisplayTutorial() {
			if (m_currentDisplayedTutorial != null) {
				m_currentDisplayedTutorial.gameObject.SetActive(false);
			}
		}
#endregion
	}
}