using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _2048Figure.Adaptation
{
    public class AdaptationTest : MonoBehaviour
    {
        private void Awake()
        {
            ChangeSize();
        }
    
        private void ChangeSize()
        {
            float width = Screen.width * 2.5f;
            int height = Screen.height;
            if ((int) width < height)
            {
                Camera.main.orthographicSize = 6;
            }
            else
                Camera.main.orthographicSize = 5f;
        }

      
    }
}

