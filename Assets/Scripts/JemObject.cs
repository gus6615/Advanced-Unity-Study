using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JemObject : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    public int jemCode;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer= this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = DataCtrl.instance.jems[jemCode].sprite;

        rigid.AddForce(new Vector2(Random.Range(-7f, 7f), Random.Range(3f, 7f)), ForceMode2D.Impulse);
        rigid.AddTorque(Random.Range(-20f, 20f));
    }

    // Update is called once per frame
    void Update()
    {
        Color color = spriteRenderer.color;
        if (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            spriteRenderer.color = color;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
