using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Trap
{
    public class FallTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onFalling;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health health))
            {
                _onFalling?.Invoke();
               health.Dead();
            }
        }
    }
}