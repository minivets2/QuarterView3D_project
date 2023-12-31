using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private Player _player;
    private bool _isPlayingDamageAnimation;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy") || _isPlayingDamageAnimation) return;
        
        _isPlayingDamageAnimation = true;
        GetAttack(other.GetComponent<Enemy>().damage);
    }

    void GetAttack(float enemyDamage)
    {
        _player.DecreaseHealth(enemyDamage);

        AudioManager.instance.PlaySFX(SoundName.Hit);
        _player.GetAnimation().SetTrigger("doHit");
            
        GameManager.instance.hp.SetHp();
    
        if (_player.GetHealth() > 0)
        {
                
        }
        else
        {
            Dead();
        }
    }
    
    private void Dead()
    {
            
    }
    
    public void OnDamageAnimationComplete()
    {
        _isPlayingDamageAnimation = false;
    }
}
