using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class AnimationAnalyser {
        #region attributes
        private string m_previousAnimation = "idle";
        private Animator m_turtleAnimator;
        private delegate void AnimationDelegate(string tag);
        private Dictionary<string, string> m_tagAnimationDict = new Dictionary<string, string>();
        #endregion

        #region custom methods
        public AnimationAnalyser(Animator turtle_Animator) {
            m_turtleAnimator = turtle_Animator;
            
            m_tagAnimationDict.Add("PathNodeNormal"                     , "idle");
            m_tagAnimationDict.Add("PathNodeMoveUp"                     , "swimUp");
            m_tagAnimationDict.Add("PathNodeMoveDown"                   , "swimDown");
            m_tagAnimationDict.Add("PathNodeMoveLeft"                   , "swimLeft");
            m_tagAnimationDict.Add("PathNodeMoveRight"                  , "swimRight");
            m_tagAnimationDict.Add("PathNodeRotateLeft"                 , "rotateLeft");
            m_tagAnimationDict.Add("PathNodeRotateRight"                , "rotateRight");
            m_tagAnimationDict.Add("PathNodeSeaweed"                    , "idle");
            m_tagAnimationDict.Add("PathNodeSpeedup"                    , "swimForward");
            m_tagAnimationDict.Add("PathNodeMoveUpAnimationChecker"     , "swimDown");
            m_tagAnimationDict.Add("PathNodeMoveDownAnimationChecker"   , "swimUp");
            m_tagAnimationDict.Add("PathNodeMoveLeftAnimationChecker"   , "idle");
            m_tagAnimationDict.Add("PathNodeMoveRightAnimationChecker"  , "idle");
            m_tagAnimationDict.Add("PathNodeRotateLeftAnimationChecker" , "idle");
            m_tagAnimationDict.Add("PathNodeRotateRightAnimationChecker", "idle");
            m_tagAnimationDict.Add("PathNodeSeaweedAnimationChecker"    , "idle");
            m_tagAnimationDict.Add("PathNodeSpeedupAnimationChecker"    , "idle");
            m_tagAnimationDict.Add("PathNodeStart"                      , "idle");
            m_tagAnimationDict.Add("PathNodeEnd"                        , "idle");
        }

        public void Analysis(string tag) {
            if (m_tagAnimationDict.ContainsKey(tag) == false)
                return;

            m_turtleAnimator.SetBool(m_previousAnimation, false);
            m_previousAnimation = m_tagAnimationDict[tag];
            m_turtleAnimator.SetBool(m_previousAnimation, true);
        }
        #endregion
    }
}