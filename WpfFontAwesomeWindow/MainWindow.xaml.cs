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
using System.Runtime.InteropServices;

namespace WpfFontAwesomeWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y,
            int nReserved, IntPtr hWnd, IntPtr prcRect);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);
        struct RECT { public int left, top, right, bottom; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StackPanelMinimize_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void StackPanelMaximize_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void StackPanelClose_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ShowSystemMenu(double x, double y)
        {
            IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            RECT pos;
            GetWindowRect(hWnd, out pos);
            IntPtr hMenu = GetSystemMenu(hWnd, false);
            int cmd = TrackPopupMenu(hMenu, 0x100, pos.left + (int)x, pos.top + (int)y, 0, hWnd, IntPtr.Zero);

            if (cmd > 0)
            {
                SendMessage(hWnd, 0x112, (IntPtr)cmd, IntPtr.Zero);
            }
        }

        private void ContentControlWindowSystemMenu_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowSystemMenu(e.GetPosition(this).X, e.GetPosition(this).Y);
        }

        private void ContentControlWindowSystemMenu_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ContentControlWindowCaption_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ContentControlWindowCaption_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}
