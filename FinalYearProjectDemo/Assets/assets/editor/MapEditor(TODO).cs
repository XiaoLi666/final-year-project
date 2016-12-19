using UnityEditor;
using UnityEngine;
using System.Collections;
using GameLogic;

// TODO: may change the name to map modifier
public class GameMapEditor : EditorWindow {
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;

	[MenuItem ("Window/Map Editor")]
	public static void ShowWindow () {
		EditorWindow.GetWindow (typeof(GameMapEditor));
	}

	void OnEnable() {
		Debug.Log ("OnEnable()");
	}

	void OnGUI() {
		GUILayout.BeginVertical ();
		GUILayout.Label ("Game Map Editor", EditorStyles.boldLabel);

		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();
	}
}
