using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class AIScript : Character1
{
    private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    private List<GameObject> bricksInScene;
    private GameObject targetBrick;
    private bool isBuildingBridge = false;

    protected override void Start()
    {
        GameObject winObject = GameObject.FindWithTag("Win");
        movePositionTransform = winObject.transform;
        Debug.Log(movePositionTransform.position);
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        bricksInScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        targetBrick = null;
    }

    private void Update()
    {
        if (isBuildingBridge && listBrick.Count > 0)
        {
            BuildBridge();
        }
        else if (!isBuildingBridge && listBrick.Count < 10)
        {
            FindBrick();
        }
        else
        {
            if (movePositionTransform == null)
            {
                movePositionTransform = GameObject.FindWithTag("Win").transform;
            }
            navMeshAgent.destination = movePositionTransform.position;
            bricksInScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        }
    }

    private void FindBrick()
    {
        GameObject nearestBrick = FindNearestMatchingBrick();
        if (nearestBrick != null && nearestBrick != targetBrick)
        {
            targetBrick = nearestBrick;
            navMeshAgent.destination = targetBrick.transform.position;
        }

    }

    private GameObject FindNearestMatchingBrick()
    {
        GameObject nearestBrick = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject brick in bricksInScene)
        {
            if (brick == null)
            {
                continue;
            }
            Brick brickScript = brick.GetComponent<Brick>();
            if (brickScript != null && brickScript.colorBrickValue == colorPlayer)
            {
                float distanceToBrick = Vector3.Distance(transform.position, brick.transform.position);
                if (distanceToBrick < nearestDistance)
                {
                    nearestBrick = brick;
                    nearestDistance = distanceToBrick;
                }
            }
        }

        return nearestBrick;
    }

    private void BuildBridge()
    {
        if (listBrick.Count == 0)
        {
            isBuildingBridge = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CollideWithBrick(other);
        OnBridge(other);
    }

    protected override void CollideWithBrick(Collider other)
    {
        base.CollideWithBrick(other);
        bricksInScene.Remove(other.gameObject);
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
                    isBuildingBridge = true;
                }
                else
                {
                    isBuildingBridge = false;
                }
            }
        }
    }
}