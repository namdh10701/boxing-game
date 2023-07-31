using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputEvent _inputEvent;
    public void Init(InputEvent inputEvent)
    {
        _inputEvent = inputEvent;
    }
    public void HighPunch()
    {
        _inputEvent.HighPunch.Invoke();
    }

    public void LowPunch()
    {
        _inputEvent.LowPunch.Invoke();
    }

    public void Block()
    {
        _inputEvent.Block.Invoke();
    }
    private void StopBlock()
    {
        _inputEvent.ReleaseBlock.Invoke();
    }

    public void DodgleLeft()
    {
        _inputEvent.DodgeLeft.Invoke();
    }
    public void DodgleRight()
    {
        _inputEvent.DodgeRight.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DodgleLeft();
        }
        else
        if (Input.GetKeyDown(KeyCode.D))
        {
            DodgleRight();
        }
        else
        if (Input.GetKeyDown(KeyCode.J))
        {
            LowPunch();
        }
        else
        if (Input.GetKeyDown(KeyCode.K))
        {
            HighPunch();
        }
        else
        if (Input.GetKey(KeyCode.W))
        {
            Block();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            StopBlock();
        }
    }
}
