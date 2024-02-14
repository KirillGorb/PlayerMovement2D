using System.Threading.Tasks;
using UnityEngine;

public static class MovebleTo
{
    public static async Task MoveToAsync(Transform transform, Vector3 to, float speed)
    {
        while (transform.position != transform.position + to)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + to, speed * Time.deltaTime);
            await Task.Yield();
        }
    }

    public static void MoveTo(Transform transform, Vector3 to, float speed) =>
        transform.position = Vector3.MoveTowards(transform.position, transform.position + to, speed * Time.deltaTime);

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

    public static async Task RotateToTimeAsync(Transform transform, Vector3 to, float speed)
    {
        var dir = transform.position - to;
        var angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var newRot = Quaternion.Euler(0, angel, 0);
        while (transform.rotation != newRot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, speed * Time.deltaTime);
            await Task.Yield();
        }
    }
}