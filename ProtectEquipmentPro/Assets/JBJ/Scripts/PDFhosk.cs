using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDFhosk : MonoBehaviour
{
    public GameObject realUiPdf;
    public bool pdfClickState;
    public Button pdfButton_forUi;
   
    
    public void OnClickButton_PDFon()
    {
        if(!pdfClickState)
        {
            pdfClickState = true;
            realUiPdf.SetActive(true);
        }
        else if(pdfClickState)
        {
            pdfClickState = false;
            realUiPdf.SetActive(false);
        }
    }

    


}
