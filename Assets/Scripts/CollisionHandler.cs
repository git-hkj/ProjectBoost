using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Parameters for tuning in the inspector
    [SerializeField] float levelDelay = 1.0f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    //Cache variables
    AudioSource audioSource;

    //State variables
    private bool isTransitioning    =  false;
    private bool collisionDisabled   = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Welcome to the game");
    }

    // Update is called once per frame
    void Update()
    {
        EnableCheatCodes();
    }
    
    //To process the collision of Rocket
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
            {
                case "Start":
                    Debug.Log("This is the launch pad");
                    break;
                case "Finish":
                    OnLevelCompletion();
                    break;
                case "Fuel":
                    Debug.Log("Refuelling the tank");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        
    }

    //To activate the crash sequence upon collision
    void StartCrashSequence()
    {
        Debug.Log("You've crashed your rocket !!!");
        crashParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelDelay);
    }
    //To reload the current level upon collision
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //To engage the level success completion
    void OnLevelCompletion()
    {
        Debug.Log("Bravo, you've arrived !");
        successParticles.Play();
        isTransitioning = true;
        audioSource.Stop(); 
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(NextLevel), levelDelay);

    }
    //To load the next level upon completion
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+ 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        { 
            nextSceneIndex= 0;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    //to activate the cheat codes fo debugging
    void EnableCheatCodes()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;   //toggle collision
        }
    }
}
