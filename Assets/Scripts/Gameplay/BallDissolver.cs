using System;
using UnityEngine;

namespace Gameplay
{
    public enum DissolveState
    {
        Dissolve,
        Restore
    }

    public class BallDissolver
    {
        public event Action DissolveComplete;
        private static readonly int Dissolve = Shader.PropertyToID("_DissolveLevel");

        private readonly Material _ballMaterial;
        private bool _canDissolve, _disappear;
        private float _currentTime, _startValue, _endValue;

        public BallDissolver(Material ballMat)
        {
            _ballMaterial = ballMat;
            _canDissolve = false;
        }

        public void StartDissolve(DissolveState desiredState)
        {
            _currentTime = 0;
            _startValue = desiredState == DissolveState.Dissolve ? 0 : 1;
            _endValue = desiredState == DissolveState.Dissolve ? 1 : 0;

            _canDissolve = true;
        }

        public void Update()
        {
            if (!_canDissolve) return;

            _currentTime += Time.deltaTime;
            var value = Mathf.Lerp(_startValue, _endValue, Mathf.Clamp01(_currentTime / 1f));

            _ballMaterial.SetFloat(Dissolve, value);

            if (!(_currentTime >= 1)) return;

            _canDissolve = false;
            DissolveComplete?.Invoke();
        }
    }
}