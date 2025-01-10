using Tactics.GameGrid.Implementation.Services;
using Zenject;

namespace Tactics.GameGrid.Sandbox.Services
{
    public class DebugTileIndexes : IInitializable
    {
        [Inject]
        GameGridSpawner _gameGridSpawner;
        
        public void Initialize()
        {
            EnterDebugIndexes();
        }

        private void EnterDebugIndexes()
        {
            var nodes = _gameGridSpawner.Grid.Graph.Nodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                var tileText = $"{nodes[i].Content.XIndex} : {nodes[i].Content.YIndex}";
                nodes[i].Content.GameTileComponent.SetTileText(tileText);
            }
        }
    }
}