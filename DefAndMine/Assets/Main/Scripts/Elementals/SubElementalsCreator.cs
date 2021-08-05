using UnityEngine;

public class SubElementalsCreator : MonoBehaviour
{
    [SerializeField] private Elemental elemental;
    [SerializeField] private EElement reactElement;
    [SerializeField] private float impactValueForSpawn = 10;
    [SerializeField] private Elemental prefab;

    private float receivedImpact = 0f;


    void OnValidate()
    {
        elemental = GetComponent<Elemental>();
    }

    void Awake()
    {
        elemental.OnGetImpact += OnGetImpact;
    }


    private void OnGetImpact(EElement element, float value)
    {
        if (element == reactElement)
        {
            receivedImpact += value;

            while (receivedImpact >= impactValueForSpawn)
            {
                receivedImpact -= impactValueForSpawn;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        Instantiate(prefab, elemental.transform.position, elemental.transform.rotation, elemental.transform.parent).Init(elemental);
    }
}