using Flamingo.Board;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BoardCreationTool : EditorWindow
{
    private static bool _isEditing = false;
    private static List<Board.Tile> _tiles;
    private static Vector3 _cubePosition;
    private static Vector3 _deltaMove = Vector3.zero;
    private const float TILE_SIZE = 0.5f;
    private void OnEnable()
    {
        _cubePosition = Vector3.zero;
    }
    static BoardCreationTool()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }
    private static void OnSceneGUI(SceneView sceneView)
    {
        if (_isEditing)
        {
            Vector3 currentPosition = Vector3.zero;
            foreach (var item in _tiles)
            {
                DrawCube(currentPosition);
                currentPosition = GetPosition(item.Next, currentPosition, TILE_SIZE * 2);
            }
            DrawMoveHandle();
            DrawEditing(sceneView);
        }
        else
        {
            DrawDefault(sceneView);
        }
    }
    private static Vector3 GetPosition(Board.Direction direction, Vector3 position, float distance)
    {
        return direction switch
        {
            Board.Direction.Left => position + new Vector3(0, 0, distance),
            Board.Direction.Right => position + new Vector3(0, 0, -distance),
            Board.Direction.Front => position + new Vector3(-distance, 0, 0),
            Board.Direction.Back => position + new Vector3(distance, 0, 0),
            _ => Vector3.zero
        };
    }
    private static void DrawDefault(SceneView sceneView)
    {
        Handles.BeginGUI();
        DrawButton(StartMapCreation, "Create map", sceneView, 0);
        Handles.EndGUI();
    }
    private static void DrawEditing(SceneView sceneView)
    {
        Handles.BeginGUI();
        DrawButton(ExportMap, "Export map", sceneView, 0);
        DrawButton(() => { _isEditing = false; }, "Cancel", sceneView, 1);
        Handles.EndGUI();
    }
    private static void DrawButton(Action onClick, string label, SceneView sceneView, int index)
    {
        var buttonWidth = 100;
        var buttonHeight = 30;
        var padding = 10;
        var buttonRect = new Rect(
            sceneView.position.width - buttonWidth - padding,
            sceneView.position.height - buttonHeight * 2 - (index * buttonHeight) - padding - (padding * index),
            buttonWidth,
            buttonHeight
        );
        if (GUI.Button(buttonRect, label))
        {
            onClick();
        }
    }
    private static void StartMapCreation()
    {
        _isEditing = true;
        _deltaMove = Vector3.zero;
        _cubePosition = Vector3.zero;
        _tiles = new List<Board.Tile>();
        _tiles.Add(new Board.Tile { });
    }
    public static void DrawCube(Vector3 position)
    {
        Handles.color = Color.yellow;
        if (Event.current.type == EventType.Repaint)
        {
            Handles.CubeHandleCap(
                  0,
                  position,
                  Quaternion.identity,
                  TILE_SIZE,
                  EventType.Repaint
                  );
        }
    }
    public static void DrawMoveHandle()
    {
        var newPosition = SnapToGrid(Handles.PositionHandle(_cubePosition, Quaternion.identity));
        if (newPosition == Vector3.zero)
        {
            return;
        }

        _deltaMove += _cubePosition - newPosition;
        _cubePosition = newPosition;

        if (_deltaMove.magnitude < (TILE_SIZE + 0.2F))
        {
            if (Mathf.Abs(_deltaMove.x) > Mathf.Abs(_deltaMove.z))
            {
                _deltaMove.z = 0;
            }
            else
            {
                _deltaMove.x = 0;
            }
            return;
        }

        int tilesToAdd = 0;
        Board.Direction direction = Board.Direction.Front;

        if (Mathf.Abs(_deltaMove.x) > Mathf.Abs(_deltaMove.z))
        {
            direction = _deltaMove.x > 0 ? Board.Direction.Front : Board.Direction.Back;
            tilesToAdd = Mathf.FloorToInt(Mathf.Abs(_deltaMove.x) / (TILE_SIZE + 0.2F));
        }
        else
        {
            direction = _deltaMove.z < 0 ? Board.Direction.Left : Board.Direction.Right;
            tilesToAdd = Mathf.FloorToInt(Mathf.Abs(_deltaMove.z) / (TILE_SIZE + 0.2F));
        }

        AddTiles(tilesToAdd, direction);
        _deltaMove = Vector3.zero;
    }
    private static void AddTiles(int tilesToAdd, Board.Direction direction)
    {
        if (_tiles.Count == 0)
        {
            Board.Tile newTile = new Board.Tile
            {
                Next = direction,
            };
            _tiles.Add(newTile);
            return;
        }

        _tiles[^1] = new Board.Tile
        {
            Next = direction,
        };
        for (int i = 0; i < tilesToAdd; i++)
        {
            Board.Tile newTile = new Board.Tile
            {
                Next = direction,
            };
            _tiles.Add(newTile);
        }
    }
    private static Vector3 SnapToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x / TILE_SIZE) * TILE_SIZE,
            position.y,
            Mathf.Round(position.z / TILE_SIZE) * TILE_SIZE
        );
    }
    private static void ExportMap()
    {
        Board board = new Board { Tiles = _tiles.ToArray() };
        string json = JsonConvert.SerializeObject(board, Formatting.Indented);

        string path = EditorUtility.SaveFilePanel("Save Map", "", "newMap.json", "json");

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, json);
            Debug.Log($"Map saved to: {path}");
        }
    }
}
