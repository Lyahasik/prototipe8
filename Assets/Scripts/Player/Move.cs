using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour, IUpdate
{
    public void LaunchUpdate()
    {
        CheckKeyDown();
        CheckKeyUp();

        if (_timeFreeze > 0.0f)
		{
            Defrost();
		}
        else
		{
            UpdatePosition();
            UpdateRotation();
            UpdateAnimator();
        }
    }

    #region position

    public float Speed = 1.0f;

    private Animator _animatorCharacter;

    private bool _runForward = false;
    private bool _runBackward = false;
    private bool _runLeft = false;
    private bool _runRight = false;

    public void SetAnimatorCharacter(Animator animatorCharacter)
    {
        _animatorCharacter = animatorCharacter;
    }

    private void CheckKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _runForward = true;
            _runBackward = false;
            _runLeft = false;
            _runRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _runForward = false;
            _runBackward = true;
            _runLeft = false;
            _runRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _runForward = false;
            _runBackward = false;
            _runLeft = true;
            _runRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _runForward = false;
            _runBackward = false;
            _runLeft = false;
            _runRight = true;
        }
    }

    private void CheckKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            _runForward = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _runBackward = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _runLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _runRight = false;
        }
    }

    private void UpdateAnimator()
    {
        _animatorCharacter.SetBool("RunForward", _runForward);
        _animatorCharacter.SetBool("RunBackward", _runBackward);
        _animatorCharacter.SetBool("RunLeft", _runLeft);
        _animatorCharacter.SetBool("RunRight", _runRight);
    }

    private void UpdatePosition()
    {
        if (_runForward == true)
        {
            transform.Translate(new Vector3(0.0f, 0.0f, 1.0f * Speed) * Time.deltaTime);
        }
        else if (_runBackward == true)
        {
            transform.Translate(new Vector3(0.0f, 0.0f, -1.0f * Speed * 0.5f) * Time.deltaTime);
        }
        else if (_runLeft == true)
        {
            transform.Translate(new Vector3(-1.0f * Speed, 0.0f, 0.0f) * Time.deltaTime);
        }
        else if (_runRight == true)
        {
            transform.Translate(new Vector3(1.0f * Speed, 0.0f, 0.0f) * Time.deltaTime);
        }
    }

    #endregion position

    #region rotation


    private Vector3 _screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);

    private void UpdateRotation()
	{
		Vector3 _directionPointerFromCenter = Input.mousePosition - _screenCenter;
        Vector3 _directionRotation = new Vector3(_directionPointerFromCenter.x, 0.0f, _directionPointerFromCenter.y);

		transform.rotation = Quaternion.FromToRotation(Vector3.forward, _directionRotation);
    }

    #endregion rotation

    private float _timeFreeze = 0.0f;

    public void Freeze(float timeSeconds)
	{
        _timeFreeze = timeSeconds;
        StopAnimator();
    }

    private void StopAnimator()
    {
        _animatorCharacter.SetBool("RunForward", false);
        _animatorCharacter.SetBool("RunBackward", false);
        _animatorCharacter.SetBool("RunLeft", false);
        _animatorCharacter.SetBool("RunRight", false);
    }

    private void Defrost()
	{
        if (_timeFreeze > 0.0f)
		{
            _timeFreeze -= 1.0f * Time.deltaTime;
		}
	}
}