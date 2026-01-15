using UnityEngine;

public class MouseAttack : MonoBehaviour
{
    [SerializeField] int mouseDamage = 1;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("MouseAttack: No Camera found with the 'MainCamera' tag!");
        }
    }

    public void Attack()
    {
        if (cam == null || !Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // for debugging
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);
            //print(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<IHealth>().OnHit(mouseDamage);
            }
        }
    }
}