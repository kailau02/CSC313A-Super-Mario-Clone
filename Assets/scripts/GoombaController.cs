using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{

    private bool dead = false;
    private bool walkRight = true;
    private float timeSinceChangeDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(walkRight ? 1 : -1,0);
        }
        timeSinceChangeDirection += Time.deltaTime;
    }

    public void onSquash() {
        if (!dead) {
            dead = true;
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("edge") || other.tag.Equals("step") || other.tag.Equals("pipe")) {
            if (timeSinceChangeDirection > 3f) {
                walkRight = !walkRight;
                timeSinceChangeDirection = 0f;
            }
        }
    }
}
