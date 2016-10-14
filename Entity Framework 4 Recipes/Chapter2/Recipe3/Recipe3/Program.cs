using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter2.linktable");
                context.ExecuteStoreCommand("delete from chapter2.artist");
                context.ExecuteStoreCommand("delete from chapter2.album");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                // add an artist with two albums
                var artist = new Artist { FirstName = "Alan", LastName = "Jackson" };
                var album1 = new Album { AlbumName = "Drive" };
                var album2 = new Album { AlbumName = "Live at Texas Stadium" };
                artist.Albums.Add(album1);
                artist.Albums.Add(album2);
                context.Artists.AddObject(artist);

                // add an album for two artists
                var artist1 = new Artist { FirstName = "Tobby", LastName = "Keith" };
                var artist2 = new Artist { FirstName = "Merle", LastName = "Haggard" };
                var album = new Album { AlbumName = "Honkytonk University" };
                artist1.Albums.Add(album);
                artist2.Albums.Add(album);
                context.Albums.AddObject(album);

                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("Artists and their albums...");
                var artists = from a in context.Artists select a;
                foreach (var artist in artists)
                {
                    Console.WriteLine("{0} {1}", artist.FirstName, artist.LastName);
                    foreach (var album in artist.Albums)
                    {
                        Console.WriteLine("\t{0}", album.AlbumName);
                    }
                }

                Console.WriteLine("\nAlbums and their artists...");
                var albums = from a in context.Albums select a;
                foreach (var album in albums)
                {
                    Console.WriteLine("{0}", album.AlbumName);
                    foreach (var artist in album.Artists)
                    {
                        Console.WriteLine("\t{0} {1}", artist.FirstName, artist.LastName);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
