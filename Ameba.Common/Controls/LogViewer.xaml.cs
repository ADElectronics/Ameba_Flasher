using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Ameba.Common.Controls
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }));
        }
    }
    public class LogEntry : PropertyChangedBase
    {
        public UInt32 Index { get; set; }
        public string Message { get; set; }
    }

    public class CollapsibleLogEntry : LogEntry
    {
        public List<LogEntry> Contents { get; set; }
    }

    public partial class LogViewer : UserControl
    {
        public ObservableCollection<LogEntry> LogEntries { get; set; }
        public UInt32 IndexTotal { get; private set; }

        public void AddEntry(LogEntry en)
        {
            if(en.Index > IndexTotal)
            {
                Dispatcher.BeginInvoke((Action)(() => LogEntries.Add(en)));
                IndexTotal = en.Index;
            }  
        }

#pragma warning disable CS0114
        public void AddText(string text)
#pragma warning restore CS0114
        {
            Dispatcher.BeginInvoke((Action)(() => LogEntries.Add(new LogEntry() { Index = IndexTotal++, Message = text })));
        }

        public LogViewer()
        {
            InitializeComponent();
            IndexTotal = 0;
            LogEntries = new ObservableCollection<LogEntry>();
            DataContext = this;
        }
    }
}
