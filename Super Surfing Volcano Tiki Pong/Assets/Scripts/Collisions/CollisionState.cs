﻿using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour {

    public LayerMask oceanLayer;
    public LayerMask skyLayer;
    public bool hitSky;
    public bool surfing;
    public Vector2 topPosition = Vector2.zero;
    public Vector2 bottomPosition = Vector2.zero;
    public float collisionRadius = 10.0f;

    [SerializeField]
    private Color debugCollisionColor = Color.red;

    void FixedUpdate() {
        Vector2 position = bottomPosition;
        position.x += transform.position.x;
        position.y += transform.position.y;
        surfing = Physics2D.OverlapCircle(position, collisionRadius, oceanLayer);

        position = topPosition;
        position.x += transform.position.x;
        position.y += transform.position.y;
        hitSky = Physics2D.OverlapCircle(position, collisionRadius, skyLayer);
    }

    void OnDrawGizmos() {
        Gizmos.color = debugCollisionColor;
        Vector2[] positions = new Vector2[] { topPosition, bottomPosition };

        foreach (Vector2 position in positions) {
            Vector2 pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            Gizmos.DrawWireSphere(pos, collisionRadius);
        }
    }
}
