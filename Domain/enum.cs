using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum TheLoai : int
    {
        #region
        [StringValue(@"Italian")]
        Italian = 1,
        [StringValue(@"American")]
        American = 2,
        [StringValue(@"Thai")]
        Thai = 3,
        [StringValue(@"Japanese")]
        Japanese = 4
        #endregion




    }
    public enum DoKho : int
    {
        #region
        [StringValue(@"Easy")]
        Easy = 1,
        [StringValue(@"Intermediate")]
        Intermediate = 2,
        [StringValue(@"Advanced")]
        Advanced = 3,
        [StringValue(@"Expert")]
        Expert = 4
        #endregion




    }
}
