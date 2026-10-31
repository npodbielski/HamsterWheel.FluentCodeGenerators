using System.Text.Encodings.Web;
using System.Text.Json;

namespace HamsterWheel.FluentCodeGenerators;

public static class JsonElementExtensions
{
    private static readonly JsonSerializerOptions Readable = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    extension(JsonElement element)
    {
        /// <summary>
        /// The C# literal of the JSON value: a string is quoted and escaped, numbers, booleans and
        /// null are written as they are, and a structured value (object, array) is written as its
        /// raw JSON text.
        /// </summary>
        public string ToCodeLiteral() =>
            element.ValueKind switch
            {
                JsonValueKind.String => JsonSerializer.Serialize(element.GetString(), Readable),
                JsonValueKind.Number => element.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "null",
                _ => element.GetRawText(),
            };
    }
}
