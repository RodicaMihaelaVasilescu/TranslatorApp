using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateApp.Models
{
  public class WordModel
  {

    public int Id { get; set; }
    public string OriginalText { get; set; }
    public string TranslatedText { get; set; }
    public string PhoneticSymbols { get; set; }
    public string Nouns { get; set; }
    public string Verbs { get; set; }
    public string Adjectives { get; set; }
    public string Phrasals { get; set; }
    public string Notes { get; set; }
    public bool IsFavorite { get; set; }
    public bool CanBeDeleted { get; set; }
  }
}
