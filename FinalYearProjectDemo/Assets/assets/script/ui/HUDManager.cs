using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameLogic;

namespace UI {
	public class HUDManager : MonoBehaviour {
		#region attributes
		[SerializeField] private Text m_timerLabel;
		[SerializeField] private Text m_countDown;
		[SerializeField] private GameObject m_resultPanel;
		[SerializeField] private GameObject m_pausePanel;
		[SerializeField] private List<Text> m_results;

		private GameWorld m_gameWorld;
		private float m_timer;

		public GameWorld GameWorld {
			get { return m_gameWorld; }
			set { m_gameWorld = value; }
		}
		#endregion

		#region override methods
		private void Update() {
			// TODO: not implement this for the demo 3/1/2017
			if (Input.GetKeyDown(KeyCode.Escape)) {
				m_pausePanel.SetActive(true);
				m_gameWorld.PauseGame();
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
			m_gameWorld.Go();
		}

		public void UpdateTimer(float timer) {
			m_timer = timer;
			m_timerLabel.text = "Timer: " + m_timer.ToString("0.00") + "s";
		}

		public void ShowResultPanel() {
			m_resultPanel.SetActive(true);
			m_results[0].text = m_timer.ToString("0.00") + "s";
			m_results[1].text = PlayingData.GetInstance().GestureCompletionData["PathNodeMoveLeft"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[0];
			m_results[2].text = PlayingData.GetInstance().GestureCompletionData["PathNodeMoveRight"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[1];
			m_results[3].text = PlayingData.GetInstance().GestureCompletionData["PathNodeMoveUp"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[2];
			m_results[4].text = PlayingData.GetInstance().GestureCompletionData["PathNodeMoveDown"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[3];
			m_results[5].text = PlayingData.GetInstance().GestureCompletionData["PathNodeRotateLeft"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[4];
			m_results[6].text = PlayingData.GetInstance().GestureCompletionData["PathNodeRotateRight"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[5];
			m_results[7].text = PlayingData.GetInstance().GestureCompletionData["PathNodeSeaweed"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[6];
			m_results[8].text = PlayingData.GetInstance().GestureCompletionData["PathNodeSpeedup"].ToString() + "/" + DataCollection.GetInstance().MapData.MapConfigList[7];
		}

		public void ResultPanelOKButtonClick(int id) {
			// TODO: going to save the game result
			DataCollection.GetInstance().SavePlayerData();
			SceneManager.LoadScene(id);
		}

		public void PausePanelResumeButtonClick() {
			m_pausePanel.SetActive(false);
			m_gameWorld.ResumeGame();
		}

		public void PausePanelQuitButtonClick(int id) {
			// without saving any data
			m_gameWorld.ResumeGame();
			SceneManager.LoadScene(id);
		}
		#endregion
	}
}