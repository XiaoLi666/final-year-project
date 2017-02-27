using System.Collections.Generic;
using System;

namespace GameLogic {
    [Serializable]
    public class MapData {
        private List<int> m_mapConfigList = new List<int>();
        private int m_pathNodeCountLimit;

        public List<int> MapConfigList {
            get {
                return m_mapConfigList;
            }
            set {
                m_mapConfigList = value;
            }
        }

        public int PathNodeCountLimit {
            get {
                return m_pathNodeCountLimit;
            }
            set {
                m_pathNodeCountLimit = value;
            }
        }
    }
}