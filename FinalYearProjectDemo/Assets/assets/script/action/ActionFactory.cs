using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionFactory {
		#region static methods for creating actions
		static public ActionBase CreateActionRaycasting(GameObject owner) {
			return new ActionRaycasting (owner);
		}
		static public ActionBase CreateActionRotate(GameObject owner, float angle, ActionRotate.ROTATE_DIRECTION direction) {
			return new ActionRotate (owner, angle, direction);
		}
		static public ActionBase CreateActionEatSeaweed(GameObject owner, GameObject seaweed) {
			return new ActionEatSeaweed(owner, seaweed);
		}
		static public ActionBase CreateActionPlayTutorialMode(GameObject owner) {
			return new ActionPlayTutorialMode(owner);
		}
		static public ActionBase CreateActionPlayCustomMode(GameObject owner) {
			return new ActionPlayCustomMode(owner);
		}
		#endregion
	}
}