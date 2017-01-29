using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UI {
	public class StartGameOnClick : MonoBehaviour {
		public void LoadLevelById (int id) {
			SceneManager.LoadScene (id);
		}
	}
}
