using Game.Core.Enums;
using UnityEngine;

namespace Game.Managers
{
    public class SpriteFactory : MonoBehaviour, IProvidable
    {
        [SerializeField] private Sprite AppleSprite;
        [SerializeField] private Sprite PortalSprite;

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        public Sprite GetTexture(InteractableObjectType interactableObjectType)
        {
            return interactableObjectType switch
            {
                InteractableObjectType.EMPTY => null,
                InteractableObjectType.WALL => null,
                InteractableObjectType.PORTAL => PortalSprite,
                InteractableObjectType.APPLE => AppleSprite,
                _ => null
            };
        }
    }
}