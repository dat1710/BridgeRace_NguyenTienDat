using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

public class PlayerController : Character1
{
    [SerializeField] protected CharacterController characterController;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float moveSpeed = 15;
    private FixedJoystick joystick;
    private Vector3 velocity;
    private bool canMoveForward = true;

    protected override void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        base.Start();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = -joystick.Horizontal;
        float vertical = -joystick.Vertical;

        // Kiểm tra xem có input từ joystick không
        if (horizontal != 0f || vertical != 0f)
        {
            // Tính toán hướng mới dựa trên input
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            // Chuyển đổi hướng thành quaternion dựa trên hướng trên mặt phẳng XZ
            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
            // Áp dụng rotation mới cho nhân vật
            transform.rotation = newRotation;
        }
        if (canMoveForward || joystick.Vertical < 0)
        {
            Vector3 move = new Vector3(joystick.Horizontal * -moveSpeed, 0, joystick.Vertical * -moveSpeed);
            characterController.Move(move * Time.deltaTime);
        }
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
        characterController.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        base.CollideWithBrick(other);
        OnBridge(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            canMoveForward = true;
        }
    }
    public void OnBridge(Collider item)
    {
        if (item.CompareTag("Stair"))
        {
            MeshRenderer stairMesh = item.GetComponent<MeshRenderer>();
            Material stairMaterial = colorData.GetColorData(colorPlayer);

            if (stairMesh.material.color != stairMaterial.color)
            {
                if (listBrick.Count > 0)
                {
                    stairMesh.enabled = true;
                    stairMesh.material = stairMaterial;
                    GameObject lastBrick = listBrick[listBrick.Count - 1];
                    listBrick.RemoveAt(listBrick.Count - 1);
                    Destroy(lastBrick);
                    canMoveForward = true;
                }
                else
                {
                    canMoveForward = false;
                }
            }
            else
            {
                canMoveForward = true;
            }
        }
    }
}
