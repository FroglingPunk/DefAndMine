using UnityEngine;
using UIElementsList;

public class Builder : MonoBehaviour
{
    [SerializeField] private UIList uIList;
    [SerializeField] private Structure[] structuresPrefabs;

    private Structure chosenPrefab;
    private Transform towerPhantomTransform;


    void Awake()
    {
        uIList.Init(structuresPrefabs, (prefab) => chosenPrefab = prefab as Structure, null, null);
    }

    void Update()
    {
        if (chosenPrefab == null)
        {
            return;
        }

        //if (Input.GetMouseButton(0))
        //{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask;

            if (chosenPrefab.Place == EPlace.Elevation)
            {
                layerMask = 1 << 9;
            }
            else
            {
                layerMask = 1 << 10;
            }


            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (towerPhantomTransform == null)
                {
                    towerPhantomTransform = Instantiate(chosenPrefab, transform).transform;
                    Destroy(towerPhantomTransform.GetComponent<Collider>());
                    Destroy(towerPhantomTransform.GetComponent<Structure>());
                }

                towerPhantomTransform.position = hit.point;
            }



            layerMask = 1 << 12;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.TryGetComponent(out Structure structure))
                {
                    if ((structure as IUnionStructure)?.TryUnion(chosenPrefab))
                    {
                        towerPhantomTransform.position = structure.transform.position + Vector3.up * structure.transform.localScale.y * 0.5f;
                    }
                }
            }
        //}

        if (Input.GetMouseButtonUp(0))
        {
            if (towerPhantomTransform)
            {
                Destroy(towerPhantomTransform.gameObject);
            }


            /*Ray*/ ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            /*int*/ layerMask = 1 << 12;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.TryGetComponent(out Structure structure))
                {
                    Structure unionStructure = (structure as IUnionStructure)?.TryUnion(chosenPrefab);
                    if (unionStructure)
                    {
                        if (TryBuild(unionStructure, structure.transform.position, structure))
                        {
                            Destroy(structure.gameObject);
                        }
                    }
                }
            }
            else
            {
                if (chosenPrefab.Place == EPlace.Elevation)
                {
                    layerMask = 1 << 9;
                }
                else
                {
                    layerMask = 1 << 10;
                }

                if (Physics.Raycast(ray, out hit, 100f, layerMask))
                {
                    TryBuild(chosenPrefab, hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            chosenPrefab = null;
        }
    }


    public Structure TryBuild(Structure prefab, Vector3 position, Structure unionStructure = null)
    {
        int layerMask = 1 << 12;

        Structure structure = Instantiate(prefab, transform);
        structure.transform.position = position;

        Collider[] hitColliders = Physics.OverlapBox(structure.transform.position, structure.transform.localScale / 2, Quaternion.identity, layerMask);

        if (hitColliders.Length == 0)
        {
            return structure;
        }
        else if (unionStructure && hitColliders.Length == 1 && hitColliders[0] == unionStructure.GetComponent<Collider>())
        {
            return structure;
        }
        else
        {
            Destroy(structure.gameObject);
            return null;
        }
    }
}