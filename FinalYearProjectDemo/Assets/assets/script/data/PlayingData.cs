using System;
using System.Text;
using System.Collections.Generic;

using UnityEngine;

namespace GameLogic {
    public class PlayingData {
        #region attributes
		// private
        private static PlayingData m_instance = new PlayingData();
		private PlayerData m_playerData;
		// Temp attribute, bad coding, need to remove it later !!!!
		private Dictionary<string, int> m_typeIndexMapData;

		// public
		public MapData m_mapData;
		public PlayerData PlayerData { get { return m_playerData; } }

		private List<GestureCompletenessData> m_gestureCompletenessData = new List<GestureCompletenessData>();
		public class GestureCompletenessData : MonoBehaviour {
			public string m_name;
			public float m_rate;
		}
		public List<GestureCompletenessData> GestureCompletenessDataList { get { return m_gestureCompletenessData; } }

		#endregion

		#region custom methods
		private PlayingData() {
			m_typeIndexMapData = new Dictionary<string, int>();
			m_typeIndexMapData.Add("PathNodeMoveLeft"	,0);
			m_typeIndexMapData.Add("PathNodeMoveRight"	,1);
			m_typeIndexMapData.Add("PathNodeMoveUp"		,2);
			m_typeIndexMapData.Add("PathNodeMoveDown"	,3);
			m_typeIndexMapData.Add("PathNodeRotateLeft"	,4);
			m_typeIndexMapData.Add("PathNodeRotateRight",5);
			m_typeIndexMapData.Add("PathNodeSeaweed"	,6);
			m_typeIndexMapData.Add("PathNodeSpeedup"	,7);

			m_playerData = new PlayerData();
			DataCollection.GetInstance().LoadMapData();
			m_mapData = DataCollection.GetInstance().MapData;
		}

        public static PlayingData GetInstance() {
            return m_instance;
        }
		public void InsertGestureCompletionDataBy(string m_tag, int value) {
			m_playerData.GestureCompletionData[m_tag] += value;
		}
		public void AddGestureDataBy(string name, bool is_completed) {
			m_playerData.AddGestureDataBy(name, is_completed);
		}
		public string GetResultBy(string type) {
			return m_playerData.GestureCompletionData[type].ToString() + "/" + m_mapData.MapConfigList[m_typeIndexMapData[type]];
		}
		public string GetResultJsonString() {
			string json_string = "";
			m_gestureCompletenessData.Clear();

			GestureCompletenessData gcd = new GestureCompletenessData();
			string type = "PathNodeMoveLeft";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeMoveRight";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeMoveUp";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeMoveDown";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeRotateLeft";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeRotateRight";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeSeaweed";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			gcd = new GestureCompletenessData();
			type = "PathNodeSpeedup";
			gcd.m_name = type;
			gcd.m_rate = m_playerData.GestureCompletionData[type] / m_mapData.MapConfigList[m_typeIndexMapData[type]];
			GestureCompletenessDataList.Add(gcd);

			foreach (GestureCompletenessData data in m_gestureCompletenessData) {
				json_string += JsonUtility.ToJson(data).ToString();
				json_string += ",\n";
			}

			return json_string;
		}

		// Based on the old design
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.Append("####################");
			builder.AppendLine();
			builder.Append(DateTime.Now.ToString());
			builder.AppendLine();
			foreach (string key in m_typeIndexMapData.Keys) {
				builder.Append(key);
				builder.Append(": ");
				builder.Append(GetResultBy(key));
				builder.AppendLine();
			}
			builder.Append("####################");
			builder.AppendLine();
			return builder.ToString();
		}
        #endregion
    }
}