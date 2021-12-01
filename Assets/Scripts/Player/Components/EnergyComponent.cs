using UnityEngine;
using UnityEngine.VFX;

public class EnergyComponent : Component
{
    [SerializeField] private PoolAttribute _energy;

    [SerializeField] private Attribute _energyRegen;

    [SerializeField] private Renderer _energyImage;

    [SerializeField] private VisualEffect _vfx;
    private Renderer EnergyRenderer => _energyImage;

    private const float MAX_TRANSPARENCY = 4;
    private static readonly int Transparency = Shader.PropertyToID("Transparency");

    private void Start()
    {
        _energy.Fill();

    }

    private void FixedUpdate()
    {
        _energy.CurrentValue += _energyRegen * Time.fixedDeltaTime;
    }

    private bool HasRenderingComponents()
    {
        return _energyImage && _vfx;
    }

    private void Update()
    {
        ChangeEnergyPower();
    }

    private void ChangeEnergyPower()
    {
        if (!HasRenderingComponents())
        {
            return;
        }

        var force = Mathf.Lerp(MAX_TRANSPARENCY, 1.0f, _energy.FilledPercentage);
        _energyImage.material.SetFloat(Transparency, force);
        _vfx.SetFloat("percentage", _energy.FilledPercentage);
    }

    public void ChangeEnergyColor(Color color)
    {
        if (!HasRenderingComponents())
        {
            return;
        }

        EnergyRenderer.material.color = color;
        _vfx.SetVector4("color", color);

    }

    public bool TryUseEnergy(float amount)
    {
        if (!HasEnergy(amount))
        {
            return false;
        }

        UseEnergy(amount);
        return true;
    }

    public bool HasEnergy(float amount)
    {
        return _energy.CurrentValue >= amount;
    }

    private void UseEnergy(float amount)
    {
        _energy.CurrentValue -= amount;
    }
}
