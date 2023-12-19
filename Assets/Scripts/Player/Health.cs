using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField]private int _health = 5;
        public Action Dying;
        private void GetDamage(int damage)
        {
            _health=_health-damage;
        }

        private void AddHealth(int health)
        {
            _health=_health+health;
        }

        public void Dead()
        {
            _health = 0;
            Dying?.Invoke();
            SceneManager.LoadScene(0);
        }
    }
}