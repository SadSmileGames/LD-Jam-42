using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private Node[,] grid;
    private float nodeDiameter;
    private int gridSizeX;
    private int gridSizeY;


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        Node playerNode = NodeFromWorldPoint(TransformConversion.Convert2Vector2(player.position));

        if(grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (node == playerNode)
                    Gizmos.color = Color.blue;
                
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        Vector2 bottomLeft = new Vector2(worldBottomLeft.x, worldBottomLeft.y);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D[] hits;
                hits = Physics2D.CircleCastAll(worldPoint, nodeRadius, Vector2.zero, 0, unwalkableMask);

                foreach (RaycastHit2D hit in hits)
                {
                    Debug.Log(hit.collider.name);
                }

                bool walkable = (hits.Length == 0);

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours (Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public Vector2 GetRandomNodePosition()
    {
        int randomX = Random.Range(0, gridSizeX - 1);
        int randomY = Random.Range(0, gridSizeY - 1);

        if (grid[randomX, randomY].walkable)
            return grid[randomX, randomY].worldPosition;
        else
            GetRandomNodePosition();

        return grid[randomX, randomY].worldPosition;
    }
}
