using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class Helper : MonoBehaviour
{
    public static Helper help;

    private void Awake()
    {
        if (help is null)
            help = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Move item in an instant, no animation
    /// as if teleport
    /// </summary>
    /// <param name="item"></param>
    /// <param name="target"></param>
    public void MoveObject(Transform item, Transform target)
    {
        item.position = target.position;
        item.rotation = target.rotation;
    }

    public void DestroyItem(GameObject item)
    {
        Destroy(item);
    }

    public void ChildObliterator(Transform parent)
    {
        foreach (Transform t in parent)
        {
            Destroy(t.gameObject);
        }
    }

    public void MoveItemX(Transform item)
    {
        item.DOLocalMoveX(1.5f, 0.5f);
    }

    public void RotateItemYAdd90(Transform item)
    {
        item.DOLocalRotate(new Vector3(0, 90, 0), 1);
    }

    public void RotateItemYRemove90(Transform item)
    {
        item.DOLocalRotate(new Vector3(0, 0, 0), 1);
    }

    public void RotateItemYNegative90(Transform item)
    {
        item.DOLocalRotate(new Vector3(0, -90, 0), 1);
    }

    public void RotateElectricalBoxHandle(Transform item)
    {
        item.DOLocalRotate(new Vector3(45, 0, 0), 1);
    }

    public string TranslateColliderToLocationName(Collider colInput)
    {
        if (colInput is null)
            return "Somewhere";
        string input = colInput.name;
        if (input.StartsWith("Hall"))
            return "Hallway";
        if (input.StartsWith("Worker"))
            return "Worker's bedroom";
        if (input == "Garden")
            return "Exit / Bottom fish tank";
        if (input == "FishTank")
            return "Fish tank";
        if (input.StartsWith("Stair"))
            return "Stairwell";
        if (char.IsDigit(input.Last()))
        {
            input = input.Insert(input.IndexOf(input.Last()), " ");
            return input;
        }
        return input;
    }

    public void DoFadeInText(TMP_Text text)
    {
        text.DOFade(1, 1);
    }

    public void DoFadeOutText(TMP_Text text)
    {
        text.DOFade(0, 1);
    }

    public void ShowToastMessage(string message)
    {
        ToastReceiver.ShowToastMessage(message);
    }
}
