using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameLogic;

namespace UI {
    public class UIManager : MonoBehaviour {
        #region attributes
        // Views
        [SerializeField] private GameObject m_mainView;
        [SerializeField] private GameObject m_selectionView;
        [SerializeField] private GameObject m_introductonView;
        [SerializeField] private GameObject m_setLimitationView;
        [SerializeField] private GameObject m_customModeView;
        // Input fields
        [SerializeField] private List<InputField> m_inputFieldList;
        // Sliders
        [SerializeField] private List<Slider> m_sliderList;
        // Path Node limitation
        [SerializeField] private InputField m_pathNodeCountLimitInputField;
        [SerializeField] private List<Text> m_maxLimitationText;
        private GameObject m_currentView;
        private int m_pathNodeLimitation;

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
            ShowView(m_mainView);
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
        #endregion

        #region Selection view UI logic
        public void SelectionViewIntroductionBtnClick() {
            ShowView(m_introductonView);
        }
        public void SelectionViewTutorialModeBtnClick(int id) {
            PathNodeGenerator.m_generationMode = PathNodeGenerator.GENERATION_MODE.GENERATION_manual;
            SceneManager.LoadScene(id);
        }
        public void SelectionViewCustomModeBtnClick(int id) {
            PathNodeGenerator.m_generationMode = PathNodeGenerator.GENERATION_MODE.GENERATION_auto;
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
            ShowView(m_customModeView);
        }
        public void SetLimitationViewBackBtnClick() {
            ShowView(m_mainView);
        }
        #endregion

        #region Custom mode view UI logic
        public void CustomModeViewOKBtnClick() {
            MapData map_data = new MapData();
            for (int i = 0; i < m_inputFieldList.Count; ++i) {
                map_data.MapConfigList.Add(Convert.ToInt32(m_inputFieldList[i].text));
            }
            map_data.PathNodeCountLimit = m_pathNodeLimitation;
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
