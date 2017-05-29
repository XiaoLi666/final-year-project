using UnityEngine;

namespace GameLogic {
	public class PathNode : MonoBehaviour {
		#region attributes
		[SerializeField] private GameObject m_player;
		[SerializeField] private Vector3 m_direction;
		private GameObject m_nextPathNode = null;
		#endregion

		#region override methods (can do customization for each path node)
		void Awake() {}
		void Start () {}
		void Update () { return; }
		#endregion
	}
}