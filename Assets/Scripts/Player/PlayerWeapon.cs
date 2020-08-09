using UnityEngine;

namespace PlayerOfflineScipts
{
    public class PlayerWeapon : BaseWeapon
    {
        [Header("Scripts")]
        [SerializeField] private SoundPlayer _soundPlayer;        

        private void Start()
        {
            _reloadTime = 0.3f;
            _reloaded = true;
        }

        private void OnEnable()
        {
            OnShoot += PlayShootSound;
        }

        private void OnDisable()
        {
            OnShoot -= PlayShootSound;
        }

        private void Update()
        {        
            Shoot();
        }

        private void PlayShootSound()
        {
            _soundPlayer.Play(SoundNames.PlayerBullet);
        }
    }
}