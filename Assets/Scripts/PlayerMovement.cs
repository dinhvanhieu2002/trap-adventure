 using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpaleGround;

    private float dirX = 0f;
    private bool isJumping = false;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioClip jumpSound;

    private MovementState state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //get code from prefabPlayer
        int playerCharacter = PlayerPrefs.GetInt("PlayerCharacter");
        HandlePlayerAppearence(playerCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded())
            {
                //jumpSoundEffect.Play();
                SoundManager.instance.PlaySound(jumpSound);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
            } else if(isJumping)
            {
                SoundManager.instance.PlaySound(jumpSound);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = false;
            }

        }

        updateAnimationState();
    }

    void updateAnimationState()
    {
        MovementState state;
        if(dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        } else if(dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        } else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        } else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpaleGround);
    }


    public void HandlePlayerAppearence(int playerCode)
    {
        AnimatorOverrideController overrideController = anim.runtimeAnimatorController as AnimatorOverrideController;

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;
        }
        AnimationClip animationClipDeath = Resources.Load<AnimationClip>("Animations/Players/Player_Death");
        overrideController["Player_Death"] = animationClipDeath;

        // Assign the new override controller to the Animator
        switch (playerCode)
        {
            case 0:
                overrideController.runtimeAnimatorController = Resources.Load<AnimatorController>("Animations/Players/VirtualGuy/Player");
                overrideController["Player_Falling"] = Resources.Load<AnimationClip>("Animations/Players/VirtualGuy/Player_Falling");
                overrideController["Player_Running"] = Resources.Load<AnimationClip>("Animations/Players/VirtualGuy/Player_Running");
                overrideController["Player_Idle"] = Resources.Load<AnimationClip>("Animations/Players/VirtualGuy/Player_Idle");
                overrideController["Player_Jumping"] = Resources.Load<AnimationClip>("Animations/Players/VirtualGuy/Player_Jumping");
                overrideController["Player_Hit"] = Resources.Load<AnimationClip>("Animations/Players/VirtualGuy/Player_Hit");
                break;
            case 1:
                overrideController.runtimeAnimatorController = Resources.Load<AnimatorController>("Animations/Players/NinjaFrog/Player");
                overrideController["Player_Falling"] = Resources.Load<AnimationClip>("Animations/Players/NinjaFrog/Player_Falling");
                overrideController["Player_Running"] = Resources.Load<AnimationClip>("Animations/Players/NinjaFrog/Player_Running");
                overrideController["Player_Idle"] = Resources.Load<AnimationClip>("Animations/Players/NinjaFrog/Player_Idle");
                overrideController["Player_Jumping"] = Resources.Load<AnimationClip>("Animations/Players/NinjaFrog/Player_Jumping");
                overrideController["Player_Hit"] = Resources.Load<AnimationClip>("Animations/Players/NinjaFrog/Player_Hit");
                break;
            case 2:
                overrideController.runtimeAnimatorController = Resources.Load<AnimatorController>("Animations/Players/PinkMan/Player");
                overrideController["Player_Falling"] = Resources.Load<AnimationClip>("Animations/Players/PinkMan/Player_Falling");
                overrideController["Player_Running"] = Resources.Load<AnimationClip>("Animations/Players/PinkMan/Player_Running");
                overrideController["Player_Idle"] = Resources.Load<AnimationClip>("Animations/Players/PinkMan/Player_Idle");
                overrideController["Player_Jumping"] = Resources.Load<AnimationClip>("Animations/Players/PinkMan/Player_Jumping");
                overrideController["Player_Hit"] = Resources.Load<AnimationClip>("Animations/Players/PinkMan/Player_Hit");
                break;
            case 3:
                overrideController.runtimeAnimatorController = Resources.Load<AnimatorController>("Animations/Players/MaskDude/Player");
                overrideController["Player_Falling"] = Resources.Load<AnimationClip>("Animations/Players/MaskDude/Player_Falling");
                overrideController["Player_Running"] = Resources.Load<AnimationClip>("Animations/Players/MaskDude/Player_Running");
                overrideController["Player_Idle"] = Resources.Load<AnimationClip>("Animations/Players/MaskDude/Player_Idle");
                overrideController["Player_Jumping"] = Resources.Load<AnimationClip>("Animations/Players/MaskDude/Player_Jumping");
                overrideController["Player_Hit"] = Resources.Load<AnimationClip>("Animations/Players/MaskDude/Player_Hit");
                break;
        }

        anim.runtimeAnimatorController = overrideController;

    }
}
