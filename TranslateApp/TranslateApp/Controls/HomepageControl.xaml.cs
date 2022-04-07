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
    }

    private void WordList_KeyDown(object sender, KeyEventArgs e)
    {
      var u = e.OriginalSource as UIElement;
      if (e.Key == Key.Enter/* && u != null*/)
      {
        e.Handled = true;
        var item = WordList.SelectedItem as WordModel;
        if (item != null)
        {
          if (item.Id == 0)
          {
            viewModel.AddLineInDatabase(item);
            WordList.ScrollIntoView(WordList.Items[WordList.Items.Count - 1]);
          }
          else
          {
            viewModel.SaveChangesToDatabase(item);
          }
        }
        u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
      }
    }


    private void FavoriteButton_Click(object sender, RoutedEventArgs e)
    {
      var item = WordList.SelectedItem as WordModel;
      if (item != null)
      {
        viewModel.MarkAsFavorite(item);
      }
    }

    private void AllFavoritesButton_Click(object sender, RoutedEventArgs e)
    {
      viewModel.ShowAllFavorites = !viewModel.ShowAllFavorites;
    }

    private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
    {
      var item = WordList.SelectedItem as WordModel;
      if (item == null)
      {
        return;
      }
      const string message =
        "Are you sure that you would like to close the form?";
      const string caption = "Form Closing";
      var result = System.Windows.MessageBox.Show(message, caption,
                                   MessageBoxButton.OKCancel);

      // If the no button was pressed ...
      if (result == MessageBoxResult.OK)
      {
        // cancel the closure of the form.
        viewModel.DeleteItem(item);
      }

    }
  }
}