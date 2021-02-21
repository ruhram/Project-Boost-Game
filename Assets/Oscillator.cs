using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector = new Vector3(10f, 10f, 10f);
    [SerializeField][Range(0,1)]float MovementFactor;
    [SerializeField] float period = 1f;
    Vector3 StartingPos;
    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time * period;

        const float tau = Mathf.PI * 2f; //about 6.28
        float rawSinWave = Mathf.Sin(cycle * tau);
        MovementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = MovementVector * MovementFactor;
        transform.position = StartingPos + offset;
    }
}
