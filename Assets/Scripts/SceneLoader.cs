using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class SceneLoader : MonoBehaviour
{
	public void LoadSceneByName(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void LoadSceneByIndex(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
}
