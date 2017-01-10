using UnityEditor;
using UnityEngine;
using System.Collections;
using GameLogic;

[CustomEditor(typeof(GameMap))]
public class GameMapEditor : Editor {
	private GameMap m_gameMap;

	private void OnEnable() {
		m_gameMap = target as GameMap;
	}

	public override void OnInspectorGUI() {
		base.DrawDefaultInspector();

		EditorGUILayout.BeginHorizontal("Box");
		GUILayout.Label ("Game Map Editor");
		if (GUILayout.Button("Edit")) {
			MapEditorWindow.ShowWindow (m_gameMap);
		}
		EditorGUILayout.EndHorizontal ();
	}
}