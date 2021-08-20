using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
       switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is the launching point. You can start now.");
                break;
            case "Finish":
                NextLevel();
                Debug.Log("Level Completed!");
                break;
            case "Fuel":
                Debug.Log("Refuled!!!");
                break;
            default:
                ReloadLevel();
                Debug.Log("Your ship crashed!");
                break;
        } 
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
