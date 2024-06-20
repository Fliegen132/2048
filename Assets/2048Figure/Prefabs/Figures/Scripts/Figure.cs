using System.Collections;
using _2048Figure.Architecture.ServiceLocator;
using _2048Figure.UI;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace _2048.Figures
{
    public class Figure : MonoBehaviour
    {
        private AudioSource _audioSource => GetComponent<AudioSource>();
        private FigureTexture _figureTexturing;
        private FigureData _figureData;
        private ScoreView _scoreView;
        private bool _wasCollised = false;
        private Figure _collidedFigure;
        private CubeRecordView _cubeRecordView;
        private bool _useSkins = true;
        private float _resizeValue = 0.05f;
        private AudioClip _clip;
        [SerializeField] private Color color = new Color(1, 0.7764707f, 0.2431373f, 1);
        
        private void Awake()
        {
            _scoreView = ServiceLocator.current.Get<ScoreView>();
            _figureTexturing = ServiceLocator.current.Get<FigureTexture>();
            _cubeRecordView = ServiceLocator.current.Get<CubeRecordView>();

            if(_useSkins)
                _figureTexturing.UseSprites(this); //if u need 
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Figure") && !_wasCollised)
            {
                _collidedFigure = collision.contacts[0].collider.gameObject.GetComponent<Figure>();
                CheckCollision(_collidedFigure);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Figure") && !_wasCollised)
            {
                _collidedFigure = collision.contacts[0].collider.gameObject.GetComponent<Figure>();
                CheckCollision(_collidedFigure);
            }
        }

        private void CheckCollision(Figure otherFigure)
        {
            if (otherFigure != null)
            {
                FigureData otherFigureData = otherFigure.GetData();

                if (otherFigureData.CurrentOrder < _figureData.CurrentOrder &&
                    otherFigureData.CurrentSize == _figureData.CurrentSize)
                {
                    _wasCollised = true;
                    
                    otherFigureData.CurrentSize *= 2;
                    otherFigure.transform.Find("Canvas/SizeText").GetComponent<TextMeshProUGUI>().text =
                        $"{otherFigureData.CurrentSize}";
                    otherFigure.GetComponent<Rigidbody2D>().AddForce(transform.up * 5, ForceMode2D.Impulse);
                    if(otherFigure.transform.localScale.x < 1)
                        otherFigure.transform.localScale = new Vector3(otherFigure.transform.localScale.x +  _resizeValue, otherFigure.transform.localScale.y + _resizeValue);
                    otherFigure._audioSource.Play();
                    _scoreView.SetScore(otherFigureData.CurrentSize);
                    if(_useSkins)
                        _figureTexturing.ChangeSprite(otherFigure);  //=> or change sprite
                    else
                        _figureTexturing.ChangeColor(otherFigure);  //=> or change sprite
                    Invoke(nameof(WaitCollised), 0.5f);
                    _cubeRecordView.SetNewRecord(otherFigureData.CurrentSize);
                    
                    
                    SetDefault();
                }
            }
        }

        public void AddScoreAfterBoom()
        {
            _scoreView.SetScore(_figureData.CurrentSize);
        }

        public void SetDefault()
        {
            _figureData.IsActive = false;
            _figureData.CurrentSize = 2;
            if (_useSkins)
                gameObject.GetComponent<SpriteRenderer>().sprite = _figureTexturing._skins[0];
            else
                gameObject.GetComponent<SpriteRenderer>().color = color;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            transform.position = transform.parent.position;
            transform.rotation = Quaternion.Euler(0,0,0);
            gameObject.SetActive(false);
        }
        
        public void UpdateFigure()
        {
            int a = Random.Range(0, _cubeRecordView.GetNumberChanges());
            for (int i = 0; i < a; i++)
            {
                if(transform.localScale.x < 1)
                    transform.localScale = new Vector3(transform.localScale.x +  _resizeValue, transform.localScale.y + _resizeValue);
                _figureData.CurrentSize *= 2;
                if(_useSkins)
                    _figureTexturing.ChangeSprite(this);  //=> or change sprite
                else
                    _figureTexturing.ChangeColor(this);  //=> or change sprite
            }
            transform.Find("Canvas/SizeText").GetComponent<TextMeshProUGUI>().text =
                $"{_figureData.CurrentSize}";
        }
        
        public void LoadFigure(int size)
        {
            while (size > 2)
            {
                if(transform.localScale.x < 1)
                    transform.localScale = new Vector3(transform.localScale.x +  _resizeValue, transform.localScale.y + _resizeValue);
                if(_useSkins)
                    _figureTexturing.ChangeSprite(this);  //=> or change sprite
                else
                    _figureTexturing.ChangeColor(this);  //=> or change sprite
                size /= 2;
            }
                
            transform.Find("Canvas/SizeText").GetComponent<TextMeshProUGUI>().text =
                $"{_figureData.CurrentSize}";
        }
        
        private void WaitCollised()
        {
            _wasCollised = false;
        }


        public void Init(string _name)
        {
            _figureData = new FigureData { Name = _name};
            transform.position = transform.parent.position;
        }

        

        public FigureData GetData() => _figureData;
    }
}
