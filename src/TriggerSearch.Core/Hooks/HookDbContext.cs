using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TriggerSearch.Core.Hooks
{
    public class HookDbContext : DbContext
    {
        private Func<HookTrackingResult, object> _func;
        public HookDbContext(DbContextOptions options, IHookFunction hookFunction) : base(options)
        {
            _func = hookFunction.TriggerSave;
        }

        public void AddEvent(Func<HookTrackingResult, object> func)
        {
            _func = func;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var changed = GetChangeTracking();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            _func?.Invoke(changed);
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var changed = GetChangeTracking();
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            _func?.Invoke(changed);
            return result;
        }

        private HookTrackingResult GetChangeTracking()
        {
            if (_func == null)
                return null;

            var result = new HookTrackingResult();
            var entities = ChangeTracker.Entries();

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        result.EntriesAdded.Add(entity);
                        break;
                    case EntityState.Deleted:
                        result.EntriesDeleted.Add(entity);
                        break;
                    case EntityState.Modified:
                        result.EntriesModified.Add(entity);
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    default:
                        break;
                }
            }
            return result;

        }
    }
}
