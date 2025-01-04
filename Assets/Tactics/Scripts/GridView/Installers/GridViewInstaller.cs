using Tactics.GridView.Services;
using UnityEngine;
using Zenject;

namespace Tactics.GridView.Installers
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
            Container.BindInterfacesTo<GridSpawner>().AsSingle().WithArguments(gridSize, tileSize, tilePrefab, tilesRoot);
        }
    }
}