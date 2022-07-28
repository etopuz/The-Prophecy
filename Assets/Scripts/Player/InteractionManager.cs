using UnityEngine;
using TheProphecy.Interfaces;

namespace TheProphecy.Interactions
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private int _rayLength = 20;
        [SerializeField] private LayerMask _interactableLayer;

        private Camera _mainCamera;
        private Touch _touch;
        private Vector2 _touchStartPos;
        private Ray _ray;
        private RaycastHit2D _hit;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            InteractWithTouch();
        }

        private void InteractWithTouch()
        {
            if (Input.touchCount > 0)
            {

                _touch = Input.GetTouch(0);
                if (_touch.phase == TouchPhase.Began)
                {
                    _touchStartPos = _touch.position;
                    _ray = _mainCamera.ScreenPointToRay(_touchStartPos);

                    if (_hit = Physics2D.Raycast(_ray.origin, _ray.direction, _rayLength, _interactableLayer))
                    {
                        IInteractable interactable = _hit.collider.GetComponent<IInteractable>();

                        if (interactable != null)
                        {
                            interactable.OnInteract();
                        }
                    }

                }
            }
        }

    }

}
