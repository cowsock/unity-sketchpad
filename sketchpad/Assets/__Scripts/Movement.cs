using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    // public member variables will be available in the inspector for quick modification (even while running the game)
    // This is nice for making quick design adjustments, but potentially messy if a stable value has been found. 
    public float speed;

    Vector3 inputDirection; // the direction we're headed, according to user input. 
	
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    // As such, it is a good place to poll for user input, to get the freshest possible reading. 
	void Update () {
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // this sets inputDirection with a value from -1 to 1 in the x and z dimensions according to user input, and 0 in the y direction.

        float dt = Time.deltaTime; // time since last update
        Vector3 pos = transform.position; // current position by accessing this GameObject's transform
        // we can't directly edit the x/y/z floats of our transform, so we have to assign to this temp Vector3 and then copy 
        // the whole thing over to transform.position
        pos.x += inputDirection.x * dt * speed;
        pos.z += inputDirection.z * dt * speed;
        transform.position = pos;
    }
}
