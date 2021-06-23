using Constructor.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUnitBehaviour : BaseUnitBehaviour
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private SerializableResourcesPackage drop;

    public override ResourcesStorage Drop => new ResourcesStorage(drop.All);


    void Update()
    {
        if (Behaviour == EUnitBehaviour.Movement)
        {
            Move();
        }
        else if (Behaviour == EUnitBehaviour.Battle)
        {
            Battle();
        }
        else if (Behaviour == EUnitBehaviour.Taking)
        {
            Take();
        }
    }


    public override void Init(ETeam team, MovementPath movementPath, Structure targetStructure)
    {
        Health = new UnitHealth(maxHealth);
        Team = team;
        TargetStructure = targetStructure;
        MovementPath = movementPath;

        SetBehaviour(EUnitBehaviour.Movement);
    }

    #region Movement Variables
    private int currentPathCellID = 0;

    private Vector3 startPosition;
    private Vector3 finalPosition;

    private Quaternion startRotation;
    private Quaternion finalRotation;

    private float angle;
    private float distance;
    private float positionProgress = 0f;
    private float rotationProgress = 0f;
    #endregion

    private void Move()
    {
        if (currentPathCellID < MovementPath.cells.Length)
        {
            positionProgress += (movementSpeed / distance * Time.deltaTime);
            rotationProgress += (rotationSpeed / angle * Time.deltaTime);

            transform.position = Vector3.Lerp(startPosition, finalPosition, positionProgress);
            transform.rotation = Quaternion.Lerp(startRotation, finalRotation, rotationProgress);

            if (positionProgress > 0.75f)
            {
                if (currentPathCellID + 2 < MovementPath.cells.Length)
                {
                    currentPathCellID++;

                    startRotation = transform.rotation;
                    finalRotation = Quaternion.LookRotation(MovementPath.cells[currentPathCellID + 1].transform.position - transform.position, transform.up);
                    rotationProgress = 0f;
                    angle = Quaternion.Angle(startRotation, finalRotation);


                    startPosition = transform.position;
                    finalPosition = Vector3.Lerp(MovementPath.cells[currentPathCellID].transform.position, MovementPath.cells[currentPathCellID + 1].transform.position, 0.5f);
                    positionProgress = 0f;
                    distance = Vector3.Distance(startPosition, finalPosition);
                }
            }

            if (positionProgress >= 1f)
            {
                transform.position = finalPosition;

                if (++currentPathCellID < MovementPath.cells.Length)
                {
                    startPosition = transform.position;
                    finalPosition = MovementPath.cells[currentPathCellID].transform.position;
                    startRotation = transform.rotation;
                    finalRotation = Quaternion.LookRotation(finalPosition - startPosition, transform.up);
                    positionProgress = 0f;
                    rotationProgress = 0f;
                    angle = Quaternion.Angle(startRotation, finalRotation);
                    distance = Vector3.Distance(startPosition, finalPosition);
                }
            }
        }
        else
        {
            SetBehaviour(EUnitBehaviour.Taking);
        }
    }

    private void Take()
    {

    }

    private void Battle()
    {

    }


    private void SetBehaviour(EUnitBehaviour newBehaviour)
    {
        if (Behaviour != newBehaviour)
        {
            Behaviour = newBehaviour;

            switch (newBehaviour)
            {
                case EUnitBehaviour.Movement:
                    if (currentPathCellID < MovementPath.cells.Length)
                    {
                        startPosition = transform.position;
                        finalPosition = MovementPath.cells[currentPathCellID].transform.position;
                        startRotation = transform.rotation;
                        finalRotation = Quaternion.LookRotation(finalPosition - startPosition, transform.up);
                        positionProgress = 0f;
                        rotationProgress = 0f;
                        angle = Quaternion.Angle(startRotation, finalRotation);
                        distance = Vector3.Distance(startPosition, finalPosition);
                    }
                    break;

                case EUnitBehaviour.Taking:

                    break;

                case EUnitBehaviour.Battle:

                    break;
            }
        }
    }
}