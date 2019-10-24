using System;

namespace CoreCooking.Website.ViewModels
{
    public class ViewModelBase
    {
        public Guid Guid { get; set; }

        public bool IsNew()
        {
            return this.Guid.Equals(Guid.Empty);
        }
    }
}
