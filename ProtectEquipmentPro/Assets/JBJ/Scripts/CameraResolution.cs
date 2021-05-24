using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{

    private void Awake()
    {
        FixTheScreenSize();
    }

    [SerializeField] private Color settingColor;
    public void FixTheScreenSize()
    {
        //Camera.main.pixelHeight Camera.main.pixelWidth 카메라를 통해서 현재 화면의 가로세로를 가져온다
        //그 후 찍이는 좌표값을 비율에 맞게 조정해준다.
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        //float scaleHeight = ((float)Screen.width / Screen.height) / ((float)Camera.main.pixelWidth / Camera.main.pixelHeight);
        float scaleWidth = 1f / scaleHeight;
        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        camera.rect = rect;
    }
 
    void OnPreCull()
    {
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;
    }

    public void RectBlack()
    {
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;
    }

   

}
