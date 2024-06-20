using _2048Figure.Architecture.ServiceLocator;
using _2048Figure.UI;
using _2048Figure.User;
using GamePush;
using UnityEngine;

namespace _2048.Figures
{
    public class BombBeh : MonoBehaviour
    {
        private GameObject _collider;
        private EnableShopp _enableShopp;
        private GameObject _particle;
        private void Start()
        {
            _particle = Resources.Load<GameObject>("Particle/Boom");
            _collider = transform.Find("BoomCollider").gameObject;
            _collider.SetActive(false);
            _enableShopp = GameObject.FindObjectOfType<EnableShopp>();
          
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Figure>())
            {
                Boom();
            }
        }
        
        private void Boom()
        {
            _collider.SetActive(true);
            _enableShopp.wasBuy = false;
            var particleSystem = Instantiate(_particle, transform.position, Quaternion.identity);
            particleSystem.GetComponent<ParticleSystem>().Play();
            Invoke(nameof(SetDefault), 0.1f);
        }

        

        private void SetDefault()
        {
            transform.position = transform.parent.position;
            transform.rotation = Quaternion.identity;
            transform.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.SetActive(false);
        }
    }
}