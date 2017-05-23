using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameEvent;

namespace GameServer {
	public class ServerManager {
		#region attributes
		private static ServerManager m_instance = new ServerManager();
		private bool m_connected = false;
		public bool Connected {
			set { m_connected = value; }
			get { return m_connected; }
		}
		#endregion

		#region custom methods
		private ServerManager() {}
		public static ServerManager GetInstance() {
			return m_instance;
		}

		public IEnumerator LoginRequest(string name, string password, EventHandlerUI handler) {
			WWWForm form = new WWWForm();

			form.AddField("authkey"	, ServerDataInfo.AUTH_KEY);
			form.AddField("auth"	, ServerDataInfo.AUTH);
			form.AddField("username", name);
			form.AddField("password", password);
			form.AddField("gameid"	, ServerDataInfo.GAME_ID);

			WWW www = new WWW(ServerDataInfo.URL, form);
			yield return www;

			if (www.error == null) {
				m_connected = true;
				Debug.Log(www.text);
				Dictionary<string, object> json_objects_all = MiniJSON.Json.Deserialize(www.text) as Dictionary<string, object>;

				if (json_objects_all["success"] != null) {
					string result = json_objects_all["success"].ToString(); // error // success

					if (result == "success") {
						string json_data = MiniJSON.Json.Serialize(json_objects_all["data"]);
						Dictionary<string, object> json_objects_data = MiniJSON.Json.Deserialize(json_data) as Dictionary<string, object>;
						PlayerPrefs.SetString("sessionid", json_objects_data["sessionId"].ToString());
						PlayerPrefs.SetString("sessionkey", json_objects_data["sessionKey"].ToString());
					}

					handler.EventUserLoginCallback(result, name);
				}
			} else {
				Debug.Log("Login Error!");
			}
		}

		public IEnumerator UploadData(string data) {
			WWWForm form = new WWWForm();

			form.AddField("authkey"		, ServerDataInfo.AUTH_KEY);
			form.AddField("auth"		, ServerDataInfo.AUTH);
			form.AddField("sessionid"	, PlayerPrefs.GetString("sessionid"));
			form.AddField("sessionkey"	, PlayerPrefs.GetString("sessionkey"));
			form.AddField("gameplayid"	, ServerDataInfo.GAME_ID);
			form.AddField("data"		, data);

			WWW www = new WWW(ServerDataInfo.URL, form);
			yield return www;

			Debug.Log(www.text);
		}
		#endregion
	}
}
