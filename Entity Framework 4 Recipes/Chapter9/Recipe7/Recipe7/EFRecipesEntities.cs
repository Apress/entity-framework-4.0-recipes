using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Objects;

namespace Recipe7
{
    public partial class EFRecipesEntities
    {
        public void StartSelfTracking()
        {
            var entities = this.ObjectStateManager.GetObjectStateEntries(~EntityState.Detached).Where(e => !e.IsRelationship).Select(e => e.Entity).OfType<IObjectWithChangeTracker>();
            foreach (var entity in entities)
            {
                entity.StartTracking();
            }
        }

        public override int SaveChanges(SaveOptions options)
        {
            var affected = base.SaveChanges(options);
            if (SaveOptions.AcceptAllChangesAfterSave == (SaveOptions.AcceptAllChangesAfterSave & options))
            {
                var entities = this.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged).Where(e => !e.IsRelationship).Select(e => e.Entity).OfType<IObjectWithChangeTracker>();
                foreach (var entity in entities)
                {
                    entity.AcceptChanges();
                }
            }
            return affected;
        }
    }
}