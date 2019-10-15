using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the following class was created using Sebastian Lague's "Creating a 2D Platformer" tutorial playlist:
// https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ
public class PlayerController : MonoBehaviour {

    public LayerMask collisionMask;
    public CollisionInfo collisions;
    public GameObject AstronautImgObject;
    public float MaxClimbingAngle;
    public float MaxDescendingAngle;

    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;

	// Use this for initialization
	void Start () {
        collider = AstronautImgObject.GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    public void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            // set rayOrigin on left side based on direction of movement in y axis
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            // move rays on the x axis
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D _hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (_hit)
            {
                velocity.y = (_hit.distance - skinWidth) * directionY;
                // change raylength once we hit something
                rayLength = _hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }

            // make sure player doesn't stutter when changing slope angles
            if (collisions.climbingSlope)
            {
                float directionX = Mathf.Sign(velocity.x);
                rayLength = Mathf.Abs(velocity.x) + skinWidth;
                Vector2 rayOrigin2 = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin2, Vector2.right * directionX, rayLength, collisionMask);

                if (hit)
                {
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    if (slopeAngle != collisions.slopeAngle)
                    {
                        velocity.x = (hit.distance - skinWidth) * directionX;
                    }
                }
            }
        }
    }

    public void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            // set rayOrigin on left side based on direction of movement in y axis
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            // move rays on the x axis
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D _hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (_hit)
            {

                // get angle of slope
                float slopeAngle = Vector2.Angle(_hit.normal, Vector2.up);
                print("Slope " + slopeAngle);


                // adjust velocity if hitting outside ray and slope of collision plane is climbable
                if (i == 0 && slopeAngle <= MaxClimbingAngle)
                {
                    ClimbSlope(ref velocity, slopeAngle);
                }

               

                if (!collisions.climbingSlope || slopeAngle > MaxClimbingAngle)
                {
                    //velocity.x = 0;
                    velocity.x = (_hit.distance - skinWidth) * directionX;
                    // change raylength once we hit something
                    rayLength = _hit.distance;

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }  
            }
        }
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    void DescendSlope(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= MaxDescendingAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        // set corners based on bounds
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        // set spacing based on bounds and how many rays we want
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right, climbingSlope, descendingSlope;
        public float slopeAngle;

        public void Reset()
        {
            // reset all to false
            above = below = left = right = climbingSlope = descendingSlope = false;
            slopeAngle = 0f;
        }
    }
}
