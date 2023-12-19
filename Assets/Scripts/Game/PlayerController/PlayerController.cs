using System.Collections;
using Game.Core.Enums;
using Game.Core.SnakeBase;
using Game.Managers;
using Settings;
using UnityEngine;

namespace Game.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputManager InputManager;

        private float _tickTime;
        private CardinalDirection _snakeControlDirection;
        private Snake _snake;

        public void Initialize(Snake snake)
        {
            _snake = snake;
            StartCoroutine(GameTickLoop());
        }

        public void StopController()
        {
            InputManager.DisableInputUi();
            StopAllCoroutines();
        }

        private IEnumerator GameTickLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(GameConfig.TickRate);

                GameTick();
            }
        }

        protected void GameTick()
        {
            var input = InputManager.GetAndConsumeInput();

            var desiredCardinalDirection = input switch
            {
                InputKeyType.FORWARD => CardinalDirection.FORWARD,
                InputKeyType.BACKWARD => CardinalDirection.BACK,
                InputKeyType.RIGHT => CardinalDirection.RIGHT,
                InputKeyType.LEFT => CardinalDirection.LEFT,
                InputKeyType.NONE => _snakeControlDirection,
                _ => _snakeControlDirection
            };

            if (!_snakeControlDirection.IsOppositeOf(desiredCardinalDirection))
            {
                _snakeControlDirection = desiredCardinalDirection;
            }

            _snake.Advance(_snakeControlDirection);
        }
    }
}