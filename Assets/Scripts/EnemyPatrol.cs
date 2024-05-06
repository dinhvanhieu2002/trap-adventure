
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Meme")]
    [SerializeField] private Transform meme;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;


    [Header("Meme Animator")]
    [SerializeField] private Animator anim;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private void Awake()
    {
        initScale = meme.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (meme.position.x >= leftEdge.position.x)
            MoveInDirection(-1);
            else
            {
                //Change direction
                DirectionChange();

            }
        }
        else
        {
            if (meme.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                //Change direction
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }


    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        // Make meme face direction
        meme.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        // Move in that direction
        meme.position = new Vector3(meme.position.x + Time.deltaTime * _direction * speed, meme.position.y, meme.position.z);
    }
}
