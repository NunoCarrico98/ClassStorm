using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float Timer { get; private set; }

    private void Awake()
    {
        Timer = 0;
    }

    public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    private void Update()
    {
        Timer += Time.deltaTime;
    }
}
