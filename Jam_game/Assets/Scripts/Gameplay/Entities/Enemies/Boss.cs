namespace Gameplay.Entities.Enemies
{
    public class Boss : Enemy
    {
        public override void KillThis()
        {
            base.KillThis();
            
            DropThingy();
        }

        private void DropThingy()
        {
            // idk do something in the future
        }
    }
}