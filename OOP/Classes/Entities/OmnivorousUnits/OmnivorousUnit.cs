namespace OOP.Classes.Entities.OmnivorousUnits
{
    public class OmnivorousUnit<TFood,TPartner> : Unit <TFood,TPartner> 
        where TFood : IFood
        where TPartner : AbstractUnit
    {
        public OmnivorousUnit(int x, int y) : base(x, y)
        {

        }

        protected override AbstractUnit CreateChild()
        {
            return new OmnivorousUnit<TFood, TPartner>(X, Y);
        }
    }

}
