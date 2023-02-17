using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    //Parameters for tuning in the inspector
    [SerializeField] float upwardThrust      = 10.0f; 
    [SerializeField] float rotationThrust    = 10.0f;
    [SerializeField] AudioClip mainEngineThruster;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem dSideParticles;
    [SerializeField] ParticleSystem aSideParticles;

    //Cache variables
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

    //For processing the inputs
    //Process the thrusting of rocket
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Pressed SPACE - Thrusting");
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
        
    }

    //Process the rotation of the rocket
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Pressed 'A' - Rotate Left");
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Pressed 'D' - ´Rotate Right");
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    //To start thrusting of the rocket
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * upwardThrust * Time.deltaTime);
        if (!audioSource.isPlaying) //play only if it's not already playing it
        {
            audioSource.PlayOneShot(mainEngineThruster);
        }

        //to display the main engine thrusting particles
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    //To stop thrusting of the rocket
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    //To engage the leftward rotate of Rocket
    void RotateLeft()
    {
        ApplyRotation(1.0f);

        //to display the side thrusting particles 
        if (!aSideParticles.isPlaying)
        {
            aSideParticles.Play();
        }
    }
    //To engage the rightward rotate of Rocket
    void RotateRight()
    {
        ApplyRotation(-1.0f);

        //to display the side thrusting particles 
        if (!dSideParticles.isPlaying)
        {
            dSideParticles.Play();
        }
    }
    //To stop rotation of Rocket
    void StopRotation()
    {
        aSideParticles.Stop();
        dSideParticles.Stop();
    }
    //To apply rotation of the frame
    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        rotationThisFrame = rotationThisFrame * rotationThrust;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
