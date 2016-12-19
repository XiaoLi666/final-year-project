using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class TriggerFactory {
		static public TriggerBase CreateTrigger(TriggerBase.TRIGGER_TYPE trigger_type, GameObject player) {
			TriggerBase new_trigger = null;
			switch (trigger_type) {
			case TriggerBase.TRIGGER_TYPE.TRIGGER_moveLeft:
				new_trigger = new TriggerMoveLeft (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_moveRight:
				new_trigger = new TriggerMoveRight (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_moveUp:
				new_trigger = new TriggerMoveUp (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_moveDown:
				new_trigger = new TriggerMoveDown (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_moveforward:
				new_trigger = new TriggerMoveForward (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_rotateRight:
				new_trigger = new TriggerRotateRight (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_rotateLeft:
				new_trigger = new TriggerRotateLeft (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_gameStart:
				new_trigger = new TriggerStartGame (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_gameEnd:
				new_trigger = new TriggerGameEnd (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_speedup:
				new_trigger = new TriggerSpeedup (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_eatSeaweed:
				new_trigger = new TriggerEatSeaweed (trigger_type);
				break;
			case TriggerBase.TRIGGER_TYPE.TRIGGER_none:
				return null;
			}

			if (new_trigger != null) {
				new_trigger.User = player;
			}

			return new_trigger;
		}
	}
}