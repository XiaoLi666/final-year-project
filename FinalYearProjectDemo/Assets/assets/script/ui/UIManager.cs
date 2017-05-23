using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using GameLogic;
using GameEvent;
using GameServer;

namespace GameUI {
    public class UIManager : MonoBehaviour {
		#region attributes
		// Views
		[SerializeField] private GameObject m_startView;
		[SerializeField] private GameObject m_loginView;
		[SerializeField] private GameObject m_registerView;
        [SerializeField] private GameObject m_mainView;
        [SerializeField] private GameObject m_selectionView;
        [SerializeField] private GameObject m_introductonView;
        [SerializeField] private GameObject m_setLimitationView;
        [SerializeField] private GameObject m_customModeView;
		// Event handler
		[SerializeField] private GameObject m_eventHandlerUI;
		// Input fields
		[SerializeField] private InputField m_loginViewNameInputField;
		[SerializeField] private InputField m_loginViewPasswordInputField;
		[SerializeField] private InputField m_registerViewNameInputField;
		[SerializeField] private InputField m_registerViewPasswordInputField;
		[SerializeField] private InputField m_registerViewConfirmPasswordInputField;
        [SerializeField] private List<InputField> m_inputFieldList;
        // Sliders
        [SerializeField] private List<Slider> m_sliderList;
		[SerializeField] private Slider m_gameSpeedSlider;
        // Path Node limitation
        [SerializeField] private InputField m_pathNodeCountLimitInputField;
        [SerializeField] private List<Text> m_maxLimitationText;
		
		private EventHandlerUI m_eventHandlerUIClass;
        private GameObject m_currentView;
        private int m_pathNodeLimitation;
		private float m_gameSpeed;

        public enum CONFIG_TYPE {
            CONFIG_TYPE_moveLeft = 0,
            CONFIG_TYPE_moveRight = 1,
            CONFIG_TYPE_moveUp = 2,
            CONFIG_TYPE_moveDown = 3,
            CONFIG_TYPE_rotateLeft = 4,
            CONFIG_TYPE_rotateRight = 5,
            CONFIG_TYPE_seaweed = 6,
            CONFIG_TYPE_speedup = 7
        }
		#endregion

		#region override methods
        // Use this for initialization
        void Start() {
			m_eventHandlerUIClass = m_eventHandlerUI.GetComponent<EventHandlerUI>();
			ShowView(m_startView);

			// This is the default value for user login (not hardcoding)
			m_loginViewNameInputField.text = ServerDataInfo.DEFAULT_USER_NAME;
			m_loginViewPasswordInputField.text = ServerDataInfo.DEFAULT_PASSWORD;
		}
		#endregion

		#region custom methods
		#region Main view UI logic
        public void MainViewStartBtnClick() {
            ShowView(m_selectionView);
        }
        public void MainViewSettingBtnClick() {
            ShowView(m_setLimitationView);
        }
		public void MainViewBackBtnClick() {
			// TODO: process logout logic here
		}
		#endregion

		#region Start view UI logic
		public void StartViewLoginBtnClick() {
			ShowView(m_loginView);
		}
		public void StartViewRegisterBtnClick() {
			ShowView(m_registerView);
		}
		public void StartViewPlayOfflineBtnClick() {
			ShowView(m_startView);
		}
		#endregion

		#region Login view UI logic
		public void LoginViewOkBtnClick() {
			m_eventHandlerUIClass.EventUserLogin(m_loginViewNameInputField.text, m_loginViewPasswordInputField.text);
		}
		public void LoginViewOkBtnClickCallback() {
			ShowView(m_mainView); // login successfully, show the main view
		}

		public void LoginViewBackBtnClick() {
			// TODO: logic for logout
			ShowView(m_startView);
		}
		#endregion

		#region Register view UI logic
		public void RegisterViewOkBtnClick() {
			// TODO: logic for registration
		}
		public void RegisterViewBackBtnClick() {
			ShowView(m_mainView);
		}
		#endregion

