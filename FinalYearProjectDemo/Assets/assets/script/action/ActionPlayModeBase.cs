using UnityEngine;

namespace GameLogic {
	public class ActionPlayModeBase : ActionBase {
		#region attributes
		protected Player m_playerClass = null;
		protected GestureAnalyser m_gestureAnalyser;
		protected AnimationAnalyser m_animationAnalyser;
		protected Transform m_seaweed;
		#endregion

		#region override methods
		public ActionPlayModeBase(GameObject owner) : base(owner) {
			m_playerClass = owner.GetComponent<Player>();
			m_gestureAnalyser = m_playerClass.GestureAnalyser;
			m_animationAnalyser = m_playerClass.AnimationAnalyser;
		}

		public override bool Update(bool pause = false) {
			return base.Update(pause);
		}
		#endregion

		#region custom methods shared by different play mode
		protected void CollideBy(string tag) {
			switch (tag) {
				case "PathNodeRotateLeft":
					HandleCollisionRotateLeft();
					break;
				case "PathNodeRotateRight":
					HandleCollisionRotateRight();
					break;
				case "PathNodeEnd":
					HandleCollisionEnd();
					break;
				case "PathNodeSeaweed":
					HandleEatSeaweed();
					break;
			}
		}

		private void HandleCollisionRotateLeft() {
			m_playerClass.AddAction(ActionFactory.CreateActionRotate(m_owner, 90.0f, ActionRotate.ROTATE_DIRECTION.ROTATE_left));

		}
		private void HandleCollisionRotateRight() {
			m_playerClass.AddAction(ActionFactory.CreateActionRotate(m_owner, 90.0f, ActionRotate.ROTATE_DIRECTION.ROTATE_right));
		}
		private void HandleCollisionEnd() {
			m_playerClass.GameWorld.GameEnd();
		}
		private void HandleEatSeaweed() {
			m_seaweed = m_playerClass.GetRaycastAction().Hit.transform.Find("Seaweed");
		}
		protected void PostGestureLogic() {
			if (m_seaweed != null) {
				m_playerClass.AddAction(ActionFactory.CreateActionEatSeaweed(m_owner, m_seaweed.gameObject));
			}
		}
		#endregion
	}
}