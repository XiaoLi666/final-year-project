using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class PlayerDataCollection {
        #region attributes
        private static PlayerDataCollection m_instance = new PlayerDataCollection();
        private string m_name { get; set; }
        #endregion

        #region custom methods
        private PlayerDataCollection() {
        }

        public static PlayerDataCollection GetInstance() {
            return m_instance;
        }
        #endregion
    }
}