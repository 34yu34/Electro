
public class AbilityComponent : Component
{
    private Ability _ability;

    public delegate void AbilityUseEvent();

    public event AbilityUseEvent OnAbilityUse;

    public void AttachAbility(Ability ability)
    {
        RemoveAbility();
       _ability = Instantiate(ability, transform);
    }

    public void UseAbility()
    {
        if (_ability)
        {
            _ability.Use();
            if (OnAbilityUse != null)
            {
                OnAbilityUse.Invoke();
            }
        }
    }

    private void RemoveAbility()
    {
        if (!_ability) return;
        
        Destroy(_ability.gameObject);
        _ability = null;
    }

}
