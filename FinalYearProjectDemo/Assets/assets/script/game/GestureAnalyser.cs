using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class GestureAnalyser {
#region attributes
        private GestureTracker m_gestureTracker = null;
        private Dictionary<string, CharacterAction> m_tagActionDict = new Dictionary<string, CharacterAction>();
        private string m_cachedTag = "";
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

        // Calling per tick
        public void Analysis(string tag, RaycastHit hit) {
            if (tag != "null") {
                m_cachedTag = tag;
            }

            CharacterAction char_action = GameDictionary.PairedAction[m_gestureTracker.GetIdentifiedGesture()];
            if (char_action == m_tagActionDict[m_cachedTag]) {
                // Did the correct action!
                if (char_action != CharacterAction.Idle)
                    Debug.Log("You just have done a correct action!" + char_action.ToString());
            }
        }
#endregion
    }
}
