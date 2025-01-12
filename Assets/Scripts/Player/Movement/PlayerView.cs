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
        [SerializeField] private Transform _cameraPivot;

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
            sequence.Pause();
            Vector3 lastPostion = transform.position;
            foreach (Vector3 position in path)
            {
                Quaternion direction = Quaternion.LookRotation(position - lastPostion, Vector2.up).normalized;
                if (direction.eulerAngles != _cameraPivot.forward)
                {
                    sequence.Append(_cameraPivot.DORotateQuaternion(direction, _movementDuration).SetEase(_easing));
                }
                sequence.Append(transform.DOMove(position, _movementDuration).SetEase(_easing));
                sequence.AppendInterval(_pauseInterval);
                lastPostion = position;
            }
            sequence.OnComplete(OnCompleteMovement);
            sequence.Play();
        }

        private void OnCompleteMovement()
        {
            _playerMovement.FinishMovement();
        }
    }
}
