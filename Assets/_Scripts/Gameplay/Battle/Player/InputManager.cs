using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputEvent InputEvent;
    public void HighPunch()
    {
        InputEvent.HighPunch.Invoke();
    }

    public void LowPunch()
    {
        InputEvent.LowPunch.Invoke();
    }

    public void Block()
    {
        InputEvent.Block.Invoke();
    }
    private void StopBlock()
    {
        InputEvent.ReleaseBlock.Invoke();
    }

    public void DodgleLeft()
    {
        InputEvent.DodgeLeft.Invoke();
    }
    public void DodgleRight()
    {
        InputEvent.DodgeRight.Invoke();
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
