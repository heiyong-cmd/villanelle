using UnityEngine;

public class Interactive : MonoBehaviour
{
    public virtual void OnClickedAction()
    {
        Debug.Log("ok");
    }

    // Unity 自带的方法：当鼠标点击这个物体的 Collider 时自动触发
    private void OnMouseDown()
    {
        OnClickedAction();
    }
}