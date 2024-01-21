using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 5f, finalSpeed = 15f, rotateSpeed = 350f;
    private bool isClicked;

    private float curPoinX, curPoinY;
    [NonSerialized] public Vector3 FinalPosition;

    public enum Axis 
    {
        Vertical, Horizontal
    }

    public Axis CarAxis;

    private enum Direction
    {
        Right, Left, Top, Bottom, None
    }

    private Direction CarDirectionX = Direction.None;
    private Direction CarDirectionY = Direction.None;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        curPoinX = Input.mousePosition.x;
        curPoinY = Input.mousePosition.y;
    }

    void OnMouseUp()
    {
        if (Input.mousePosition.x - curPoinX > 0)
        {
            CarDirectionX = Direction.Right;
        } else {
            CarDirectionX = Direction.Left;
        }

        if (Input.mousePosition.y - curPoinY > 0)
        {
            CarDirectionY = Direction.Top;
        } else {
            CarDirectionY = Direction.Bottom;
        }

        isClicked = true;
    }

    void Update()
    {
        if (FinalPosition.x !=0)
        {
            transform.position = Vector3.MoveTowards(transform.position, FinalPosition, finalSpeed * Time.deltaTime);
            
            Vector3 lookAtPos = FinalPosition - transform.position;
            lookAtPos.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookAtPos), Time.deltaTime * rotateSpeed);
        }

        if (transform.position == FinalPosition)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isClicked && FinalPosition.x == 0)
        {
            Vector3 whichWay =  CarAxis == Axis.Horizontal ? Vector3.forward : Vector3.left;

            speed = Mathf.Abs(speed);
            if (CarDirectionX == Direction.Left && CarAxis == Axis.Horizontal)
            {
                speed *= -1;
            }
            else if (CarDirectionY == Direction.Bottom && CarAxis == Axis.Vertical)
            {
                speed *= -1;
            }

            _rb.MovePosition(_rb.position + whichWay * speed * Time.fixedDeltaTime);
        }
    }
}
