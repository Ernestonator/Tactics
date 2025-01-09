using Tactics.GameGrid.Implementation.Services;
using UnityEngine;
using Zenject;

namespace Tactics.GameGrid.Installers
{
    public class GridViewInstaller : MonoInstaller
    {
        [SerializeField]
        private int gridSize;
        [SerializeField]
        private float tileSize = 1;
        [SerializeField]
        private GameObject tilePrefab;
        [SerializeField]
        private Transform tilesRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameGridSpawner>().AsSingle().WithArguments(gridSize, tileSize, tilePrefab, tilesRoot);
        }
    }
}