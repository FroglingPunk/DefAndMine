using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalBehaviour", menuName = "Elementals/Behaviour", order = 51)]
public class ElementalBehaviour : ScriptableObject
{
    [SerializeField] private EElement elementalElement;
    [SerializeField] private ElementReaction[] reactions;

    public EElement Element => elementalElement;


    public void React(Elemental elemental, EElement element, float impact)
    {
        ElementReaction elementReaction = null;
        for(int i  = 0; i < reactions.Length; i++)
        {
            if(reactions[i].element == element)
            {
                elementReaction = reactions[i];
                break;
            }
        }

        if (elementReaction == null)
        {
            return;
        }

        for (int i = 0; i < elementReaction.reactions.Length; i++)
        {
            if(elemental == null)
            {
                return;
            }

            Reaction reaction = elementReaction.reactions[i];

            switch (reaction.reactType)
            {
                case EReact.GetDamage:
                    elemental.GetDamage(impact * reaction.multiply);
                    break;

                case EReact.Heal:
                    elemental.Heal(impact * reaction.multiply);
                    break;

                case EReact.Morph:
                    elemental.Morph(reaction.morphElement, impact * reaction.multiply);
                    break;
            }
        }
    }

    public bool IsUnites(EElement element, ref EElement unionResult)
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            ElementReaction elementReaction = reactions[i];

            if (elementReaction.element == element)
            {
                for (int p = 0; p < elementReaction.reactions.Length; p++)
                {
                    if (elementReaction.reactions[p].reactType == EReact.Morph)
                    {
                        unionResult = elementReaction.reactions[p].morphElement;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    [Serializable]
    public class ElementReaction
    {
        public EElement element;
        public Reaction[] reactions;
    }

    [Serializable]
    public class Reaction
    {
        public EReact reactType;
        public float multiply;
        public EElement morphElement;
    }

    public enum EReact
    {
        GetDamage,
        Heal,
        Morph
    }
}