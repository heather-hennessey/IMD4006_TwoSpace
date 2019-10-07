using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public float WalkSpeed;
    public float JumpForce;
    //public AnimationClip _walk, _jump;
    //public Animation _Legs;
    public Transform _GroundCast;
    public Camera _cam;
    public Transform Ship;
    public bool mirror;
    
    public bool alive;
    public GameObject GM;

    private bool _canJump, _canWalk;
    private float camWidth, camHeight;
    private bool _isWalk, _isJump;
    private float rot, _startScale;
    private Rigidbody2D rig;
    private Vector2 _inputAxis;
    private RaycastHit2D _hit;

	void Start ()
    {
        alive = true;
        rig = gameObject.GetComponent<Rigidbody2D>();
        _startScale = transform.localScale.x;

        //camHeight = 2.0f * _cam.orthographicSize;
        //camWidth = camHeight * _cam.aspect;
    }

    void Update()
    {
        if (_hit = Physics2D.Linecast(new Vector2(_GroundCast.position.x, _GroundCast.position.y + 0.0f), _GroundCast.position))
        {
            if (!_hit.transform.CompareTag("Player"))
            {
                _canJump = true;
                _canWalk = true;
            }
        }
        else _canJump = false;

        _inputAxis = new Vector2(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"));
        if (_inputAxis.y > 0 && _canJump)
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

        if (!mirror)
        {
            //rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(_startScale, _startScale, 1);
            //_Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }
        if (mirror)
        {
            //rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(-_startScale, _startScale, 1);
            //_Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

        if (_inputAxis.x != 0)
        {
            rig.velocity = new Vector2(_inputAxis.x * WalkSpeed * Time.deltaTime, rig.velocity.y);

            //if (_canWalk)
            //{
            //    _Legs.clip = _walk;
            //    _Legs.Play();
            //}
        }

        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        if (_isJump)
        {
            rig.AddForce(new Vector2(0, JumpForce));
            //_Legs.clip = _jump;
            //_Legs.Play();
            _canJump = false;
            _isJump = false;
        }

        //float clampXMax = Ship.position.x + (camWidth / 2.0f);
        //float clampXMin = Ship.position.x - (camWidth / 2.0f);
        //Debug.Log(clampXMax);

        //float clampYMax = Ship.position.y + (camHeight / 2.0f) - (Ship.lossyScale.y * 3.0f);
        //float clampYMin = Ship.position.y - (camHeight / 2.0f);

        //rig.transform.position = new Vector2(Mathf.Clamp(rig.transform.position.x, clampXMin, clampXMax), Mathf.Clamp(rig.transform.position.y, clampYMin, clampYMax));
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
