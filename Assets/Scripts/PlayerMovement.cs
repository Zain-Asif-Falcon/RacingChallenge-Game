using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Transform PlayerCarTransformObject;
    private float PlayerMovementSpeed = 3.5f;
    // private float PlayerMovementSpeed_Easy = 3.5f;
    // private float PlayerMovementSpeed_Hard = 4.5f;
    private float PlayerMovementSpeed_Easy = 4.5f;
    private float PlayerMovementSpeed_Hard = 5.5f;
    private float PlayerRotationSpeed = 2.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("GameMode") == GameConstants.Easy_SceneName) PlayerMovementSpeed = PlayerMovementSpeed_Easy;
        else PlayerMovementSpeed = PlayerMovementSpeed_Hard;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || IsRightButtonPressed) MovePlayerRight();
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || IsLeftButtonPressed) MovePlayerLeft();
        if (PlayerCarTransformObject.rotation.z != 90)
        {
            PlayerCarTransformObject.rotation = Quaternion.Lerp(PlayerCarTransformObject.rotation, Quaternion.Euler(0, 0, 0), 10f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || IsForwardButtonPressed) MovePlayerForward();
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || IsBackwardButtonPressed) MovePlayerBackward();
        Vector3 PlayerPosition = PlayerCarTransformObject.position;
        PlayerPosition.x = Mathf.Clamp(PlayerPosition.x, -2.12f, 2.12f);
        PlayerPosition.y = Mathf.Clamp(PlayerPosition.y, -4.35f, 4.35f);
        PlayerCarTransformObject.position = PlayerPosition;
    }
    private void MovePlayerRight()
    {
        PlayerCarTransformObject.position += new Vector3(PlayerMovementSpeed * Time.deltaTime, 0, 0);
        PlayerCarTransformObject.rotation = Quaternion.Lerp(PlayerCarTransformObject.rotation, Quaternion.Euler(0, 0, -45), PlayerRotationSpeed * Time.deltaTime);
    }
    private void MovePlayerLeft()
    {
        PlayerCarTransformObject.position -= new Vector3(PlayerMovementSpeed * Time.deltaTime, 0, 0);
        PlayerCarTransformObject.rotation = Quaternion.Lerp(PlayerCarTransformObject.rotation, Quaternion.Euler(0, 0, 45), PlayerRotationSpeed * Time.deltaTime);
    }
    private bool IsForwardButtonPressed = false;
    private bool IsBackwardButtonPressed = false;
    private bool IsLeftButtonPressed = false;
    private bool IsRightButtonPressed = false;

    public AudioSource ForwardPedalSound_Easy;
    public AudioSource ForwardPedalSound_Hard;
    public AudioSource BreakPedalSound_Easy;
    public AudioSource BreakPedalSound_Hard;
    private bool IsForwardPedalSoundPlaying = false;
    private bool IsBreakPedalSoundPlaying = false;


    public void KeepPlayerMovingForward()
    {
        IsForwardButtonPressed = true;
        if(IsForwardPedalSoundPlaying)
        {
            return;
        } 
        else
        {
            if (PlayerPrefs.GetString("GameMode") == GameConstants.Easy_SceneName) ForwardPedalSound_Easy.Play();
            else ForwardPedalSound_Hard.Play();
            IsForwardPedalSoundPlaying = true;
        }            
    }
    public void KeepPlayerMovingBackward()
    {
        IsBackwardButtonPressed = true;
        if(IsBreakPedalSoundPlaying)
        {
            return;
        } 
        else
        {
            if (PlayerPrefs.GetString("GameMode") == GameConstants.Easy_SceneName) BreakPedalSound_Easy.Play();
            else BreakPedalSound_Hard.Play();
            IsBreakPedalSoundPlaying = true;
        }
    }
    public void KeepPlayerMovingLeft()
    {
        IsLeftButtonPressed = true;
    }
    public void KeepPlayerMovingRight()
    {
        IsRightButtonPressed = true;
    }
}
