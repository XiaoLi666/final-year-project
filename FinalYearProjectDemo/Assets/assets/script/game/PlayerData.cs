using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GameLogic {
    // [Serializable]
    public class PlayerData {
		#region attributes
		private Dictionary<string, int> m_gesturesCompletionData = new Dictionary<string, int>();
		public Dictionary<string, int> GestureCompletionData {
			get { return m_gesturesCompletionData; }
		}
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
		#endregion
	}
}
