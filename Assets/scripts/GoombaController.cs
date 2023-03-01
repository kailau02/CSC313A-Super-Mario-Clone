using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{

    private bool dead = false;
    private bool walkRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walkRight) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1,0);
        }
    }

    public void onSquash() {
        if (!dead) {
            dead = true;
            Destroy(gameObject);
        }
    }
}
