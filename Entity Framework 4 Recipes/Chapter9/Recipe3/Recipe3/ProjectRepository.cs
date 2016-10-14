using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe3
{
    public class ProjectRepository
    {
        public ProjectRepository()
        {
        }

        public List<Project> RetrieveAll()
        {
            using (var context = new EFRecipesEntities())
            {
                return context.Projects.ToList();
            }
        }

        public void UpdateProject(Project project, Project originalProject)
        {
            // First approach
            using (var context = new EFRecipesEntities())
            {
                context.Projects.Attach(project);
                context.Projects.ApplyOriginalValues(originalProject);
                context.SaveChanges();
            }
        }
    }
}