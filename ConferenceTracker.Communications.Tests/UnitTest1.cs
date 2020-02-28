using ConferenceTracker.Communications.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ConferenceTracker.Communications.Tests
{
	public class UnitTest1
	{
		[Fact]
		public async Task CanSendSms()
		{
			var sut = new TwilioSmsService();
			Assert.True(await sut.SendText("+13056322230", "test message"));

		}
	}
}
