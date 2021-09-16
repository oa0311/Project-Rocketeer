using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETER - for turning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float mainRotation = 100f;
    [SerializeField] AudioClip rocketBoost;

    [SerializeField] ParticleSystem centralBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    
    // CACHE - e.g. references for readability or speed
    Rigidbody rigid;
    AudioSource audioSource;
    
    
    // STATE - private instance (member) variables

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    //This is the propulsion method for the rocket
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigid.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(rocketBoost);
            }
            if(!centralBoosterParticles.isPlaying)
            {
                centralBoosterParticles.Play();
            }
            
        }
        else 
        {
            audioSource.Stop();
            centralBoosterParticles.Stop();
        }
    }

    //This is the method to rotate the rocket
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(mainRotation);
            if (!rightBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-mainRotation);
            if (!leftBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Play();
            }
        }
            else 
            {
                rightBoosterParticles.Stop();
                leftBoosterParticles.Stop();
            }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigid.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigid.freezeRotation = false; // unfreezing rotation so t he physics system can take over
    }
}