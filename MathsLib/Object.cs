namespace MathsLib
{
    public class Object
    {
        public Object(Vector3D[] vertices)
        {
            Vertices = vertices;
            Aabb = new AxisAlignedBoundingBox(vertices);
        }
        public Vector3D Position { get; set; } = Vector3D.Zero;
        public Quaternion Rotation { get; set; } = Quaternion.Zero;
        public Vector3D Scale { get; set; } = Vector3D.One;
    
        private Vector3D Translation { get; set; } = Vector3D.Zero;
        private Vector3D[] Vertices { get; set; }
        private AxisAlignedBoundingBox Aabb { get; set; }
    
    }
}