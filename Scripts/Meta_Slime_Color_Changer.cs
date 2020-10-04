using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta_Slime_Color_Changer : MonoBehaviour
{

    public Color new_clr;
    float red;
    float green;
    float blue;


    public SpriteRenderer sr; 


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("switch_clr", 0.5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void switch_clr()
    {
        //Debug.Log("switch");
        red = Random.Range(0f, 1f);
        green = Random.Range(0f, 1f);
        blue = Random.Range(0f, 1f);
        //yield return new WaitForSeconds(0.25f);

        new_clr = new Color(red, green, blue, 1);
        sr.color = new_clr;
    }
}
