using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Screen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //load level 1
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
