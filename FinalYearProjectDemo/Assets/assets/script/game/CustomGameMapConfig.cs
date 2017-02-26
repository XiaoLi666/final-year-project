using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class CustomGameMapConfig : ScriptableObject {
    public List<int> m_configList;

    void OnEnable() {
        EditorUtility.SetDirty(this);
    }
}
