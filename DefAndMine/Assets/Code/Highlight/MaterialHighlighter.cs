using UnityEngine;

public class MaterialHighlighter : HighlighterBase
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _highlightMaterial;

    private Material _defaultMaterial;


    public override void SetState(bool state)
    {
        if (_defaultMaterial == null)
        {
            _defaultMaterial = _renderer.sharedMaterial;
        }

        _renderer.sharedMaterial = state ? _highlightMaterial : _defaultMaterial;
    }
}