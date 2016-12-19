using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
using System.Collections.Generic;

/*
 * Tracks gestures 
 * 
 * Sampling period of gesture adujustable.
 * 
 */

[RequireComponent (typeof(HandStateTracker))]

public class GestureTracker : MonoBehaviour {
	[SerializeField]
	Gesture _identifiedGesture;

	[SerializeField] [Range(0.0f, 5.0f)]
	float _samplingPeriod = 1.0f;

	LeapServiceProvider leapServiceProvider;
	HandStateTracker handStateTracker;
	float timeToGo;
	public List<string> dataList;

	void Start () {
		handStateTracker = GetComponent<HandStateTracker> ();
		_identifiedGesture = Gesture.NoGesture; //default

		timeToGo = Time.fixedTime + _samplingPeriod;
		dataList.Add ("\"Gestures\":[\n");
	}

	void FixedUpdate () {

		if (Time.fixedTime >= timeToGo) { //reduce sample rate

			timeToGo = Time.fixedTime + _samplingPeriod;

			_identifiedGesture = Gesture.NoGesture; //default

			if (IdentifyBothClosedFistGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyBothParallelGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyLeftUlnarRightRadialGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyRightUlnarLeftRadialGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyBothFlexionGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyBothExtensionGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyLeftFlexionRightExtensionGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyRightFlexionLeftExtensionGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyLeftSupinationGesture ()) {
				GestureDataCollection ();
				return;
			}
			if (IdentifyRightSupinationGesture ()) {
				GestureDataCollection ();
				return;
			}
//			if (IdentifyBothSupinationGesture ()) {
//				GestureDataCollection ();
//				return;
//			}
		}

	}

	public Gesture GetIdentifiedGesture() {
		return _identifiedGesture;
	}

	public string GetIdentifiedGestureString() {
		return _identifiedGesture.ToString();
	}

	bool IdentifyBothParallelGesture() {
		if (handStateTracker.GetUlnarDeviation ().magnitude == 0 
			&& handStateTracker.GetRadialDeviation().magnitude == 0 
			&& handStateTracker.GetPronation () == Vector2.one 
			&& handStateTracker.GetExtension().magnitude == 0
			&& handStateTracker.GetFlexion().magnitude == 0 
			&& handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.BothParallel;
			return true;
		}
		return false;
	}

	bool IdentifyLeftUlnarGesture() {
		if (handStateTracker.GetUlnarDeviation () == Vector2.right 
			&& handStateTracker.GetRadialDeviation() == Vector2.zero 
			&& handStateTracker.GetPronation () == Vector2.one 
			&& handStateTracker.GetExtension().magnitude == 0
			&& handStateTracker.GetFlexion().magnitude == 0 
			&& handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.LeftUlnar;
			return true;
		}
		return false;
	}

	bool IdentifyRightUlnarGesture() {
		if (handStateTracker.GetUlnarDeviation () == Vector2.up 
			&& handStateTracker.GetRadialDeviation() == Vector2.zero 
			&& handStateTracker.GetPronation () == Vector2.one 
			&& handStateTracker.GetExtension ().magnitude == 0
			&& handStateTracker.GetFlexion ().magnitude == 0 
			&& handStateTracker.GetOpenedFist () == Vector2.one) {
			_identifiedGesture = Gesture.RightUlnar;
			return true;
		}
		return false;
	}

	bool IdentifyLeftUlnarRightRadialGesture() {
		if (handStateTracker.GetUlnarDeviation () == Vector2.right 
			&& handStateTracker.GetRadialDeviation() == Vector2.up 
			&& handStateTracker.GetPronation () == Vector2.one 
			&& handStateTracker.GetExtension ().magnitude == 0
			&& handStateTracker.GetFlexion ().magnitude == 0 
			&& handStateTracker.GetOpenedFist () == Vector2.one) {
			_identifiedGesture = Gesture.LeftUlnarRightRadial;
			return true;	
		}
		return false;
	}

