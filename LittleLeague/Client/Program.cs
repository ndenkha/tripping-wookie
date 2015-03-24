using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddPlayers();
            //RegisterPlayers();
        }

        static void AddPlayers()
        {
            using (var db = new DbContext())
            {
                db.Players.Add(new Player("John", "Doe"));
                db.SaveChanges();
            }
        }

        static void RegisterPlayers()
        {
            using (var db = new DbContext())
            {
                foreach (var player in db.Players)
                {
                    player.Register();
                }

                db.SaveChanges();
            }
        }
    }
}
