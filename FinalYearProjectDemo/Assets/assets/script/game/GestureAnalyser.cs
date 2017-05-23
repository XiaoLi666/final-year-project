using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class GestureAnalyser {
		#region attributes
        private GestureTracker m_gestureTracker = null;
        private Dictionary<string, CharacterAction> m_tagActionDict = new Dictionary<string, CharacterAction>();
		private string m_tag = "PathNodeStart";
		private bool m_isChecked = false;
		private bool m_complete = false;
		private bool m_firstGesture = true;
		private bool m_justEnter = false;

		private Transform seaweed_to_delete = null;

		// Simple and fast way to do tutorial checking, need to polish in the future
		public AnimationAnalyser m_animationAnalyser = null;
		public Player m_playerComponent = null;
		public GameWorld m_gameWorld = null;
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

        public void InitGestureCheck(RaycastHit hit, ref bool tutorial_pause) {
			if (!m_firstGesture) {
				if (m_justEnter && !m_complete) {
					m_gameWorld.ShowHUDMiss();
					// PlayingData.GetInstance().UpdateMissCount(1);
				}
				m_justEnter = false;
				m_complete = false;
			}

			if (m_tagActionDict.ContainsKey(hit.collider.tag)) {
				m_tag = hit.collider.tag;
				m_isChecked = false;
				m_justEnter = true;

				if (GameWorld.m_mode == GameWorld.GAME_MODE.GAME_MODE_tutorial) {
					iTween.Pause();
					m_playerComponent.PauseAction = true;
					tutorial_pause = true;
					m_gameWorld.DisplayTutorial(m_tag);
				}

				if (m_tag == "PathNodeSeaweed") {
					seaweed_to_delete = hit.transform.Find("Seaweed");
				}

				if (m_firstGesture) {
					m_firstGesture = false;
				}
			}
			else { // if hit by the gesture check terminator, stop checking the gesture
				m_isChecked = true;
			}
		}

        // Calling per tick
        public void Analysis(ref bool tutorial_pause) {
            if (m_isChecked == true) {
                return;
            }

            CharacterAction char_action = GameDictionary.PairedAction[m_gestureTracker.GetIdentifiedGesture()];
            if (m_tagActionDict.ContainsKey(m_tag) && char_action == m_tagActionDict[m_tag]) {
				m_isChecked = true;

				m_complete = true;
				m_gameWorld.ShowHUDComplete();

				if (GameWorld.m_mode == GameWorld.GAME_MODE.GAME_MODE_tutorial) {
					tutorial_pause = false;
					iTween.Resume();
					// Resume the animation analyzer by call its analyzer function once
					m_animationAnalyser.Analysis("", tutorial_pause);
					m_playerComponent.PauseAction = false;
					m_gameWorld.UndisplayTutorial();
				} else {
					PlayingData.GetInstance().InsertGestureCompletionDataBy(m_tag, 1);
					Debug.Log(char_action.ToString());
				}

				if (seaweed_to_delete != null) {
					Object.Destroy(seaweed_to_delete.gameObject);
					seaweed_to_delete = null;
				}
			}
        }
	#endregion
    }
}
