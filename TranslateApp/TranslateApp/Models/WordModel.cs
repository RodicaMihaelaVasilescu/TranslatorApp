using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateApp.Models
{
  public class WordModel/* : BaseModel*/

  {
    private string _translatedText;

    public int Id { get; set; }
    public string OriginalText { get; set; }
    public string TranslatedText { get; set; }
    public string PhoneticSymbols { get; set; }
    public string Nouns { get; set; }
    public string Verbs { get; set; }
    public string Adjectives { get; set; }
    public string Phrasals { get; set; }
    public string Expressions { get; set; }

    public List<string> NounsList { get; set; }
    public List<string> VerbsList { get; set; }
    public List<string> AdjectivesList { get; set; }
    public List<string> PhrasalsList { get; set; }
    public List<string> ExpressionsList { get; set; }
  }
}
