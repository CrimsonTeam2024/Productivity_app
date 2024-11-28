using System.Collections.Generic;
using UnityEngine;

public static class SpriteManager
{
    private static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    public static Sprite GetSprite(string key)
    {
        if (!spriteCache.ContainsKey(key))
        {
            spriteCache[key] = Resources.Load<Sprite>($"Sprites/{key}");
        }
        return spriteCache[key];
    }
}