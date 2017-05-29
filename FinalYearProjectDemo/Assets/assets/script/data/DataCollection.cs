using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GameLogic {
    public class DataCollection {
        #region attributes
        private static DataCollection m_instance = new DataCollection();
        private string m_MapDatapath = Application.persistentDataPath + "/map_data_collection.dat";
        private string m_playerDataPath = "play_data.txt";
        private MapData m_mapData = null;

		public MapData MapData {
            get { return m_mapData; }
            set { m_mapData = value; }
        }
        #endregion

        #region custom methods
        private DataCollection() {
			LoadMapData();
		}

        public static DataCollection GetInstance() {
            return m_instance;
        }

        public void SaveMapData(MapData map_data) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(m_MapDatapath);
            bf.Serialize(file, map_data);
            file.Close();
            MapData = map_data;
        }
        
        public void LoadMapData() {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(m_MapDatapath)) {
                FileStream file = File.Open(m_MapDatapath, FileMode.Open);
                m_mapData = (MapData)bf.Deserialize(file);
                file.Close();
            } else {
                Debug.Log("Load map data failed, file does not exist!");
            }
        }

		public void SavePlayerData() {
			//BinaryFormatter bf = new BinaryFormatter();
			//FileStream file = File.Create(m_playerDataPath);
			//bf.Serialize(file, m_playerData);
			//file.Close();
			File.AppendAllText(m_playerDataPath, PlayingData.GetInstance().ToString());
		}

		public string GetPlayerData() {
			// data for each gesture (including successful and failed)
			string player_data = "";
			player_data += PlayingData.GetInstance().PlayerData.GetJsonString();
			player_data += PlayingData.GetInstance().GetResultJsonString();

			GameObject _leapServiceProvider = GameObject.Find("LeapMotionServiceProvider");

			_leapServiceProvider.GetComponent<HandStateTracker>().dataList.Add("]\n");
			_leapServiceProvider.GetComponent<GestureTracker>().dataList.Add("]");
			for (int i = 0; i < _leapServiceProvider.GetComponent<HandStateTracker>().dataList.Count; i++) {
				player_data += _leapServiceProvider.GetComponent<HandStateTracker>().dataList[i];
			}
			player_data += ",";
			for (int i = 0; i < _leapServiceProvider.GetComponent<GestureTracker>().dataList.Count; i++) {
				player_data += _leapServiceProvider.GetComponent<GestureTracker>().dataList[i];
			}
			player_data += "}";

			return player_data;
		}
        #endregion
    }
}