using UnityEngine;
using System.Collections.Generic;
using GameLogic;

[CreateAssetMenu()]
public class GameMap : ScriptableObject {
	// public PathNodeGenerator.PATHNODE_TYPE[] m_pathNodeTypes;
	public List<PathNodeGenerator.PATHNODE_TYPE> m_pathNodeTypes  = new List<PathNodeGenerator.PATHNODE_TYPE>();
}