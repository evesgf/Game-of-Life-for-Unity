using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteCtrl : MonoBehaviour
{
    public bool IsLife;
    public int X;
    public int Y;

    private Button m_Button;
    private Image m_Image;
	// Use this for initialization
	void Start ()
	{
	    m_Button = GetComponent<Button>();
	    m_Image = GetComponent<Image>();
        EventTriggerListener.Get(m_Button.gameObject).onClick = OnButtonClick;
        EventTriggerListener.Get(m_Image.gameObject).onClick = OnButtonClick;
	}

    private void OnButtonClick(GameObject go)
    {
        //在这里监听按钮的点击事件
        if (go == m_Button.gameObject)
        {
            SetLife();
        }
    }

    private void SetLife()
    {
        if (IsLife)
        {
            this.GetComponent<Image>().color = Color.white;
            IsLife = false;
        }
        else
        {
            this.GetComponent<Image>().color = Color.red;
            IsLife = true;
        }
    }
}
