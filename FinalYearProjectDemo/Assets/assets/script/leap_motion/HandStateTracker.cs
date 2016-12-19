using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
using System.Collections.Generic;

//Tracks the state of a hand.
/*
 * 1. Wrist flexion and extension //flapping up and down : direction.y +1 -1
 * 2. Wrist Supination and Pronation //flip face up and down : palmnormal
 * 3. Wrist Ulnar Deviation and Radial Deviation //bending wrist outward and inward 30deg : direction.x
 * 4. Neutral position //handshake position
 * 5. Closed fist, opened fist //fingers extended (isextended) is 0 and 5
 * Note, should allow multiple actions to be tracked together, not just isolate as one.
 * */

[RequireComponent (typeof(LeapServiceProvider))]

public class HandStateTracker : MonoBehaviour {

	[SerializeField]
	Vector2 _flexion, _extension, _supination, _pronation, _ulnarDeviation, _radialDeviation, _closedFist, _openedFist;

	[SerializeField]	[Range(0, 5)]
	int _fingersOffset;

	[SerializeField]	[Range(0.0f, 1.0f)]
	float _motionRange;
	 
	[SerializeField]	[Range(0.0f, 10.0f)]
	float _dataCaptureInterval;

	private float timeToGo;
	private Hand leftHand, rightHand;
	private LeapServiceProvider m_Provider;

	public List<string> dataList;

	void Start () {
		m_Provider = GetComponent<LeapServiceProvider> ();
		dataList.Add ("\"Hand States\":[\n");
	}

	void FixedUpdate () {
		Frame frame = m_Provider.CurrentFrame;
		leftHand = null; 
		rightHand = null;
	

		foreach (Hand hand in frame.Hands){
			if (hand.IsLeft)
				leftHand = hand;
			
			if (hand.IsRight)
				rightHand = hand;
		}

		//requires either hand to be present
		if (leftHand == null && rightHand == null) {
			return;
		}

		_flexion = _extension = _supination = _pronation = _ulnarDeviation = _radialDeviation = _closedFist = _openedFist = Vector2.zero;

		if (leftHand != null) {
			_openedFist.x = TrackOpenedFist (leftHand, _fingersOffset);
			_closedFist.x = TrackClosedFist (leftHand, _fingersOffset);
			_flexion.x = TrackFlexion (leftHand, _motionRange);
			_extension.x = TrackExtension (leftHand, _motionRange);
			_supination.x = TrackSupination (leftHand, _motionRange);
			_pronation.x = TrackPronation (leftHand, _motionRange);
			_ulnarDeviation.x = TrackUlnarDeviation (leftHand, 0.2f);
			_radialDeviation.x = TrackRadialDeviation (leftHand, 0.2f);
		}

		if (rightHand != null) {
			_openedFist.y = TrackOpenedFist (rightHand, _fingersOffset);
			_closedFist.y = TrackClosedFist (rightHand, _fingersOffset);
			_flexion.y = TrackFlexion (rightHand, _motionRange);
			_extension.y = TrackExtension (rightHand, _motionRange);
			_supination.y = TrackSupination (rightHand, _motionRange);
			_pronation.y = TrackPronation (rightHand, _motionRange);
			_ulnarDeviation.y =  TrackRadialDeviation (rightHand, 0.2f);//right hand is reversed
			_radialDeviation.y = TrackUlnarDeviation (rightHand, 0.2f); //right hand is reversed
		}

		dataList.Add(HandDataCollection(leftHand, rightHand));
	}

	int TrackOpenedFist(Hand hand, int offset) {
		int returnValue = 0;
		int extendCount = 0;
		
		foreach (Finger finger in hand.Fingers){
			if (finger.IsExtended)
				extendCount++;
		}

		if (extendCount >= 5 - offset)
			returnValue = 1;

		return returnValue;
	}

	int TrackClosedFist(Hand hand, int offset) {
		int returnValue = 0;
		int extendCount = 0;

		foreach (Finger finger in hand.Fingers){
			if (finger.IsExtended)
				extendCount++;
		}

		if(extendCount <= 0 + offset)
			returnValue = 1;
		
		return returnValue;
	}

	int TrackFlexion(Hand hand, float range) {
		int returnValue = 0;

		if (hand.Direction.y > 0 && hand.Direction.y > range)
			returnValue = 1;

		return returnValue;
	}

	int TrackExtension(Hand hand, float range) {
		int returnValue = 0;

		if (hand.Direction.y < 0 && hand.Direction.y < -range)
			returnValue = 1;

		return returnValue;
	}

	int TrackPronation(Hand hand, float range) {
		int returnValue = 0;

		if (hand.PalmNormal.y < 0 && hand.PalmNormal.y < -range)
			returnValue = 1;

		return returnValue;
	}

	int TrackSupination(Hand hand, float range) {
		int returnValue = 0;

		if (hand.PalmNormal.y > 0 && hand.PalmNormal.y > (range))
			returnValue = 1;

		return returnValue;
	}

