using UnityEngine;

// 强制要求物体上有这俩组件，防止报错
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 5f;

    [Header("立绘设置")]
    public Sprite spriteBack;  // 对应 W/上箭头 (立绘 A)
    public Sprite spriteFront; // 对应 S/下箭头 (立绘 B)

    // 内部组件引用
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;

    void Awake()
    {
        // 获取组件引用
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. 获取输入 (Unity默认设置中，Horizontal对应AD/左右，Vertical对应WS/上下)
        // GetAxisRaw 返回 -1, 0, 1，移动手感更灵敏，适合2D像素或RPG
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 2. 处理立绘切换逻辑
        // 如果正在向上移动 (y > 0)
        if (movement.y > 0.1f)
        {
            if (spriteBack != null) sr.sprite = spriteBack;
        }
        // 如果正在向下移动 (y < 0)
        else if (movement.y < -0.1f)
        {
            if (spriteFront != null) sr.sprite = spriteFront;
        }

        // 注意：这里没有写关于 x (左右) 的判断
        // 所以当玩家只按左或右时，代码不会执行上面任何一行，
        // 图片就会保持原样（即保持最后一次按下或者上的状态）。
    }

    // 涉及物理移动（Rigidbody）最好写在 FixedUpdate 里
    void FixedUpdate()
    {
        // 移动逻辑：当前位置 + 方向 * 速度 * 时间
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}