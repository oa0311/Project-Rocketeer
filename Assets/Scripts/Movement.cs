using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETER - for turning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float mainRotation = 100f;
    [SerializeField] AudioClip rocketBoost;
    
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

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigid.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(rocketBoost);
            }
            
        }
        else 
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(mainRotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-mainRotation);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigid.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigid.freezeRotation = false; // unfreezing rotation so t he physics system can take over
    }
}