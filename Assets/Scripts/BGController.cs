using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : Singleton<BGController>
{
    public Sprite[] sprites;

    public SpriteRenderer bgImage;
    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if(bgImage != null && sprites != null && sprites.Length > 0)
        {
            int randIdx = Random.Range(0, sprites.Length);
            if(sprites[randIdx] != null)
            {
                bgImage.sprite = sprites[randIdx];  
            }
        }
    }
}
