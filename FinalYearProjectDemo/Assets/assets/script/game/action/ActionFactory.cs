using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class ActionFactory {
		static public ActionBase CreateActionMoveForward(GameObject owner, ActionBase.ACTION_CONDITION action_condition, float move_speed) {
			ActionMoveForward new_action = new ActionMoveForward (action_condition, owner, move_speed);
			return new_action;
		}

		static public ActionBase CreateActionCollisionDetection(GameObject owner, ActionBase.ACTION_CONDITION action_condition) {
			ActionCollisionDetection new_action = new ActionCollisionDetection ();
			return null;
		}

		// static public ActionBase CreateActionCollisionDetection(GameObject owner, ActionBase.ACTION_CONDITION) {
		// }
			
		/*
		// TODO: going to remove this function
		static public ActionBase CreateAction(ActionBase.ACTION_TYPE action_type, ActionBase.ACTION_CONDITION action_condition, GameObject player) {
			ActionBase new_action = null;
			switch (action_type) {
			case ActionBase.ACTION_TYPE.ACTION_collisionDetection:
				new_action = new ActionCollisionDetection ();
				break;
			case ActionBase.ACTION_TYPE.ACTION_jump:
				new_action = new ActionJump ();
				break;
			case ActionBase.ACTION_TYPE.ACTION_moveForward:
				new_action = new ActionMoveForward ();
				break;
			case ActionBase.ACTION_TYPE.ACTION_moveLeft:
				new_action = new ActionMoveLeft ();
				break;
				// TODO: going to create more types of actions here:
			}

			if (new_action != null) {
				new_action.Owner = player;
				new_action.ActionType = action_type;
				new_action.ActionCondition = action_condition;
			}

			return new_action;
		}
		*/
	}
}