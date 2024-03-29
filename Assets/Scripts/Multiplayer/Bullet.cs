﻿using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class Bullet : MonoBehaviour
    {
        [Header("Explosion")]
        [SerializeField] private GameObject _bulletExplosion;

        [Header("Movement speed")]
        [SerializeField] private float _speed;
        
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PhotonView _photonView;

        [TagSelector] 
        [SerializeField] private string _soundPlayer;
        
        private bool _exploded;

        private void Start()
        {
            SoundPlayer soundPlayer = GameObject.FindWithTag(_soundPlayer).GetComponent<SoundPlayer>();
            soundPlayer.Play(SoundNames.PlayerBullet);
        }
        
        private void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {        
            if (_exploded) { return; }

            if (other.CompareTag("Ground") || other.gameObject.CompareTag("Lava"))
            {
                Explode();
            }

            if (other.CompareTag("Player"))
            {
                if (Equals(_photonView.Owner, other.GetComponent<PhotonView>().Owner)) return;
                
                PhotonNetwork.Instantiate(_bulletExplosion.name, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        public void Explode()
        {
            if (!_photonView.IsMine) return;
            
            _exploded = true;
            gameObject.SetActive(false);

            PhotonNetwork.Instantiate(_bulletExplosion.name, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
