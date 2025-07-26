using System;
using UnityEngine;

namespace Inventories
{
    [System.Serializable]
    public struct InventoryItem : IEquatable<InventoryItem>, ISerializationCallbackReceiver
    {

        [field: SerializeField]
        public InventoryItemData Data { get; private set; }
        [field: SerializeField]
        public Vector2Int Position { get; private set; }
        [field: SerializeField]
        public Orientation Orientation { get; private set; }

        [field: SerializeField, HideInInspector]
        public string Guid { get; private set; }

        public InventoryItem(InventoryItemData data, Vector2Int position)
        {
            Data = data;
            Position = position;
            Orientation = 0;
            Guid = System.Guid.NewGuid().ToString();
        }

        public Vector2Int[] GetCells() => GetCells(Position, Orientation);

        public Vector2Int[] GetCells(Vector2Int from, Orientation orientation)
        {
            Vector2Int[] dataOccupiedSpace = Data.OccupiedSpace;
            Vector2Int[] output = new Vector2Int[dataOccupiedSpace.Length];

            for (int i = 0; i < dataOccupiedSpace.Length; i++)
            {
                Vector2Int current = dataOccupiedSpace[i];
                output[i] = from + orientation switch
                {
                    Orientation.Right => current,
                    Orientation.Up => new Vector2Int(current.y, current.x),
                    Orientation.Left => new Vector2Int(-current.x, current.y),
                    Orientation.Down => new Vector2Int(current.y, -current.x),
                    _ => Vector2Int.zero,
                };
            }

            return output;
        }

        public bool Equals(InventoryItem other)
        {
            return Guid == other.Guid;
        }

        public override bool Equals(object obj)
        {
            return obj is InventoryItem other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Guid != null ? Guid.GetHashCode() : 0);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrEmpty(Guid))
                Guid = System.Guid.NewGuid().ToString();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
    }
}