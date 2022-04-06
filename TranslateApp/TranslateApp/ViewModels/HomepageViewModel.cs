using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TranslateApp.Database;
using TranslateApp.Models;

namespace TranslateApp.ViewModels
{
  public class HomepageViewModel : BaseViewModel
  {
    private TranslateAppEntities _context;
    private ObservableCollection<WordModel> wordList;
    public ObservableCollection<WordModel> WordList
    {
      get { return wordList; }
      set
      {
        if (wordList == value)
        {
          return;
        }
        wordList = value;
        OnPropertyChanged();
      }
    }
    public HomepageViewModel()
    {
      _context = new TranslateAppEntities();
      RefreshList();
    }

    public String Translate(String word)
    {
      var toLanguage = "ro";//English  
      var fromLanguage = "en";//Deutsch  
      var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
      var webClient = new WebClient
      {
        Encoding = System.Text.Encoding.UTF8
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
    public void LoadData()
    {
      var wordList = new ObservableCollection<Word>(_context.Words.ToList());
      WordList = new ObservableCollection<WordModel>();
      foreach (var w in wordList)
      {
        var word = new WordModel
        {
          Id = w.Word_Id,
          OriginalText = w.OriginalText,
          TranslatedText = w.TranslatedText,
          PhoneticSymbols = w.PhoneticSymbols,
        };

        WordList.Add(word);
      }

    }

    public void SaveChangesToDatabase(WordModel word)
    {
      if (_context.Words.Any(w => w.OriginalText == word.OriginalText))
      {
        return;
      }

      if (word.Id != 0)
      {
        var dbItem = _context.Words.FirstOrDefault(w => w.Word_Id == word.Id);
        dbItem.OriginalText = word.OriginalText;
        dbItem.PhoneticSymbols = word.PhoneticSymbols;
        dbItem.TranslatedText = word.TranslatedText;
        _context.SaveChanges();
      }
      else
      {
        if(string.IsNullOrEmpty(word.TranslatedText))
        {
          word.TranslatedText = Translate(word.OriginalText);
        }

        _context.Words.Add(new Word
        {
          OriginalText = word.OriginalText,
          PhoneticSymbols = word.PhoneticSymbols,
          TranslatedText = word.TranslatedText
        });
        _context.SaveChanges();
        RefreshList();
      }
    }

    private void RefreshList()
    {
      WordList = new ObservableCollection<WordModel>(_context.Words.Select(w => new WordModel
      {
        OriginalText = w.OriginalText,
        TranslatedText = w.TranslatedText
      }));
    }
  }
}
