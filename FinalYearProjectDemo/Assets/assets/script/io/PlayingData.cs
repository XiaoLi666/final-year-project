using System;
using System.Text;
using System.Collections.Generic;

namespace GameLogic {
    public class PlayingData {
        #region attributes
        private static PlayingData m_instance = new PlayingData();
		private PlayerData m_playerData;
		public MapData m_mapData;

		// TODO: for testing only
		public int m_missCount;

		// TODO: move it to ServerManager, going to remove this
		private bool m_loggedIn = false;
		public bool LoggedIn {
			set { m_loggedIn = value; }
			get { return m_loggedIn; }
		}

		// Temp attribute, bad coding, need to remove it later !!!!
		private Dictionary<string, int> m_typeIndexMapData;
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

		// It is involved in Gesture Analyser, going to update it
		public void UpdateMissCount(int value) {
			m_missCount++;
			if (m_missCount > 6) {
				m_mapData.Speed = 0.5f;
			}
		}

		// TODO: need to find a method to make the type and id consistent
		public string GetResultBy(string type) {
			return m_playerData.GestureCompletionData[type].ToString() + "/" + m_mapData.MapConfigList[m_typeIndexMapData[type]];
		}

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