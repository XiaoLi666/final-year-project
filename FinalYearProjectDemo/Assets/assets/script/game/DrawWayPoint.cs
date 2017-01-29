using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class DrawWayPoint : MonoBehaviour {
		void OnDrawGizmos(){
			Gizmos.color=Color.blue;
			Gizmos.DrawWireSphere(transform.position,.25f);
		}
	}
}