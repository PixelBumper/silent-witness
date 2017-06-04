using UnityEngine;

public class Clonable : MonoBehaviour, IClonable
{
    public void Clone()
    {
        Instantiate(this);
    }
}
