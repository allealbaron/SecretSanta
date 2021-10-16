namespace SecretSanta.Model
{
    public class Relationship
    {

        /// <summary>
        /// Person who gives the present
        /// </summary>
        public InvisibleFriend Giver { get; set; }

        /// <summary>
        /// Person who receives the present
        /// </summary>
        public InvisibleFriend Receiver { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="giver">Giver</param>
        /// <param name="receiver">Receiver</param>
        public Relationship(InvisibleFriend giver, InvisibleFriend receiver)
        {
            Giver = giver;
            Receiver = receiver;
        }

    }
}
