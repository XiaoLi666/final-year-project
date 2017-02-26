using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameLogic;

namespace UI {
    public class UIManager : MonoBehaviour {
        #region attributes
        // custom map data
        [SerializeField]
        private CustomGameMapConfig m_customConfig; // save data locally (as local database), for demo
        // Views
        [SerializeField]
        private GameObject m_mainView;
        [SerializeField]
        private GameObject m_selectionView;
        [SerializeField]
        private GameObject m_introductonView;
        [SerializeField]
        private GameObject m_customModeView;
        // Input fields
        [SerializeField]
        private List<InputField> m_inputFieldList;
        // Sliders
        [SerializeField]
        private List<Slider> m_sliderList;

        public enum CONFIG_TYPE {
            CONFIG_TYPE_moveLeft = 0,
            CONFIG_TYPE_moveRight = 1,
            CONFIG_TYPE_moveUp = 2,
            CONFIG_TYPE_moveDown = 3,
            CONFIG_TYPE_rotateLeft = 4,
            CONFIG_TYPE_rotateRight = 5,
            CONFIG_TYPE_seaweed = 6,
            CONFIG_TYPE_speedup=  7
        }
        #endregion

        #region override methods
        // Use this for initialization
        void Start() {
            if (m_customConfig.m_configList.Count > 0) {
                for (int i = 0; i < m_customConfig.m_configList.Count; ++i) {
                    SetValueForInputField(i, m_customConfig.m_configList[i].ToString());
                }
            }
        }
        #endregion

        #region custom methods
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


        // UI buttons logic
        // Main view
        public void MainViewStartBtnClick() {
            // enter selection view
            m_mainView.SetActive(false);
            m_selectionView.SetActive(true);
        }
        
        // Selection view
        public void SelectionViewIntroductionBtnClick() {
        }

        public void SelectionViewTutorialModeBtnClick(int id) {
            PathNodeGenerator.m_generationMode = PathNodeGenerator.GENERATION_MODE.GENERATION_manual;
            SceneManager.LoadScene(id);
        }

        public void SelectionViewCustomModeBtnClick() {
            m_selectionView.SetActive(false);
            m_customModeView.SetActive(true);
        }

        // Introduction view
        public void IntroductionViewBackBtnClick() {
        }
        
        // Custom mode view
        public void CustomModeViewStartBtnClick(int id) {
            // Save the current config
            for (int i = 0; i < m_inputFieldList.Count; ++ i) {
                m_customConfig.m_configList[i] = (Convert.ToInt32(m_inputFieldList[i].text));
            }
            PathNodeGenerator.m_generationMode = PathNodeGenerator.GENERATION_MODE.GENERATION_auto;
            SceneManager.LoadScene(id);
        }
        
        public void CustomModeViewBackBtnClick() {
            m_customModeView.SetActive(false);
            m_selectionView.SetActive(true);
        }
        #endregion
    }
}
