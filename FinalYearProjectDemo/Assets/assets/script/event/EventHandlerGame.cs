using UnityEngine;

using GameUI;
using GameServer;

namespace GameEvent {
	public class EventHandlerGame : MonoBehaviour {
		#region attribute
		[SerializeField] private GameObject m_hudManager = null;
		private HUDManager m_hudManagerClass;
		#endregion

		#region override methods
		void Start() {
			m_hudManagerClass = m_hudManager.GetComponent<HUDManager>();
		}
		#endregion

		#region custom methods
		public void EventUploadData(string data) {
			StartCoroutine(ServerManager.GetInstance().UploadData(data));
		}
		public void EventUploadDataCallback(string result) {

		}
		#endregion
	}
}