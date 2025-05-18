using UnityEngine;

namespace Game.Player
{
    public interface IPlayerInput
    {
        public bool Enabled { get; set; }

        public bool PrimaryAction { get; }

        public bool SecondaryAction { get; }

        public bool Pickup { get; }

        public bool Throw { get; }

        public int ScrollDelta { get; }

        public Vector2 Motion { get; }

        public Vector2 Rotation { get; }
    }
}