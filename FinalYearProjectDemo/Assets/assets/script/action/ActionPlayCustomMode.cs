using UnityEngine;

namespace GameLogic {
	class ActionPlayCustomMode : ActionPlayModeBase {
		#region override methods
		public ActionPlayCustomMode(GameObject owner) : base(owner) {
			m_actionType = ACTION_TYPE.ACTION_playCustomMode;
		}

		public override bool Update(bool pause = false) {
			// For gesture once per component
			if (m_playerClass.AllowAnalyzeGesture) {
				m_gestureAnalyser.InitGestureCheck();
				m_playerClass.AllowAnalyzeGesture = false;
			}
			m_gestureAnalyser.Analysis();

			if (m_gestureAnalyser.IsChecked) {
				PostGestureLogic();
			}

			// For animation once per component
			if (m_playerClass.AllowAnalyzeAnimation) {
				m_animationAnalyser.Analysis(m_playerClass.RaycastingTag);
				m_playerClass.AllowAnalyzeAnimation = false;
				CollideBy(m_playerClass.RaycastingTag);
			}
			
			return true;
		}
		#endregion
	}
}
