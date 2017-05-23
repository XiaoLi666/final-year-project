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
		#endregion
	}
}