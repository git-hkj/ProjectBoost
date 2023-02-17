using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition =  transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    //to process an oscillating movement of any object
    void Oscillate()
    {
        if(period <= Mathf.Epsilon) { return;}
        float cycle = Time.time / period; //growing over time
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycle * tau); //return values between -1 & 1
        movementFactor = (rawSineWave + 1.0f) / 2.0f; //recalculated to go from 0 to 1
        Vector3 deltaX = movementVector * movementFactor;
        transform.position = startPosition + deltaX;
    }
}
