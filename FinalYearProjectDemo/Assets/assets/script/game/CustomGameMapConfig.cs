using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class CustomGameMapConfig : ScriptableObject {
    public List<int> m_configList;
    public int m_pathNodeLimitation;

    void OnEnable() {
        EditorUtility.SetDirty(this);
    }
}
