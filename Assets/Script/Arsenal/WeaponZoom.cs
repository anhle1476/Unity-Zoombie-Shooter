using Script.Base.Constants;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Script.Arsenal
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponZoom : MonoBehaviour
    {
        [SerializeField]
        private float zoomInPOV = 20f;

        [SerializeField]
        private float zoomOutPOV = 60f;

        [SerializeField]
        private float zoomInMouseXSensitivity = 2f;

        [SerializeField]
        private float zoomInMouseYSensitivity = 2f;

        [SerializeField]
        private float zoomOutMouseXSensitivity = 2f;

        [SerializeField]
        private float zoomOutMouseYSensitivity = 2f;


        private Camera _camera;
        private RigidbodyFirstPersonController _fpController;

        private void Awake()
        {
            _camera = Camera.main;
            _fpController = FindObjectOfType<RigidbodyFirstPersonController>();
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
            ChangeMouseSensitivity(zoomInMouseXSensitivity, zoomInMouseYSensitivity);
        }

        public void WeaponZoomOut()
        {
            _camera.fieldOfView = zoomOutPOV;
            ChangeMouseSensitivity(zoomOutMouseXSensitivity, zoomOutMouseYSensitivity);
        }

        private void ChangeMouseSensitivity(float xSensity, float ySensity)
        {
            _fpController.mouseLook.XSensitivity = xSensity;
            _fpController.mouseLook.YSensitivity = ySensity;
        }
    }
}