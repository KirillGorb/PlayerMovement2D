using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using System;

public static class MovebleTo
{
    public static async Task MoveToAsync(Transform transform, Vector3 to, float speed, float _time = 10,Action toWin = default)
    {
        float time = _time;
        while (transform.position != to && time >= 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, to, speed * Time.deltaTime);
            await Task.Yield();
        }

        transform.position = to;
        toWin?.Invoke();
    }

    public static void MoveTo(Transform transform, Vector3 to, float speed) =>
        transform.position = Vector3.MoveTowards(transform.position, to, speed * Time.deltaTime);

    public static void RotateToTime(Transform transform, Vector3 to, float speed)
    {
        var dir = transform.position - to;
        var angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var newRot = Quaternion.Euler(0, angel, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, speed * Time.deltaTime);
    }

    public static void RotateTo(Transform transform, Vector3 to)
    {
        var dir = transform.position - to;
        var angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var newRot = Quaternion.Euler(0, angel, 0);
        transform.rotation = newRot;
    }

    public static async Task RotateToTimeAsync(Transform transform, Vector3 to, float speed, float _time = 10,Action toWin= default)
    {
        var dir = transform.position - to;
        var angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var newRot = Quaternion.Euler(0, angel, 0);
        float time = _time;
        while (transform.rotation != newRot && time >= 0)
        {
            time -= Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, speed * Time.deltaTime);
            await Task.Yield();
        }

        transform.rotation = newRot;
        toWin?.Invoke();
    }
}