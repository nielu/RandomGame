using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    public Canvas quitMenu;
    public Button startNormal;
    public Button startWierd;
    public Button exit;


    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
