namespace Core.Battle
{
    public class Card 
    {
        public CardType CardType { get; private set; }
        public CardRank CardRank { get; private set; }

        public Card(CardType type, CardRank rank)
        {
            CardType = type;
            CardRank = rank;
        }
    }
}
