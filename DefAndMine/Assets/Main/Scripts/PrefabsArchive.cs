using UnityEngine;

public class PrefabsArchive : MonoBehaviour
{
    private static PrefabsArchive instance;
    public static PrefabsArchive Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PrefabsArchive>();

                if (instance == null)
                {
                    instance = Instantiate(Resources.Load<PrefabsArchive>("PrefabsArchive"));
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    [SerializeField] private ELementSpriteDictionary elementsSprites;
    [SerializeField] private ELementBehaviourDictionary elementalsBehaviours;


    public Sprite GetElementIcon(EElement element)
    {
        return elementsSprites.ContainsKey(element) ? elementsSprites[element] : null;
    }

    public ElementalBehaviour GetElementalBehaviour(EElement element)
    {
        return elementalsBehaviours.ContainsKey(element) ? elementalsBehaviours[element] : null;
    }
}