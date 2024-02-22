namespace Play.Movement.Setting
{
    public class ModificateLogic
    {
        private float _mod= 0;
        
        public void AddModifSpeed(float mod) => _mod += mod;
        public void RemoveModifSpeed(float mod) => _mod -= mod;
        public void ClearModifSpeed(float mod) => _mod = 0;
        
        public float GetMod => _mod;
    }
}