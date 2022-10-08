using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Sendgrid.Webhooks.Converters;

namespace Sendgrid.Webhooks.Tests
{
    [TestFixture]
    public class BooleanConverterTests
    {
        private BooleanConverter _converter;
        private JsonSerializerOptions _options;

        [SetUp]
        public void SetUp()
        {
            _converter = new BooleanConverter();
            _options = new JsonSerializerOptions();
        }

        [TestCase(typeof(bool), true)]
        [TestCase(typeof(string), false)]
        public void CanConvert_Boolean_Only(Type tested, bool expected)
        {
            var result = _converter.CanConvert(tested);
            Assert.AreEqual(expected, result);
        }

        [TestCase("0", false)]
        [TestCase("1", true)]
        public void ReadJson_String_ConvertsToBoolean(String value, bool expected)
        {
	        var reader = new Utf8JsonReader(new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(value)));
            reader.Read();
            var result = _converter.Read(ref reader, typeof(bool), _options);

            Assert.AreEqual(expected, result);
        }

        [TestCase("0", false)]
        [TestCase("1", true)]
        public void WriteJson_Bool_ConvertsToString(String expected, bool value)
        {
	        string json;

	        ArrayBufferWriter<byte> stream = new ArrayBufferWriter<byte>();
	        using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
	        {
		        _converter.Write(writer, value, _options);
	        }
	        json = Encoding.UTF8.GetString(stream.WrittenSpan);
	        Console.WriteLine(json);

            Assert.AreEqual(expected, json);
        }
    }
}