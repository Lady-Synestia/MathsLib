// See https://aka.ms/new-console-template for more information

using MathsLib;

// simple script to be used with profilers to compare and assess the efficiency of the module

Console.WriteLine("Hello World!");


/*float delta = 0.0002f;

Vector3D[] vertices = [new(0, 0, 0), new(1, 0, 0), new(0, 1, 0),new(0, 0, 1),new(1, 1, 0),new(1, 0, 1),new(0, 1, 1),new(1, 1, 1), new(0, 0, 0), new(1, 0, 0), new(0, 1, 0),new(0, 0, 1),new(1, 1, 0),new(1, 0, 1),new(0, 1, 1),new(1, 1, 1)];
Quaternion rotation = new(0, Vector3D.Z);
Vector3D position = Vector3D.X;
Vector3D scale = Vector3D.One;
float spinAngle = 0;
Vector3D spinAxis = new (1, 0.25f, 0.75f);
float orbitAngle = 0;
float orbitDistance = 15;
Vector3D orbitAxis = new (0.5f, 0.5f, 0);


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

}*/

/*
float theta = 0;


Vector4D v = new(1, 0, 0, 1);
//Console.WriteLine(v);

Matrix4D T = Matrix4D.Translation(new Vector3D(0, 0, 0));
//Console.WriteLine(T);


//Console.WriteLine(R);

Matrix4D S = Matrix4D.Scale(new Vector3D(1, 1, 1));
//Console.WriteLine(S);



while (theta < 360)
{
    Matrix4D R = Matrix4D.Rotation(theta, Vector3D.Z);
    //Console.WriteLine(R);
    Matrix4D TRS = Matrix4D.TRS(T, R, S);
    //Console.WriteLine(TRS);
    Vector3D r = TRS * v;
    Console.WriteLine($"{r}\n{r.Magnitude}\n");
    theta += 30;
}
*/

Console.WriteLine(new Vector3D(1, 0, -1));
Console.WriteLine(new Vector3D(0.1f, -0.1f, 4214));
Console.WriteLine(new Vector3D(739.29472f, 0.0876284f, 324.23f));
Console.WriteLine(new Vector3D(3.8299e20f, 9.3829e-10f, 5.392939282e12f));
Console.WriteLine(new Vector3D(3.29438292e-14f, 0, 0));