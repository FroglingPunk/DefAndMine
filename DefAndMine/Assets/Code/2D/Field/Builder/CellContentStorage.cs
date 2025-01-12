using System.Collections.Generic;
using UnityEngine;

public class CellContentStorage : MonoBehaviour
{
    [System.Serializable]
    public class SerializableCellContent
    {
        public ECellContent type;
        public GameObject contentGameObject;
    }

    [SerializeField] private List<SerializableCellContent> _contentTemplates;

    private static CellContentStorage _instance;


    private void Awake()
    {
        _instance = this;
    }


    public static GameObject GetContentTemplate(ECellContent contentType)
    {
        return _instance._contentTemplates.Find(c => c.type == contentType).contentGameObject;
    }
}