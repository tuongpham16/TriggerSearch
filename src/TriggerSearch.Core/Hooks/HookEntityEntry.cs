using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriggerSearch.Core.Hooks
{
    public class HookedEntityEntry
    {
        public object Entity { get; private set; }
        public EntityState State { get; private set; }
        //
        // Summary:
        //     Provides access to change tracking information and loading information for all
        //     collection navigation properties of this entity.
        public IEnumerable<HookPropertyEntry> Collections { get; private set; }
        //
        // Summary:
        //     Provides access to change tracking information and loading information for all
        //     reference (i.e. non-collection) navigation properties of this entity.
        public IEnumerable<ReferenceEntry> References { get; private set; }
        //
        // Summary:
        //     Provides access to change tracking information and operations for all properties
        //     of this entity.
        public IEnumerable<HookPropertyEntry> Properties { get; private set; }

        //
        // Summary:
        //     Provides access to change tracking information and operations for all properties is change
        //     of this entity.
        public IEnumerable<HookPropertyEntry> PropertiesModified { get; private set; }

        //
        // Summary:
        //     Provides access to change tracking information and operations for all navigation
        //     properties of this entity.
        public IEnumerable<HookPropertyEntry> Navigations { get; private set; }


        public static implicit operator HookedEntityEntry(EntityEntry entry)
        {
            return new HookedEntityEntry()
            {
                Entity = entry.Entity,
                State = entry.State,
                Collections = entry.Collections.Select(item => new HookPropertyEntry()
                                                        {
                                                            Name = item.Metadata.Name,
                                                            CurrentValue = item.CurrentValue
                                                        }),
                Navigations = entry.Navigations.Select(item => new HookPropertyEntry()
                                                        {
                                                            Name = item.Metadata.Name,
                                                            CurrentValue = item.CurrentValue
                                                        }),
                Properties = entry.Properties.Select(item => new HookPropertyEntry()
                                                        {
                                                            Name = item.Metadata.Name,
                                                            OriginalValue = item.OriginalValue,
                                                            CurrentValue = item.CurrentValue
                                                        }),
                PropertiesModified = entry.Properties.Where(item => item.IsModified)
                                                        .Select(item => new HookPropertyEntry()
                                                        {
                                                            Name = item.Metadata.Name,
                                                            OriginalValue = item.OriginalValue,
                                                            CurrentValue = item.CurrentValue
                                                        }),
                References = entry.References
            };
        }
    }
}
