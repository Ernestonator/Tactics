using TMPro;
using UnityEngine;

namespace Tactics.GameGrid.Implementation.Components
{
    public class GameTileComponent : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text tileIndex;
        [SerializeField]
        private MeshRenderer _meshRenderer;

        Color _defaultColor;
        
        private void Awake()
        {
            _defaultColor = _meshRenderer.material.color;
            SetTileText("");
        }

        public void ChangeColor(Color color)
        {
            _meshRenderer.material.color = color;
        }

        public void ResetColor()
        {
            _meshRenderer.material.color = _defaultColor;
        }

        public void SetTileText(string text)
        {
            tileIndex.text = text;
        }
    }
}