using UnityEngine;

public class StarDustEffect : MonoBehaviour
{
    public enum Output { Side, PointAllDirections, PointOneDirection }

    public Output CurrentOutput = Output.Side;

    void Update()
    {
        var shape = GetComponent<ParticleSystem>().shape;

        switch (CurrentOutput)
        {
            case Output.Side:
                shape.shapeType = ParticleSystemShapeType.Box;
                var box = shape.scale;
                box.x = 1;
                box.y = 5;
                box.z = 0.5f;
                shape.randomDirectionAmount = 0.0f;
                break;
            case Output.PointAllDirections:
                shape.shapeType = ParticleSystemShapeType.Circle;
                shape.radius = 1;
                shape.randomDirectionAmount = 1.0f;
                break;
            case Output.PointOneDirection:
                shape.shapeType = ParticleSystemShapeType.Cone;
                shape.angle = 8;
                shape.arc = 360;
                shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
                shape.randomDirectionAmount = 0;
                break;
            default:
                break;
        }
    }
}
