using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f; //the f means that this is a floating point variable.

    //define a bunch of variables related to the player movement
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;

    //awake and fixedupdate are native to unity and every class will have them
    void Awake() {
      //called regardless of whether the script is enabled or not.
      //this method just binds all of the components attached to the player character to variables that we can play with later.
      floorMask = LayerMask.GetMask("Floor");
      anim = GetComponent<Animator>();
      playerRigidBody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate() {
      //called automatically on scripts that updates physics. Rigidbody tells us that the player asset is a "physics" asset.
      float h = Input.GetAxisRaw("Horizontal"); //snaps to full speed instead of ramp-up 
      //by default, Horizontal axis is mapped to A and D.
      float v = Input.GetAxisRaw("Vertical");
      Move(h, v);
      Turning();
      Animating(h, v);
    }

    void Move(float h, float v) {
      //Set takes 3 arguments, which map to axes of movement. we don't want flight, so Y axis is 0.
      movement.Set(h, 0f, v);
      movement = movement.normalized * speed * Time.deltaTime; //deltaTime is time between updates
      playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning() {
      Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit floorHit;
      if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
        Vector3 playerToMouse = floorHit.point - transform.position;
        playerToMouse.y = 0f;
        //quaternion is a class in unity
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        playerRigidBody.MoveRotation(newRotation);
      }
    }
    
    void Animating(float h, float v) {
      //will return true if h or v is not zero. This represents whether or not we pressed a direction which gets us walking.
      bool walking = h != 0f || v != 0f;
      anim.SetBool("IsWalking", walking);
    }
}
