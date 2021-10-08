using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using DynamicData;
using ReactiveUI;

namespace Err_114.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private readonly SourceList<string> m_data = new();

    public string Greeting => "Welcome to Avalonia!";

    public MainWindowViewModel()
    {
      m_data.Connect()
            .ObserveOn( RxApp.MainThreadScheduler )
            .Bind( out var display_list )
            .Subscribe();

      Items = display_list;
    }

    public ReadOnlyObservableCollection<string> Items { get; }

    public void ToggleData()
    {
      if( m_data.Count <= 1 )
      {
        m_data.Edit( Updater =>
                     {
                       Updater.Clear();

                       for( int i = 0; i < 100; i++ )
                       {
                         Updater.Add(i.ToString());
                       }
                     } );
        
        return;
      }
      
      m_data.Edit( Updater =>
                   {
                     Updater.Clear();
                     Updater.Add("Test");
                   } );
    }
  }
}