using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(GameMap))]
public class GameMapEditor : Editor {
	private GameMap m_gameMap;
	private ReorderableList m_nodeList = null;
	private SerializedObject m_gameMapObj = null;
	private const int TOP_PADDING = 2;

	private void OnEnable() {
		m_gameMap = target as GameMap;

		m_gameMapObj = new SerializedObject (m_gameMap);
		m_nodeList = new ReorderableList (m_gameMapObj,m_gameMapObj.FindProperty ("m_pathNodeTypes"),true,true,true,true);

		m_nodeList.drawHeaderCallback = (rect) => EditorGUI.LabelField (rect, "Drag and drop to reorder the path node list");
		m_nodeList.drawElementCallback = (Rect rect, int index, bool is_active, bool is_focused) => {
			rect.y += 2;
			rect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(rect, m_nodeList.serializedProperty.GetArrayElementAtIndex(index));
		};
	}

	void OnInspectorUpdate() {
		Repaint ();
	}

	public override void OnInspectorGUI() {
		// base.DrawDefaultInspector();
		serializedObject.Update();
		m_nodeList.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
		if (GUI.changed) {
			m_nodeList.serializedProperty.serializedObject.ApplyModifiedProperties();
		}
	}
}