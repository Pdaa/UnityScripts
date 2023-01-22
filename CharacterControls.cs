using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
 
public class CharacterControls : MonoBehaviour {
 
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 5.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	private bool inventory = false;
	public float mouseSensitivity = 400.0f;
    public float clampAngle = 80.0f;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis 
 
 
	void Awake () {
		Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
	    GetComponent<Rigidbody>().freezeRotation = true;
	    GetComponent<Rigidbody>().useGravity = false;
	}
 
	void FixedUpdate () {


 		if (Input.GetKeyDown(KeyCode.Tab))
            {
                inventory = ! inventory;
            }
            // Hide and lock cursor when right mouse button pressed
            if (inventory == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Unlock and show cursor when right mouse button released
            if (inventory == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

		if (inventory == false)
        {

         float mouseX = Input.GetAxis("Mouse X");
         float mouseY = -Input.GetAxis("Mouse Y");
 
         rotY += mouseX * mouseSensitivity * Time.deltaTime;
         rotX += mouseY * mouseSensitivity * Time.deltaTime;
 
         rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
 
         Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
         transform.rotation = localRotation;



	    if (grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = GetComponent<Rigidbody>().velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
 
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
	        }
	    }
 		}
	    // We apply gravity manually for more tuning control
	    GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravity * GetComponent<Rigidbody>().mass, 0));
 
	    grounded = false;
	}
 
	void OnCollisionStay () {
	    grounded = true;    
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}