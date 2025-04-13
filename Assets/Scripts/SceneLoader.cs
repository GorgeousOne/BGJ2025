using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	public Button introButton, playButton, exitButton;
	public CanvasGroup intro;

	private void Start() {
		introButton.onClick.AddListener(StartIntro);
		playButton.onClick.AddListener(StartGame);
		exitButton.onClick.AddListener(EndGame);
	}

	void StartIntro()
	{
		intro.gameObject.SetActive(true);
		LeanTween.alphaCanvas(intro, 1f, 0.5f);
	}

	void StartGame()
	{
		LoadSceneByName("GameScene");
	}

	void EndGame()
	{
		Application.Quit();
	}
	void LoadSceneByName(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}


}
