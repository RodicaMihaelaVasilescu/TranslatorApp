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
using TranslateApp.Utils;
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

    public bool SaveChangesToDatabase(WordModel word)
    {
      var dbItem = _context.Words.FirstOrDefault(w => w.Word_Id == word.Id);
      if (dbItem == null)
      {
        //the word does not exist
        return false;
      }

      try
      {
        dbItem.OriginalText = word.OriginalText;
        dbItem.PhoneticSymbols = word.PhoneticSymbols;
        dbItem.TranslatedText = word.TranslatedText;
        _context.SaveChanges();
      }
      catch
      {
        return false;
      }

      return true;
    }

    public bool AddLineInDatabase(WordModel word)
    {
      if (string.IsNullOrEmpty(word.OriginalText) ||
        _context.Words.Any(w => w.OriginalText == word.OriginalText))
      {
        return false;
      }

      word.TranslatedText = Util.TranslateWord(word.OriginalText);

      _context.Words.Add(new Word
      {
        OriginalText = word.OriginalText,
        PhoneticSymbols = word.PhoneticSymbols,
        TranslatedText = word.TranslatedText,
        Notes = word.Notes
      });

      _context.SaveChanges();
      RefreshList();
      return true;
    }

    public void RefreshList()
    {
      WordList = new ObservableCollection<WordModel>(_context.Words.Select(w => new WordModel
      {
        Id = w.Word_Id,
        OriginalText = w.OriginalText,
        TranslatedText = w.TranslatedText,
        PhoneticSymbols = w.PhoneticSymbols,
        Notes = w.Notes
      }));
    }
  }
}
