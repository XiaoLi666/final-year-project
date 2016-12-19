using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class PathNode : MonoBehaviour {
		public TriggerBase.TRIGGER_TYPE m_triggerType = TriggerBase.TRIGGER_TYPE.TRIGGER_none;
		public TriggerBase m_trigger = null;
		public bool m_isTouched;

		private GameObject m_player = null;

		// private m_preConnector;
		// private m_postConnector;

		void Awake() {
			Player player_script = GameObject.Find ("Player").GetComponent<Player> ();
			m_player = player_script.m_player;

			m_trigger = TriggerFactory.CreateTrigger (m_triggerType, m_player);
		}

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			if (!m_isTouched) {
				return;
			}
		}
	}
}