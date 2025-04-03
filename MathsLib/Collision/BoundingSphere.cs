using System;


namespace MathsLib
{
    public class BoundingSphere
    {
        public Vector3D Centre { get; }
        public float Radius { get; }
    
        public override string ToString() => $"Centre: {Centre}, Radius: {MathF.Round(Radius, 4)}";

        public BoundingSphere(Vector3D centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }

        public BoundingSphere(Vector3D[] vertices)
        {
            float diameter = 0;
            Vector3D furthestA = vertices[0];
            Vector3D furthestB = vertices[1];
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                for (int j = i + 1; j < vertices.Length; j++)
                {
                    float distance = Vector3D.Distance(vertices[i], vertices[j]);
                    if (distance > diameter)
                    {
                        diameter = distance;
                        furthestA = vertices[i];
                        furthestB = vertices[j];
                    }
                }
            }
            Centre = Vector3D.Lerp(furthestA, furthestB, 0.5f);
            Radius = diameter/2;
        }

        public bool Intersects(BoundingSphere other) => Vector3D.DistanceSquared(Centre, other.Centre) <= (Radius + other.Radius) * (Radius + other.Radius);
    }
}