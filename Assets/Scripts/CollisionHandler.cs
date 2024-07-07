using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public float timeToNextLevel = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionEnabled = true;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Invoke("LoadNexLevel", timeToNextLevel);

        if (Input.GetKeyDown(KeyCode.C))
            isCollisionEnabled = !isCollisionEnabled;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Friendly hitted");
                break;
            case "Finish":
                //Debug.Log("Finish hitted");
                FinishSequence();
                break;
            case "Fuel":
                //Debug.Log("Fuel hitted");
                break;
            default:
                //Debug.Log("Some else hitted");
                if (isCollisionEnabled)
                    StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        if(!isTransitioning)
        {
            audioSource.PlayOneShot(crashAudio);
            successParticle.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", timeToNextLevel);
        }
    }

    private void FinishSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(successAudio);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNexLevel", timeToNextLevel);
    }

    void ReloadLevel()
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //isTransitioning = false;
    }
    void LoadNexLevel()
    {
        //SceneManager.LoadScene(0);
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        nextLevel = nextLevel < SceneManager.sceneCountInBuildSettings ? nextLevel : 0;
        SceneManager.LoadScene(nextLevel);
        //isTransitioning = false;
        //Debug.Log("isTransitioning = false");
    }
}
