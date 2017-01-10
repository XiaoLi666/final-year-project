using UnityEngine;
using System.Collections;

namespace GameLogic {
	// This is not a generic trigger class
	// Specific class for gesture trigger
	// Gesture trigger is only for triggering the availability of specific action
	public class GestureTrigger {
		public enum GESTURE_TRIGGER_TYPE {
			GESTURE_TRIGGER_doNothing,
			GESTURE_TRIGGER_moveLeft,
			GESTURE_TRIGGER_moveRight,
			GESTURE_TRIGGER_turnLeft,
			GESTURE_TRIGGER_turnRight,
			GESTURE_TRIGGER_jump,
			GESTURE_TRIGGER_creep,
			GESTURE_TRIGGER_gameStart,
			GESTURE_TRIGGER_gameEnd,
			GESTURE_TRIGGER_speedUp,
			GESTURE_TRIGGER_eatSeaweed
		};

		private GameObject m_player;
		private PathNode m_pathNode;
		private GESTURE_TRIGGER_TYPE m_gestureTriggerType;

		public GameObject Player {
			set { m_player = value; }
			get { return m_player; }
		}
		public PathNode PathNode {
			set { m_pathNode = value; }
			get { return m_pathNode; }
		}
		public GESTURE_TRIGGER_TYPE GestureTriggerType {
			set { m_gestureTriggerType = value; }
			get { return m_gestureTriggerType; }
		}


		public GestureTrigger(GameObject player, PathNode path_node) {
			m_player = player;
			m_pathNode = path_node;
		}
	}
}