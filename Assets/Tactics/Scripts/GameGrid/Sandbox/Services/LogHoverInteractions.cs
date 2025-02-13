using System;
using Tactics.GameGrid.Implementation.Services;
using Tactics.Raycasting.Data.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tactics.GameGrid.Sandbox.Services
{
    public class LogHoverInteractions : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject]
        private GridInteractions _gridInteractions;

        public void Initialize()
        {
            _gridInteractions.OnTileHoverStart.Subscribe(OnHoverStart).AddTo(_compositeDisposable);
            _gridInteractions.OnTileHoverEnd.Subscribe(OnHoverEnd).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void OnHoverStart(InteractionData interactionData)
        {
            Debug.Log($"OnHoverStart with {interactionData.Interactable.GameObject.name}");
        }

        private void OnHoverEnd(InteractionData interactionData)
        {
            Debug.Log($"OnHoverEnd with {interactionData.Interactable.GameObject.name}");
        }
    }
}
