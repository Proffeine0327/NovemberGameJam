using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOWButtonScript : MonoBehaviour
{
    public Sprite[] sprites;
    //0 up, 1 down, 2 left, 3 right
    public int buttonType;
    public int currentTransformIndex;
    public int playerType;

    private void Awake()
    {
        buttonType = Random.Range(0, 4);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[buttonType];
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            var t1 = TugOfWar.instance.playerOneTransforms[currentTransformIndex];
            var t2 = TugOfWar.instance.playerTwoTransforms[currentTransformIndex];
            transform.position = Vector3.Lerp(transform.position, playerType == 0 ? t1.position : t2.position, 0.05f);
        }
    }
}
