using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace ReadTyping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string InputText;
        public bool buttonPressed = false;
        public List<char> characterList;
        public List<string> wordList;
        private int characterLoop;
        private int wordLoop;
        private bool toggleChecking = false;
        private bool correct = false;
        private bool failed = false;
        private bool wordComplete = false;

        private void read_Click(object sender, RoutedEventArgs e)
        {
            InputText = inputBox.Text;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            InputText = rgx.Replace(InputText, "");
            wordList = InputText.Split(' ').ToList();
            textBlock.Text = InputText;
            inputBox.Visibility = Visibility.Hidden;
            textBlock.Visibility = Visibility.Visible;
            wordBox.Content = wordList[0];
            if(wordList.Count != 1)
                nextWord.Content = wordList[wordLoop + 1];
            toggleChecking = true;
        }

        public void ReadKey()
        {
            SpinWait.SpinUntil(() => correct);
        }

        public Task<bool> Checker(string e)
        {
            return Task.Run(() =>
            {
                string requiredCharacter = wordList[wordLoop][characterLoop].ToString();
                if (e.ToLower() == requiredCharacter.ToLower())
                {
                    correct = true;
                    characterLoop++;
                    int currentPosition = characterLoop;
                    int endOfWord = wordList[wordLoop].Length;
                    if (currentPosition == endOfWord)
                    {
                        wordLoop++;
                        characterLoop = 0;
                        wordComplete = true;
                    }
                }
                else
                {
                    failed = true;
                }
                return true;
            });
        }

        private async void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (wordList != null)
            {
                if (toggleChecking && e.Key != Key.Enter)
                {
                    await Checker(e.Key.ToString());
                    if (failed)
                    {
                        Main.Background = new SolidColorBrush(Colors.OrangeRed);
                        failed = false;
                    }
                    else if (correct)
                    {
                        BrushConverter bc = new BrushConverter();
                        Main.Background = (Brush)bc.ConvertFrom("#FF212121");
                        bool set = false;
                        switch (characterLoop)
                        {
                            case 1:
                                good1.Visibility = Visibility.Visible;
                                break;
                            case 2:
                                good2.Visibility = Visibility.Visible;
                                break;
                            case 3:
                                good3.Visibility = Visibility.Visible;
                                break;
                            case 4:
                                good4.Visibility = Visibility.Visible;
                                break;
                            case 5:
                                good5.Visibility = Visibility.Visible;
                                break;
                            case 6:
                                good6.Visibility = Visibility.Visible;
                                break;
                            case 7:
                                good7.Visibility = Visibility.Visible;
                                break;
                            case 8:
                                good8.Visibility = Visibility.Visible;
                                break;
                            case 9:
                                good9.Visibility = Visibility.Visible;
                                break;
                            case 10:
                                good10.Visibility = Visibility.Visible;
                                break;
                            case 11:
                                good11.Visibility = Visibility.Visible;
                                break;

                        }
                        if (wordLoop != wordList.Count)
                        {
                            if(wordComplete)
                                HideIcons();
                            wordComplete = false;
                            wordBox.Content = wordList[wordLoop];
                            if(wordList.Count != 1 && wordList.Count - 1 > wordLoop + 1)
                             nextWord.Content = wordList[wordLoop + 1];
                            if(wordLoop > 0)
                            lastWord.Content = wordList[wordLoop - 1];
                        }
                        else if (wordComplete && wordLoop == wordList.Count)
                        {
                            Done.Visibility = Visibility.Visible;
                            wordList.Clear();
                        }
                    }
                }
            }
        }
        public void HideIcons()
        {
            good1.Visibility = Visibility.Hidden;
            good2.Visibility = Visibility.Hidden;
            good3.Visibility = Visibility.Hidden;
            good4.Visibility = Visibility.Hidden;
            good5.Visibility = Visibility.Hidden;
            good6.Visibility = Visibility.Hidden;
            good7.Visibility = Visibility.Hidden;
            good8.Visibility = Visibility.Hidden;
            good9.Visibility = Visibility.Hidden;
            good10.Visibility = Visibility.Hidden;
            good11.Visibility = Visibility.Hidden;
        }
    }
}
