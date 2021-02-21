using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField]float RcsThrust = 100f;
    [SerializeField]float MainThrust = 50f;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Death;

    [SerializeField] ParticleSystem MainEngineParticle;
    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem DeathParticle;
    enum State {Alive, Dying, Transcending}
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive){ return;} //ignore collision
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    print("ok");
                }break;
            case "Finish":
                {
                    StartSuccessSequence();
                }
                break;
            default:
                {
                    StartDeathSequence();
                }
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        Invoke("LoadFirstLevel", 1f);
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        DeathParticle.Play();
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        Invoke("LoadToNextScene", 1f);
        audioSource.PlayOneShot(Success);
        SuccessParticle.Play();
    }

    private void LoadToNextScene()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
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
            ThrustInput();

        }
        else
        {
            audioSource.Stop();
            MainEngineParticle.Stop();
        }
    }

    private void ThrustInput()
    {
        float ThrustThisFrame = MainThrust;
        rigidBody.AddRelativeForce(Vector3.up * ThrustThisFrame * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
        }
        MainEngineParticle.Play();
    }
}
