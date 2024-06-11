using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

public class PlayerController : Character1
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float moveSpeed = 15;
    [SerializeField] private GameObject botPrefab;
    private Vector3 velocity;
    private bool canMoveForward = true;

    protected override void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject a = Instantiate(botPrefab, transform.position, transform.rotation);
        }
        base.Start();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
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
