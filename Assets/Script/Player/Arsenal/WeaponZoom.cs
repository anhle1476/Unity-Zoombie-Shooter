using Script.Base.Constants;
using UnityEngine;

namespace Script.Player.Arsenal
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponZoom : MonoBehaviour
    {
        [SerializeField]
        private float zoomInPOV = 20f;

        [SerializeField]
        private float zoomOutPOV = 60f;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            WeaponZoomOut();
        }

        private void Update()
        {
            if (Input.GetButton(InputConst.ZOOM))
            {
                WeaponZoomIn();
            }
            else
            {
                WeaponZoomOut();
            }
        }

        public void WeaponZoomIn()
        {
            _camera.fieldOfView = zoomInPOV;
        }

        public void WeaponZoomOut()
        {
            _camera.fieldOfView = zoomOutPOV;
        }


    }
}