	bool IdentifyRightUlnarLeftRadialGesture() {
		if (handStateTracker.GetUlnarDeviation () == Vector2.up 
			&& handStateTracker.GetRadialDeviation() == Vector2.right 
			&& handStateTracker.GetPronation () == Vector2.one 
			&& handStateTracker.GetExtension ().magnitude == 0
			&& handStateTracker.GetFlexion ().magnitude == 0 
			&& handStateTracker.GetOpenedFist () == Vector2.one) {
			_identifiedGesture = Gesture.RightUlnarLeftRadial;
			return true;	
		}
		return false;
	}

	bool IdentifyBothFlexionGesture() {
		if (/*handStateTracker.GetPronation () == Vector2.one &&*/ handStateTracker.GetFlexion() == Vector2.one
			&& handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.BothFlexion;
			return true;
		}
		return false;
	}

	bool IdentifyBothExtensionGesture() {
		if (/*handStateTracker.GetPronation () == Vector2.one &&*/ handStateTracker.GetExtension() == Vector2.one
			&& handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.BothExtension;
			return true;
		}
		return false;
	}

	bool IdentifyLeftFlexionRightExtensionGesture() {
		if (/*handStateTracker.GetPronation () == Vector2.one &&*/ handStateTracker.GetExtension() == Vector2.up
			&& handStateTracker.GetFlexion()  == Vector2.right /*&& handStateTracker.GetOpenedFist() == Vector2.one*/) {
			_identifiedGesture = Gesture.LeftFlexionRightExtension;
			return true;
		}			
		return false;
	}

	bool IdentifyRightFlexionLeftExtensionGesture() {
		if (/*handStateTracker.GetPronation () == Vector2.one &&*/ handStateTracker.GetExtension() == Vector2.right
			&& handStateTracker.GetFlexion()  == Vector2.up /*&& handStateTracker.GetOpenedFist() == Vector2.one*/) {
			_identifiedGesture = Gesture.RightFlexionLeftExtension;
			return true;
		}	
		return false;
	}

	bool IdentifyBothSupinationGesture() {
		if (handStateTracker.GetSupination () == Vector2.one && handStateTracker.GetExtension().magnitude == 0
			&& handStateTracker.GetFlexion().magnitude == 0 && handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.BothSupination;
			return true;
		}
		return false;
	}

	bool IdentifyLeftSupinationGesture() {
		if (handStateTracker.GetSupination () == Vector2.right && handStateTracker.GetPronation() == Vector2.up /*handStateTracker.GetExtension().magnitude == 0
			&& handStateTracker.GetFlexion().magnitude == 0*/ && handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.LeftSupinationRightPronation;
			return true;
		}
		return false;
	}

	bool IdentifyRightSupinationGesture() {
		if (handStateTracker.GetSupination () == Vector2.up && handStateTracker.GetPronation() == Vector2.right/*handStateTracker.GetExtension().magnitude == 0
			&& handStateTracker.GetFlexion().magnitude == 0*/ && handStateTracker.GetOpenedFist() == Vector2.one) {
			_identifiedGesture = Gesture.RightSupinationLeftPronation;
			return true;
		}
		return false;
	}

	bool IdentifyBothClosedFistGesture() {
		if (handStateTracker.GetClosedFist () == Vector2.one) {
			_identifiedGesture = Gesture.BothClosedFist;
			return true;
		}
		return false;
	}

	bool IdentifyBothOpenedFistGesture() {
		if (handStateTracker.GetOpenedFist () == Vector2.one) {
			_identifiedGesture = Gesture.BothOpenedFist;
			return true;
		}
		return false;
	}

	void GestureDataCollection() {

		string str = "";
		if (dataList.Count > 1)
			str += ",\n";
		
		str = "{\n" +
			"\"Time\": \"" + Time.fixedTime + " s\", \n" +
			"\"Gesture\":\"";
		
		str += _identifiedGesture;
		str += "\"\n}";

		dataList.Add (str);
		//Debug.Log (str);
	}
}