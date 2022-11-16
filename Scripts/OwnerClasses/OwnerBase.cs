using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegisterApplication.Scripts.OwnerClasses
{
    public abstract class OwnerBase
    {
        public readonly string ownerName;
        public readonly OwnerBase ownerType;

        protected OwnerBase(string _ownerName, OwnerBase _ownerType)
        {
            ownerName = _ownerName;
            ownerType = _ownerType;
        }
    }
}
