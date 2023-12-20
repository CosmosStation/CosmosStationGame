using UnityEngine;
using UnityEngine.Events;

namespace Executors
{
    public class IfExecutor : Executor
    {
        [SerializeField] bool IsAbleToExecute = false;
        [SerializeField] bool _destroyObjects,
            _enableOrDisableObjs;
        [SerializeField] UnityEvent OnExecuted;

        public override void Execute(float signal)
        {
            if (_destroyObjects) Destroy(gameObject);
            else if (_enableOrDisableObjs) gameObject.SetActive(signal >= 1);

            if (IsAbleToExecute)
            {
                OnExecuted?.Invoke();
            }
        }

        public void EnableExecute()
        {
            IsAbleToExecute = true;
        }

        public void DisableExecute()
        {
            IsAbleToExecute = false;
        }
    }
}