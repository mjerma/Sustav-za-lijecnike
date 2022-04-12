using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SustavZaLijecnike
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Ambulanta ambulanta;
        public Zaposlenik currentUser;
        public Pacijent tempPacijent;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetAmbulanta(Zaposlenik zaposlenik) 
        {
            ambulanta = Repository.GetAmbulanta(zaposlenik);
            currentUser = zaposlenik;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (MainFrame == null) return;
            string rbContent = radioButton.Content.ToString();

            switch (rbContent)
            {
                case "Pacijenti":
                    MainFrame.Navigate(new Uri("Pages/PacijentiPage.xaml", UriKind.Relative));
                    break;
                case "Narudžbe":
                    MainFrame.Navigate(new Uri("Pages/NarudzbePage.xaml", UriKind.Relative));
                    break;
                case "Termini":
                    MainFrame.Navigate(new Uri("Pages/TerminiPage.xaml", UriKind.Relative));
                    break;
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = e.Source;

            ScrollViewer scv = (ScrollViewer)sender;
            scv.RaiseEvent(eventArg);
            e.Handled = true;
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();
        }
    }
}