		#region Selection view UI logic
		public void SelectionViewIntroductionBtnClick() {
            ShowView(m_introductonView);
        }
        public void SelectionViewTutorialModeBtnClick(int id) {
            PathNodeGenerator.m_generationMode = PathNodeGenerator.GENERATION_MODE.GENERATION_tutorial;
			GameWorld.m_mode = GameWorld.GAME_MODE.GAME_MODE_tutorial;
			SceneManager.LoadScene(id);
        }
        public void SelectionViewCustomModeBtnClick(int id) {
            PathNodeGenerator.m_generationMode = PathNodeGenerator.GENERATION_MODE.GENERATION_custom;
			GameWorld.m_mode = GameWorld.GAME_MODE.GAME_MODE_custom;
			SceneManager.LoadScene(id);
		}
        public void SelectionViewBackBtnClick() {
            ShowView(m_mainView);
        }
		#endregion

		#region Introduction view UI logic
        public void IntroductionViewBackBtnClick() {
            ShowView(m_selectionView);
        }
		#endregion

		#region Set limitation view UI logic
        public void SetLimitationViewNextBtnClick() {
            // Save the current config
            m_pathNodeLimitation = Convert.ToInt32(m_pathNodeCountLimitInputField.text);
			m_gameSpeed = m_gameSpeedSlider.value;

			ShowView(m_customModeView);
        }
        public void SetLimitationViewBackBtnClick() {
            ShowView(m_mainView);
        }

		public void GameSpeedSliderChanged() {
			GameWorld.m_speed = m_gameSpeedSlider.value;
		}
		#endregion

		#region Custom mode view UI logic
        public void CustomModeViewOKBtnClick() {
            MapData map_data = new MapData();
            for (int i = 0; i < m_inputFieldList.Count; ++i) {
                map_data.MapConfigList.Add(Convert.ToInt32(m_inputFieldList[i].text));
            }
            map_data.PathNodeCountLimit = m_pathNodeLimitation;
			map_data.Speed = m_gameSpeed;

			DataCollection.GetInstance().SaveMapData(map_data);
            ShowView(m_mainView);
        }
        public void CustomModeViewBackBtnClick() {
            ShowView(m_setLimitationView);
        }
		#endregion

		#region Other utility functions
        // Slider value change functions
        public void ConfigSliderValueChangeCheck(int index) {
            SyncInputFieldValueBySliderValue(index);
        }

        // Input field value change functions
        public void InputFieldValueChangeCheck(int index) {
            if (m_inputFieldList[index].text == "")
                m_inputFieldList[index].text = "0";

            SyncSliderValueByInputFieldValue(index);
        }

        public void SetLimitationViewValueChangeCheck() {
            if (m_pathNodeCountLimitInputField.text == "") {
                m_pathNodeCountLimitInputField.text = "0";
            }
        }

        private void SyncInputFieldValueBySliderValue(int index) {
            m_inputFieldList[index].text = m_sliderList[index].value.ToString();
        }

        private void SyncSliderValueByInputFieldValue(int index) {
            m_sliderList[index].value = Convert.ToInt32(m_inputFieldList[index].text);
        }

        private void SetValueForInputField(int index, string value) {
            m_inputFieldList[index].text = value;
            SyncSliderValueByInputFieldValue(index);
        }

        private void ShowView(GameObject view) {
            if (m_currentView == view) return;
            if (m_currentView != null) m_currentView.SetActive(false);
            view.SetActive(true);
            m_currentView = view;

            if (m_currentView == m_setLimitationView) {
                InitSetLimitationView();
            } else if (m_currentView == m_customModeView) {
                InitCustomModeView();
            }
        }

        private void InitSetLimitationView() {
            DataCollection.GetInstance().LoadMapData();
            if (DataCollection.GetInstance().MapData != null) {
                m_pathNodeCountLimitInputField.text = DataCollection.GetInstance().MapData.PathNodeCountLimit.ToString();
				m_gameSpeedSlider.value = DataCollection.GetInstance().MapData.Speed;

			}
        }

        private void InitCustomModeView() {
            DataCollection.GetInstance().LoadMapData();
            MapData map_data = DataCollection.GetInstance().MapData;
            if (map_data != null) {
                if (map_data.MapConfigList.Count > 0) {
                    for (int i = 0; i < map_data.MapConfigList.Count; ++i) {
                        SetValueForInputField(i, map_data.MapConfigList[i].ToString());
                    }
                }
            }
            // Update the display
            for (int i = 0; i < m_maxLimitationText.Count; ++i) {
                m_maxLimitationText[i].text = m_pathNodeLimitation.ToString();
            }
            for (int i = 0; i < m_sliderList.Count; ++i) {
                m_sliderList[i].maxValue = m_pathNodeLimitation;
            }
        }
		#endregion
		#endregion
    }
}
