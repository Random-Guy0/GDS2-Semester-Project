using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControls : MonoBehaviour
{
    [SerializeField] private Animator[] animators;
    [SerializeField] private GameObject leftMouse;
    [SerializeField] private GameObject rightMouse;
    [SerializeField] private SpriteRenderer[] sr;
    [SerializeField] private Sprite[] sprites;

    //enum Keys {W, A, S, D };

    private void Start()
    {
        InitialiseSprites();
    }

    void Update()
    {
        PressedW();
        PressedA();
        PressedS();
        PressedD();
        PressedLMB();
        PressedRMB();

    }

    private void InitialiseSprites()
    {
        sr[0].sprite = sprites[0];
        sr[1].sprite = sprites[2];
        sr[2].sprite = sprites[4];
        sr[3].sprite = sprites[6];
        leftMouse.SetActive(false);
        rightMouse.SetActive(false);
    }

    private void PressedW()
    {
        if (sr[0] == null) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            sr[0].sprite = sprites[1];
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            sr[0].sprite = sprites[0];
        }
        /*if (animators[0] == null) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            animators[0].SetTrigger("Press");
        }
        else if (Input.GetKeyUp(KeyCode.W))
            animators[0].ResetTrigger("Press");*/
    }

    private void PressedA()
    {
        if (sr[1] == null) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            sr[1].sprite = sprites[3];
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            sr[1].sprite = sprites[2];
        }
    }

    private void PressedS()
    {
        if (sr[2] == null) return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            sr[2].sprite = sprites[5];
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            sr[2].sprite = sprites[4];
        }
    }

    private void PressedD()
    {
        if (sr[3] == null) return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            sr[3].sprite = sprites[7];
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            sr[3].sprite = sprites[6];
        }
    }

    private void PressedLMB()
    {
        if (leftMouse == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            leftMouse.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouse.SetActive(false);
        }

        //bool status = (Input.GetMouseButtonDown(0)) ?  true : false;
        //leftMouse.SetActive(status);
    }

    private void PressedRMB()
    {
        if (rightMouse == null) return;

        if (Input.GetMouseButtonDown(1))
        {
            rightMouse.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightMouse.SetActive(false);
        }
    }
}
