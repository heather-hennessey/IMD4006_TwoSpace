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

    void Start ()
    {
        alive = true;
        //rig = gameObject.GetComponent<Rigidbody2D>();
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
        }

        _inputAxis = new Vector2(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"));
        if (_inputAxis.y > 0 && controller.collisions.below)
        {
            _canWalk = false;
            _isJump = true;
        }
        if (alive == false)
        {
            Debug.Log("Player Died");
            GameManager gmScript = GM.GetComponent<GameManager>();
            gmScript.EndGame();
        }
    }

    void FixedUpdate()
    {
        //Vector3 dir = _cam.ScreenToWorldPoint(Input.mousePosition) - _Blade.transform.position;
        //dir.Normalize();

        //if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x + 0.2f)
        //    mirror = false;
        //if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x - 0.2f)
        //    mirror = true;

        if (_inputAxis.x > 0)
            mirror = false;
        else if (_inputAxis.x < 0)
            mirror = true;

        // handle flipping of sprite depending direction of facing 
        if (!mirror)
            transform.localScale = new Vector3(_startScale, _startScale, 1);
        if (mirror)
            transform.localScale = new Vector3(-_startScale, _startScale, 1);

        //if (_inputAxis.x != 0)
        //{
        //    //rig.velocity = new Vector2(_inputAxis.x * WalkSpeed * Time.deltaTime, rig.velocity.y);
        //    velocity.x = _inputAxis.x * WalkSpeed * Time.deltaTime;

        //    //if (_canWalk)
        //    //{
        //    //    _Legs.clip = _walk;
        //    //    _Legs.Play();
        //    //}
        //}

        //else
        //{
        //    //rig.velocity = new Vector2(0, rig.velocity.y);
        //    velocity.x = 0;
        //}

        Debug.Log(controller.collisions.below);

        if (_isJump)
        {
            //rig.AddForce(new Vector2(0, JumpForce));
            velocity.y = JumpVelocity;
            //_Legs.clip = _jump;
            //_Legs.Play();
            _canJump = false;
            _isJump = false;
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
            
        
            
            //if(Ship.position.x > rig.position.x)
            //{
                //rig.velocity = new Vector2(rig.velocity.y, 1 * TractorBeamSpeed * Time.deltaTime);
            //}
            //else
            //{
                //rig.velocity = new Vector2(rig.velocity.y, -1 * TractorBeamSpeed * Time.deltaTime);
            //}

        }
    }

    public bool IsMirror()
    {
        return mirror;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, _GroundCast.position);
    //}
}
