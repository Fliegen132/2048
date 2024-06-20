using System;
using _2048Figure.Architecture.Scripts.ADBehaviour;
using _2048Figure.Architecture.ServiceLocator;
using TMPro;
using UnityEngine;
using GamePush;
public class CubeRecordView : MonoBehaviour, IService
{
    private int currentRec = 2;
    private int numberChanges = 0;
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI textRec;
    private void Awake()
    {
        window.SetActive(false);
        string a = GP_Player.GetString("maxsize");
        string[] b = new string[2];
        if (!string.IsNullOrEmpty(a))
        {
            b = a.Split(' ');
            currentRec = Int32.Parse(b[0]);
            numberChanges = Int32.Parse(b[1]); 
        }
    }

    public void SetNewRecord(int rec)
    {
        if(rec <= currentRec)
            return;
        
        window.SetActive(true);
        numberChanges++;
        currentRec = rec;
        textRec.text = $"Новая фигура: {currentRec}";
       
        Invoke(nameof(Deactivate), 1);
    }

    private void Deactivate()
    {
        window.SetActive(false);
    }

    public void Save()
    {
        string a = $"{currentRec} {numberChanges}";
        GP_Player.Set("maxsize", a);
    }

    public int GetNumberChanges() => numberChanges;
    
   
}
