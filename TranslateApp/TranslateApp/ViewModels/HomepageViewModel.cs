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
using System.Windows.Input;
using Prism.Commands;

namespace TranslateApp.ViewModels
{
  public class HomepageViewModel : BaseViewModel
  {
    private TranslateAppEntities _context;
    private ObservableCollection<WordModel> wordList;
    private string searchItem;
    private ObservableCollection<WordModel> AllWordList;
    private bool showAllFavorites;

    public bool ShowAllFavorites
    {
      get { return showAllFavorites; }
      set
      {
        showAllFavorites = value;
        if (value)
        {
          WordList = new ObservableCollection<WordModel>(AllWordList.Where(w => w.IsFavorite == value).ToList());
        }
        else
        {
          WordList = new ObservableCollection<WordModel>(AllWordList);
        }
        OnPropertyChanged();
      }
    }

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

    public string SearchItem
    {
      get { return searchItem; }
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          WordList = new ObservableCollection<WordModel>(AllWordList);
        }
        else
        {
          WordList = new ObservableCollection<WordModel>(AllWordList.Where(w => w.OriginalText.Contains(value)).ToList());
        }
        searchItem = value;
        OnPropertyChanged();
      }
    }

    public HomepageViewModel()
    {
      _context = new TranslateAppEntities();
      RefreshList();
    }

    public bool MarkAsFavorite(WordModel word)
    {
      var dbItem = _context.Words.FirstOrDefault(w => w.Word_Id == word.Id);
      if (dbItem == null)
      {
        //the word does not exist
        return false;
      }

      try
      {
        dbItem.IsFavorite = !word.IsFavorite;
        _context.SaveChanges();
      }
      catch
      {
        return false;
      }

      RefreshList();
      return true;
    }

    public void DeleteItem(WordModel word)
    {
      var dbItem = _context.Words.FirstOrDefault(w => w.Word_Id == word.Id);
      if (dbItem == null)
      {
        //the word does not exist
        return;
      }
      _context.Words.Remove(dbItem);
      _context.SaveChanges();
      RefreshList();
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
        dbItem.Notes = word.Notes;
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

      if (word.TranslatedText == null)
      {
        word.TranslatedText = Util.TranslateWord(word.OriginalText);
      }

      word.PhoneticSymbols = "[" + Util.GetPhonetic(word.OriginalText) + "]";

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
        Notes = w.Notes,
        IsFavorite = w.IsFavorite
      }));
      AllWordList = new ObservableCollection<WordModel>(WordList);
    }
  }
}
