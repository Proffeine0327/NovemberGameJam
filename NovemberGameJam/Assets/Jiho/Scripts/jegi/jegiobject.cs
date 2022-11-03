using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jegiobject : MonoBehaviour
{
    float pastypos;
    float presentypos;
    public Sprite[] sprites;
    SpriteRenderer spriterenderer;
    public bool ischange = true;
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pastypos < presentypos && ischange)
        {
            spriterenderer.sprite = sprites[1];
        }
        else if (pastypos > presentypos && ischange)
        {
            spriterenderer.sprite = sprites[2];
        }

        StartCoroutine(ypos());
    }

    IEnumerator ypos()
    {
        presentypos = transform.position.y;
        yield return new WaitForSeconds(0.2f);
        pastypos = transform.position.y;
    }
}
