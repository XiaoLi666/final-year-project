using UnityEngine;
using System.Collections;
using GameLogic;

[CreateAssetMenu()]
public class GameMap : ScriptableObject {
	public PathNodeGenerator.PATHNODE_TYPE[] m_pathNodeTypes;
	// TODO: more specific feature of single path node
}