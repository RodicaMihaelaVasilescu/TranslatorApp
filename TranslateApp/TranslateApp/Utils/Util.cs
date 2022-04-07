using System;
using System.Net;
using System.Text;
using System.Web;

namespace TranslateApp.Utils
{
  public static class Util
  {
    public static string TranslateWord(String word, string fromLanguage = "en", string toLanguage = "ro")
    {
      if (string.IsNullOrEmpty(word))
      {
        return null;
      }
      var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
      var webClient = new WebClient
      {
        Encoding = Encoding.UTF8
      };
      var result = webClient.DownloadString(url);
      try
      {
        result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
        return result;
      }
      catch
      {
        return "Error";
      }
    }

  }
}
