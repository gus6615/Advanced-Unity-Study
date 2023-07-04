using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JemObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigid;
    public int jemCode;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = DataCtrl.instance.jems[jemCode].sprite;
        rigid.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(3f, 7f)), ForceMode2D.Impulse);
        rigid.AddTorque(Random.Range(-20f, 20f), ForceMode2D.Impulse);
        PlayerCtrl.instance.playerData.jems[jemCode]++;
    }

    // Update is called once per frame
    void Update()
    {
        Color color = spriteRenderer.color;

        if (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            spriteRenderer.color= color;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
