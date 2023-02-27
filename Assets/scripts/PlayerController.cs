using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private Rigidbody2D rb2d;

    public float jumpForce = 10;
    public float movementSpeed = 10;
    private bool isSm = true;
    private bool onGround = true;
    private int blocksOn = 0;
    private bool isMoving = false;
    MarioAnimation currAnim = MarioAnimation.stand_sm;

    private string collidingPipe = null;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        setIsSmall(true);
    }

    void Update()
    {
        handleUserInput();
        updateAnimation();
        checkPipeTransport();
    }

    void checkPipeTransport() {
        if (collidingPipe == null) {
            return;
        }
        if (collidingPipe.Equals("Entrance1") && Input.GetAxis("Vertical") < -0.1f) {
            moveTo("Destination1");
        }
        else if (collidingPipe.Equals("Entrance2") && Input.GetAxis("Horizontal") > 0.1f) {
            moveTo("Destination2");
        }
    }

    void moveTo(string destName) {
        transform.position = GameObject.Find(destName).GetComponent<Transform>().position;
        if (destName == "Destination1") {
            Camera.main.GetComponent<CameraController>().setWorld(CameraController.WORLD_1_UNDERGROUND);
        }
        else if (destName == "Destination2") {
            Camera.main.GetComponent<CameraController>().setWorld(CameraController.WORLD_1);
        }
        collidingPipe = null;
    }

    void handleUserInput()
    {
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rb2d.velocity.y);

        isMoving = Mathf.Abs(rb2d.velocity.x) > 0.01f;

        checkJump();
    }

    void checkJump() {
        if (Input.GetButtonDown("Jump") && blocksOn > 0) {
            onGround = false;
            Debug.Log(blocksOn);
            rb2d.AddForce(new Vector2(0, jumpForce));
            string soundSrc = isSm ? "jump-sm" : "jump-reg";
            GameObject.Find(soundSrc).GetComponent<AudioSource>().Play();
        }
    }
    
    void setIsSmall(bool isSm)
    {
        this.isSm = isSm;
        if (isSm) {
            boxCollider2D.offset = new Vector2(0f, 0.25f);
            boxCollider2D.size = new Vector2(0.5f, 0.5f);
        } else
        {
            boxCollider2D.offset = new Vector2(0f, 0.5f);
            boxCollider2D.size = new Vector2(0.5f, 1f);
        }
    }

    void updateAnimation()
    {
        if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (isSm)
        {
            if (blocksOn == 0)
            {
                playAnimation(MarioAnimation.jump_sm, "Mario_Jump_sm");
            }
            else if (isMoving)
            {
                playAnimation(MarioAnimation.walk_sm, "Mario_Walk_sm");
            }
            else
            {
                playAnimation(MarioAnimation.stand_sm, "Mario_Stand_sm");
            }
        }
        else {
            if (blocksOn == 0)
            {
                playAnimation(MarioAnimation.jump, "Mario_Jump");
            }
            else if (isMoving)
            {
                playAnimation(MarioAnimation.walk, "Mario_Walk");
            }
            else
            {
                playAnimation(MarioAnimation.stand, "Mario_Stand");
            }
        }
    }

    void playAnimation(MarioAnimation anim, string anim_name) {
        if (currAnim != anim) {
            animator.Play(anim_name);
            currAnim = anim;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ground") || collision.tag.Equals("brick") || collision.tag.Equals("questionblock")) {
            onGround = true;
        }
        else if (collision.tag.Equals("Entrance1")) {
            collidingPipe = "Entrance1";
        }
        else if (collision.tag.Equals("Entrance2")) {
            collidingPipe = "Entrance2";
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Ground") || other.tag.Equals("brick") || other.tag.Equals("questionblock")) {
            onGround = true;
            blocksOn++;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Entrance1")) {
            collidingPipe = null;
        }
        else if (other.tag.Equals("Ground") || other.tag.Equals("brick") || other.tag.Equals("questionblock")) {
            onGround = false;
            blocksOn--;
        }
    }

    enum MarioAnimation
    {
        stand_sm,
        walk_sm,
        jump_sm,
        stand,
        walk,
        jump
    }
}
