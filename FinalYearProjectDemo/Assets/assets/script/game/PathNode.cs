using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class PathNode : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_player;
		[SerializeField]
		private Vector3 m_direction;
		private GameObject m_nextPathNode = null;
#endregion

#region custom methods
		public GameObject Player {
			set {
				m_player = value;
			}
			get {
				return m_player;
			}
		}

		public Vector3 Direction {
			set {
				m_direction = value;
			}
			get {
				return m_direction;
			}
		}

		public GameObject NextPathNode {
			set {
				m_nextPathNode = value;
			}
			get {
				return m_nextPathNode;
			}
		}
#endregion

		#region override methods
		void Awake() {
		}

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			// Could add some effects into his 
			return;
		}
#endregion
	}
}