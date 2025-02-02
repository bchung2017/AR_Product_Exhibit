using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalController : ItemController
{
    public GameObject glowPanel;
    public ParticleSystem chapstickParticles;
    Material glowPanelMat;
    bool isSelected;
    ChapstickController chapstickController;
    public string flavor;

    // Start is called before the first frame update
    void Start()
    {
        glowPanelMat = glowPanel.GetComponent<Renderer>().material;
        SetAlpha(glowPanelMat, 0);
        CursorController.Instance.OnSelect += Selected;
        chapstickController = GetComponentInChildren<ChapstickController>();
        flavor = chapstickController.flavor;
        glowPanelMat.color = chapstickController.color;
        glowPanelMat.SetColor("_EmissionColor", chapstickController.color);
        SetCustomMaterialEmissionIntensity(glowPanelMat, 8.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(CursorController.Instance.selectedRaycast.collider && (CursorController.Instance.selectedRaycast.collider.gameObject == gameObject && !isSelected) || (!isSelected && glowPanelMat.color.a > 0))
        {
            SetAlpha(glowPanelMat, CursorController.Instance.cursor.fillAmount);
        }
        if (isSelected && !ARMenuController2.Instance.isShowing)
        {
            SetAlpha(glowPanelMat, 0);
            isSelected = false;
        }
    }
    void Selected(object sender, RaycastEventArgs e)
    {
        if(e.hit.collider.gameObject == gameObject)
        {
            isSelected = true;
            SetAlpha(glowPanelMat, 1.0f);
            chapstickParticles.Play();
        }
        else
        {
            isSelected = false;
            SetAlpha(glowPanelMat, 0.0f);
            chapstickParticles.Stop();
        }

    }

    void SetAlpha(Material m, float a)
    {
        Color c = m.color;
        c.a = a;
        m.color = c;
        return;
    }

    public void SetCustomMaterialEmissionIntensity(Material m, float intensity)
    {
        // get the material at this path
        Color color = m.GetColor("_EmissionColor");

        // for some reason, the desired intensity value (set in the UI slider) needs to be modified slightly for proper internal consumption
        float adjustedIntensity = intensity - (0.4169F);

        // redefine the color with intensity factored in - this should result in the UI slider matching the desired value
        color *= Mathf.Pow(2.0F, adjustedIntensity);
        //color *= intensity;
        m.SetColor("_EmissionColor", color);
        Debug.Log("<b>Set custom emission intensity of " + intensity + " (" + adjustedIntensity + " internally) on Material: </b>" + m);
    }
}
