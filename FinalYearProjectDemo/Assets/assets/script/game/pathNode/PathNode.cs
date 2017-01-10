using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class PathNode : MonoBehaviour {
		public GestureTrigger.GESTURE_TRIGGER_TYPE m_gestureTriggerType;
		// TODO: general trigger, not use it right now
		// public List<TriggerBase.TRIGGER_TYPE> m_triggerTypes = new List<TriggerBase.TRIGGER_TYPE>();
		// public List<TriggerBase> m_triggers = new List<TriggerBase>();
		public GameObject m_player;
		public Vector3 m_direction;

		void Awake() {
		}

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			return;
		}
	}
}