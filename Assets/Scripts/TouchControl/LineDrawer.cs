using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public bool FightMode = false;
    public GameObject debugObject;
    [HideInInspector]
    public string spellStorage = "";

    private LineRenderer currentLine;

    private void Start()
    {
        currentLine = GetComponent<LineRenderer>();
        DirectionJoystick.Instance.Hide();
    }

    private void Update()
    {
        // Does not need directional joystick
        if (spellStorage == "shield")
        {
            PlayerHealth.Instance.SetInvincability(2.0f);
            spellStorage = "";
            return;
        }
    }

    private void RemoveDuplicates(LineRenderer lineRenderer)
    {
        if (lineRenderer.positionCount <= 2)
        {
            return;
        }
        Vector3 previousPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 2);
        Vector3 currentPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        if (previousPoint[0] == currentPoint[0] && previousPoint[1] == currentPoint[1])
        {
            lineRenderer.positionCount--;
        }
    }

    public void LineBegin(Touch touch)
    {
        AddPoint(currentLine, touch.position);
    }

    public void LineMove(Touch touch)
    {
        if (Vector3.Distance(AdjustPointToScreen(8, touch.position),
                             currentLine.GetPosition(currentLine.positionCount - 1)) > 0.45f)
        {
            AddPoint(currentLine, touch.position);
        }
    }

    public void LineEnd()
    {
        // Recognize spell
        Vector3[] points2 = new Vector3[currentLine.positionCount];
        currentLine.GetPositions(points2);
        PrimitiveContainer[] primitives = HighLevelRecognition.PrimitiveShapeGenerator(points2);
        spellStorage = SketchOutput.Output(primitives);
        currentLine.positionCount = 0;

        // Set player aura
        Player.Instance.GetComponent<Player>().CreateAura(spellStorage);
    }

    public void LineCancel()
    {
        currentLine.positionCount = 0;
    }

    private void AddPoint(LineRenderer lineRenderer, Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, AdjustPointToScreen(8, position));
    }

    private Vector3 AdjustPointToScreen(float cameraHeight, Vector3 position)
    {
        // Center the point to the screen
        position.x = position.x - (Screen.width / 2f);
        position.y = position.y - (Screen.height / 2f);

        // Adjust the point to the camera
        position.x = (position.x / Screen.width) * cameraHeight * (Screen.width / (float)Screen.height) * 2f;
        position.y = (position.y / Screen.height) * cameraHeight * 2f;

        return position;
    }
    public bool SpellCast(Touch touch)
    {
        // Creates joystick for player to set direction of spell
        switch (touch.phase)
        {
        case TouchPhase.Began:
            DirectionJoystick.Instance.Show();
            Vector3 position = AdjustPointToScreen(8, touch.position);
            DirectionJoystick.Instance.SetJoystick(position);
            DirectionJoystick.Instance.SetJoystickCenterPoint(position);
            return false;
            break;

        case TouchPhase.Moved:
            DirectionJoystick.Instance.SetJoystickCenterPoint(AdjustPointToScreen(8, touch.position));
            return false;
            break;

        case TouchPhase.Ended:
            Player.Instance.GetComponent<Player>().CastSpell(spellStorage);
            spellStorage = "";
            DirectionJoystick.Instance.SetJoystickCenterPoint(
                DirectionJoystick.Instance.joystick.transform.localPosition);
            DirectionJoystick.Instance.Hide();
            return true;
            break;

        case TouchPhase.Canceled:
            Player.Instance.GetComponent<Player>().CastSpell(spellStorage);
            spellStorage = "";
            DirectionJoystick.Instance.SetJoystickCenterPoint(
                DirectionJoystick.Instance.joystick.transform.localPosition);
            DirectionJoystick.Instance.Hide();
            return true;
            break;
        }
        return false;
    }

    public void SpellCancel()
    {
        DirectionJoystick.Instance.SetJoystickCenterPoint(DirectionJoystick.Instance.joystick.transform.localPosition);
        DirectionJoystick.Instance.Hide();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            Destroy(currentLine);
        }
    }
}
