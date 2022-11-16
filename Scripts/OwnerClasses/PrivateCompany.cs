using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegisterApplication.Scripts.OwnerClasses
{
    class PrivateCompany : OwnerBase
    {
        PrivateCompany(string name, OwnerBase type) : base(name, type) { }
    }
}
