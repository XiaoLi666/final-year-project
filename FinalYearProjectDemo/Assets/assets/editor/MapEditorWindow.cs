using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;

public class MapEditorWindow : EditorWindow {
	private ReorderableList m_nodeList;
	static private GameMap m_gameMap = null;
	SerializedObject m_gameMapObj = null;
	const int TOP_PADDING = 2;

	static Vector2 m_windowMaxSize = Vector2.one * 300.0f;
	static Vector2 m_windowMinSize = m_windowMaxSize;
	static Rect m_listRect = new Rect (Vector2.zero, m_windowMinSize);

	Vector2 m_scrollPos;
	string t = "Th";

	public static void ShowWindow(GameMap game_map) {
		EditorWindow window = EditorWindow.GetWindow (typeof(MapEditorWindow), true, "Map Editor");
		window.maxSize = m_windowMaxSize;
		window.minSize = m_windowMinSize;
		m_gameMap = game_map;
	}

	private void OnEnable() {
		if (m_gameMap) {
			m_gameMapObj = new SerializedObject (m_gameMap);

			m_nodeList = new ReorderableList (
				m_gameMapObj,
				m_gameMapObj.FindProperty ("m_pathNodeTypes"),
				true, true, true, true
			);

			m_nodeList.drawHeaderCallback = (rect) => EditorGUI.LabelField (rect, "To Name");
			m_nodeList.drawElementCallback = (Rect rect, int index, bool is_active, bool is_focused) => {
				rect.y += TOP_PADDING;
				rect.height = EditorGUIUtility.singleLineHeight;
				EditorGUI.PropertyField(rect, m_nodeList.serializedProperty.GetArrayElementAtIndex(index));
			};
		}
	}

	void OnInspectorUpdate() {
		Repaint ();
	}

	void OnGUI() {
		/*if (m_gameMapObj != null) {
			EditorGUILayout.BeginScrollView (Vector2.zero);
			m_gameMapObj.Update ();
			m_nodeList.DoList (m_listRect);
			m_gameMapObj.ApplyModifiedProperties ();
			EditorGUILayout.EndScrollView ();
		} else {
			// TODO: EditorGUI.HelpBox ();
		}*/

		EditorGUILayout.BeginVertical ();
		m_scrollPos = EditorGUILayout.BeginScrollView (m_scrollPos, GUILayout.Width(200), GUILayout.Height(200));
		GUILayout.Label (t);
		if (m_gameMapObj != null) {
			m_gameMapObj.Update ();
			m_nodeList.DoList (m_listRect);
			m_gameMapObj.ApplyModifiedProperties ();
		}
		EditorGUILayout.EndScrollView ();
		EditorGUILayout.EndVertical ();
	}
}