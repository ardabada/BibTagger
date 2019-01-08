using System;

namespace BibCore
{
    internal class Vector2
    {
        // X component of the vector.
        public float x;
        // Y component of the vector.
        public float y;

        // Access the /x/ or /y/ component using [0] or [1] respectively.
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        // Constructs a new vector with given x, y components.
        public Vector2(float x, float y) { this.x = x; this.y = y; }

        // Set x and y components of an existing Vector2.
        public void Set(float newX, float newY) { x = newX; y = newY; }

        // Adds two vectors.
        public static Vector2 operator +(Vector2 a, Vector2 b) { return new Vector2(a.x + b.x, a.y + b.y); }
        // Subtracts one vector from another.
        public static Vector2 operator -(Vector2 a, Vector2 b) { return new Vector2(a.x - b.x, a.y - b.y); }
        // Multiplies one vector by another.
        public static Vector2 operator *(Vector2 a, Vector2 b) { return new Vector2(a.x * b.x, a.y * b.y); }
        // Divides one vector over another.
        public static Vector2 operator /(Vector2 a, Vector2 b) { return new Vector2(a.x / b.x, a.y / b.y); }
        // Negates a vector.
        public static Vector2 operator -(Vector2 a) { return new Vector2(-a.x, -a.y); }
        // Multiplies a vector by a number.
        public static Vector2 operator *(Vector2 a, float d) { return new Vector2(a.x * d, a.y * d); }
        // Multiplies a vector by a number.
        public static Vector2 operator *(float d, Vector2 a) { return new Vector2(a.x * d, a.y * d); }
        // Divides a vector by a number.
        public static Vector2 operator /(Vector2 a, float d) { return new Vector2(a.x / d, a.y / d); }
    }
}
