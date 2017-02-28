using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class PlayingData {
        #region attributes
        private static PlayingData m_instance = new PlayingData();
		private Dictionary<string, int> m_gesturesCompletionData = new Dictionary<string, int>();

		public Dictionary<string, int> GestureCompletionData {
			get { return m_gesturesCompletionData; }
		}
		#endregion

		#region custom methods
		private PlayingData() {
			m_gesturesCompletionData.Add("PathNodeMoveLeft"		, 0);
			m_gesturesCompletionData.Add("PathNodeMoveRight"	, 0);
			m_gesturesCompletionData.Add("PathNodeMoveUp"		, 0);
			m_gesturesCompletionData.Add("PathNodeMoveDown"		, 0);
			m_gesturesCompletionData.Add("PathNodeRotateLeft"	, 0);
			m_gesturesCompletionData.Add("PathNodeRotateRight"	, 0);
			m_gesturesCompletionData.Add("PathNodeSeaweed"		, 0);
			m_gesturesCompletionData.Add("PathNodeSpeedup"		, 0);
		}

        public static PlayingData GetInstance() {
            return m_instance;
        }

		// TODO:
		public override string ToString() {
			return DateTime.Now.ToString() + Environment.NewLine;
		}
        #endregion
    }
}