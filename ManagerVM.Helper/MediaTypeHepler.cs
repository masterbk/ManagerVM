using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Helper
{
    public static class MediaTypeHepler
    {
        static Dictionary<string, string> mediaTypeToExtension = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Hình ảnh
                { "image/jpeg", ".jpg" },
                { "image/png", ".png" },
                { "image/gif", ".gif" },
                { "image/bmp", ".bmp" },
                { "image/webp", ".webp" },
                { "image/tiff", ".tiff" },

                // Video
                { "video/mp4", ".mp4" },
                { "video/x-msvideo", ".avi" },
                { "video/mpeg", ".mpeg" },
                { "video/webm", ".webm" },
                { "video/ogg", ".ogv" },

                // Âm thanh
                { "audio/mpeg", ".mp3" },
                { "audio/wav", ".wav" },
                { "audio/ogg", ".oga" },
                { "audio/aac", ".aac" },
                { "audio/flac", ".flac" },

                // Tài liệu
                { "application/pdf", ".pdf" },
                { "application/msword", ".doc" },
                { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx" },
                { "application/vnd.ms-excel", ".xls" },
                { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ".xlsx" },
                { "application/vnd.ms-powerpoint", ".ppt" },
                { "application/vnd.openxmlformats-officedocument.presentationml.presentation", ".pptx" },

                // Văn bản
                { "text/plain", ".txt" },
                { "text/html", ".html" },
                { "text/css", ".css" },
                { "application/javascript", ".js" },
                { "application/json", ".json" },
                { "application/xml", ".xml" },

                // Nén
                { "application/zip", ".zip" },
                { "application/gzip", ".gz" },
                { "application/x-tar", ".tar" },
                { "application/x-rar-compressed", ".rar" },

                // Khác
                { "application/octet-stream", ".bin" },
                { "application/x-shockwave-flash", ".swf" },
                { "application/rtf", ".rtf" },
                { "application/vnd.oasis.opendocument.text", ".odt" },
                { "application/vnd.oasis.opendocument.spreadsheet", ".ods" }
            };
        public static string MediaTypeToExtension(this string mediaType)
        {
            return mediaTypeToExtension[mediaType];
        }
    }
}
