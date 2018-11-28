using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {

    public GameObject player;
	public float speed = 25.0f;
	public float gravity = -9.8f;
    //private Inventory inventory;
    private CharacterController _charController;
    private AudioSource footstep_source;
    public AudioClip footsteps;

	// Use this for initialization
	void Start () {
        footstep_source = player.AddComponent<AudioSource>();
		_charController = GetComponent<CharacterController> ();
        //inventory = GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude(movement, speed);
		movement.y = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);
		_charController.Move(movement);
        if (deltaX == 0f && deltaZ == 0f)
        {
            footstep_source.Stop();
        }
        if (deltaX != 0f || deltaZ != 0f)
        {
            if (!footstep_source.isPlaying)
            {
                footstep_source.PlayOneShot(footsteps, 1f);
            }
        }
    }
}
