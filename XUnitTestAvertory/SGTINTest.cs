using BLL.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestAvertory
{
	public class SGTINTest
	{
		[Fact]
		public void GivenSGTINInHexadecimalFormat_WhenSGTINIsDecoded_ThenSGTINHasAllCorrectInformation()
		{
			// given
			var sgtin = "3074257BF7194E4000001A85";

			// when
			var mockLogger = new Mock<ILogger<SGTIN96Service>>();
			var sgtinService = new SGTIN96Service(mockLogger.Object);
			var decodedSGTIN = sgtinService.Decode(sgtin);

			// then
			Assert.Equal(3u, decodedSGTIN.Filter);
			Assert.Equal(614141u, decodedSGTIN.CompanyPrefix);
			Assert.Equal(812345u, decodedSGTIN.ItemReference);
			Assert.Equal(6789u, decodedSGTIN.SerialNumber);
		}

		[Theory]
		[InlineData("30HB747BA582600005AE9068")]
		[InlineData("30FB747BA582600005AE906842")]
		public void GivenInvalidHexadecimalSGTINFormat_WhenSGTINIsDecoded_ThenDecodingFailsWithError(string sgtin)
		{
			// when
			var mockLogger = new Mock<ILogger<SGTIN96Service>>();
			var sgtinService = new SGTIN96Service(mockLogger.Object);

			Assert.Throws<ArgumentException>(() => sgtinService.Decode(sgtin));
		}

		[Theory]
		[ClassData(typeof(SGTINTestData))]
		public void GivenMultipleSGTINInHexadecimalFormat_WhenSGTINIsDecoded_ThenSGTINIsProperlyDecodedAndExistsInExpectedData(string sgtin)
		{
			var mockLogger = new Mock<ILogger<SGTIN96Service>>();
			var sgtinService = new SGTIN96Service(mockLogger.Object);

			var decodedSGTIN = sgtinService.Decode(sgtin);

			Assert.True(_expectedCompanyItems.ContainsKey(decodedSGTIN.CompanyPrefix));
			Assert.True(_expectedCompanyItems.ContainsValue(decodedSGTIN.ItemReference));
		}

		private static readonly Dictionary<ulong, ulong> _expectedCompanyItems = new Dictionary<ulong, ulong>
		{
			{3319361,   407205 },
			{6314853123,    877 },
			{39266333,  21526 },
			{213645,    6152432 },
			{178504,    2577266 },
			{964495,    3508204 },
			{585675,    4259784 },
			{9488123,   147805 },
			{86906, 1437603 },
			{2973701,   682407 },
			{7942595,   565126 },
			{587451,    1074506 },
			{40777411,  36953 },
			{227113,    5935575 },
			{3733204,   647520 },
			{885734,    8756756 },
			{2871566,   145216 },
			{584202,    2876851 },
			{5032891234,    138 },
			{1999335,   642804 },
			{83250532,  52838 },
			{516232,    7222702 },
			{43907, 3406731 },
			{4584903,   679405 },
			{983682,    4365465 },
			{686748,    9119606 },
			{678362,    4156060 },
			{591271,    5771945 },
			{69124, 1253252 },
			{107222,    797011 },
			{140037,    9818542 },
			{750917,    7528000 },
			{317580317580,    7 },
			{390007,    9160738 },
			{528209,    7791950 },
			{2180435,   909583 },
			{823332,    6492584 },
			{511751,    6644044 },
			{543540,    3141656 },
			{826025,    1130405 },
			{110720,    5041310 },
			{33314622,  53619 },
			{764493764493,    4 },
			{428651,    7619450 },
			{950316,    2380884 },
			{870953,    8728496 },
			{8393604321,    179 },
			{719065,    9765179 },
			{758601,    2337085 },
			{368305,    2620417 },
			{791586,    5775051 },
			{28600054,  41912 },
			{608240,    2412148 },
			{60643, 9948173 },
			{127083,    5256251 },
			{178540,    3505943 },
			{51578, 9086830 },
			{21276, 9837077 },
			{745230,    6232899 },
			{107956107956,   2 },
			{7002011,   482701 },
			{4619611,   162323 },
			{108653,    1180386 },
			{814875,    5090895 },
			{378922,    8414833 },
			{4499823,   351572 },
			{1210999912,    123 },
			{900706,    8213657 },
			{7408273,   664337 },
			{828415,    5417914 },
			{451802,    2638491 },
			{169691,    87387 },
			{563974,    4156235 },
			{77605, 5749311 },
			{349605,    4548221 },
			{342472,    6004566 },
			{770270770270,   9 },
			{327884,    7703818 },
			{812302,    5276620 },
			{905710,    9832832 },
			{997970,    9212388 },
			{917969,    3602750 },
			{96652, 6172545 },
			{51151534,  78331 },
			{5023208822,    521 },
			{446617,    9353565 },
			{13332, 4770766 },
			{600851600851,   2 },
			{196645,    1236529 },
			{4984121,   901844 },
			{676413,    8849362 },
			{485771,    5339535 },
			{793681,    9774282 },
			{3209622,   461277 },
			{823413,    4397760 },
			{623828,    4865650 },
			{655062,    1232213 },
			{472731472731,   2 },
			{443750,    5573641 },
			{2777462999,    401 }
		};

}
}
