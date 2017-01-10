using UnityEngine;
using System.Collections;

namespace GameLogic {
	public class GameWorld : MonoBehaviour {
		[SerializeField]
		private GameObject m_player;

		// Use this for initialization
		void Start () {
			// TODO: by default should only add collision detection action to player
			// then, when the player collide with the start pathnode, then add the move forward action



			/*ActionBase action_move_forward = ActionFactory.CreateActionMoveForward(
				m_player, 
				ActionBase.ACTION_CONDITION.ACTION_permanent, 
				m_player.GetComponent<Player> ().MoveSpeed
			);
			m_player.GetComponent<Player> ().AddAction (action_move_forward);*/
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}