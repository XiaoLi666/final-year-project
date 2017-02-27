using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CustomGameMapConfig : ScriptableObject {
    public List<int> m_configList;
    public int m_pathNodeLimitation;
}