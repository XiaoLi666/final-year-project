using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class PathNodeGenerator : MonoBehaviour {
		// Public
		public GameObject[] m_pathNodes;
		public PathNodeGenerator.PATHNODE_TYPE[] m_pathNodeTypes;
		public GameMap m_map;
		public Vector3 m_startLocation;
		public Vector3 m_defaultDirection;
		public float m_generateDistance;
		public bool m_useDefaultPath = false;
		public enum PATHNODE_TYPE {
			PATHNODE_end,
			PATHNODE_MoveDown,
			PATHNODE_MoveLeft,
			PATHNODE_MoveRight,
			PATHNODE_MoveUp,
			PATHNODE_Normal,
			PATHNODE_RotateLeft,
			PATHNODE_RotateRight,
			PATHNODE_Seaweed,
			PATHNODE_Speedup,
			PATHNODE_Start
		};

		// Private:
		private Dictionary<PATHNODE_TYPE, GameObject> m_pathNodeDict = new Dictionary<PATHNODE_TYPE, GameObject> ();

		void Start () {
			m_defaultDirection.Normalize ();

			// Invalid match of path node and path node type
			if (m_pathNodes.Length != m_pathNodeTypes.Length) {
				print ("Invalid array size for m_pathNodes and m_pathNodeTypes");
			} else {
				for (int i = 0; i < m_pathNodes.Length; ++i) {
					m_pathNodeDict.Add (m_pathNodeTypes[i], m_pathNodes[i]);
				}
			}

			// Create path
			Create ();
		}

		void Update () {
			return;
		}

		// Path generate function
		private PathNodeGenerator.PATHNODE_TYPE[] Generate() {
			if (m_useDefaultPath) {
				return m_map.m_pathNodeTypes;
			}
			return null;
		}

		private void GetRotationAngle(ref float rotation_angle, PATHNODE_TYPE node_type) {
			const float rotation_threshold = 90.0F;
			switch (node_type) {
			case PATHNODE_TYPE.PATHNODE_RotateRight:
				rotation_angle += rotation_threshold;
				break;
			case PATHNODE_TYPE.PATHNODE_RotateLeft:
				rotation_angle -= rotation_threshold;
				break;
			}
		}
			
		// Function to create the map
		public void Create() {
			PATHNODE_TYPE[] path_node_list = Generate ();
			GameObject prefab_to_refer = null;
			Vector3 cur_location = m_startLocation;
			Vector3 new_location = cur_location;
			Vector3 current_direction = m_defaultDirection;
			float rotation_angle = 0.0F;
			Vector3 rotation_vector = new Vector3 (0.0F, rotation_angle, 0.0F);

			foreach (PATHNODE_TYPE node_type in path_node_list) {
				// Calculate path generation direction
				current_direction = Quaternion.Euler(0.0F, rotation_angle, 0.0F) * m_defaultDirection;

				// Calculate the location for the path node to generate
				new_location = cur_location + m_generateDistance * current_direction;
				cur_location = new_location;

				// Instantiate a new path node
				prefab_to_refer = m_pathNodeDict[node_type];
				prefab_to_refer = Instantiate (prefab_to_refer, cur_location, prefab_to_refer.transform.rotation) as GameObject;

				// Update orientation of each path node
				rotation_vector.y = rotation_angle;
				prefab_to_refer.transform.Rotate(rotation_vector);

				// Get rotation angle for next path node
				GetRotationAngle (ref rotation_angle, node_type);
			}
		}
	}
}