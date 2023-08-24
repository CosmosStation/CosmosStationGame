using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Interactables
{
    public class AbsorbItem : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particle;

        [SerializeField] 
        private GameObject player;
        [SerializeField] 
        private EnergyManager playerEnergyManager;

        private string _state;
        private List<Tween> _tweens;
        
        private void Start()
        {
            _state = "idle";
        }

        public void Absorb(List<Tween> tweens)
        {
            _state = "absorbing";
            
            var pulsingTween = tweens.Find((tween) => tween.stringId == "pulsing");
            pulsingTween.Pause();
            
            var absorbingTween = tweens.Find((tween) => tween.stringId == "absorbing");
            absorbingTween.Play();

            particle.Play();
            playerEnergyManager.increaseMode = true;
        }

        private void Update()
        {
            if (_state == "absorbing")
            {
                particle.transform.LookAt(player.transform);
                if (particle.isStopped)
                {
                    playerEnergyManager.increaseMode = false;        
                    Destroy(gameObject);
                }
            }
        }
    }
        
}
