using System;
using _2048Figure.Architecture;
using _2048Figure.Architecture.Scripts;
using _2048Figure.Architecture.ServiceLocator;
using GamePush;
using UnityEngine;

namespace _2048Figure.User
{
    public class UserInput : MonoBehaviour, IService
    {
        [SerializeField] private int currentActiveFigure;
        [SerializeField] private float xPos;
        [SerializeField] private EndGameView endGameView;
        [SerializeField] private int currentOrder;
        [SerializeField] private Transform helpLine;
        private GameObject currentFigure;
        private Vector3 touchStartPos;
        private bool canTouch = true;
        private bool _gameStart;
        private SaveLoad _saveLoad;
        private bool wasSet;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            currentActiveFigure = GP_Player.GetInt("currentfigure");
            currentOrder = GP_Player.GetInt("currentorder");
            endGameView = ServiceLocator.current.Get<EndGameView>();
            _saveLoad = ServiceLocator.current.Get<SaveLoad>();
            
            helpLine.gameObject.SetActive(true);
            xPos = 1.94f;
            Debug.Log(Pools.figures.Count);
            if (!_saveLoad.LoadActiveFigures())
            {
                UpdateCurrentFigure();
            }
            _saveLoad.LoadTransforms();
            currentFigure = Pools.figures[currentActiveFigure].gameObject;
            Invoke(nameof(SetGameStart), 0.5f);
        }
        bool canUp = false;
        private void Update()
        {
           
            if (endGameView == null || endGameView.end || Pools.figures.Count == 0 || _gameStart == false)
            {
                return;
            }
            MainFiguresBeh();
            
        }

        private void MainFiguresBeh()
        {
            if (Input.GetMouseButton(0) && canTouch)
            {
                float distance = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - currentFigure.transform.position.x;
                float newX = Mathf.Clamp(currentFigure.transform.position.x + distance, -xPos, xPos);
                if (CheckTouchPos())
                {
                    currentFigure.transform.position = new Vector3(newX, currentFigure.transform.position.y, currentFigure.transform.position.z);
                    helpLine.transform.position = new Vector3(newX, helpLine.transform.position.y);
                    canUp = true;
                }
            }
            if (Input.GetMouseButtonUp(0) && canTouch)
            {
                if(canUp == false)
                    return;
                canTouch = false;
                currentFigure.GetComponent<Rigidbody2D>().simulated = true;
                currentOrder++;
                GP_Player.Set("currentorder", currentOrder);
                helpLine.transform.position = new Vector3(0, helpLine.transform.position.y);
                helpLine.gameObject.SetActive(false);
                canUp = false;
                wasSet = false;
                FindFigure();
                Invoke(nameof(UpdateCurrentFigure), 0.5f);
            }
        }

        private bool CheckTouchPos()
        {
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x  > 2.5f || Camera.main.ScreenToWorldPoint(Input.mousePosition).x  < -2.5f)
                return false;

            return true;
        }
        
        public void FindFigure()
        {
            if (!Pools.figures[currentActiveFigure].GetData().IsActive)
            {
                currentFigure = Pools.figures[currentActiveFigure].gameObject;
                return;
            }
            while (Pools.figures[currentActiveFigure].GetData().IsActive)
            {
                currentActiveFigure++;
                if (currentActiveFigure >= Pools.figures.Count)
                {
                    currentActiveFigure = 0;
                }
                currentFigure = Pools.figures[currentActiveFigure].gameObject;
            }
        }

        public void UpdateCurrentFigure()
        {
            helpLine.gameObject.SetActive(true);
            canTouch = true;
            if(wasSet)
                return;
            Pools.figures[currentActiveFigure].UpdateFigure();
            Pools.figures[currentActiveFigure].gameObject.SetActive(true);
            Pools.figures[currentActiveFigure].GetData().IsActive = true;
            Pools.figures[currentActiveFigure].GetData().CurrentOrder = currentOrder;
        }
        
        public void Save()
        {
            GP_Player.Set("currentfigure", currentActiveFigure);
            foreach (var figure in Pools.figures)
            {
                TransformData transformData = new TransformData
                {
                    position = figure.transform.position,
                    rotation = figure.transform.rotation,
                    currentSize = figure.GetData().CurrentSize,
                    currentOrder = figure.GetData().CurrentOrder
                };

                _saveLoad.SaveTransform(transformData);
            }
            _saveLoad.SaveActiveFigures();
        }

        public void SetCurrentFigure(GameObject newFigure)
        {
            wasSet = true;
            currentFigure.SetActive(false);
            Pools.figures[currentActiveFigure].GetData().IsActive = false;
            currentFigure = newFigure;
            currentFigure.SetActive(true);
        }

        public void SetGameStart()
        {
            _gameStart = true;
        }
    }
}