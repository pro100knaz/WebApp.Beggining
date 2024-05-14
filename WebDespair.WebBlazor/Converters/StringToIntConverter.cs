using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebDespair.WebBlazor.Converters
{
	public class StringToIntConverter : JsonConverter<string?>
	{
		public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Number)
			{
				var x =	reader.GetInt32().ToString();
				return x;
			}

			return reader.GetString();
		}

		public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}
	}
}
