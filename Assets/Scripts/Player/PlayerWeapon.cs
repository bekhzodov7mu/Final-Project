using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{    
    [SerializeField] private Transform parent = null, barrel = null;        
    [SerializeField] private float reloadTime = 0;

    private Joystick _fixedjoystick = null;
    private RectTransform _joystickHandle = null;
    private float _offset = 180;
    
    private bool _reloaded;

    //Object Pooler
    Pooler pooler;

    private void Awake()
    {
        _fixedjoystick = GameObject.Find("CanvasUI/RotationJoystick").GetComponent<FixedJoystick>();
        _joystickHandle = GameObject.Find("CanvasUI/RotationJoystick/Handle").GetComponent<RectTransform>();
    }

    private void Start()
    {
        _reloaded = true;
        pooler = Pooler.Instance;
    }

    private void Update()
    {
        WeaponPosition();

        Shooting();
    }

    private void WeaponPosition()
    {
        //Change position depending on mouse position
        if (_joystickHandle.anchoredPosition.x < 0 && _joystickHandle.anchoredPosition.x != 0)//(mouse.x < playerScreenPoint.x) 
        {
            LeftSide();
        }
        else if (_joystickHandle.anchoredPosition.x > 0)
        {
            RightSide();
        }
    }

    private void Shooting()
    {
        //Shoot
        if (_reloaded)
        {
            //Pooler          
            pooler.GetPooledObject("PlayerBullet", barrel.position, barrel.rotation);            

            _reloaded = false;
            Invoke("Reload", reloadTime);
        }
    }

    private void Reload()
    {
        _reloaded = true;
    }

    private void RightSide()
    {
        Vector3 direction = Vector3.up * _fixedjoystick.Vertical + Vector3.right * _fixedjoystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (rotZ != 0)
        { transform.rotation = Quaternion.Euler(0f, 0f, -rotZ); }

        transform.position = new Vector3(parent.transform.position.x + 0.3f, parent.transform.position.y, parent.transform.position.z);
    }

    private void LeftSide()
    {
        Vector3 direction = Vector3.up * _fixedjoystick.Vertical + Vector3.right * _fixedjoystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (rotZ != 0)
        { transform.rotation = Quaternion.Euler(-180f, 0f, rotZ + _offset); }

        transform.position = new Vector3(parent.transform.position.x -0.3f, parent.transform.transform.position.y, parent.transform.position.z);
    }
}
