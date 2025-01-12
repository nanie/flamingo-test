using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.Player
{
    public class PlayerView : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;
        [SerializeField] private float _movementDuration = 1f;
        [SerializeField] private float _pauseInterval = 1f;
        [SerializeField] private Ease _easing = Ease.InOutSine;

        private void OnEnable()
        {
            _playerMovement.OnPlayerStartMovement += OnMove;
        }

        private void OnDisable()
        {
            _playerMovement.OnPlayerStartMovement -= OnMove;
        }

        private void OnMove(Vector3[] path)
        {
            Sequence sequence = DOTween.Sequence();

            foreach (Vector3 position in path)
            {
                sequence.Append(transform.DOMove(position, _movementDuration).SetEase(_easing));
                sequence.AppendInterval(_pauseInterval);
            }
            sequence.OnComplete(OnCompleteMovement);
        }

        private void OnCompleteMovement()
        {
            _playerMovement.FinishMovement();
        }
    }
}
