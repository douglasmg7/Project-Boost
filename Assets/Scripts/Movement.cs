using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoostParticle;
    [SerializeField] ParticleSystem rightBoostParticle;
    [SerializeField] ParticleSystem leftBoostParticle;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        // Thrust.
        if (Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();
    }

    private void StartThrusting()
    {
        //Debug.Log("Boost on");
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        //audioSource.Play();
        if (!mainBoostParticle.isPlaying)
        {
            mainBoostParticle.Play();
        }
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoostParticle.Stop();
    }

    void ProcessRotation()
    {
        // Turn.
        if (Input.GetKey(KeyCode.A))
            RotateRight();
        else if (Input.GetKey(KeyCode.D))
            RotateLeft();
        else
            StopRotate();
    }

    private void RotateRight()
    {
        ApplyRotation(rotationThrust);
        if (!rightBoostParticle.isPlaying)
        {
            rightBoostParticle.Play();
        }
        leftBoostParticle.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationThrust);
        if (!leftBoostParticle.isPlaying)
        {
            leftBoostParticle.Play();
        }
        rightBoostParticle.Stop();
    }

    private void StopRotate()
    {
        rightBoostParticle.Stop();
        leftBoostParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        
        rb.freezeRotation = true;   // So we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;

        if (rotationThisFrame > 0)
        {
            Debug.Log("Turn left");
        }
        else
        {
            Debug.Log("Turn right");
        }
    }
}
