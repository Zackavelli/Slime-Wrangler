using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Collider : MonoBehaviour
{

    DrawManager dm;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DrawManager>();


        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.tag == "Line" && Input.GetMouseButton(0) && dm.Line_broken == false)
        {
            Debug.Log("Line Break! Collided with:" + collision.gameObject + " at " + this.transform.position);
            dm.Line_Break();
            Instantiate(dm.Line_break_ps, this.transform.position, this.transform.rotation);
        }

        if (collision.tag == "Slime" && Input.GetMouseButton(0) && dm.Line_broken == false)
        {
            Debug.Log("Line Break! Slime!");
            dm.Line_Break();
            Instantiate(dm.Line_break_ps, this.transform.position, this.transform.rotation);
        }

        if (collision.tag == "Slime_Food" && Input.GetMouseButton(0) && dm.Line_broken == false)
        {
            Debug.Log("Line Break! Collided with:" + collision.gameObject + " at " + this.transform.position);
            dm.Line_Break();
            Instantiate(dm.Line_break_ps, this.transform.position, this.transform.rotation);
        }
    }
}
