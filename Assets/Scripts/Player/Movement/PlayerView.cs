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
        [SerializeField] private ParticleSystem _particleSimple;
        [SerializeField] private ParticleSystem _particleComplex;
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            _playerMovement.OnPlayerStartMovement += OnMove;
            _playerMovement.OnPlayerTurnEnded += OnTurnEnded;
        }

        private void OnDisable()
        {
            _playerMovement.OnPlayerStartMovement -= OnMove;
            _playerMovement.OnPlayerTurnEnded -= OnTurnEnded;
        }

        private void OnMove(Vector3[] path)
        {
            _animator.SetBool("Landing", false);
            _animator.SetBool("IsMoving", true);
           
            Sequence sequence = DOTween.Sequence();
            float jumpHeight = 0.5f;
            sequence.Pause();
            Vector3 lastPosition = transform.position;
            foreach (Vector3 position in path)
            {
                Quaternion direction = Quaternion.LookRotation(position - lastPosition, Vector2.up).normalized;
                if (direction.eulerAngles != _cameraPivot.forward)
                {
                    sequence.Join(_cameraPivot.DORotateQuaternion(direction, _movementDuration).SetEase(Ease.Linear));
                }
                if (Vector3.Distance(lastPosition, position) > 0.01f)
                {
                    Vector3 midPoint = Vector3.Lerp(lastPosition, position, 0.5f);
                    midPoint.y += jumpHeight;            
                    sequence.Append(transform.DOMove(midPoint, _movementDuration / 2).SetEase(Ease.OutQuad));
                    sequence.AppendCallback(() => _animator.SetBool("Landing", true));
                    sequence.Append(transform.DOMove(position, _movementDuration / 2).SetEase(Ease.InQuad));
                    sequence.AppendCallback(() => _animator.SetBool("Landing", false));
                    lastPosition = position;
                }

            }
            sequence.OnComplete(OnCompleteMovement).SetEase(_easing);
            sequence.Play();
        }

        private void OnCompleteMovement()
        {
            _animator.SetBool("Landing", true);
            _animator.SetBool("IsMoving", false);   
            _playerMovement.FinishMovement();
        }
        private void OnTurnEnded(bool playedMinigame, int score)
        {
            if (!playedMinigame)
            {
                _particleSimple.Play();
            }

            if (playedMinigame && score > 0)
            {
                _particleComplex.Play();
                _animator.SetTrigger("Celebrate");
            }
        }
    }
}
