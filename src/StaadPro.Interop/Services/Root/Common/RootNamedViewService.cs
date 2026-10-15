using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Services.Root.Common
{
    internal static class RootNamedViewService
    {
        internal static void CreateNamedView(object root, string viewName, int creationFlag)
        {
            ValidateName(viewName);
            ((dynamic)root).CreateNamedView(viewName, creationFlag);
        }

        internal static void SaveNamedView(object root, string viewName)
        {
            ValidateName(viewName);
            ((dynamic)root).SaveNamedView(viewName);
        }

        internal static void RemoveNamedView(object root, string viewName)
        {
            ValidateName(viewName);
            // The inspected interface passes nError by value. Zero initializes it; no status is inferred.
            ((dynamic)root).RemoveNamedView(viewName, 0);
        }

        internal static void ModifyNamedView(object root, string viewName, IEnumerable<int> entityIds,
            NamedViewEntityType entityType, int modificationFlag)
        {
            ValidateName(viewName);
            if (entityIds == null) throw new ArgumentNullException(nameof(entityIds));
            if (!Enum.IsDefined(typeof(NamedViewEntityType), entityType))
                throw new ArgumentOutOfRangeException(nameof(entityType));
            int[] ids = entityIds.ToArray();
            if (ids.Any(id => id <= 0))
                throw new ArgumentException("Entity identifiers must be positive.", nameof(entityIds));
            ((dynamic)root).ModifyNamedView(viewName, ids, (int)entityType, modificationFlag);
        }

        private static void ValidateName(string viewName)
        {
            if (viewName == null) throw new ArgumentNullException(nameof(viewName));
            if (string.IsNullOrWhiteSpace(viewName))
                throw new ArgumentException("The view name must contain text.", nameof(viewName));
        }
    }
}
