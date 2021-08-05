using UnityEngine;
using System;

[Serializable]
public class ELementBehaviourDictionary : UnitySerializedDictionary<EElement, ElementalBehaviour> { }

[Serializable]
public class ELementSpriteDictionary : UnitySerializedDictionary<EElement, Sprite> { }

[Serializable]
public class SrtuctureUnions : UnitySerializedDictionary<Structure, Structure> { }