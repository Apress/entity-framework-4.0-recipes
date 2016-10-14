using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter8.speakertalk");
                context.ExecuteStoreCommand("delete from chapter8.speaker");
                context.ExecuteStoreCommand("delete from chapter8.talk");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var speaker1 = new Speaker { Name = "Karen Stanfield" };
                var talk1 = new Talk { Title = "Simulated Annealing in C#" };
                speaker1.Talks = new List<Talk> { talk1 };

                // associations not yet complete
                Console.WriteLine("talk1.Speaker is null: {0}", talk1.Speakers == null);
                context.Speakers.AddObject(speaker1);

                // now it's fixed up
                Console.WriteLine("talk1.Speaker is null: {0}", talk1.Speakers == null);
                Console.WriteLine("Number of added entries tracked: {0}", context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Count());
                context.SaveChanges();

                // change the talk's title
                talk1.Title = "AI with C# in 3 Easy Steps";
                Console.WriteLine("talk1's state is: {0}",context.ObjectStateManager.GetObjectStateEntry(talk1).State);
                context.DetectChanges();
                Console.WriteLine("talk1's state is: {0}", context.ObjectStateManager.GetObjectStateEntry(talk1).State);

                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach(var speaker in context.Speakers.Include("Talks"))
                {
                    Console.WriteLine("Speaker: {0}",speaker.Name);
                    foreach(var talk in speaker.Talks)
                    {
                        Console.WriteLine("\tTalk Title: {0}",talk.Title);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Speaker
    {
        public int SpeakerId { get; set; }
        public string Name { get; set; }
        public IList<Talk> Talks { get; set; }
    }

    public class Talk
    {
        public int TalkId { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RevisedDate { get; set; }
        public IList<Speaker> Speakers { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
            this.SavingChanges += new EventHandler(EFRecipesEntities_SavingChanges);
        }

        private ObjectSet<Speaker> speakers;
        private ObjectSet<Talk> talks;

        public ObjectSet<Speaker> Speakers
        {
            get { return speakers ?? (speakers = CreateObjectSet<Speaker>()); }
        }

        public ObjectSet<Talk> Talks
        {
            get { return talks ?? (talks = CreateObjectSet<Talk>()); }
        }

        private void EFRecipesEntities_SavingChanges(object sender, EventArgs e)
        {
            var addedTalks = this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Where(en => en.Entity is Talk).Select(en => en.Entity as Talk);
            foreach (var talk in addedTalks)
            {
                talk.CreateDate = DateTime.Now;
                talk.RevisedDate = DateTime.Now;
            }

            var revisedTalks = this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified).Where(en => en.Entity is Talk).Select(en => en.Entity as Talk);
            foreach (var talk in revisedTalks)
            {
                talk.RevisedDate = DateTime.Now;
            }
        }

        public override int SaveChanges(SaveOptions options)
        {
            this.DetectChanges();
            return base.SaveChanges(options);
        }
    }
}
