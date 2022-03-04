using System;
using System.Collections.Generic;
using System.Text;

namespace KataBankAccount
{
    public interface IDateTimeProvider
    {
        string FormattedCurrentDate { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        private const string CurrentDateFormat = "dd/MM/yyyy";

        public string FormattedCurrentDate => Today().ToString(CurrentDateFormat);

        protected virtual DateTime Today()
        {
            return DateTime.Today;
        }
    }
}
