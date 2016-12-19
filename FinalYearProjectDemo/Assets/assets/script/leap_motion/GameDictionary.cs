using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
* GameDictionary:
* Pairs user input with character actions.
*/

public class GameDictionary : MonoBehaviour {

	public static Dictionary<Gesture, CharacterAction> PairedAction;
	public static Dictionary<CharacterAction, string> ActionText;

	// Use this for initialization
	void Start () {
		PairedAction = new Dictionary<Gesture, CharacterAction> ();

		PairedAction.Add (Gesture.NoGesture, CharacterAction.Idle);
		PairedAction.Add (Gesture.BothParallel, CharacterAction.Idle);
		//PairedAction.Add (Gesture.LeftUlnar, CharacterAction.RotateLeft);
		//PairedAction.Add (Gesture.RightUlnar, CharacterAction.RotateRight);
		PairedAction.Add (Gesture.LeftUlnarRightRadial, CharacterAction.RotateLeft);
		PairedAction.Add (Gesture.RightUlnarLeftRadial, CharacterAction.RotateRight);
		PairedAction.Add (Gesture.BothFlexion, CharacterAction.SwimUp);
		PairedAction.Add (Gesture.BothExtension, CharacterAction.SwimDown);
		PairedAction.Add (Gesture.LeftFlexionRightExtension, CharacterAction.SwimForward);
		PairedAction.Add (Gesture.RightFlexionLeftExtension, CharacterAction.SwimForward);
		//PairedAction.Add (Gesture.BothSupination, CharacterAction.Idle);
		PairedAction.Add (Gesture.LeftSupinationRightPronation, CharacterAction.SwimLeft);
		PairedAction.Add (Gesture.RightSupinationLeftPronation, CharacterAction.SwimRight);
		PairedAction.Add (Gesture.BothClosedFist, CharacterAction.Eat);
		//PairedAction.Add (Gesture.BothOpenedFist, CharacterAction.Idle);

		ActionText = new Dictionary<CharacterAction, string> ();

		ActionText.Add (CharacterAction.Idle, "Idle");
		ActionText.Add (CharacterAction.RotateLeft, "Rotate Left");
		ActionText.Add (CharacterAction.RotateRight, "Rotate Right");
		ActionText.Add (CharacterAction.SwimUp, "Swim Up");
		ActionText.Add (CharacterAction.SwimDown, "Swim Down");
		ActionText.Add (CharacterAction.SwimForward, "Swim Forward");
		ActionText.Add (CharacterAction.SwimLeft, "Swim Left");
		ActionText.Add (CharacterAction.SwimRight, "Swim Right");
		ActionText.Add (CharacterAction.Eat, "Eating");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Gestures.BothParallel.ToString());
	}
}

public enum Gesture {
	NoGesture,
	BothParallel,
	LeftUlnar,
	RightUlnar,
	LeftUlnarRightRadial,
	RightUlnarLeftRadial,
	BothFlexion,
	BothExtension,
	LeftFlexionRightExtension,
	RightFlexionLeftExtension,
	BothSupination,
	LeftSupinationRightPronation,
	RightSupinationLeftPronation,
	BothClosedFist,
	BothOpenedFist
};

public enum CharacterAction{
	SwimUp,
	SwimDown,
	RotateLeft,
	RotateRight,
	SwimLeft,
	SwimRight,
	SwimForward,
	Eat,
	Idle
};