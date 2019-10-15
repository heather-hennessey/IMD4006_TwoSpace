using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float damping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public Transform _player1;
    public Transform _player2;

    private bool faceLeft;
    private int lastX;
    private float dynamicSpeed;
    private Camera _cam;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayers();
        _cam = gameObject.GetComponent<Camera>();
    }

    public void FindPlayers()
    {
        lastX = Mathf.RoundToInt((_player1.position.x + _player2.position.x) / 2.0f);
    }

    void FixedUpdate()
    {
        if (_player1 && _player2)
        {
            int currentX = Mathf.RoundToInt((_player1.position.x + _player2.position.x) / 2.0f);
            if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true;
            lastX = Mathf.RoundToInt((_player1.position.x + _player2.position.x) / 2.0f);

            Vector3 target;
            if (faceLeft)
            {
                target = new Vector3(lastX - offset.x, ((_player1.position.y + (_player2.position.y - 10.0f)) / 2.0f) + offset.y + dynamicSpeed, transform.position.z);

            }
            else
            {
                target = new Vector3(lastX + offset.x, ((_player1.position.y + (_player2.position.y - 10.0f)) / 2.0f) + offset.y + dynamicSpeed, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }

        //float followTimeDelta = 0.8f;

        //Vector3 midpoint = (_player1.position + _player2.position) / 2.0f;

        //float distance = (_player1.position + _player2.position).magnitude;

        //Vector3 cameraDestination = midpoint - _cam.transform.forward * distance;
        //cameraDestination.x += 2.0f;
        //cameraDestination.y += 1.0f;

        //if (!outsideX())
        //{
        //    _cam.transform.position = Vector3.Slerp(_cam.transform.position, cameraDestination, followTimeDelta);

        //    if ((cameraDestination - _cam.transform.position).magnitude <= 0.05f)
        //        _cam.transform.position = cameraDestination;
        //}
    }
}
