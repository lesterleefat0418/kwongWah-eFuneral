using UnityEngine;

public class SelectHall : MonoBehaviour
{
    public Select selectHall;
    // Start is called before the first frame update
    void Start()
    {
        this.selectHall.init();
    }


    public void select(int id)
    {
        this.selectHall.set(id);
    }

}

