using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using NUnit.Framework;
using Sendgrid.Webhooks.Converters;
using System.Buffers;

namespace Sendgrid.Webhooks.Tests
{
    [TestFixture]
    public class EpochConverterTests
    {
        private EpochToDateTimeConverter _converter;
        private JsonSerializerOptions _options;

        [SetUp]
        public void SetUp()
        {
            _converter = new EpochToDateTimeConverter();
            _options = new JsonSerializerOptions();
        }

        [TestCase(typeof(DateTime), true)]
        [TestCase(typeof(string), false)]
        public void CanConvert_DateTime_Only(Type tested, bool expected)
        {
            var result = _converter.CanConvert(tested);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReadJson_Long_ConvertsToDate()
        {
            //var reader = new JTokenReader(new JValue(123));
            JsonValue val = JsonValue.Create<long>(123);
            var reader = new Utf8JsonReader(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(val.ToString())));
            reader.Read();
            var result = _converter.Read(ref reader, typeof (long), _options);

            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 2, 3), result);
        }

        [Test]
        public void ReadJson_Double_ConvertsToDate()
        {
            //var reader = new JTokenReader(new JValue((double)123.555));
            JsonValue val = JsonValue.Create<double>(123.555);
            var reader = new Utf8JsonReader(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(val.ToString())));
            reader.Read();
            var result = _converter.Read(ref reader, typeof(long), _options);

            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 2, 3, 555), result);
        }

        [Test]
        public void WriteJson_Date_ConvertsToEpoch()
        {
	        string json;

	        ArrayBufferWriter<byte> stream = new ArrayBufferWriter<byte>();
	        using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
	        {
		        _converter.Write(writer, new DateTime(1980, 1, 1), _options);
	        }
	        json = Encoding.UTF8.GetString(stream.WrittenSpan);
	        Console.WriteLine(json);

	        Assert.AreEqual("315532800", json);
        }
    }
}