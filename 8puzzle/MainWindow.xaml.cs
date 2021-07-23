using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace _8puzzle
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        ArrayList answer = null;
        int answerLong = 0;
        int answerTime = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            string[] number = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
               Random r = new Random();
               for (int i = 8; i > 0; i--)
                   {
                       string a;
                       int tmp;
                       tmp = r.Next(0, i);
                       a = number[tmp];
                       number[tmp] = number[i];
                      number[i] = a;
                 }
            Data.Text = string.Join("", number);
        }
        public void setButton(String Data)
        {
            buttonVisibility(button1, "" + Data[0]);
            buttonVisibility(button2, "" + Data[1]);
            buttonVisibility(button3, "" + Data[2]);
            buttonVisibility(button4, "" + Data[3]);
            buttonVisibility(button5, "" + Data[4]);
            buttonVisibility(button6, "" + Data[5]);
            buttonVisibility(button7, "" + Data[6]);
            buttonVisibility(button8, "" + Data[7]);
            buttonVisibility(button9, "" + Data[8]);
            /*button1.Content = Data[0];
            button2.Content = Data[1];
            button3.Content = Data[2];
            button4.Content = Data[3];
            button5.Content = Data[4];
            button6.Content = Data[5];
            button7.Content = Data[6];
            button8.Content = Data[7];
            button9.Content = Data[8];*/
        }
        public void buttonVisibility(Button a,String Data)
        {
            if (Data.Equals("0"))
            {
                a.Visibility =Visibility.Collapsed;
            }
            else
            {
                a.Visibility = Visibility.Visible;
            }
            a.Content = Data;
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            if (!(Data.Text.Length == 9))
            {
                MessageBox.Show("Wrong Format");
                return;
            }
            setButton(Data.Text);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!(Data.Text.Length == 9))
            {
                MessageBox.Show("Wrong Format");
                return;
            }
            Start.Visibility = Visibility.Collapsed;
            Set.Visibility = Visibility.Collapsed;
            setButton(Data.Text);
            Grid BFS = new Grid();
            BFS.Start(Data.Text);
            answer = null;
            bool Find = false;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時
            while (BFS.BFSQueue.Count != 0)
            {                         
                String current = BFS.BFSQueue.Dequeue();
                answer = BFS.steps;
                if (!(answer.Count == 0))
                {
                    sw.Stop();//碼錶停止
                    Find = true;
                    answer.Reverse();
                    break;
                }
                BFS.move(current);
            }
            if (Find)
            {
                Text.Content = "Use Time " + sw.Elapsed.TotalMilliseconds.ToString()+ " Millisecond";
                answerLong = answer.Count-1;
                answerTime = 0;
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(0.5);
                timer.Tick += timer_Tick;
                timer.Start();

            }
            else
            {
                Text.Content = "Can't Find Use Time " + sw.Elapsed.TotalMilliseconds.ToString() + " Millisecond";
                MessageBox.Show("Can't Find");
                Start.Visibility = Visibility.Visible;
                Set.Visibility = Visibility.Visible;
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (answerLong == answerTime)
            {
                Start.Visibility = Visibility.Visible;
                Set.Visibility = Visibility.Visible;
                timer.Stop();
            }
            setButton(""+answer[answerTime]);
            answerTime++;
        }
        private void Data_KeyDown(object sender, KeyEventArgs e)
        {
            {
                TextBox txt = sender as TextBox;

                //遮蔽非法按鍵
                if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal)
                {
                    if (txt.Text.Contains(".") && e.Key == Key.Decimal)
                    {
                        e.Handled = true;
                        return;
                    }
                    e.Handled = false;
                }
                else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
                {
                    if (txt.Text.Contains(".") && e.Key == Key.OemPeriod)
                    {
                        e.Handled = true;
                        return;
                    }
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
