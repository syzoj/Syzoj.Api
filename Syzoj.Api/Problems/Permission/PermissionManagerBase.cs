using System.Collections.Generic;

namespace Syzoj.Api.Problems.Permission
{
    public abstract class PermissionManagerBase<T> : IPermissionManager<T>
        where T : Permission<T>
    {
        protected ISet<T> permissions;

        protected void AddPermission(T perm)
        {
            if(permissions.Add(perm))
            {
                foreach(T p in perm.ImpliedPermissions)
                {
                    AddPermission(p);
                }
            }
        }

        // TODO: Handle cyclic permission
        public bool HasPermission(T perm)
        {
            return permissions.Contains(perm) || (perm.ParentPermission != null && HasPermission(perm.ParentPermission));
        }
    }
}