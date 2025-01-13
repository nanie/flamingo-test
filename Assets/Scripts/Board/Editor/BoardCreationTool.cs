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
    private static List<Vector3> _tilePositions;
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
            for (int i = 0; i < _tiles.Count; i++)
            {
                DrawCube(_tilePositions[i], i);
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
        _tilePositions = new List<Vector3>();
        AddFirstTile();
    }
    public static void DrawCube(Vector3 position, int tileIndex)
    {
        Handles.color = _tiles[tileIndex].Minigame.HasValue ? Color.red : Color.yellow;
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
        if (Event.current.type == EventType.MouseDown && (Event.current.button == 0 || Event.current.button == 1))
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Bounds cubeBounds = new Bounds(position, Vector3.one * TILE_SIZE);

            if (cubeBounds.IntersectRay(ray))
            {
                var direction = _tiles[tileIndex].Next;
                var minigame = GetUpdatedMinigameIndex(tileIndex, Event.current.button == 0, Event.current.button == 1);

                _tiles[tileIndex] = new Board.Tile
                {
                    Next = direction,
                    Minigame = minigame
                };
                Event.current.Use();
            }
        }
        if(_tiles[tileIndex].Minigame.HasValue)
        {
            Vector3 labelPosition = position + Vector3.up * (TILE_SIZE / 2 + 0.1f);
            Handles.Label(labelPosition, _tiles[tileIndex].Minigame.ToString(), new GUIStyle
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState { textColor = Color.white }
            });
        }
    }

    private static int? GetUpdatedMinigameIndex(int tileIndex, bool leftMouseButton, bool rightMouseButton)
    {
        int? currentMinigame = _tiles[tileIndex].Minigame;
        if (!currentMinigame.HasValue)
        {
            return 0;
        }

        if(rightMouseButton && currentMinigame == 0)
        {
            return null;
        }

        if(rightMouseButton)
        {
            return currentMinigame - 1;
        }

        return currentMinigame + 1;
    }

    public static void DrawMoveHandle()
    {
        EditorGUI.BeginChangeCheck();
        var newPosition = SnapToGrid(Handles.PositionHandle(_cubePosition, Quaternion.identity));
        if (!EditorGUI.EndChangeCheck())
        {
            if (Event.current.type == EventType.MouseUp)
            {
                _cubePosition = _tilePositions[^1];
            }
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
        _tiles[^1] = new Board.Tile
        {
            Next = direction,
        };
        Vector3 currentPosition = _tilePositions[^1];
        for (int i = 0; i < tilesToAdd; i++)
        {
            Board.Tile newTile = new Board.Tile
            {
                Next = direction,
            };
            _tiles.Add(newTile);
            currentPosition = GetPosition(direction, currentPosition, TILE_SIZE * 2);
            _tilePositions.Add(currentPosition);
        }
        _cubePosition = _tilePositions[^1];
    }
    private static void AddFirstTile()
    {
        Board.Tile newTile = new Board.Tile
        {
            Next = Board.Direction.Front,
        };
        _tiles.Add(newTile);
        _tilePositions.Add(Vector3.zero);
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
