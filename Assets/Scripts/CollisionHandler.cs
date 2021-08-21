using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delay = 2f;
    [SerializeField]AudioClip crashSFX;
    [SerializeField]AudioClip finishSFX;
    
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }
       
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is the launching point. You can start now.");
                break;
            case "Finish":
                NextLevelSequence();
                Debug.Log("Level Completed!");
                break;
            default:
                CrashSequence();
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

    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        // todo particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSFX);
        // todo add particle effect upon completion
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }

}
