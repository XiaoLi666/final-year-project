using UnityEngine;

using GameUI;
using GameServer;

namespace GameEvent {
	public class EventHandlerUI : MonoBehaviour {
		#region attribute
		[SerializeField] private GameObject m_uiManager = null;
		private UIManager m_uiManagerClass;
		#endregion

		#region override method
		void Start() {
			m_uiManagerClass = m_uiManager.GetComponent<UIManager>();
		}
		#endregion

		#region custom method
		public void EventUserLogin(string name, string password) {
			StartCoroutine(ServerManager.GetInstance().LoginRequest(name, password, this));
		}
		public void EventUserLoginCallback(string result, string name) {
			if (result == "success") {
				m_uiManagerClass.LoginViewOkBtnClickCallback();
			} else if (result == "error") {
				// user name wrong? password ?
			}
		}
		#endregion
	}
}