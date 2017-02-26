using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class GestureAnalyser {
        #region attributes
        private GestureTracker m_gestureTracker = null;
        private Dictionary<string, CharacterAction> m_tagActionDict = new Dictionary<string, CharacterAction>();
        private string m_tag = "PathNodeStart";
        private bool m_isCompleted = false;
        #endregion

        #region custom methods
        public GestureAnalyser(GestureTracker gesture_tracker) {
            m_gestureTracker = gesture_tracker;
            m_tagActionDict.Add("PathNodeNormal"        , CharacterAction.Idle);
            m_tagActionDict.Add("PathNodeMoveUp"        , CharacterAction.SwimUp);
            m_tagActionDict.Add("PathNodeMoveDown"      , CharacterAction.SwimDown);
            m_tagActionDict.Add("PathNodeMoveLeft"      , CharacterAction.SwimLeft);
            m_tagActionDict.Add("PathNodeMoveRight"     , CharacterAction.SwimRight);
            m_tagActionDict.Add("PathNodeRotateLeft"    , CharacterAction.RotateLeft);
            m_tagActionDict.Add("PathNodeRotateRight"   , CharacterAction.RotateRight);
            m_tagActionDict.Add("PathNodeSeaweed"       , CharacterAction.Eat);
            m_tagActionDict.Add("PathNodeSpeedup"       , CharacterAction.SwimForward);
            m_tagActionDict.Add("PathNodeStart"         , CharacterAction.Idle);
            m_tagActionDict.Add("PathNodeEnd"           , CharacterAction.Idle);
        }

        public void InitGestureCheck(RaycastHit hit) {
            if (m_tagActionDict.ContainsKey(hit.collider.tag)) {
                m_tag = hit.collider.tag;
                m_isCompleted = false;
            } else { // if hit by the gesture check terminator, stop checking the gesture
                m_isCompleted = true;
            }
        }

        // Calling per tick
        public void Analysis() {
            if (m_isCompleted == true) {
                return;
            }

            CharacterAction char_action = GameDictionary.PairedAction[m_gestureTracker.GetIdentifiedGesture()];
            if (char_action == m_tagActionDict[m_tag]) {
                m_isCompleted = true;

                if (char_action != CharacterAction.Idle) {
                    Debug.Log(char_action.ToString());
                }
            }
        }
        #endregion
    }
}
