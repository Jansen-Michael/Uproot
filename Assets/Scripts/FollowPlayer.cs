using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Vector3 offSet = new Vector3(0f, 0f, -10f);
    [SerializeField] private Vector2 xPosLimits = new Vector2(-7f, 7f);
    [SerializeField] private float speed = 10f;

    void Update()
    {
        Vector3 movePos = Vector3.MoveTowards(transform.position, player.transform.position + offSet, speed);
        movePos.x = Mathf.Clamp(movePos.x, xPosLimits.x, xPosLimits.y);
        transform.position = movePos;
    }
}
