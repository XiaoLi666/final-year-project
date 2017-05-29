using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameLogic {
    // [Serializable]
    public class PlayerData {
		#region attributes
		// private
		private Dictionary<string, int> m_gesturesCompletionData = new Dictionary<string, int>();
		private List<GestureData> m_gestureData = new List<GestureData>();

		// public
		public class GestureData : MonoBehaviour {
			public string m_name;
			public string m_time;
			public bool m_completed;
		}
		public Dictionary<string, int> GestureCompletionData { get { return m_gesturesCompletionData; } }
		public List<GestureData> GestureDataList { get { return m_gestureData; } }
		
		#endregion

		#region custom methods
		public PlayerData() {
			m_gesturesCompletionData.Add("PathNodeMoveLeft"		, 0);
			m_gesturesCompletionData.Add("PathNodeMoveRight"	, 0);
			m_gesturesCompletionData.Add("PathNodeMoveUp"		, 0);
			m_gesturesCompletionData.Add("PathNodeMoveDown"		, 0);
			m_gesturesCompletionData.Add("PathNodeRotateLeft"	, 0);
			m_gesturesCompletionData.Add("PathNodeRotateRight"	, 0);
			m_gesturesCompletionData.Add("PathNodeSeaweed"		, 0);
			m_gesturesCompletionData.Add("PathNodeSpeedup"		, 0);
		}

		public void AddGestureDataBy(string name, bool is_completed) {
			GestureData g_data = new GestureData();
			g_data.m_name = name;
			g_data.m_time = DateTime.Now.ToString();
			g_data.m_completed = is_completed;
			m_gestureData.Add(g_data);
			if (is_completed) {
				m_gesturesCompletionData[name] += 1;
			}
		}

		public string GetJsonString() {
			string data_string = "";
			foreach (GestureData g_data in m_gestureData) {
				data_string += JsonUtility.ToJson(g_data).ToString();
				data_string += ",\n";
			}
			return data_string;
		}
		#endregion
	}
}
