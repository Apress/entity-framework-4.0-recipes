using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe12
{
    class Program
    {
        static void Main(string[] args)
        {
            Cleanup();
            RunExample();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter13.track");
                context.ExecuteStoreCommand("delete from chapter13.cd");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {

                var cd1 = context.CreateObject<CD>();
                cd1.Title = "Abbey Road";
                cd1.Tracks.Add(new Track { Title = "Come Together", Artist = "The Beatles" });
                var cd2 = context.CreateObject<CD>();
                cd2.Title = "Cowboy Town";
                cd2.Tracks.Add(new Track { Title = "Cowgirls Don't Cry", Artist = "Brooks & Dunn" });
                var cd3 = context.CreateObject<CD>();
                cd3.Title = "Long Black Train";
                cd3.Tracks.Add(new Track { Title = "In My Dreams", Artist = "Josh Turner" });
                cd3.Tracks.Add(new Track { Title = "Jacksonville", Artist = "Josh Turner" });
                context.CDs.AddObject(cd1);
                context.CDs.AddObject(cd2);
                context.CDs.AddObject(cd3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // trigger proxy generation
                context.CreateProxyTypes(new Type[] { typeof(CD), typeof(Track) });
                Console.WriteLine("{0} proxies generated!", EFRecipesEntities.GetKnownProxyTypes().Count());

                var cds = context.CDs.Include("Tracks");
                foreach (var cd in cds)
                {
                    Console.WriteLine("Album: {0}", cd.Title);
                    foreach (var track in cd.Tracks)
                    {
                        Console.WriteLine("\t{0} by {1}", track.Title, track.Artist);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class CD
    {
        public virtual int CDId { get; set; }
        public virtual string Title { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }

    public class Track
    {
        public virtual string Title { get; set; }
        public virtual string Artist { get; set; }
        public virtual int CDId { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        private ObjectSet<CD> _cds;
        private ObjectSet<Track> _tracks;

        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
            _cds = CreateObjectSet<CD>();
            _tracks = CreateObjectSet<Track>();
        }

        public ObjectSet<CD> CDs
        {
            get { return _cds; }
        }

        public ObjectSet<Track> Tracks
        {
            get { return _tracks; }
        }
    }
}
