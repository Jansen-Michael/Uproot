using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindEffect : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Image windZone;

    private Vector2 leftScreenPoint;
    private Vector2 rightScreenPoint;

    public bool isMovingRight;
    public bool isActive;

    void Start()
    {
        leftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2));
        rightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2));

        isActive = false;
    }

    void Update()
    {
        transform.Translate(isMovingRight ? speed * Time.deltaTime : -speed * Time.deltaTime, 0, 0);

        if (isMovingRight)
        {
            if (transform.position.x - Screen.width - Screen.width / 2 >= rightScreenPoint.x)
            {
                transform.Translate(Screen.width * -2, 0, 0);
            }
        }
        else
        {
            if (transform.position.x + Screen.width / 2 <= leftScreenPoint.x)
            {
                transform.Translate(Screen.width * 2, 0, 0);
            }
        }
    }
}
