// See https://aka.ms/new-console-template for more information

using MathsLib;

// simple script to be used with profilers to compare and assess the efficiency of the module

Console.WriteLine("Hello World!");


float delta = 0.0002f;

Vector3D[] vertices = [new(0, 0, 0), new(1, 0, 0), new(0, 1, 0),new(0, 0, 1),new(1, 1, 0),new(1, 0, 1),new(0, 1, 1),new(1, 1, 1), new(0, 0, 0), new(1, 0, 0), new(0, 1, 0),new(0, 0, 1),new(1, 1, 0),new(1, 0, 1),new(0, 1, 1),new(1, 1, 1)];
Quaternion rotation = new(0, Vector3D.Z);
Vector3D position = Vector3D.X;
Vector3D scale = Vector3D.One;
float spinAngle = 0;
Vector3D spinAxis = new (1, 0.25f, 0.75f);
float orbitAngle = 0;
float orbitDistance = 15;
Vector3D orbitAxis = new(0.5f, 0.5f, 0);


while (true)
{
    spinAngle += delta * 50;
    if (spinAngle > 360)
        spinAngle = 0;

    rotation = new Quaternion(spinAngle, spinAxis);
    Matrix4D rotationMatrix = Matrix4D.Rotation(rotation);
    Matrix4D scaleMatrix = Matrix4D.Scale(scale);

    orbitAngle += delta * 20;
    if (orbitAngle > 360)
        orbitAngle = 0;

    Quaternion orbitRotation = new(orbitAngle, orbitAxis);
    position = Quaternion.Rotate(orbitRotation, Vector3D.X) * orbitDistance;
    Matrix4D translationMatrix = Matrix4D.Translation(position);

    Matrix4D transformMatrix = Matrix4D.TRS(translationMatrix, rotationMatrix, scaleMatrix);

    Vector3D[] transformedVertices = new Vector3D[vertices.Length];

    for (int i = 0; i < transformedVertices.Length; i++)
    {
        transformedVertices[i] = transformMatrix * vertices[i];
    }

    vertices = transformedVertices;

}