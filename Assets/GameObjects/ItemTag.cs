using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTag : MonoBehaviour {
    public enum TagTypes {
        Weaponizable,
        Equipable,
        Wearable,}
    public TagTypes tagType;
    public ItemTag[] requireTags;
    ItemScript item;
    public ItemTag()
    {
        tagType = (TagTypes)System.Enum.Parse(typeof(TagTypes), GetType().Name);
    }

    public void Start()
    {
        checkRequiredTags();
    }
    private bool checkRequiredTags()
    {
        foreach(ItemTag tag in requireTags)
        {
            if (!item.hasTag(tag))
            {
                print(gameObject.name + " missing tag " + tag.name);
                item.addNewTag(tag);
                return false;
            }

        }
        return true;
    }

}
