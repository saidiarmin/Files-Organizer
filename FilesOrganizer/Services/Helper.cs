using System.Collections.Generic;
using System.Linq;


namespace FilesOrganizer.Services
{
    static class Helper
    {
        private static readonly List<string> validExtensions = new List<string>
        {
            "JPG",
            "PNG",
            "JPEG",
            "TIFF",
            "BMP",
            "MP4",
            "MOV"
        };

        public static bool IsValidExtension(string fileExtension)
        {
            return validExtensions.Any(e => e == fileExtension.ToUpper());
        }
    }
}
