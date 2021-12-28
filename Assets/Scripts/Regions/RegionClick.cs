using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionClick : MonoBehaviour, Clickable, Hoverable
{
    private SpriteRenderer sprite;

    [SerializeField]
    private Color defColor;

    [SerializeField]
    private Color selectedColor;

    public RegionText listEntry;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = defColor;
        listEntry.SetMapRegion(gameObject);
    }

    public Color GetDefColor()
    {
        return defColor;
    }

    public Color GetSelectedColor()
    {
        return selectedColor;
    }

    public void OnMouseEnter()
    {
        SetHovered();
    }

    public void OnMouseExit()
    {
        SetUnhovered();
    }

    public void SetHovered()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.8f);
    }

    public void SetUnhovered()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.58f);
    }


    public void OnMouseUpAsButton()
    {
        CountryManager.Instance.SelectRegion( GetComponent<RegionHandler>() );
    }

    public void SetSelected()
    {
        sprite.color = selectedColor;
        listEntry.SetColorSelected();
    }

    public void SetUnselected()
    {
        sprite.color = defColor;
        listEntry.SetColorUnselected();
    }
}
