using SecretSanta.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta
{
    class Program
    {
        /// <summary>
        /// List of Friends
        /// </summary>
        static readonly List<InvisibleFriend> InvisibleFriends = new();

        /// <summary>
        /// List of Relationships
        /// </summary>
        static readonly List<Relationship> Relationships = new();

        /// <summary>
        /// Main Process
        /// </summary>
        static void Main()
        {

            LoadFriends("Friends.txt");

            LoadRelationShips("Historical.txt");

            ShuffleUntilGettingNewList();

            PrintList();
            
        }

        /// <summary>
        /// Loads friends in a list
        /// </summary>
        /// <param name="filepath">File to load</param>
        static private void LoadFriends(string filepath)
        {

            foreach (string line in File.ReadAllLines(filepath))
            {
                InvisibleFriends.Add(new InvisibleFriend(line));
            }

        }

        /// <summary>
        /// Loads relationships in a list. 
        /// </summary>
        /// <param name="filepath">File to load</param>
        static private void LoadRelationShips(string filepath)
        {

            foreach (string line in File.ReadAllLines(filepath))
            {

                string[] names = line.Split(';');

                InvisibleFriend Giver = InvisibleFriends.Where(p => p.Name == names[0]).FirstOrDefault();
                InvisibleFriend Receiver = InvisibleFriends.Where(p => p.Name == names[1]).FirstOrDefault();

                if (!(Giver is null) && !(Receiver is null))
                {
                    // Giver and Receiver participates this year
                    Relationships.Add(new(Giver, Receiver));
                }
            }

        }

        /// <summary>
        /// Method that shuffles <see cref="InvisibleFriends"/>
        /// </summary>
        static private void Shuffle()
        {
            Random random = new();

            for (int i = InvisibleFriends.Count - 1; i > 0; i--)
            {
                int position = random.Next(i);

                InvisibleFriend tempFriend = InvisibleFriends[position];

                InvisibleFriends[position] = InvisibleFriends[i];
                InvisibleFriends[i] = tempFriend;

            }

        }

        /// <summary>
        /// Shuffles the list of invisible friends
        /// checking noboby repeats his/her Secret Santa
        /// </summary>
        static void ShuffleUntilGettingNewList()
        {

            bool done = false;

            while (!done)
            {
                Shuffle();

                done = Relationships.Where(p => p.Giver == InvisibleFriends[^1] && p.Receiver == InvisibleFriends[0]).Any();

                for (int i = 1; done && i < InvisibleFriends.Count; i++)
                {
                    if (Relationships.Where(p => p.Giver == InvisibleFriends[i - 1] && p.Receiver == InvisibleFriends[i]).Any())
                    {
                        done = false; // We have found a relationship from a previous year
                    }
                }

            }

        }

        /// <summary>
        /// Prints the list of Friends
        /// </summary>
        static void PrintList()            
        {

            Console.WriteLine("Person gives a present to... ");

            for (int i = 1; i < InvisibleFriends.Count; i++)
            {
                Console.WriteLine("{0} => {1}", InvisibleFriends[i - 1].Name, InvisibleFriends[i].Name);
            }

            Console.WriteLine("{0} => {1}", InvisibleFriends[^1].Name, InvisibleFriends[0].Name);

        }

    }
}
