using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;

public class MapEditorWindow : EditorWindow {
	private ReorderableList m_nodeList;
	static private GameMap m_gameMap = null;
	SerializedObject m_gameMapObj = null;
	const int TOP_PADDING = 2;

	static Vector2 m_windowMaxSize = Vector2.one * 700.0f;
	static Vector2 m_windowMinSize = Vector2.one * 700.0f;

	Vector2 m_scrollPos = Vector2.zero;

	// TODO: going to define a lot of things here:

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

			m_nodeList.drawHeaderCallback = (rect) => EditorGUI.LabelField (rect, "Drag and drop to reorder the list");
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
		if (m_nodeList != null) {
			m_scrollPos = GUI.BeginScrollView (new Rect (0, 0, 700, 700), m_scrollPos, new Rect (0, 0, 700, m_nodeList.count * m_nodeList.elementHeight + m_nodeList.footerHeight + m_nodeList.headerHeight));
			if (m_gameMapObj != null) {
				m_gameMapObj.Update ();
				m_nodeList.DoLayoutList ();
				m_gameMapObj.ApplyModifiedProperties ();
			}
			GUI.EndScrollView ();
		}
	}
}