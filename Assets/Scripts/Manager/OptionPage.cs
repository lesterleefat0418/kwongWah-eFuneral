using UnityEngine;

public class OptionPage : MonoBehaviour
{
    public Select option;
    // Start is called before the first frame update
    void Start()
    {
        this.option.init();
    }

    public void Select(bool right)
    {
        this.option.changePage(right);
    }

}
