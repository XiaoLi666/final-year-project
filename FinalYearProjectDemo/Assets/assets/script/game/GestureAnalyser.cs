using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class GestureAnalyser {
		#region attributes
		// private
		private GestureTracker m_gestureTracker = null;
        private Dictionary<string, CharacterAction> m_tagActionDict = new Dictionary<string, CharacterAction>();
		private string m_tag = "PathNodeStart";
		private bool m_firstGesture = true;
		private bool m_isChecked = false; // bool variable for all the components
		private bool m_isCompleted = false; // bool variable for only the components above which player needs to do gestures
		private bool m_justEnter = false;

		// public
		public Player m_playerClass = null;
		public GameWorld m_gameWorld = null;
		public bool IsCompleted { get { return m_isCompleted; } }
		public bool IsChecked { get { return m_isChecked;} }
		#endregion

		#region custom methods
		public GestureAnalyser(GestureTracker gesture_tracker) {
            m_gestureTracker = gesture_tracker;
			m_tagActionDict.Add("PathNodeMoveLeft"		, CharacterAction.SwimLeft);
			m_tagActionDict.Add("PathNodeMoveRight"		, CharacterAction.SwimRight);
			m_tagActionDict.Add("PathNodeMoveUp"        , CharacterAction.SwimUp);
            m_tagActionDict.Add("PathNodeMoveDown"      , CharacterAction.SwimDown);
			m_tagActionDict.Add("PathNodeRotateLeft"	, CharacterAction.SwimLeft);	// m_tagActionDict.Add("PathNodeRotateLeft"    , CharacterAction.RotateLeft);
			m_tagActionDict.Add("PathNodeRotateRight"   , CharacterAction.SwimRight);	// m_tagActionDict.Add("PathNodeRotateRight"   , CharacterAction.RotateRight);
			m_tagActionDict.Add("PathNodeSeaweed"       , CharacterAction.Eat);
            m_tagActionDict.Add("PathNodeSpeedup"       , CharacterAction.SwimForward);
        }
		
		public void InitGestureCheck() {
			// Ignore check the previous gesture completion when just to do the first gesture
			if (!m_firstGesture) {
				if (m_justEnter && !m_isCompleted) {
					m_gameWorld.ShowHUDMiss();
					PlayingData.GetInstance().AddGestureDataBy(m_playerClass.PrevRaycastingTag, false);
				}
				m_justEnter = false;
				m_isCompleted = false;
			}
			
			// First and foremost, the tag should be available for checking
			if (m_tagActionDict.ContainsKey(m_playerClass.RaycastingTag)) {
				m_tag = m_playerClass.RaycastingTag;
				m_isChecked = false;
				m_firstGesture = false;
				m_justEnter = true;
			} else { // if the component tag does not exist, ingore this check
				Debug.Log("The component tag does not exist");
				m_isChecked = true;
			}
		}

		public void Analysis() {
			if (m_isChecked == true) {
				return;
			}

			CharacterAction char_action = GameDictionary.PairedAction[m_gestureTracker.GetIdentifiedGesture()];
			if (m_tagActionDict.ContainsKey(m_playerClass.RaycastingTag) && char_action == m_tagActionDict[m_playerClass.RaycastingTag]) {
				m_isChecked = true;
				m_isCompleted = true;
				m_gameWorld.ShowHUDComplete();
				PlayingData.GetInstance().AddGestureDataBy(m_playerClass.RaycastingTag, true);
			}
		}
		#endregion
	}
}
