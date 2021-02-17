using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField]float RcsThrust = 100f;
    [SerializeField]float MainThrust = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    print("ok");
                }break;
            case "Fuel":
                {
                    print("Fuel");
                }break;
            default:
                {
                    print("Dead");
                }break;
        }
    }
    void Rotate()
    {
        float RotationThisFrame = RcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * RotationThisFrame);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float ThrustThisFrame = MainThrust;
            rigidBody.AddRelativeForce(Vector3.up * ThrustThisFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }
}
