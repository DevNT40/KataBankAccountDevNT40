using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KataBankAccount.XUnit
{
    public class DateTimeProviderShould
    {
        [Fact]
        public void FormattedCurrentDate_ReturnsFormattedDate()
        {
            var FakDate = new FakeDateTimeProvider(new DateTime(2022, 2, 25));

            Assert.Equal("25/02/2022", FakDate.FormattedCurrentDate);
        }

        private class FakeDateTimeProvider : DateTimeProvider
        {
            private readonly DateTime _currentDateTime;

            public FakeDateTimeProvider(DateTime currentDateTime) => _currentDateTime = currentDateTime;

            protected override DateTime Today()
            {
                return _currentDateTime;
            }
        }
    }
}
