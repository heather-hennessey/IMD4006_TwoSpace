using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public float WalkSpeed;
    public float JumpForce;
    public float TimeToJumpApex;
    public AnimationClip _walk, _jump;
    public Animation _Legs;
    public Camera _cam;
    public Transform Ship;
    public bool mirror;
    public bool _canJump, _canWalk;

    public bool alive;
    public GameObject GM;

    private float camWidth, camHeight;
    private bool _isWalk, _isJump;
    private float rot, _startScale;
    private Rigidbody2D rig;
    private Vector2 _inputAxis;
    private RaycastHit2D _hit;

    private PlayerController controller;
    private Vector2 velocity;
    private float gravity;
    private float JumpVelocity;

    public bool IsInsideTractorBeam = false;
    public float TractorBeamSpeed;
    public float PlayerMoveInBeam;
    public Transform ShipPos;

    public Animator animator;

    void Start ()
    {
        alive = true;
        _canWalk = true;
        rig = gameObject.GetComponent<Rigidbody2D>();
        _startScale = transform.localScale.x;

        controller = GetComponent<PlayerController>();

        // https://www.youtube.com/watch?v=PlT44xr0iW0
        gravity = -(2 * JumpForce) / Mathf.Pow(TimeToJumpApex, 2);
        JumpVelocity = Mathf.Abs(gravity) * TimeToJumpApex;

        //camHeight = 2.0f * _cam.orthographicSize;
        //camWidth = camHeight * _cam.aspect;
    }

    void Update()
    {
        //if (_hit = Physics2D.Linecast(new Vector2(_GroundCast.position.x, _GroundCast.position.y + 0.1f), _GroundCast.position))
        //{
        //    if (!_hit.transform.CompareTag("Player"))
        //    {
        //        _canJump = true;
        //        _canWalk = true;
        //    }
        //}

        if (controller.collisions.above || controller.collisions.below)
        {
            //rig.velocity = new Vector2(rig.velocity.x, 0);
            velocity.y = 0;
            _canJump = true;
        }

        _inputAxis = new Vector2(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"));
        if (_inputAxis.y > 0 && controller.collisions.below)
        {
            //_canWalk = false;
            _isJump = true;
        }

        if (alive == false)
        {
            Debug.Log("Player Died");
            animator.SetBool("Dying", true);
            _canWalk = _canJump = false;
            //wait 2 seconds
            //GameManager gmScript = GM.GetComponent<GameManager>();
            StartCoroutine(WaitFunction());
            //gmScript.EndGame();
        }
    }

    void FixedUpdate()
    {
        if (_inputAxis.x > 0)
            mirror = false;
        else if (_inputAxis.x < 0)
            mirror = true;

        // handle flipping of sprite depending direction of facing 
        if (!mirror)
            transform.localScale = new Vector3(_startScale, _startScale, 1);
        if (mirror)
            transform.localScale = new Vector3(-_startScale, _startScale, 1);

        if (_inputAxis.x != 0 && _canWalk && controller.collisions.below)
        {
            animator.SetBool("Running", true);

            //if (_canWalk)
            //{
            //    _Legs.clip = _walk;
            //    _Legs.Play();
            //}
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (_isJump)
        {
            velocity.y = JumpVelocity;
            //_Legs.clip = _jump;
            //_Legs.Play();
            _canJump = false;
            _isJump = false;
            animator.SetBool("Flying", true);
            FindObjectOfType<SoundManagerScript>().PlaySound("Booster");
        }
        if(_canJump)
        {
            animator.SetBool("Flying", false);
        }

        velocity.x = _inputAxis.x * WalkSpeed * Time.deltaTime;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //float clampXMax = Ship.position.x + (camWidth / 2.0f);
        //float clampXMin = Ship.position.x - (camWidth / 2.0f);
        //Debug.Log(clampXMax);

        //float clampYMax = Ship.position.y + (camHeight / 2.0f) - (Ship.lossyScale.y * 3.0f);
        //float clampYMin = Ship.position.y - (camHeight / 2.0f);

        //rig.transform.position = new Vector2(Mathf.Clamp(rig.transform.position.x, clampXMin, clampXMax), Mathf.Clamp(rig.transform.position.y, clampYMin, clampYMax));


        if (IsInsideTractorBeam)
        {
            velocity.y = 1 * TractorBeamSpeed * Time.deltaTime;

            float lerpSpeed;
            if (_inputAxis.x != 0)
                lerpSpeed = PlayerMoveInBeam;
            else
                lerpSpeed = TractorBeamSpeed;
            transform.position = Vector3.Lerp(transform.position, new Vector2(ShipPos.position.x, ShipPos.position.y - 5), lerpSpeed);
        }
    }

    public bool IsMirror()
    {
        return mirror;
    }

    IEnumerator WaitFunction()
    {
        yield return new WaitForSeconds(1);
        GameManager gmScript = GM.GetComponent<GameManager>();
        gmScript.EndGame();
    }
}
