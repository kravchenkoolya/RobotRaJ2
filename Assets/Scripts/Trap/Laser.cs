
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
  [SerializeField] private UnityEvent _onLaserEnter;
  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent(out Player.PlayerController playerController))
    {
      _onLaserEnter?.Invoke();
    }
  }
}
