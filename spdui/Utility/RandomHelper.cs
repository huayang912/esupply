using System;
using System.Security.Cryptography;

namespace Dndp.Utility
{
	/// <summary>
	/// The Helper Class for Test.
	/// </summary>
	public sealed class RandomHelper
	{
        private RandomHelper()
		{
		}

		/// <summary>
		/// Generate a random string, and the length less than or equal 50
		/// </summary>
		/// <returns></returns>
		public static string RandomString()
		{
			uint width = RandomUInt(50);
			return RandomString(width);
		}
		
		public static string RandomString(uint width)
		{
			System.Text.StringBuilder ret = new System.Text.StringBuilder();
			
			for (uint i = 0; i < width; i++)
			{
				uint data = RandomUInt(26);
				data += (uint)('a');
				ret.Append((char)(data));
			}

			if (ret.Length <= 0)
			{
				ret.Append('a');
			}

			return ret.ToString();
		}

		public static uint RandomUInt()
		{
			byte[] randomData = new byte[4];
			(new RNGCryptoServiceProvider()).GetNonZeroBytes(randomData);

			return ((uint)randomData[0])
				+ ((uint)randomData[1]) * 256
				+ ((uint)randomData[2]) * 256 * 256
				+ ((uint)randomData[3]) * 256 * 256 * 256;
		}

		public static uint RandomUInt(uint maxValue)
		{
			return (RandomUInt() % maxValue);
		}

		public static uint RandomUInt(uint minValue, uint maxValue)
		{
			return ((RandomUInt() % maxValue) + minValue) % maxValue;
		}

		public static int RandomInt()
		{
			uint flag = RandomUInt() % 2;
			if (flag == 0)
			{
				return (int)RandomUInt(int.MaxValue);
			}
			else
			{
				return -((int)RandomUInt(int.MaxValue));
			}
		}

		public static double RandomDouble()
		{
			uint flag = RandomUInt() % 2;
			if (flag == 0)
			{
				return ((double)RandomUInt()) / (256.0*256.0*256.0*256.0);
			}
			else
			{
				return -((double)RandomUInt()) / (256.0*256.0*256.0*256.0);
			}
		}

		public static byte[] RandomBinaryBlob()
		{
			uint len = RandomUInt(2000);
			
			byte[] randomData = new byte[len];
			(new RNGCryptoServiceProvider()).GetNonZeroBytes(randomData);

			return randomData;
		}

		public static bool RandomBool()
		{
			int randomInt = RandomInt();
			return (randomInt % 2 == 0);
		}

		public static string RadomEmailAddress()
		{
			return RandomString(20) + "@test.com";
		}

	}
}