	int TrackUlnarDeviation(Hand hand, float range) {
		int returnValue = 0;

		if (hand.Direction.x < 0 && hand.Direction.x < (range * -1)) {
			
			Finger middleFinger = null;
			foreach (Finger finger in hand.Fingers) {
				if (finger.Type == Finger.FingerType.TYPE_MIDDLE) {
					middleFinger = finger;
					break;
				}
			}

			if (middleFinger == null) {
				return returnValue;
			}

			//float absTipnWristDifference = Mathf.Sqrt (Mathf.Pow (middleFinger.TipPosition.x, 2) - Mathf.Pow (hand.Arm.WristPosition.x, 2));
			float absWristnElbowDifference = Mathf.Sqrt (Mathf.Pow (hand.Arm.ElbowPosition.x, 2) - Mathf.Pow (hand.Arm.WristPosition.x, 2));

			if (/*absTipnWristDifference >= 0.01f &&*/ absWristnElbowDifference <= 0.15f)
				returnValue = 1;
		}
		return returnValue;
	}

	int TrackRadialDeviation(Hand hand, float range) {
		int returnValue = 0;

		if (hand.Direction.x > 0 && hand.Direction.x > (range)) {
			
			Finger middleFinger = null;
			foreach (Finger finger in hand.Fingers) {
				if (finger.Type == Finger.FingerType.TYPE_MIDDLE) {
					middleFinger = finger;
					break;
				}
			}

			if (middleFinger == null) {
				return returnValue;
			}

			float absWristnElbowDifference = Mathf.Sqrt (Mathf.Pow (hand.Arm.ElbowPosition.x, 2) - Mathf.Pow (hand.Arm.WristPosition.x, 2));

			if (/*absTipnWristDifference >= 0.01f &&*/ absWristnElbowDifference <= 0.15f) //sacrificed due to limited range of motion
				returnValue = 1;
		}
		return returnValue;
	}

	public Vector2 GetFlexion() {
		return _flexion;
	}

	public Vector2 GetExtension() {
		return _extension;
	}

	public Vector2 GetSupination() {
		return _supination;
	}

	public Vector2 GetPronation() {
		return _pronation;
	}

	public Vector2 GetUlnarDeviation() {
		return _ulnarDeviation;
	}

	public Vector2 GetRadialDeviation() {
		return _radialDeviation;
	}

	public Vector2 GetClosedFist() {
		return _closedFist;
	}

	public Vector2 GetOpenedFist() {
		return _openedFist;
	}

	public bool HasHands() {
		if (rightHand == null || leftHand == null)
			return false;
		else
			return true;
	}

	string HandDataCollection(Hand handL, Hand handR) {

		if (Time.fixedTime >= timeToGo) {
			timeToGo = Time.fixedTime + _dataCaptureInterval;

			string str = "";
			if (dataList.Count > 1)
				str += ",\n";
			str = "{" +
				            "\"Time\": \"" + Time.fixedTime + " s\", \n" +
				            "\"Hands\": [\n";
			if (handL != null) {
				str += "{\"Hand\":\"Left\", \n";
				str += "\"Basis\":\"" + handL.Basis + "\", \n";
				str += "\"Direction\":\"" + handL.Direction + "\", \n";
				str += "\"Fingers\":\"" + handL.Fingers.Count + "\", \n";
				str += "\"GrabAngle\":\"" + handL.GrabAngle + "\", \n";
				str += "\"GrabStrength\":\"" + handL.GrabStrength + "\", \n";
				str += "\"PalmNormal\":\"" + handL.PalmNormal + "\", \n";
				str += "\"PalmPosition\":\"" + handL.PalmPosition + "\", \n";
				str += "\"PalmVelocity\":\"" + handL.PalmVelocity + "\", \n";
				str += "\"PalmWidth\":\"" + handL.PalmWidth + "\", \n";
				str += "\"PinchDistance\":\"" + handL.PinchDistance + "\", \n";
				str += "\"PinchStrength\":\"" + handL.PinchStrength + "\", \n";
				str += "\"TimeVisible\":\"" + handL.TimeVisible + "\", \n";
				str += "\"WristPosition\":\"" + handL.WristPosition + "\"\n} \n";
			}

			if (handL != null && handR != null)
				str += ",";

			if (handR != null) {
				str += "{\"Hand\":\"Right\", \n";
				str += "\"Basis\":\"" + handR.Basis + "\", \n";
				str += "\"Direction\":\"" + handR.Direction + "\", \n";
				str += "\"Fingers\":\"" + handR.Fingers.Count + "\", \n";
				str += "\"GrabAngle\":\"" + handR.GrabAngle + "\", \n";
				str += "\"GrabStrength\":\"" + handR.GrabStrength + "\", \n";
				str += "\"PalmNormal\":\"" + handR.PalmNormal + "\", \n";
				str += "\"PalmPosition\":\"" + handR.PalmPosition + "\", \n";
				str += "\"PalmVelocity\":\"" + handR.PalmVelocity + "\", \n";
				str += "\"PalmWidth\":\"" + handR.PalmWidth + "\", \n";
				str += "\"PinchDistance\":\"" + handR.PinchDistance + "\", \n";
				str += "\"PinchStrength\":\"" + handR.PinchStrength + "\", \n";
				str += "\"TimeVisible\":\"" + handR.TimeVisible + "\", \n";
				str += "\"WristPosition\":\"" + handR.WristPosition + "\"\n} \n";
			}

			str += "]\n}";
			//Debug.Log (str);
			return str;
		}

		return null;
	}
}