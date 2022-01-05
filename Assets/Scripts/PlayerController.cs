using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 40f;
    [SerializeField] private float _jumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private LayerMask _colliders;

    private Transform _groundChecker;
    private const float _groundedRadius = .2f;
    public bool _isGrounded;
    private Rigidbody2D _body;
    private bool _isRight = true;
    private Vector2 _velocity = Vector2.zero;
    [SerializeField]private float _horizontalMove = 0f;
    private bool _isJump = false;


    // Start is called before the first frame update
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _groundChecker = transform;

    }

    	// Update is called once per frame
	private void Update () 
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            _isJump = true;
        }

    }

    // FixedUpdate is called every physics update
    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, _groundedRadius, _colliders, QueryTriggerInteraction.Ignore);
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundChecker.position, _groundedRadius, _colliders);
        // for (int i = 0; i < colliders.Length; i++) 
        // {
        //     if (colliders[i].gameObject != gameObject) 
        //     {
        //         _isGrounded = true;
        //     }
        // }
        // Move our character
		Move(_horizontalMove * Time.fixedDeltaTime);
        _isJump = false;
    }

    private void Move(float move) 
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, _body.velocity.y);
        // And then smoothing it out and applying it to the character
        _body.velocity = Vector2.SmoothDamp(_body.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        if ((move > 0 && !_isRight) || (move < 0 && _isRight))
        {
            // flip the player.
            Flip();
        }

        if (_isJump && _isGrounded) 
        {
            _isGrounded = false;
            _body.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_isRight = !_isRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
