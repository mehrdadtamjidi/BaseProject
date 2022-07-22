using Entities.Account;
using Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Access
{
    public class UserRole : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region Relations

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        #endregion
    }
}
