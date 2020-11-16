using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public int sceneToLoad;

  public void LoadScene()
  {
    SceneManager.LoadScene(sceneToLoad);
  }

  public void LoadScene(int _scene)
  {
    SceneManager.LoadScene(_scene);
  }
}
