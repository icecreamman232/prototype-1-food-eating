using System;
using System.Collections.Generic;
using JustGame.Script.Data;
using JustGame.Script.Helper;
using JustGame.Script.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JustGame.Script.Player
{
    public enum FacingDirection
    {
        RIGHT,
        LEFT,
    }

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_LEFT,
        UP_RIGHT,
        DOWN_LEFT,
        DOWN_RIGHT,
        
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float m_moveLength;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private FacingDirection m_facingDirection;
        [SerializeField] private Vector2 m_direction;
        [SerializeField] private float m_obstacleRayCastLenght;
        
        [Header("References")]
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private BoxCollider2D m_collider2D;
        [SerializeField] private LayerScriptableData m_layerScriptableData;

        private List<RaycastHit2D> m_verticalRayCastHit;
        private List<RaycastHit2D> m_horizontalRayCastHit;

        private Direction dir;
        private MainInputAction m_inputAction;

        private void Start()
        {
            m_inputAction = new MainInputAction();
            m_inputAction.Enable();
            m_inputAction.PC.Move.performed += MovementPerform;
            m_inputAction.PC.Move.canceled += MovementCancel;

            m_facingDirection = FacingDirection.RIGHT;

            m_verticalRayCastHit = new List<RaycastHit2D>();
            m_horizontalRayCastHit = new List<RaycastHit2D>();
        }
        
        private void MovementPerform(InputAction.CallbackContext context)
        {
            m_direction =  context.ReadValue<Vector2>();
        }
        
        private void MovementCancel(InputAction.CallbackContext context)
        {
            m_direction =  context.ReadValue<Vector2>();
        }
        
        private void OnDestroy()
        {
            m_inputAction.Disable();
        }
        
        
        private void Update()
        {
            if (m_direction == Vector2.zero) return;
            
            if (HitObstacle())
            {
                m_direction = Vector2.zero;
                return;
            }

            FlipPlayer();
            
            transform.position += (Vector3)(m_direction * (Time.deltaTime * m_moveSpeed));
        }

        private void FlipPlayer()
        {
            if (m_direction.x > 0 && m_facingDirection == FacingDirection.LEFT)
            {
                m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
                m_facingDirection = FacingDirection.RIGHT;
            }
            else if(m_direction.x < 0 && m_facingDirection == FacingDirection.RIGHT)
            {
                m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
                m_facingDirection = FacingDirection.LEFT;
            }
        }

        private bool HitObstacle()
        {
            var bounds = m_collider2D.bounds;
            
            var topOrigin = new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y);
            var botOrigin = new Vector2(bounds.center.x, bounds.center.y - bounds.extents.y);
            var leftOrigin = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y);
            var rightOrigin = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y);
            var topLeftOrigin = new Vector2(bounds.min.x, bounds.max.y);
            var topRightOrigin = bounds.max;
            var botLeftOrigin = bounds.min;
            var botRightOrigin = new Vector2(bounds.max.x, bounds.min.y);

            RaycastHit2D top;
            RaycastHit2D bot;
            RaycastHit2D left;
            RaycastHit2D right;
            RaycastHit2D topLeft;
            RaycastHit2D topRight;
            RaycastHit2D botLeft;
            RaycastHit2D botRight;

            if (m_direction is { x: >= 1, y: 0 })
            {
                dir = Direction.RIGHT;
            }
            else if (m_direction is { x: <= -1, y: 0 })
            {
                dir = Direction.LEFT;
            }
            else if (m_direction is { x: 0, y: >= 1 })
            {
                dir = Direction.UP;
            }
            else if (m_direction is { x: 0, y: <= -1 })
            {
                dir = Direction.DOWN;
            }
            else if (m_direction is { x:-1, y:1 })
            {
                dir = Direction.UP_LEFT;
            }
            else if (m_direction is { x:1, y:1 })
            {
                dir = Direction.UP_RIGHT;
            }
            else if (m_direction is { x:-1, y:-1 })
            {
                dir = Direction.DOWN_LEFT;
            }
            else if (m_direction is { x:1, y:-1 })
            {
                dir = Direction.DOWN_RIGHT;
            }

            bool hasObstacle = false;
            
            switch (dir)
            {
                case Direction.UP:
                    top = DebugHelper.RayCast(topOrigin, Vector2.up, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    topLeft = DebugHelper.RayCast(topLeftOrigin, Vector2.up, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    topRight = DebugHelper.RayCast(topRightOrigin, Vector2.up, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = top.collider!=null || topLeft.collider!=null || topRight.collider!=null;
                    break;
                case Direction.DOWN:
                    bot = DebugHelper.RayCast(botOrigin, Vector2.down, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    botLeft = DebugHelper.RayCast(botLeftOrigin, Vector2.down, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    botRight = DebugHelper.RayCast(botRightOrigin, Vector2.down, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = bot.collider!=null || botLeft.collider!=null || botRight.collider!=null;
                    break;
                case Direction.LEFT:
                    left = DebugHelper.RayCast(leftOrigin, Vector2.left, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    topLeft = DebugHelper.RayCast(topLeftOrigin, Vector2.left, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    botLeft = DebugHelper.RayCast(botLeftOrigin, Vector2.left, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = left.collider!=null || topLeft.collider!=null || botLeft.collider!=null;
                    break;
                case Direction.RIGHT:
                    right = DebugHelper.RayCast(rightOrigin, Vector2.right, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    topRight = DebugHelper.RayCast(topRightOrigin, Vector2.right, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    botRight= DebugHelper.RayCast(botRightOrigin, Vector2.right, m_obstacleRayCastLenght,
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = right.collider!=null || topRight.collider!=null || botRight.collider!=null;
                    break;
                case Direction.UP_LEFT:
                    top = DebugHelper.RayCast(topOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    topLeft = DebugHelper.RayCast(topLeftOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    left = DebugHelper.RayCast(leftOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = top.collider!=null || topLeft.collider!=null || left.collider!=null;
                    break;
                case Direction.UP_RIGHT:
                    top = DebugHelper.RayCast(topOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    topRight = DebugHelper.RayCast(topRightOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    right = DebugHelper.RayCast(rightOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = top.collider!=null || topRight.collider!=null || right.collider!=null;
                    break;
                case Direction.DOWN_LEFT:
                    bot = DebugHelper.RayCast(botOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    botLeft = DebugHelper.RayCast(botLeftOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    left = DebugHelper.RayCast(leftOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = bot.collider!=null || botLeft.collider!=null || left.collider!=null;
                    break;
                case Direction.DOWN_RIGHT:
                    bot = DebugHelper.RayCast(botOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    botRight = DebugHelper.RayCast(botRightOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    right = DebugHelper.RayCast(rightOrigin,m_direction,m_obstacleRayCastLenght, 
                        m_layerScriptableData.ObstacleMask, Color.red, true);
                    hasObstacle = bot.collider!=null || botRight.collider!=null || right.collider!=null;
                    break;
            }

            switch (dir)
            {
                case Direction.UP:
                    break;
                case Direction.DOWN:
                    break;
                case Direction.LEFT:
                    break;
                case Direction.RIGHT:
                    break;
                case Direction.UP_LEFT:
                    break;
                case Direction.UP_RIGHT:
                    break;
                case Direction.DOWN_LEFT:
                    break;
                case Direction.DOWN_RIGHT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return hasObstacle;
        }
    }
}

