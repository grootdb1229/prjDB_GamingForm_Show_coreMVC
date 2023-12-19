using System.Web;

namespace prjDB_GamingForm_Show.Models.Member
{
    public class ReadHtmlContext
    {
        public static string ReadCshtmlFile(string filePath) 
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            try
            {
                // 讀取cshtml檔案的內容
                string cshtmlContent = File.ReadAllText(filePath);

                // 使用HtmlDecode解碼HTML實體
                string decodedHtml = HttpUtility.HtmlDecode(cshtmlContent);

                return decodedHtml;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }
}
