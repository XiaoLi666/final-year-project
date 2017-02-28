using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GameLogic {
    public class DataCollection {
        #region attributes
        private static DataCollection m_instance = new DataCollection();
        private string m_MapDatapath = Application.persistentDataPath + "/map_data_collection.dat";
        private string m_playerDataPath = Application.persistentDataPath + "/player_data_collection.dat";
        private MapData m_mapData = null;
        private PlayerData m_playerData = null;

        public MapData MapData {
            get { return m_mapData; }
            set { m_mapData = value; }
        }
        public PlayerData PlayerData {
            get { return m_playerData; }
            set { m_playerData = value; }
        }
        #endregion

        #region custom methods
        private DataCollection() {
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
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(m_playerDataPath);
            bf.Serialize(file, m_playerData);
            file.Close();
        }

        public void LoadPlayerData() {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(m_playerDataPath)) {
                FileStream file = File.Open(m_playerDataPath, FileMode.Open);
                m_playerData = (PlayerData)bf.Deserialize(file);
                file.Close();
            }
            else {
                Debug.Log("Load player data failed, file does not exist!");
            }
        }

        #endregion
    }
}