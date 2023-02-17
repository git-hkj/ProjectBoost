using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //To process the quitting of application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit Game command has been initiated");
            Application.Quit();
        }
    }
 
}
