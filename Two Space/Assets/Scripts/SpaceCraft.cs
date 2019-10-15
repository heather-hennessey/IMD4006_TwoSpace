using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraft : MonoBehaviour {
    public float MoveSpeed;
    public float ThrustForce;
    public bool mirror;
    public Camera _cam;
    public Transform Astro;
    
    public bool alive;
    public GameObject GM;

    private float _startScale;
    private float camWidth, camHeight;
    private Rigidbody2D rig;
    private Vector2 _inputAxis;
    private RaycastHit2D _hit;

    public Animator animator;
    

	// Use this for initialization
	void Start () {
        alive = true;
        rig = gameObject.GetComponent<Rigidbody2D>();
        _startScale = transform.localScale.x;

        //camHeight = 2.0f * _cam.orthographicSize;
        //camWidth = camHeight * _cam.aspect;
    }

    // Update is called once per frame
    void Update () {
        _inputAxis = new Vector2(Input.GetAxisRaw("P1_Horizontal"), Input.GetAxisRaw("P1_Vertical"));
	}

    void FixedUpdate()
    {
        if (_inputAxis.x != 0)
        {
            rig.velocity = new Vector2(_inputAxis.x * MoveSpeed * Time.deltaTime, rig.velocity.y);

            if (_inputAxis.x > 0)
                mirror = false;
            else
                mirror = true;
        }
        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        if (_inputAxis.y > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, _inputAxis.y * ThrustForce * Time.deltaTime);
        }
        else
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
        }

        //float clampXMax = Astro.position.x + (camWidth / 2.0f);
        //float clampXMin = Astro.position.x - (camWidth / 2.0f);

        //float clampYMax = Astro.position.y + (camHeight / 2.0f) - (Astro.lossyScale.y * 3.0f);
        //float clampYMin = Astro.position.y - (camHeight / 2.0f);

        //rig.transform.position = new Vector2(Mathf.Clamp(rig.transform.position.x, clampXMin, clampXMax), Mathf.Clamp(rig.transform.position.y, clampYMin, clampYMax));


        if (!mirror)
        {
            transform.localScale = new Vector3(_startScale, _startScale, 1);
        }
        else
        {
            transform.localScale = new Vector3(-_startScale, _startScale, 1);
        }
        if(alive == false)
        {
            Debug.Log("Spacecraft Died");
            //GameManager gmScript = GM.GetComponent<GameManager>();
            FindObjectOfType<beam_collision>().HideBeam();
            animator.SetBool("Death", true);
            StartCoroutine(WaitFunction());
            //gmScript.EndGame();
        }
    }

    public void Died()
    {
        GameManager gmScript = GM.GetComponent<GameManager>();
        gmScript.EndGame();
    }

    IEnumerator WaitFunction()
    {
        yield return new WaitForSeconds(1);
        GameManager gmScript = GM.GetComponent<GameManager>();
        gmScript.EndGame();
    }
}
