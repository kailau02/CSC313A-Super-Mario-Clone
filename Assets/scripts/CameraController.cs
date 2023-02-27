using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    public static string WORLD_1 = "1";
    public static string WORLD_1_UNDERGROUND = "1_underground"; 
    private string currWorld = WORLD_1;

    public void setWorld(string worldName) {
        currWorld = worldName;
        if (currWorld.Equals(WORLD_1)) {
            GetComponent<Camera>().backgroundColor = new Color(107f/255f, 139f/255f, 1f);
        } else {
            GetComponent<Camera>().backgroundColor = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }

    void SetPosition() {
        float newX = 0;
        if (currWorld.Equals(WORLD_1)) {
            newX = Mathf.Clamp(playerTransform.position.x, -50f, 50f);
            transform.position = new Vector3(newX, 0, -10);
        } else if (currWorld.Equals(WORLD_1_UNDERGROUND)) {
            transform.position = new Vector3(70, 0, -10);
        }
    }

}
