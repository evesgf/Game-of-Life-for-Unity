using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateSprite : MonoBehaviour
{
    public GameObject Sprite;
    public Transform StartPos;
    public int Width;
    public int Hight;
    public int SpriteWidth;

    private GameObject[,] Sprites;

    public bool StartcalCulate;

    public float Interval;

    private List<GameObject> IsLifes;
    private List<GameObject> NoLifes;

    // Use this for initialization
    void Start ()
	{
        IsLifes=new List<GameObject>();
        NoLifes=new List<GameObject>();

        Sprites =new GameObject[Width,Hight];
        Create();

        Invoke("Calculate", Interval);
	}

    public void StartCalculate()
    {
        StartcalCulate = !StartcalCulate;
    }

    private void Calculate()
    {
        //set life
        if (IsLifes.Count>0)
        {
            for (int i = 0; i < IsLifes.Count; i++)
            {
                IsLifes[i].GetComponent<SpriteCtrl>().IsLife = true;
                IsLifes[i].GetComponent<Image>().color = Color.red;
            }
            IsLifes.Clear();
        }
        if (NoLifes.Count>0)
        {
            //set no life
            for (int i = 0; i < NoLifes.Count; i++)
            {
                NoLifes[i].GetComponent<SpriteCtrl>().IsLife = false;
                NoLifes[i].GetComponent<Image>().color = Color.white;
            }
            NoLifes.Clear();
        }

        //check
        if (StartcalCulate)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Hight; j++)
                {
                    var re=Judge(i,j);
                    if (re == 0)
                    {
                        IsLifes.Add(Sprites[i,j]);
                    }
                    if (re == 2)
                    {
                        NoLifes.Add(Sprites[i, j]);
                    }
                }
            }
        }

        Invoke("Calculate", Interval);
    }

    private int Judge(int i,int j)
    {
        var lifeCount = 0;
        var returnNum = 0;

        //获取周围8个细胞
        if (i - 1 >= 0 && j - 1 >= 0)
        {
            var s1 = Sprites[i - 1, j - 1];
            if (s1.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (j - 1 >= 0)
        {
            var s2 = Sprites[i, j - 1];
            if (s2.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (j - 1 >= 0 && i + 1<Width)
        {
            var s3 = Sprites[i+1, j - 1];
            if (s3.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (i - 1 >= 0)
        {
            var s4 = Sprites[i - 1, j];
            if (s4.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (i + 1<Width)
        {
            var s5 = Sprites[i + 1, j];
            if (s5.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (i - 1 >= 0 && j + 1 < Hight)
        {
            var s6 = Sprites[i - 1, j + 1];
            if (s6.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (j + 1 < Hight)
        {
            var s7 = Sprites[i, j + 1];
            if (s7.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }


        if (i+1<Width && j + 1 <Hight)
        {
            var s8 = Sprites[i + 1, j + 1];
            if (s8.GetComponent<SpriteCtrl>().IsLife)
                lifeCount++;
        }

        if (lifeCount == 3)
        {
            returnNum= 0;
        }
        else if (lifeCount == 2)
        {
            returnNum= 1;
        }
        else
        {
            returnNum= 2;
        }

        return returnNum;
    }

    private void Create()
    {
        var newx=StartPos.position.x;
        var newy = StartPos.position.y;

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Hight; j++)
            {
                var newPos=new Vector3(i * SpriteWidth+ newx,j * SpriteWidth + newy,0);
                GameObject clone= (GameObject) Instantiate(Sprite, newPos, Sprite.transform.rotation);
                clone.transform.SetParent(gameObject.transform);
                clone.name = i + "_" + j;

                var temp=clone.GetComponent<SpriteCtrl>();
                temp.X = i;
                temp.Y = j;

                Sprites[i,j] = clone;
            }
        }
    }
}
