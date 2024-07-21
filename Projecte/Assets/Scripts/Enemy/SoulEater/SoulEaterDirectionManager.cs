using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoulEaterDirectionManager : MonoBehaviour
{
    enum Dir
    {
        UP = 1, DOWN = 2, LEFT = 4, RIGHT = 8
    }
    struct Direction
    {
        public Vector3 vector;
        public Dir toOpen;
        public Dir toClose;

        public Direction(Vector3 vector, Dir toOpen, Dir toClose)
        {
            this.vector = vector;
            this.toOpen = toOpen;
            this.toClose = toClose;
        }
    }
    private static readonly float SQRT2 = Mathf.Sqrt(2)/2f;
    private static readonly Vector3 UP_LEFT = new Vector3(-SQRT2, 0, SQRT2);
    private static readonly Vector3 DOWN_LEFT = new Vector3(-SQRT2, 0, -SQRT2);
    private static readonly Vector3 DOWN_RIGHT = new Vector3(SQRT2, 0, -SQRT2);
    private static readonly Vector3 UP_RIGHT = new Vector3(SQRT2, 0, SQRT2);
    private static readonly Direction[] DIRECTIONS = new Direction[] {
        new Direction(UP_LEFT, Dir.DOWN|Dir.RIGHT, Dir.UP|Dir.LEFT),
        new Direction(Vector3.left, Dir.RIGHT, Dir.LEFT),
        new Direction(DOWN_LEFT, Dir.UP|Dir.RIGHT, Dir.DOWN|Dir.LEFT),
        new Direction(DOWN_RIGHT, Dir.UP|Dir.LEFT, Dir.DOWN|Dir.RIGHT),
        new Direction(Vector3.right, Dir.LEFT, Dir.RIGHT),
        new Direction(UP_RIGHT, Dir.DOWN|Dir.LEFT, Dir.UP|Dir.RIGHT)
    };

    private SoulEaterController controller;
    private Collider dragonCollider;
    private Dir openDirs;
    private List<Direction> openDirections;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<SoulEaterController>();
        dragonCollider = GetComponent<Collider>();
        InitDirections();
    }

    private void InitDirections()
    {
        openDirs = Dir.UP|Dir.DOWN|Dir.LEFT|Dir.RIGHT;
        openDirections = new List<Direction>(DIRECTIONS.Length);

        float distance = (controller.stateTimeBase + controller.stateTimeOffset)*controller.linearSpeed;
        foreach (Direction direction in DIRECTIONS)
        {
            if (Physics.BoxCast(dragonCollider.bounds.center, 1.1f*dragonCollider.bounds.extents, direction.vector,
            controller.transform.rotation, distance))
            {
                openDirs &= ~direction.toClose;
            }
            else
            {
                openDirections.Add(direction);
            }
        }
    }

    public Vector3 NewDirection()
    {
        int index = UnityEngine.Random.Range(0, openDirections.Count);
        Direction newDirection = openDirections[index];
        openDirs &= ~newDirection.toClose;
        openDirs |= newDirection.toOpen;
        openDirections = Array.FindAll(DIRECTIONS, direction => (direction.toClose & ~openDirs) == 0).ToList();
        return newDirection.vector;
    }
}
