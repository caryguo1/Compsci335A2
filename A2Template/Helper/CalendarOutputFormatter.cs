using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using A2Template.Models;

namespace WebAPIvCard.Helper
{
    public class CalendarOutputFormatter : TextOutputFormatter
    {
        public CalendarOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/calendar"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        // Helper function for ensuring that special characters work properly.
        private string EscapeText(string text)
        {
            return text
                .Replace("\\", "\\\\")
                .Replace(";", "\\;")
                .Replace(",", "\\,")
                .Replace("\r\n", "\\n")
                .Replace("\n", "\\n")
                .Replace("\r", "\\n");
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            Event retrievedEvent = (Event)context.Object;
            StringBuilder builder = new StringBuilder();

            const string CRLF = "\r\n";

            builder.Append("BEGIN:VCALENDAR").Append(CRLF);
            builder.Append("VERSION:2.0").Append(CRLF);
            builder.Append("PRODID:bguo686").Append(CRLF);

            builder.Append("BEGIN:VEVENT").Append(CRLF);
            builder.Append("UID:").Append(retrievedEvent.Id.ToString()).Append(CRLF);
            builder.Append("DTSTAMP:").Append(DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmss'Z'")).Append(CRLF);
            builder.Append("DTSTART:").Append(retrievedEvent.Start).Append(CRLF);
            builder.Append("DTEND:").Append(retrievedEvent.End).Append(CRLF);
            builder.Append("SUMMARY:").Append(EscapeText(retrievedEvent.Summary)).Append(CRLF);
            builder.Append("DESCRIPTION:").Append(EscapeText(retrievedEvent.Description)).Append(CRLF);
            builder.Append("LOCATION:").Append(EscapeText(retrievedEvent.Location)).Append(CRLF);

            builder.Append("END:VEVENT").Append(CRLF);
            builder.Append("END:VCALENDAR").Append(CRLF);

            string outString = builder.ToString();
            byte[] outBytes = selectedEncoding.GetBytes(outString);
            var response = context.HttpContext.Response.Body;
            return response.WriteAsync(outBytes, 0, outBytes.Length);
        }
    }
}
