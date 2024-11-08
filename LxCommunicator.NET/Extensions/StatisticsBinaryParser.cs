using System;
using System.Collections.Generic;
using System.IO;

namespace Loxone.Communicator.Extensions
{
	public class StatisticsBinaryParser 
	{
		public static List<DataPoint> ParseBinaryResponse(byte[] binaryData, int numberOfOutputs)
		{
			var dataPoints = new List<DataPoint>();

			using (var memoryStream = new MemoryStream(binaryData))
			using (var reader = new BinaryReader(memoryStream))
			{
				while (memoryStream.Position < memoryStream.Length)
				{
					uint unixTimestamp = ReadBigEndianUInt32(reader);
					DateTime timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
					
					var values = new List<double>();
					for (int i = 0; i < numberOfOutputs; i++)
					{
						double value = ReadBigEndianDouble(reader);
						values.Add(value);
					}
					
					var dataPoint = new DataPoint
					{
						Timestamp = timestamp,
						Values = values
					};

					dataPoints.Add(dataPoint);
				}
			}

			return dataPoints;
		}

		private static uint ReadBigEndianUInt32(BinaryReader reader)
		{
			byte[] bytes = reader.ReadBytes(sizeof(uint));
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			return BitConverter.ToUInt32(bytes, 0);
		}

		private static double ReadBigEndianDouble(BinaryReader reader)
		{
			byte[] bytes = reader.ReadBytes(sizeof(double));
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			return BitConverter.ToDouble(bytes, 0);
		}
	}
	
	public class DataPoint
	{
		public DateTime Timestamp { get; set; }
		public List<double> Values { get; set; } = new List<double>();
	}
}