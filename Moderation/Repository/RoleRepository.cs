using Moderation.DbEndpoints;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    internal class RoleRepository: Repository<RoleAndPermission>
    {
        public RoleRepository(Dictionary<Guid, RoleAndPermission> data) : base(data) { }

        public RoleRepository() : base() { }

        public override bool Add(Guid key, RoleAndPermission role)
        {
            RoleEndpoints.CreateRole(role);
            return true;
        }

        public override bool Remove(Guid key)
        {
            RoleEndpoints.DeleteRole(key);
            return true;
        }

        public override RoleAndPermission? Get(Guid key)
        {
            return RoleEndpoints.ReadRole().Find((role) => role.Id == key);
        }

        public override IEnumerable<RoleAndPermission> GetAll()
        {
            return RoleEndpoints.ReadRole();
        }

        public override bool Contains(Guid key)
        {
            return RoleEndpoints.ReadRole().Exists((role) => role.Id == key);
        }

        public override bool Update(Guid key, RoleAndPermission value)
        {
            RoleEndpoints.UpdateRoleName(key, value.Name);
            RoleEndpoints.UpdateRolePermissions(key, value.Permissions);
            return true;
        }
    }
}