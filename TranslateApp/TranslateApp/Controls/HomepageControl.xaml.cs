using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TranslateApp.Models;
using TranslateApp.ViewModels;

namespace TranslateApp.Controls
{
  /// <summary>
  /// Interaction logic for HomepageControl.xaml
  /// </summary>
  public partial class HomepageControl : UserControl
  {
    private HomepageViewModel viewModel;

    public HomepageControl()
    {
      InitializeComponent();
      viewModel = new HomepageViewModel();
      DataContext = viewModel;
      viewModel.LoadData();
    }

    private void WordList_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
    {
      // Only act on Commit
      //var context = new DictionaryEntities();

      //if (e.EditAction == DataGridEditAction.Commit)

      //{
      //  var c = e.Row.DataContext as WordModel;
      //  var x = context.Words.Where(w => w.Word_ID == c.ID);
      //  x = new Word
      //  {
      //    Word_ID = 
      //  };

      //  context.Save();


    }

    private void WordList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
      // Only act on Commit
      //var context = new DictionaryEntities();

      //if (e.EditAction == DataGridEditAction.Commit)

      //{
      //  var c = e.Row.DataContext as WordModel;
      //  var x = context.Words.Where(w => w.Word_ID == c.ID);


      //  context.SaveChanges();
      //}
    }

    private void WordList_KeyDown(object sender, KeyEventArgs e)
    {
      var u = e.OriginalSource as UIElement;
      if (e.Key == Key.Enter/* && u != null*/)
      {
        e.Handled = true;
        var item = WordList.SelectedItem as WordModel;
        viewModel.SaveChangesToDatabase(item);
        u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
      }
    }
  }
}