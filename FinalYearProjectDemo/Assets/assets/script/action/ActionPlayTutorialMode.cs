using UnityEngine;

namespace GameLogic {
	public class ActionPlayTutorialMode : ActionPlayModeBase {
		#region attributes
		private bool m_tutorialPause = false;
		private string m_cachedTagForAnimationAnalysis = "";
		#endregion

		#region override methods
		public ActionPlayTutorialMode(GameObject owner) : base(owner) {
			m_actionType = ACTION_TYPE.ACTION_playTutorialMode;
		}

		public override bool Update(bool pause = false) {
			// For gesture once per component
			if (m_playerClass.AllowAnalyzeGesture) {
				Pause();
				m_gestureAnalyser.InitGestureCheck();
				m_playerClass.AllowAnalyzeGesture = false;
			}
			m_gestureAnalyser.Analysis();

			if (m_gestureAnalyser.IsChecked) {
				iTween.Resume();
				m_tutorialPause = false;
				m_playerClass.PauseAction = false;
				PostGestureLogic();
			}

			// For animation once per component
			if (m_playerClass.AllowAnalyzeAnimation) {
				if (m_tutorialPause /*set in the Pause()*/) {
					m_cachedTagForAnimationAnalysis = m_playerClass.RaycastingTag;
					return true; // this logic needs to be polished
				}
				else if (m_cachedTagForAnimationAnalysis != "") {
					m_playerClass.RaycastingTag = m_cachedTagForAnimationAnalysis;
					m_cachedTagForAnimationAnalysis = "";
				}

				m_animationAnalyser.Analysis(m_playerClass.RaycastingTag);
				m_playerClass.AllowAnalyzeAnimation = false;

				CollideBy(m_playerClass.RaycastingTag);
			}

			return true;
		}
		#endregion

		#region custom methods
		private void Pause() {
			iTween.Pause();
			m_playerClass.PauseAction = true; // this will make all player's actions paused
			m_playerClass.GameWorld.DisplayTutorial(m_playerClass.RaycastingTag);
			m_tutorialPause = true;
		}
		#endregion
	}
}
