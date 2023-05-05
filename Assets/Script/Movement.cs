using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField, Range(min: 0, max: 1)] float moveDuration = 0.1f;

    [SerializeField, Range(min: 0, max: 1)] float jumpHeight = 0.5f;

    [SerializeField] int leftMovelimit;

    [SerializeField] int rightMovelimit;

    [SerializeField] int backMovelimit;


    public UnityEvent<Vector3> OnJumpEnd;

    void Update()
    {
        if (DOTween.IsTweening(transform))
            return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(key: KeyCode.W) || Input.GetKeyDown(key: KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }

        else if (Input.GetKeyDown(key: KeyCode.S) || Input.GetKeyDown(key: KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(key: KeyCode.D) || Input.GetKeyDown(key: KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }

        else if (Input.GetKeyDown(key: KeyCode.A) || Input.GetKeyDown(key: KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }

        if (direction == Vector3.zero)
            return;

        Move(direction: direction);

    }

    public void Move(Vector3 direction)
    {
        var targetPosition = transform.position + direction;


        if (targetPosition.x < leftMovelimit ||
            targetPosition.x > rightMovelimit ||
            targetPosition.z < backMovelimit ||
            TreeStop.AllPositions.Contains(item: targetPosition))
        {
            targetPosition = transform.position;
        }
       

        transform.DOJump(
            endValue: targetPosition,
            jumpPower: jumpHeight, 
            numJumps: 1, 
            duration: moveDuration)
            .onComplete  =BroadCastPosisionOnJumpEnd;

        transform.forward = direction;
    }

    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMovelimit = -horizontalSize / 2;
        rightMovelimit = horizontalSize / 2;
        backMovelimit = backLimit;

    }

    private void BroadCastPosisionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

}
